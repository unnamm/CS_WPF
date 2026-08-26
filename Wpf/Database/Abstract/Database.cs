using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Database.Abstract
{
    public abstract class Database : IDatabase
    {
        readonly DbConnection _connect;
        public Database(DbConnection connection) => _connect = connection;
        public bool IsConnected => _connect.State == ConnectionState.Open;

        public Task ConnectAsync(CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(_connect.DataSource))
                throw new Exception("path is empty");

            if (File.Exists(_connect.DataSource))
                Console.WriteLine($"open file: {_connect.DataSource}");
            else
                Console.WriteLine($"create new file: {_connect.DataSource}");

            return _connect.OpenAsync(token);
        }
    }
}
