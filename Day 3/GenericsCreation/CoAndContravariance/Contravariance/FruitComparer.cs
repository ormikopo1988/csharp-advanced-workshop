namespace CoAndContravariance.Contravariance
{
    public class FruitComparer : IMyComparer<Fruit>
    {
        public int Compare(Fruit item1, Fruit item2)
        {
            return (int)(item1.Weight - item2.Weight);
        }
    }
}