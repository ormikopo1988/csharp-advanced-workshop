using System;

namespace HashSetBasics
{
    public class EquatableEmployee : IEquatable<EquatableEmployee>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        // This way the HashSet will know how to do the comparison with these EquatableEmployee reference type objects
        public bool Equals(EquatableEmployee other)
        {
            return this.Id.Equals(other.Id);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}