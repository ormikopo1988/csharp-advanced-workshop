using System.Collections.Generic;

namespace CreatingGenerics
{
    public interface ICustomList<T>
    {
        Node<T> First { get; }
        void AddLast(T value);
        bool Any();
        void Remove(T data);
        IEnumerable<TOutput> AsEnumerableOf<TOutput>();
    }
}