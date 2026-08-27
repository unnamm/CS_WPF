using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace Database.Abstract
{
    public abstract class DBbase : IDatabase, IDisposable
    {
        readonly DbConnection _connect;
        readonly ILogger _logger;
        public DBbase(DbConnection connection, ILogger logger)
        {
            _connect = connection;
            _logger = logger;
        }

        public bool IsConnected => _connect.State == ConnectionState.Open;

        public virtual Task ConnectAsync(CancellationToken token = default)
        {
            if (string.IsNullOrWhiteSpace(_connect.DataSource))
                throw new Exception("path is empty");

            var directory = Path.GetDirectoryName(_connect.DataSource);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            if (File.Exists(_connect.DataSource))
                _logger.LogInformation("open file: {_connect.DataSource}", _connect.DataSource);
            else
                _logger.LogInformation("create new file: {_connect.DataSource}", _connect.DataSource);

            return _connect.OpenAsync(token);
        }

        public Task CloseAsync() => _connect.CloseAsync();
        public void Dispose() => _connect.Dispose();

        /// <summary>
        /// write
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Task<int> NonQueryAsync(string query, CancellationToken token = default)
        {
            using var cmd = _connect.CreateCommand();
            cmd.CommandText = query;
            return cmd.ExecuteNonQueryAsync(token);
        }

        /// <summary>
        /// read
        /// </summary>
        /// <param name="query"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<object[]>> ReaderAsync(string query, CancellationToken token = default)
        {
            using var cmd = _connect.CreateCommand();
            cmd.CommandText = query;

            var list = new List<object[]>();
            using var reader = await cmd.ExecuteReaderAsync(token);
            while (await reader.ReadAsync(token))
            {
                var rows = new object[reader.FieldCount];
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    rows[i] = reader[i];
                }
                list.Add(rows);
            }
            return list;
        }
    }
}
