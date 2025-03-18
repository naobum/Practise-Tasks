using System.Collections;

namespace Practise_Tasks.Interfaces
{
    public interface ISubsetFinder<T> where T : IEnumerable
    {
        T FindSubset(T fullSet);
    }
}
