namespace CreatingGenerics
{
    public class Node<T>
    {
        // link to next Node in list
        public Node<T> next = null;

        // value of this Node
        public T data;
    }
}