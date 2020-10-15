namespace CoAndContravariance.BasicConcepts.DemoOne
{
    public interface IGarageManager<in T>
    {
        void Park(T item);
    }
}