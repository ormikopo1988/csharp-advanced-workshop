using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocksAndMonitor
{
    internal class Account
    {
        private readonly object withdrawLock = new object();

        private int balance;

        public Account(int initialBalance)
        {
            this.balance = initialBalance;
        }

        public void WithdrawAmount(int amount)
        {
            for (int i = 0; i < 10; i++)
            {
                var balance = Withdraw(amount);

                if (balance > 0)
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} | Task {Task.CurrentId} - Balance left: {balance}");
                }
                else
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} | Task {Task.CurrentId} - Not enough balance");
                }
            }
        }

        private int Withdraw(int amount)
        {
            // Using the lock / Monitor constructs this exception will never be thrown. 
            // Without lock / monitor construct we would have a problem.
            if (balance < 0)
            {
                throw new Exception("Not enough balance.");
            }

            // The following block without lock would give an exception.
            if (balance >= amount)
            {
                Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} | Task {Task.CurrentId} - Amount withdrawn: {amount}");

                balance -= amount;

                return balance;
            }

            // The following two code blocks do the exact same thing
            // Locking using Monitor class
            //Monitor.Enter(withdrawLock);

            //try
            //{
            //    if (balance >= amount)
            //    {
            //        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} | Task {Task.CurrentId} - Amount withdrawn: {amount}");

            //        balance -= amount;

            //        return balance;
            //    }
            //}
            //finally
            //{
            //    Monitor.Exit(withdrawLock);
            //}

            // Locking using lock statement
            //lock(withdrawLock)
            //{
            //    if (balance >= amount)
            //    {
            //        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} | Task {Task.CurrentId}: Amount withdrawn: {amount}");

            //        balance -= amount;

            //        return balance;
            //    }
            //}

            return 0;
        }
    }
}
