namespace Practise_Tasks.ExtensionMethods
{
    public static class Extensions
    {
        public static char[] ToUniqueCharsArray(this string input)
        {
            var result = new List<char>();

            foreach (char c in input)
            {
                if (!result.Contains(c))
                    result.Add(c);
            }

            return result.ToArray();
        }
    }
}
