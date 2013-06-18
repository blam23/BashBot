using System;

namespace WSBashBot
{
    // Used for storing commands in an easily to manipulate format.
    // Also contains some useful static strings related to commands.
    public class BashCommand
    {
        public static string Chat = "SAY";
        public static string Bout = "BOUT";
        public static string Disconnect = "DISCONNECT";
        public static string Spectators = "SPECS";
        public static string Game = "GAME";
        public static string NewGame = "NEWGAME";
        public static string Ping = "PING";
        public static string BoutEndString = " -1 0 0 0 0 END 0";

        public string Name;
        public string Info;
        public string Value;

        public BashCommand(string name, string info, string value)
        {
            Name = name;
            Info = info;
            Value = value;
        }

        public override string ToString()
        {
            return String.Format("{0} {1};{2}", Name, Info, Value);
        }

    }
}
