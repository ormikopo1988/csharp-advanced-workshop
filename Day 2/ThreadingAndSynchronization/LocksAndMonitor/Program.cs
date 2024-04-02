using System.Threading.Tasks;
using System;
using LocksAndMonitor;

var account = new Account(20000);

var task1 = Task.Factory.StartNew(() => account.WithdrawAmount(3000));
var task2 = Task.Factory.StartNew(() => account.WithdrawAmount(3000));
var task3 = Task.Factory.StartNew(() => account.WithdrawAmount(3000));

// Audience Question: What could be done here in order to not have any problem 
// inside the code of Account class even if we did not put a lock inside the 
// critical section of code?

// Main thread please wait for all other tasks to complete
Task.WaitAll(task1, task2, task3);

Console.WriteLine("All tasks completed!");

Console.ReadLine();