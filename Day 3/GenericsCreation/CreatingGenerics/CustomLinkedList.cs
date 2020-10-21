using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace CreatingGenerics
{
    public class CustomLinkedList<T> : ICustomList<T>
    {
        private Node<T> root = null;

        public Node<T> First
        {
            get
            {
                return root;
            }
        }

        public bool Any()
        {
            return root != null;
        }

        public void AddLast(T value)
        {
            var newNode = new Node<T> 
            { 
                data = value 
            };

            if (root == null)
            {
                root = newNode;
            }
            else
            {
                Node<T> current = root;

                while (current.next != null)
                {
                    current = current.next;
                }

                current.next = newNode;
            }
        }

        public void Remove(T data)
        {
            if (root != null && Object.Equals(root.data, data))
            {
                var node = root;
                root = node.next;
                node.next = null;
            }
            else
            {
                Node<T> current = root;

                while (current.next != null)
                {
                    if (current.next != null && Object.Equals(current.next.data, data))
                    {
                        var node = current.next;
                        current.next = node.next;
                        node.next = null;

                        break;
                    }

                    current = current.next;
                }
            }
        }

        // Enables the iteration using a foreach loop on a CustomLinkedList instance
        public IEnumerator<T> GetEnumerator()
        {
            Node<T> curr = root;

            while (curr != null)
            {
                // yield is a special keyword that can be used only in the context of iterators. 
                // It instructs the compiler to convert this regular code to a state machine. 
                // The generated code keeps track of where you are in the collection and it implements methods such as MoveNext and Current.
                yield return curr.data;
                curr = curr.next;
            }
        }

        public IEnumerable<TOutput> AsEnumerableOf<TOutput>()
        {
            var converter = TypeDescriptor.GetConverter(typeof(T));

            foreach (var item in this)
            {
                yield return (TOutput)converter.ConvertTo(item, typeof(TOutput));
            }
        }
    }
}