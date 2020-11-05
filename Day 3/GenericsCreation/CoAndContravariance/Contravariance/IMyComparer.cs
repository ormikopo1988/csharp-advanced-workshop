namespace CoAndContravariance.Contravariance
{
    public interface IMyComparer<in T>
    {
        int Compare(T x, T y);
    }
}
