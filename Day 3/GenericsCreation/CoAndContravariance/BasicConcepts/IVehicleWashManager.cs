using System.Collections.Generic;

namespace CoAndContravariance.BasicConcepts
{
    public interface IVehicleWashManager<out T>
    {
        IEnumerable<T> GetAllWashed();
    }
}