namespace CoAndContravariance.BasicConcepts.DemoTwo
{
    public interface IMyComparer<in T>
    {
        int Compare(T x, T y);
    }
}
