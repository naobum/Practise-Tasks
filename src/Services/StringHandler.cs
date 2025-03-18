using Practise_Tasks.Interfaces;
using System.Text;

namespace Practise_Tasks.Services
{
    public class StringHandler : IStringHandler
    {
        public string GetNewString(string oldString)
        {
            string result;
            if (oldString.Length % 2 == 0)
            {
                (string half1, string half2) = DivideInHalf(oldString);
                result = Reverse(half1) + Reverse(half2);
            }
            else
                result = Reverse(oldString) + oldString;
            return result;
        }
        private string Reverse(string input)
        {
            var result = new StringBuilder();

            for (int i = input.Length - 1; i >= 0; i--)
                result.Append(input[i]);

            return result.ToString();
        }

        private (string, string) DivideInHalf(string input)
        {
            StringBuilder half1 = new(), half2 = new();

            for (int i = 0; i < input.Length / 2; i++)
            {
                half1.Append(input[i]);
                half2.Append(input[i + input.Length / 2]);
            }

            return (half1.ToString(), half2.ToString());
        }
    }
}
