using Practise_Tasks.Interfaces;

namespace Practise_Tasks.Services
{
    public class CharsRepeatsCounter : IItemsRepeatsCounter<char, string>
    {
        public Dictionary<char, int> CountRepeats(string collection)
        {
            var result = new Dictionary<char, int>();

            foreach (char c in collection)
            {
                if (result.ContainsKey(c))
                    result[c]++;
                else 
                    result[c] = 0; // not 1, because we count repetitions, not amount 
            }

            return result;
        }
    }
}
