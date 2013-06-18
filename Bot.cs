using System;
using System.Collections.Generic;
using System.Timers;

namespace WSBashBot
{
    // The actual bot class!
    public class Bot
    {
        // Login Info
        public string Name;
        private readonly string _passwordhash;

        // Server Info
        public string IP;
        public int Port;

        // Client we use to communicate
        public BashTcpClient Client;

        // Used when building the user list, can be partial
        //  not to be used to show bouts
        private List<BashBout> _tempUsers = new List<BashBout>();

        // Complete User list
        public List<BashBout> Users = new List<BashBout>();

        // Timer to send "PING" every 30 seconds.
        public Timer PingTimer = new Timer(30000);

        // An event that fires when the bot detects a chat line
        public delegate void CommandReceivedHandler(BashCommand line);
        public event CommandReceivedHandler ChatRecieved;
        protected virtual void OnChatRecieved(BashCommand line)
        {
            if (ChatRecieved != null) ChatRecieved(line);
        }

        public event CommandReceivedHandler CommandRecieved;        
        protected virtual void OnCommandRecieved(BashCommand line)
        {
            if (CommandRecieved != null) CommandRecieved(line);
        }

        // An event that happens when the Bout list is updated.
        public delegate void BoutListUpdateHandler(List<BashBout> bouts);
        public event BoutListUpdateHandler BoutListUpdate;
        protected virtual void OnBoutListUpdate(List<BashBout> bouts)
        {
            if (BoutListUpdate != null) BoutListUpdate(bouts);
        }

        // Simple wrapper to see if the bot is connected or not.
        public bool Connected
        {
            get { return Client.Connected; }
        }

        public Bot(string name, string password, string ip, int port)
        {
            Name = name;
            // We need to hash the password before we can use it to login.
            _passwordhash = BashTcpClient.Hash(password);
            IP = ip;
            Port = port;
            Client = new BashTcpClient();
            PingTimer.Elapsed += PingTimerOnElapsed;
        }

        private void PingTimerOnElapsed(object sender, ElapsedEventArgs elapsedEventArgs)
        {
            if (Client.Connected)
            {
                Client.WriteLine("PING");
            }
        }

        // Connets to the server, logs in,
        //  starts the PING timer and
        //  performs any startup tasks.
        public void Start() 
        {
            Login();
            ReadLoop();
        }

        // Stops the bot!
        public void Stop()
        {
            if (Client.Connected)
            {
                Client.WriteLine("DISCONNECT BYE!");
                Client.Close();
            }
            PingTimer.Stop();
        }

        // The asynchronous read loop.
        // This handles the reading and parsing of all commands
        //  from the connected server.
        private async void ReadLoop()
        {
            while (Client.Connected)
            {
                // Asyncronously read line
                var line = await Client.Reader.ReadLineAsync();
                if (line != null) // If the line is null that means the connection has ended.
                {
                    // Convert the line to a BashCommand and then process it
                    ProcessLine(BashCommandParser.Parse(line));
                }
                else
                {
                    // If we disconnect, try to reconnect!
                    Console.WriteLine("Disconnected, Reconnecting..");
                    Client = new BashTcpClient();
                    Login();
                }
            }
        }

        // Handles Chat & Bout Commands
        public void ProcessLine(BashCommand command)
        {
            // Send the command to all event handlers
            OnCommandRecieved(command);
            if (command.Name == BashCommand.Chat)
            {
                // Send the chat to all event handlers
                OnChatRecieved(command);
            }
            else if (command.Name == BashCommand.Bout)
            {
                if (command.Value == BashCommand.BoutEndString)
                {
                    // Here we have built up _tempUsers fully
                    //  so we set Users to _tempUsers
                    //  and create a new List for the next BOUT list
                    Users = _tempUsers;
                    _tempUsers = new List<BashBout>();
                    // Send the users to all event handlers
                    OnBoutListUpdate(Users);
                }
                else
                {
                    // Parse and add the bout to the temporary list
                    var bout = BashBoutParser.Parse(command.Value);
                    _tempUsers.Add(bout);
                }
            }
        }

        // Sends the login information to the server
        //  spectates and starts ping timer.
        public void Login()
        {
            Client.Connect(IP, Port);
            Client.WriteLine("NICK {0}", Name); // Not sure if this is needed anymore..
            Client.WriteLine("mlogin {0} {1}", Name, _passwordhash);
            Client.WriteLine("SPEC - TATE)"); // Spectates
            PingTimer.Start();
        }

        // Sends a chat message
        public void SendChatMessage(string message)
        {
            if (Client.Connected)
            {
                Client.WriteLine("SAY {0}", message);
            }
        }

        // Sends whatever data you want
        public void Send(string command)
        {
            if (Client.Connected)
            {
                Client.WriteLine(command);
            }
        }

        // Sends a BashCommand
        public void Send(BashCommand command)
        {
            Send(command.ToString());
        }
    }
}
