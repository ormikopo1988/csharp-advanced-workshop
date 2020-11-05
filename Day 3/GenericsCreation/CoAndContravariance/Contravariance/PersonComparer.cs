namespace CoAndContravariance.Contravariance
{
    public class PersonComparer : IMyComparer<Person>
    {
        public int Compare(Person x, Person y)
        {
            return string.CompareOrdinal(x.Name, y.Name);
        }
    }
}
