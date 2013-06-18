using System;
using System.Collections.Generic;
using System.Linq;
namespace WSBashBot
{
    // Stores some information about a Bout
    // TODO: Add extra information to both parsing and here.
    public class BashBout
    {
        public int ID;
        public int Qi;
        public string Name;
        public string Country;
        public string DateJoined;
        public string Raw;

        public override string ToString()
        {
            return String.Format("{0} - {1}", Country, Name);
        }
    }
}
