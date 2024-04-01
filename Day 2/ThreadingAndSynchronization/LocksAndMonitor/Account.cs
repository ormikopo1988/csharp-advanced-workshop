using System;
using System.Threading;
using System.Threading.Tasks;

namespace LocksAndMonitor
{
    internal class Account
    {
        private readonly object withdrawLock = new();

        private int _balance;

        public Account(int initialBalance)
        {
            _balance = initialBalance;
        }

        public void WithdrawAmount(int amount)
        {
            for (var i = 0; i < 10; i++)
            {
                var withdrawnBalance = Withdraw(amount);

                if (withdrawnBalance > 0)
                {
                    Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} | Task {Task.CurrentId} - Balance left: {_balance}");
                }
                else
                {
                    Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} | Task {Task.CurrentId} - Not enough balance");
                }
            }
        }

        private int Withdraw(int amount)
        {
            // Using the lock / Monitor constructs this exception will never be thrown. 
            // Without lock / monitor construct we would have a problem.
            if (_balance < 0)
            {
                throw new Exception("Not enough balance.");
            }

            // The following block without lock would give an exception.
            if (_balance >= amount)
            {
                Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} | Task {Task.CurrentId} - Amount withdrawn: {amount}");

                _balance -= amount;

                return _balance;
            }

            // The following two code blocks do the exact same thing
            // Locking using Monitor class
            //Monitor.Enter(withdrawLock);

            //try
            //{
            //    if (_balance >= amount)
            //    {
            //        Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} | Task {Task.CurrentId} - Amount withdrawn: {amount}");

            //        _balance -= amount;

            //        return _balance;
            //    }
            //}
            //finally
            //{
            //    Monitor.Exit(withdrawLock);
            //}

            // Locking using lock statement
            //lock (withdrawLock)
            //{
            //    if (_balance >= amount)
            //    {
            //        Console.WriteLine($"Thread {Environment.CurrentManagedThreadId} | Task {Task.CurrentId}: Amount withdrawn: {amount}");

            //        _balance -= amount;

            //        return _balance;
            //    }
            //}

            return 0;
        }
    }
}
