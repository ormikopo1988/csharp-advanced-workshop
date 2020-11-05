using System.Collections.Generic;

namespace CoAndContravariance.BasicConcepts
{
    public class GarageManager<T> : IGarageManager<T>
    {
        List<T> itemsInGarage;
        
        public GarageManager() => itemsInGarage = new List<T>();
        
        public void Park(T item) => itemsInGarage.Add(item);
    }
}