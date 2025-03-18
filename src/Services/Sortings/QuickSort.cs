using Practise_Tasks.Interfaces;
using System;

namespace Practise_Tasks.Services.Sortings
{
    public class QuickSort : ISorter<string, char>
    {
        public void Sort(ref string symbols)
        {
            if (symbols == null || symbols.Length == 0)
                return;
            symbols = new string(Sort(symbols.ToCharArray()));
        }
        private char[] Sort(char[] symbols)
        {
            if (symbols == null || symbols.Length == 0)
                return Array.Empty<char>();
            Sort(symbols, 0, symbols.Length - 1);
            return symbols;
        }

        private void Sort(char[] array, int left, int right)
        {
            if (left < right)
            {
                int pivot = Partition(array, left, right);
                Sort(array, left, pivot - 1);
                Sort(array, pivot + 1, right);
            }
        }

        private int Partition(char[] array, int left, int right)
        {
            char pivot = array[right];
            int i = left - 1;
            for (int j = left; j < right; j++)
            {
                if (array[j] <= pivot)
                {
                    i++;
                    Swap(ref array[i], ref array[j]);
                }
            }
            Swap(ref array[i + 1], ref array[right]);
            return i + 1;
        }

        private void Swap(ref char a, ref char b)
        {
            (a, b) = (b, a);
        }
    }
}
