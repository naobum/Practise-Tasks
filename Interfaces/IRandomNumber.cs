namespace Practise_Tasks.Interfaces
{
    public interface IRandomNumber
    {
        Task<int> GetIntAsync(int min, int max);
    }
}