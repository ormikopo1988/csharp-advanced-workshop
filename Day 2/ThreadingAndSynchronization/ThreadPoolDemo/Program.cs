using ThreadPoolDemo;

Console.WriteLine($"Is main thread a thread pool thread? {Thread.CurrentThread.IsThreadPoolThread}");

var employee = new Employee
{
    Name = "John Doe",
    CompanyName = "ACME"
};

ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayEmployeeInfo!), employee);

// The below two examples are just a way you can use to be
// able to set the maximum threads. There is a bit of a caveat here. 
// That number could be anything. You can even say thousand, but it wouldn't really matter. 
// At one point of time, you'll only have a certain amount of threads that can do the processing. 
// Everything else will have to wait. But this is important because you want to set a max number of threads. 
// If you know what your usage is going to look like, you want to keep it in a very controlled manner. 
// Of course you have the min threads, which is equal to the number of different CPUs in the current machine and that's the minimum it gets. 
// But the fact that you have the power to set it, it makes it so much better because now, you have less overhead with dealing with threads.

// Set the max number of threads in the thread pool - 1st way
// Get the number of processors in the host machine 
//var processorsCount = Environment.ProcessorCount;

//ThreadPool.SetMaxThreads(processorsCount * 2, processorsCount * 2);

// Set the max number of threads in the thread pool - 2nd way
var workerThreads = 0;
var completionPortThreads = 0;
ThreadPool.GetMinThreads(out workerThreads, out completionPortThreads);
ThreadPool.SetMaxThreads(workerThreads * 2, completionPortThreads * 2);

Console.ReadLine();

// It does expect an object type and not an Employee type specifically that we pass
static void DisplayEmployeeInfo(object employee)
{
    // Is this indeed a thread pool thread?
    Console.WriteLine($"Is thread running this piece of code a thread pool thread? {Thread.CurrentThread.IsThreadPoolThread}.");

    var emp = employee as Employee;

    Console.WriteLine($"Person name is: {emp!.Name} and company name is: {emp.CompanyName}");
}