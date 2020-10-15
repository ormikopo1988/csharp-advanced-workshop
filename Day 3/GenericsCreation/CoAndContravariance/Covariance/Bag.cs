using System.Collections.Generic;

namespace CoAndContravariance.Covariance
{
    public interface IBag<out T>
    {
        T Get(int index);
    }

    public class Bag<T> : IBag<T>
    {
        private List<T> _items = new List<T>();

        public T Get(int index) => _items[index];

        public void Add(T item) => _items.Add(item);
    }
}
