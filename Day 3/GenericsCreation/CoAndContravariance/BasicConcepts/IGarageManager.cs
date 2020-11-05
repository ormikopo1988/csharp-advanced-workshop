namespace CoAndContravariance.BasicConcepts
{
    public interface IGarageManager<in T>
    {
        void Park(T item);
    }
}