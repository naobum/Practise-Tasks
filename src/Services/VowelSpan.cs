using Practise_Tasks.Interfaces;
using System.Text;

namespace Practise_Tasks.Services
{
    public class VowelSpan : ISubsetFinder<string>
    {
        private const string VOWELS = "aeiouy";
        public string FindSubset(string fullSrting)
        {
            int startPos = 0;
            int endPos = fullSrting.Length - 1;

            while (startPos < endPos)
            {
                if (!VOWELS.Contains(fullSrting[startPos]))
                    startPos++;

                if (!VOWELS.Contains(fullSrting[endPos]))
                    endPos--;

                if (VOWELS.Contains(fullSrting[startPos]) && VOWELS.Contains(fullSrting[endPos]))
                    break;
            }

            if (startPos >= endPos && !VOWELS.Contains(fullSrting[endPos]))
                return String.Empty;

            var maxSubstring = new StringBuilder();
            for (int i = startPos; i <= endPos; i++)
                maxSubstring.Append(fullSrting[i]);
            return maxSubstring.ToString();
        }
    }
}
