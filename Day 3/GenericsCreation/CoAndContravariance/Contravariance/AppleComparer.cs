namespace CoAndContravariance.Contravariance
{
    public class AppleComparer : IMyComparer<Apple>
    {
        // The logic here is exactly the same as in the base FruitComparer
        public int Compare(Apple item1, Apple item2)
        {
            return (int)(item1.Weight - item2.Weight);
        }
    }
}