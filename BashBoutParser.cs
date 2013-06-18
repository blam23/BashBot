namespace WSBashBot
{
    // Class to parse BOUT strings into a BashBout object
    // TODO: Implement missing values
    public static class BashBoutParser
    {
        // Reference: 41651 2983 326 2 0 0 [ApOc]coolgabe1 0 1794 1189 0 CA 2011-01-10 20:00:11
        //  0 - ID
        //  1 - QI
        //  2 - ?
        //  3 - ?
        //  4 - ?
        //  5 - ?
        //  6 - Name
        //  7 - ?
        //  8 - ?
        //  9 - ?
        // 11 - Country
        // 12 - DateJoined ?
        // 13 - TimeJoined ?

        public static BashBout Parse(string value)
        {
            var split = value.Trim().Split(' ');
            var id = int.Parse(split[0]);
            var qi = int.Parse(split[1]);
            var name = split[6];
            var country = split[11];
            var joined = split[12] + ' ' + split[13];

            return new BashBout
                {
                    ID = id,
                    Qi = qi,
                    Name = name,
                    Country = country,
                    DateJoined = joined,
                    Raw = value
                };
        }
    }
}