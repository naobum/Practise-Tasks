using Practise_Tasks.Interfaces;
using System.Text;

namespace Practise_Tasks.Services
{
    public class ReverseInputValidate : IInputValidate
    {
        private static readonly char[] VALID_CHARS = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k',
            'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        public string GetInvalidChars(string? input)
        {
            if (input == null)
                return string.Empty;

            var result = new StringBuilder();

            foreach (char c in input)
                if (!VALID_CHARS.Contains(c))
                    result.Append(c);
               
            return result.ToString();
        }

        public bool IsValid(string? input)
        {
            if (String.IsNullOrEmpty(input))
                return false;
            
            var result = true;

            foreach (char c in input)
                if (!VALID_CHARS.Contains(c))
                {
                    result = false;
                    break;
                }

            return result;
        }
    }
}
