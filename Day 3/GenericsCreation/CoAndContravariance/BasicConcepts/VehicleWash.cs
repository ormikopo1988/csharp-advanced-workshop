using System.Collections.Generic;

namespace CoAndContravariance.BasicConcepts
{
    public class VehicleWash<T> : IVehicleWashManager<T>
    {
        List<T> itemsWashed;

        public VehicleWash() => itemsWashed = new List<T>();
        
        public IEnumerable<T> GetAllWashed()
        {
            return itemsWashed;
        }
    }
}