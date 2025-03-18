namespace Practise_Tasks.Interfaces
{
    public interface IItemsCounter<T, TCollection> where T : notnull
    {
        Dictionary<T, int> CountItems(TCollection collection);
    }
}
