using CoAndContravariance.BasicConcepts;
using CoAndContravariance.Contravariance;
using CoAndContravariance.Covariance;
using System;
using System.Collections.Generic;

namespace CoAndContravariance
{
    class Program
    {
        static void Main(string[] args)
        {
            BaseConceptsDemo();
            CovarianceDemo();
            ContravarianceDemo();
        }

        #region Base Concepts Demo

        static void BaseConceptsDemo()
        {
            // The golden rule of variance
            // Let’s assume we have these 2 classes:
            // - public class Car : Vehicle {}
            // - public class Vehicle{}

            // If we have Covariance(<out T>) then:
            // - We can pass a Car where a Vehicle is expected
            // Without covariance, this relation needs to be 1-1 (we pass a Car where a Car is expected and we pass a Vehicle where a Vehicle is expected).

            // If we have Contravariance(<in T >) then:
            // - We can use a Vehicle where a Car is expected
            // Without contravariance, this relation needs to be 1-1 (we pass a Car where a Car is expected and we pass a Vehicle where a Vehicle is expected).

            // Which of the two will compile and why? Try removing variant keyword from interface declaration and see what happens
            //IGarageManager<Vehicle> vehicleGarageManager = new GarageManager<Car>();
            //IGarageManager<Car> carGarageManager = new GarageManager<Vehicle>();

            // Which of the two will compile and why? Try removing variant keyword from interface declaration and see what happens
            //IVehicleWashManager<Vehicle> vehicleWash = new VehicleWash<Car>();
            //IVehicleWashManager<Car> carWash = new VehicleWash<Vehicle>();
        }

        #endregion

        #region Covariance Demo

        static void CovarianceDemo()
        {
            var myFruits = new List<Fruit>
            {
                new Apple { Name = "Apple1" },
                new Banana { Name = "Banana1" }
            };

            // Now try to create a new List<Apple> instead and see what the problem is to pass this new List to the PrintFruits method
            // What change to PrintFruits would make the code compile and work again?
            //var myFruits = new List<Apple>
            //{
            //    new Apple { Name = "Apple1" },
            //    new Apple { Name = "Apple2" }
            //};

            PrintFruits(myFruits);
        }

        private static void PrintFruits(IList<Fruit> fruits)
        {
            foreach (var fruit in fruits)
            {
                Console.WriteLine($"Fruit name: {fruit.Name}");
            }
        }

        #endregion

        #region Contravariance Demo

        static void ContravarianceDemo()
        {
            // CONTRAVARIANCE DEMO
            IMyComparer<Person> personComparer = new PersonComparer();
            var comparisonResult = Compare(personComparer);

            Console.WriteLine(comparisonResult);
        }

        static int Compare(IMyComparer<Student> comparer)
        {
            var s1 = new Student("John");
            var s2 = new Student("Peter");

            return comparer.Compare(s1, s2);
        }

        #endregion
    }
}