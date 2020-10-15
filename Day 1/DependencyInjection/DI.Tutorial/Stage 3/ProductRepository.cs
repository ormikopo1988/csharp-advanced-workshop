using System;

namespace DI.Tutorial.Stage3
{
    public class ProductRepository : IProductRepository
    {
        public ProductRepository(ILogger logger)
        {
            _Logger = logger;
        }

        ILogger _Logger;

        void IProductRepository.Save()
        {
            Console.WriteLine("Product inventory updated.");

            _Logger.Log("HELLO ATHENS!");
        }
    }

    public interface IProductRepository
    {
        void Save();
    }
}