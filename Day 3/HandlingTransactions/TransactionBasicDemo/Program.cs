using System;
using System.Data.SQLite;
using System.IO;

namespace TransactionBasicDemo
{
    class Program
    {
        static string dbFilePath = "./TestDb.sqlite";
        static string dbConnectionString = $"Data Source={dbFilePath};Version=3;";

        static void Main(string[] args)
        {
            CreateDb();
            CreateDatabaseTables();
            RollbackTransactionExample();
            QueryDb();
            SuccessTransactionExample();
            QueryDb();
            ClearDatabase();
        }

        private static void QueryDb()
        {
            using (var dbConnection = new SQLiteConnection(dbConnectionString))
            {
                dbConnection.Open();

                var userRows = dbConnection.ExecuteQuery<User>("SELECT * FROM User");

                foreach (var user in userRows)
                {
                    Console.WriteLine($"User: {user.Id} - {user.Username}");
                }
            }

            using (var dbConnection = new SQLiteConnection(dbConnectionString))
            {
                dbConnection.Open();

                var photoRows = dbConnection.ExecuteQuery<Photo>("SELECT * FROM Photo");

                foreach (var photo in photoRows)
                {
                    Console.WriteLine($"Photo: {photo.Id} - {photo.Label} of user: {photo.UserId}");
                }
            }
        }

        static void RollbackTransactionExample()
        {
            using (var dbConnection = new SQLiteConnection(dbConnectionString))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.ExecuteNonQuery("INSERT INTO User VALUES(1, 'Admin')", transaction);
                        dbConnection.ExecuteNonQuery("INSERT INTO Photo VALUES(1, 'TestValue1', 1)", transaction);
                        dbConnection.ExecuteNonQuery("INSERT INTO Photo VALUES(1, 'TestValue2', 1)", transaction); // Throws exception due to primary key constraint 

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        // Rollback the transaction on error
                        transaction.Rollback();
                    }
                }
            }
        }

        static void SuccessTransactionExample()
        {
            using (var dbConnection = new SQLiteConnection(dbConnectionString))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.ExecuteNonQuery("INSERT INTO User VALUES(1, 'Admin')", transaction);
                        dbConnection.ExecuteNonQuery("INSERT INTO Photo VALUES(1, 'TestValue1', 1)", transaction);
                        dbConnection.ExecuteNonQuery("INSERT INTO Photo VALUES(2, 'TestValue2', 1)", transaction);

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        // Rollback the transaction on error
                        transaction.Rollback();
                    }
                }
            }
        }

        static void CreateDb()
        {
            if (!File.Exists(dbFilePath))
            {
                SQLiteConnection.CreateFile(dbFilePath);
            }
        }

        static void CreateDatabaseTables()
        {
            using (var dbConnection = new SQLiteConnection(dbConnectionString))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.ExecuteNonQuery(@"
                            CREATE TABLE IF NOT EXISTS [User] (
                                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Username] NVARCHAR(100) NOT NULL
                            )
                        ");

                        dbConnection.ExecuteNonQuery(@"
                            CREATE TABLE IF NOT EXISTS [Photo] (
                                [Id] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
                                [Label] NVARCHAR(100) NOT NULL,
                                [UserId] INTEGER NOT NULL,
                                CONSTRAINT FK_User_Photo FOREIGN KEY (UserId)
                                REFERENCES User (Id)
                                ON DELETE CASCADE
                                ON UPDATE CASCADE
                            )
                        ");

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        // Rollback the transaction on error
                        transaction.Rollback();
                    }
                }
            }
        }

        static void ClearDatabase()
        {
            using (var dbConnection = new SQLiteConnection(dbConnectionString))
            {
                dbConnection.Open();

                using (var transaction = dbConnection.BeginTransaction())
                {
                    try
                    {
                        dbConnection.ExecuteNonQuery("DROP TABLE Photo;");
                        dbConnection.ExecuteNonQuery("DROP TABLE User;");

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);

                        // Rollback the transaction on error
                        transaction.Rollback();
                    }
                }
            }
        }
    }
}