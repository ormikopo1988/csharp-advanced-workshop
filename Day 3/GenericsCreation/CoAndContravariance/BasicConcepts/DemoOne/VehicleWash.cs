
using System.Collections.Generic;

namespace CoAndContravariance.BasicConcepts.DemoOne
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