using System.Collections.Generic;

namespace WSBashBot
{
    // Contains information about a Room
    public class BashRoom
    {
        public string Name { get; set; }
        public string IP;
        public int Port;
        public string FullIP
        {
            get { return string.Format("{0}:{1}", IP, Port); }
        }

        public List<string> Clients;
        public int ClientCount
        {
            get { return Clients.Count; }
        }

        public string Description
        {
            get { return BashColourStripper.Strip(_description); }
            set { _description = value; }
        }

        private string _description;

        public BashRoom(string ip, int port, string name)
        {
            IP = ip;
            Name = name;
            Port = port;
            Clients = new List<string>();
            Description = "";
        }

        public void SetDescription(string description)
        {
            Description = description;
        }

        public void AddClients(IEnumerable<string> clients)
        {
            Clients.AddRange(clients);
        }
    }
}
