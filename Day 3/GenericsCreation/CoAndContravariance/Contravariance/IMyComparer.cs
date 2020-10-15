namespace CoAndContravariance.Contravariance
{
    public interface IMyComparer<in T>
    {
        int Compare(T item1, T item2);
    }
}
