using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace TransactionBasicDemo
{
    public static class SQLiteExtensions
    {
        public static IEnumerable<T> ExecuteQuery<T>(this SQLiteConnection connection, string commandText, IDbTransaction transaction = null, object param = null)
        {
            // Ensure we have a connection
            if (connection == null)
            {
                throw new NullReferenceException("Please provide a connection");
            }

            // Ensure that the connection state is Open
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // Use Dapper to execute the given query
            return connection.Query<T>(commandText, param, transaction);
        }

        public static int ExecuteNonQuery(this SQLiteConnection connection, string commandText, IDbTransaction transaction = null, object param = null)
        {
            // Ensure we have a connection
            if (connection == null)
            {
                throw new NullReferenceException("Please provide a connection");
            }

            // Ensure that the connection state is Open
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            // Use Dapper to execute the given query
            return connection.Execute(commandText, param, transaction);
        }
    }
}