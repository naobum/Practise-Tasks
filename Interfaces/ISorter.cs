namespace Practise_Tasks.Interfaces
{
    public interface ISorter<TCollection, T> 
        where TCollection : IEnumerable<T> 
        where T : IComparable
    {
        void Sort(ref TCollection collection);
    }
}
