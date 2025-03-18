using Practise_Tasks.Interfaces;

namespace Practise_Tasks.Services
{
    public class CharsCounter : IItemsCounter<char, string>
    {
        public Dictionary<char, int> CountItems(string collection)
        {
            var result = new Dictionary<char, int>();

            foreach (char c in collection)
            {
                if (result.ContainsKey(c))
                    result[c]++;
                else 
                    result[c] = 1; 
            }

            return result;
        }
    }
}
