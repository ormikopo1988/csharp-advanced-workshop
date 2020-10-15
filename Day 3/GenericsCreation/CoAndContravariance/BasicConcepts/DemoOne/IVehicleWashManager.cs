using System.Collections.Generic;

namespace CoAndContravariance.BasicConcepts.DemoOne
{
    public interface IVehicleWashManager<out T>
    {
        IEnumerable<T> GetAllWashed();
    }
}