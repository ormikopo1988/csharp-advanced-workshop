namespace GenericConstraints
{
    public class Node<T>
    {
        public Node(T t) => (Next, Data) = (null, t);

        public Node<T> Next { get; set; }

        public T Data { get; set; }
    }
}