using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Transactions;

namespace TransactionScopeDemo
{
    class Program
    {
        static string dbConnectionString1 = $"Data Source=V-ORMEIK;Initial Catalog=handlingTransactionsDemo;Integrated Security=True;Pooling=False";
        static string dbConnectionString2 = $"Data Source=V-ORMEIK;Initial Catalog=secondDb;Integrated Security=True;Pooling=False";

        static void Main(string[] args)
        {
            ClearDatabase1();
            ClearDatabase2();
            CreateReadCommitedTransactionWithFiveMinutesTimeout();
            CreateTransactionInsideOtherTransaction();
            InsertAnotherRowAndQueryWithSuppressTransaction();
            CallRollbackExplicitlyFromATransaction();
            CreateTransactionThatSpanTwoDifferentDatabases();
            CreateTransactionOperationsAsync().GetAwaiter().GetResult();
            ClearDatabase1();
            ClearDatabase2();

            Console.ReadLine();
        }

        static void CreateReadCommitedTransactionWithFiveMinutesTimeout()
        {
            var option = new TransactionOptions();
            option.IsolationLevel = IsolationLevel.ReadCommitted;
            option.Timeout = TimeSpan.FromMinutes(5);

            using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (var dbConnection = new SqlConnection(dbConnectionString1))
                {
                    dbConnection.Open();

                    var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User1')");

                    dbConnection.Execute(command);
                }

                scope.Complete();
            }
        }

        static void CreateTransactionInsideOtherTransaction()
        {
            // No problems in creating a transaction inside another (nested) transaction. 
            // You should define the behaviour or the inner transaction. This behaviour is dependent on the value of TransactionScopeOption. 
            // If you select Required as TransactionScopeOption, it will join its outer transaction. 
            // That means if the outer transaction is committed then the inner transaction will commit. 
            // If the outer transaction is rolled back, then the inner transcation will be rolled back. 
            // If you select the RequiredNew value of TrasnactionScopeOption, a new transaction will be created and this transaction will independently be committed or rolled back.
            var option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };

            using (var scopeOuter = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (var dbConnection = new SqlConnection(dbConnectionString1))
                {
                    dbConnection.Open();

                    var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User2')");

                    dbConnection.Execute(command);
                }

                using (var scopeInner = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    using (var dbConnection = new SqlConnection(dbConnectionString1))
                    {
                        dbConnection.Open();

                        var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User3')");

                        dbConnection.Execute(command);
                    }

                    scopeInner.Complete();
                }

                scopeOuter.Complete();
            }
        }

        static void InsertAnotherRowAndQueryWithSuppressTransaction()
        {
            var option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };

            using (var scopeOuter = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (var dbConnection = new SqlConnection(dbConnectionString1))
                {
                    dbConnection.Open();

                    var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User4')");

                    dbConnection.Execute(command);
                }

                // You can also use another transaction with Suppress option if you want to just do something outside of the current ambient transaction
                IEnumerable<User> users = null;

                using (var scopeSuppress = new TransactionScope(TransactionScopeOption.Suppress, option))
                {
                    using (var dbConnection = new SqlConnection(dbConnectionString1))
                    {
                        dbConnection.Open();

                        var command = new CommandDefinition("SELECT * FROM [dbo].[User] with (NOLOCK)");

                        users = dbConnection.Query<User>(command);
                    }

                    scopeSuppress.Complete();
                }

                foreach (var user in users)
                {
                    Console.WriteLine($"User => Id: {user.Id}, Username: {user.Username}");
                }

                scopeOuter.Complete();
            }
        }

        static void CallRollbackExplicitlyFromATransaction()
        {
            // If you do not call the TransactionScope.Complete() method then the transaction will automatically be rolled back. 
            // If you need to explicitly call rollback for some scenarios, then you have two options:
            // - Executing Transaction.Current.Rollback() will rollback the current transaction.
            // - Executing TransactionScope.Dispose() will also rollback the current transaction.
            // If you explicitly call Transaction.Rollback() or TranactionScope.Dispose() then you should not call the TransactionScope.Complete() method.
            // If you do so then you will get an ObjectDisposeException.
            // "Cannot access a disposed object. Object name 'TransactionScope'".
            var option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };

            using (var scopeOuter = new TransactionScope(TransactionScopeOption.Required, option))
            {
                using (var dbConnection = new SqlConnection(dbConnectionString1))
                {
                    dbConnection.Open();

                    var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User5')");

                    dbConnection.Execute(command);
                }

                using (var scopeInner = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    using (var dbConnection = new SqlConnection(dbConnectionString1))
                    {
                        dbConnection.Open();

                        var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User6')");

                        dbConnection.Execute(command);
                    }

                    Transaction.Current.Rollback();
                }
            }
        }

        static void CreateTransactionThatSpanTwoDifferentDatabases()
        {
            try
            {
                // Create the TransactionScope to execute the commands, guaranteeing
                // that both commands can commit or roll back as a single unit of work.
                var option = new TransactionOptions();
                option.IsolationLevel = IsolationLevel.ReadCommitted;
                option.Timeout = TimeSpan.FromMinutes(5);

                using (var scope = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    using (var connection1 = new SqlConnection(dbConnectionString1))
                    {
                        // Opening the connection automatically enlists it in the
                        // TransactionScope as a lightweight transaction.
                        connection1.Open();

                        // Create the first command object and execute it
                        var command1 = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User7')");

                        connection1.Execute(command1);

                        // If you get here, this means that command1 succeeded. By nesting
                        // the using block for connection2 inside that of connection1, you
                        // conserve server and network resources as connection2 is opened
                        // only when there is a chance that the transaction can commit.
                        using (var connection2 = new SqlConnection(dbConnectionString2))
                        {
                            // The transaction is escalated to a full distributed
                            // transaction when connection2 is opened.
                            connection2.Open();

                            // Execute the second command in the second database.
                            var command2 = new CommandDefinition("INSERT INTO [dbo].[Photo] VALUES('Photo1')");

                            connection2.Execute(command2);
                        }
                    }

                    // The Complete method commits the transaction. If an exception has been thrown,
                    // Complete is not called and the transaction is rolled back.
                    scope.Complete();

                    IEnumerable<User> users = null;
                    IEnumerable<Photo> photos = null;

                    using (var scopeSuppress = new TransactionScope(TransactionScopeOption.Suppress, option))
                    {
                        using (var connection1 = new SqlConnection(dbConnectionString1))
                        {
                            connection1.Open();

                            var command = new CommandDefinition("SELECT * FROM [dbo].[User] with (NOLOCK)");

                            users = connection1.Query<User>(command);

                            using (var connection2 = new SqlConnection(dbConnectionString2))
                            {
                                // The transaction is escalated to a full distributed
                                // transaction when connection2 is opened.
                                connection2.Open();

                                // Execute the second command in the second database.
                                var command2 = new CommandDefinition("SELECT * FROM [dbo].[Photo] with (NOLOCK)");

                                photos = connection2.Query<Photo>(command2);
                            }
                        }

                        scopeSuppress.Complete();
                    }

                    foreach (var user in users)
                    {
                        Console.WriteLine($"User => Id: {user.Id}, Username: {user.Username}");
                    }

                    foreach (var photo in photos)
                    {
                        Console.WriteLine($"Photo => Id: {photo.Id}, Label: {photo.Label}");
                    }
                }
            }
            catch (TransactionAbortedException ex)
            {
                Console.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
            }
        }

        static async Task CreateTransactionOperationsAsync()
        {
            var option = new TransactionOptions
            {
                IsolationLevel = IsolationLevel.ReadCommitted,
                Timeout = TimeSpan.FromSeconds(60)
            };

            using (var scope = new TransactionScope(TransactionScopeOption.Required, option, TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var dbConnection = new SqlConnection(dbConnectionString1))
                {
                    await dbConnection.OpenAsync().ConfigureAwait(false);

                    var command = new CommandDefinition("INSERT INTO [dbo].[User] VALUES('User8')");

                    await dbConnection.ExecuteAsync(command).ConfigureAwait(false);
                }

                scope.Complete();
            }
        }

        static void ClearDatabase1()
        {
            using (var dbConnection = new SqlConnection(dbConnectionString1))
            {
                dbConnection.Open();

                var command = new CommandDefinition("DELETE FROM [dbo].[User]");

                dbConnection.Execute(command);
            }
        }

        static void ClearDatabase2()
        {
            using (var dbConnection = new SqlConnection(dbConnectionString2))
            {
                dbConnection.Open();

                var command = new CommandDefinition("DELETE FROM [dbo].[Photo]");

                dbConnection.Execute(command);
            }
        }
    }
}