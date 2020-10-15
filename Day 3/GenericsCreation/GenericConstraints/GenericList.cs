using System.Collections.Generic;

namespace GenericConstraints
{
    public class GenericList<T> where T : Employee
    {
        private Node<T> head;

        public void AddHead(T t)
        {
            var n = new Node<T>(t) { Next = head };
            head = n;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = head;

            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        public T FindFirstOccurrence(string s)
        {
            var current = head;
            T t = null;

            while (current != null)
            {
                // The constraint enables access to the Name property.
                // The constraint specifies that all items of type T are guaranteed to be either an Employee object or an object that inherits from Employee
                if (current.Data.Name == s)
                {
                    t = current.Data;
                    break;
                }
                else
                {
                    current = current.Next;
                }
            }
            return t;
        }
    }
}