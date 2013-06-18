using System.Text;

namespace WSBashBot
{
    // Removes the Toribash Colour tags from text
    public static class BashColourStripper
    {
        public static string Strip(string input)
        {
            var builder = new StringBuilder();
            for (var i = 0; i < input.Length; i++)
            {
                if (i < input.Length-2 && input[i] == '^' && char.IsDigit(input[i + 1]) && char.IsDigit(input[i + 2]))
                    i += 2;
                else
                    builder.Append(input[i]);
            }

            return builder.ToString();
        }
    }
}
