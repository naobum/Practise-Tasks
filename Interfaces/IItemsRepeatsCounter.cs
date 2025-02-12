namespace Practise_Tasks.Interfaces
{
    public interface IItemsRepeatsCounter<T, TCollection> where T : notnull
    {
        Dictionary<T, int> CountRepeats(TCollection collection);
    }
}
