using CoAndContravariance.BasicConcepts.DemoOne;
using CoAndContravariance.BasicConcepts.DemoTwo;
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
            BaseConceptsDemoOne();
            CovarianceDemo();
            ContravarianceDemo();
        }

        #region Base Concepts Demo One

        static void BaseConceptsDemoOne()
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

        #region Base Concepts Demo Two

        static void BaseConceptsDemoTwo()
        {
            // COVARIANCE DEMO

            // By knowing that Student and Teacher inherit from Person, this means that you can substitute Person with Teacher or Student everywhere a Person is expected. 
            // For example, if a method has an input parameter of type Person, you can always pass an instance of Student or Teacher. 
            // Also, if a method is declared to return a Person, the implementation can actually return a Student or a Teacher. 
            // These are pure OOP principles and the rules of inheritance and polymorphism so this behavior is very natural to us.
            // Now the question is: what if the method accepts as a parameter not a Person, but a generic type G<Person>? Does that mean we can pass G<Teacher> or G<Student>?

            // Let's start with a simple example. Define the PrintNames method below which
            // receives an array of Person objects as input and writes all their names to the console. 
            // But what if instead of an array of Person, we pass an array of Teacher or Student? Like so:
            Student[] students =
            {
                new Student("John"),
                new Student("Peter")
            };

            PrintNames(students);

            // This code compiles, works and makes perfect sense. Both Student and Teacher have names so being able to print them by passing Student[] or Teacher[] 
            // instead of Person[] makes our method way more useful than if we had provide an exact type match of Person[]

            // With this example in mind you may start thinking that anywhere we expect a generic type of Person
            // we should be able to pass an object with the generic parameter 
            // being Teacher or Student. Not really. Let’s look at a different example like below
            try
            {
                Person[] people = students;
                people[0] = new Teacher("Paul");
                Student student = students[0];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Follow the assignments carefully. 
            // The original array is of type Student[]. 
            // However, due to the fact we can substitute Person[] with Student[], on line 72 we get a reference to array of Person objects. 
            // Every Teacher is a Person so the statement on line 73 is allowed by the compiler. 
            // However, this is the place where we get the exception at runtime:
            // "Attempted to access an element as a type incompatible with the array."
            // And of course, this is something we would expect. 
            // If this code had worked, what would the assignment statement on line 74 do? 
            // We would be trying to assign a Teacher to a Student. These two types are not compatible so that wouldn’t make sense.

            // Being able to pass an array of Student or Teacher where an array of Person is expected means that arrays are covariant. 
            // That was a deliberate decision from the beginning when the language did not support generic types. 
            // Although there may be some dangerous consequences as we’ve seen, this behavior brings a lot of flexibility
            // to implement general purpose algorithms like in our PrintNames method.

            // We’ve seen there are cases when it makes perfect sense for a type to be covariant which can make some algorithms more general. 
            // But we’ve also seen a counter example where arrays covariance led to an exception at runtime. 
            // Let’s take a closer look at the PrintNames and Update methods.

            // Clearly, the PrintNames method is only reading the array elements. 
            // On the other hand, Update modifies the array.
            // And this is exactly what leads to the problematic behavior. 
            // With arrays being covariant, there is no way we can know the exact type of the input parameter – it can be Person[], Student[] or Teacher[].
            // So, we can’t safely do updates. In our case, if the exact type of the input happens to be Teacher[] or Person[], the code will work. 
            // If it’s Student[] though, it will throw an exception like we’ve already seen.

            // But how about the cases when covariance is useful? 
            // If our generic interface contains only read operations with respect to the generic parameter, we know we are pretty much in a safe place. 
            // Let’s now build some intuition about what it means for a generic interface to have its generic parameter as read-only:
            // - Imagine the T parameter only appears as a method return type in an interface type declaration
            // - That’s a perfect example for a type that can be made covariant. This is simply achieved by adding the out keyword before the T parameter
            // - The generic type parameter T is said to be at an output position in this interface. 
            // - Output positions are limited to function return values, property get accessors, and certain delegate positions

            // CONTRAVARIANCE DEMO

            // Consider the Compare method below.
            // This method creates two students and compares them via the received comparer of type IMyComparer<Student>.
            // What if we want to compare Student objects in the same way we compare Person objects? 
            // We already have an implementation that compares Person objects by Name – this is implemented in the PersonComparer class. 
            // Why not just use it for our students comparison, like so:
            BasicConcepts.DemoTwo.IMyComparer<Person> personComparer = new PersonComparer();
            var comparisonResult = Compare(personComparer);
            Console.WriteLine(comparisonResult);

            // This code would not compile (try removing in keyword from IMyComparer interface to see for yourself) and prior to C# 4 there was nothing we could do about it. 
            // The reason is clear – we are passing an object implementing IMyComparer<Person> but the method expects IMyComparer<Student>. 
            // The types just don’t match. This is unfortunate though. 
            // Logically if we have a way to compare two persons, we should be able to use that to compare students because the students are actually persons 
            // with some additional characteristics. If we say that two persons with the same name are equal, 
            // and we want to use that comparison logic for students, we should be able to do that.

            // This is where contravariance comes into place. In a very similar way to what we had with covariance, 
            // we need to annotate the generic type parameter in the interface. 
            // This time instead of out we’re going to use the in keyword.
            // In contrast with output positions, input positions are limited to method input parameters and some locations in delegate parameters.
            // Now our IMyComparer interface is declared as contravariant.
            // By doing that, we can pass the PersonComparer to the Compare method that expects a Student comparer.
            // The compiler is happy and we have achieved our goal to reuse our person comparer when comparing students.
        }

        static void PrintNames(Person[] people)
        {
            foreach (var person in people)
            {
                Console.WriteLine(person.Name);
            }
        }

        static void Update(Person[] people)
        {
            people[0] = new Teacher("Paul");
        }

        static int Compare(BasicConcepts.DemoTwo.IMyComparer<Student> comparer)
        {
            var s1 = new Student("John");
            var s2 = new Student("Peter");

            return comparer.Compare(s1, s2);
        }

        #endregion

        #region Covariance Demo

        static void CovarianceDemo()
        {
            WithStandardGenerics();
            WithCustomGenerics();
        }

        private static void WithStandardGenerics()
        {
            var bagOfApples = new List<Covariance.Apple>()
            {
                new Covariance.Apple {Name = "Granny Smith" },
                new Covariance.Apple {Name = "Cox's Orange Pippin" },
                new Covariance.Apple {Name = "Golden Delicious" }
            };

            IEnumerable<Covariance.Fruit> bagOfFruit = bagOfApples;

            foreach (Covariance.Fruit fruit in bagOfFruit)
            {
                Console.WriteLine(fruit.Name);
            }

            bagOfApples.Add(new Covariance.Apple { Name = "Braeburn" });

            Console.WriteLine("---");

            foreach (Covariance.Fruit fruit in bagOfFruit)
            {
                Console.WriteLine(fruit.Name);
            }

            Console.WriteLine("---");

            //bagOfApples.Add(new Banana { Name = "Blue Java" });
            //bagOfFruit.Add(new Banana { Name = "Blue Java" });
        }

        private static void WithCustomGenerics()
        {
            var bagOfApples = new Bag<Covariance.Apple>();

            bagOfApples.Add(new Covariance.Apple { Name = "Granny Smith" });
            bagOfApples.Add(new Covariance.Apple { Name = "Cox's Orange Pippin" });
            bagOfApples.Add(new Covariance.Apple { Name = "Golden Delicious" });

            IBag<Covariance.Fruit> bagOfFruit = bagOfApples;

            Console.WriteLine(bagOfFruit.Get(0).Name);
            Console.WriteLine(bagOfFruit.Get(1).Name);

            //bagOfApples.Add(new Banana { Name = "Blue Java" });
            //bagOfFruit.Add(new Banana { Name = "Blue Java" });
        }

        #endregion

        #region Contravariance Demo

        private static void ContravarianceDemo()
        {
            var apples = new List<Contravariance.Apple>
            {
                new Contravariance.Apple {Name = "Granny Smith", Weight = 80, ForEating = true },
                new Contravariance.Apple {Name = "Arthur Turner", Weight = 70, ForEating = false },
                new Contravariance.Apple {Name = "Honeygold", Weight = 80, ForEating = true },
                new Contravariance.Apple {Name = "Kerry Pippin", Weight = 82, ForEating = true },
                new Contravariance.Apple {Name = "Newton Wonder", Weight = 75, ForEating = false }
            };

            foreach (var apple in apples)
            {
                Console.WriteLine(apple);
            }

            Console.WriteLine();

            //Sort(apples, new AppleComparer()); // This is redundant. We can compare fruits based on their weight so we can just use the base FruitComparer for any kind of fruit.
            Sort(apples, new FruitComparer());

            foreach (var apple in apples)
            {
                Console.WriteLine(apple);
            }
        }

        private static void Sort(List<Contravariance.Apple> collection, Contravariance.IMyComparer<Contravariance.Apple> comparer)
        {
            collection.Sort(new ComparerAdapter<Contravariance.Apple>(comparer));
        }

        #endregion
    }
}