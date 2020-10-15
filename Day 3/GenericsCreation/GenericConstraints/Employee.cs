namespace GenericConstraints
{
    public class Employee
    {
        public Employee(string name, int id) => (Name, ID) = (name, id);
        public string Name { get; set; }
        public int ID { get; set; }
    }
}