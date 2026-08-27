namespace Sdk.Extensions
{
    public static class PrimitiveExtensions
    {
        public static string ToReadable(this bool value)
        {
            return value ? "Active ✅" : "Inactive ❌";
        }

        /// <summary>
        /// Splits a command string into arguments respecting quoted strings.
        /// Handles both spaces and dashes as separators.
        /// </summary>
        public static string[] SplitArgs(this string input)
        {
            if (string.IsNullOrWhiteSpace(input))
            {
                return Array.Empty<string>();
            }

            var args = new List<string>();
            var currentArg = new System.Text.StringBuilder();
            bool inQuotes = false;

            foreach (char c in input)
            {
                if (c == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if ((c == ' ' || c == '-') && !inQuotes)
                {
                    if (currentArg.Length > 0)
                    {
                        args.Add(currentArg.ToString());
                        currentArg.Clear();
                    }

                    // Add dash as its own argument if it's a separator
                    if (c == '-')
                    {
                        args.Add("-");
                    }
                }
                else
                {
                    currentArg.Append(c);
                }
            }

            // Add any remaining argument
            if (currentArg.Length > 0)
            {
                args.Add(currentArg.ToString());
            }

            return args.ToArray();
        }
    }
}
