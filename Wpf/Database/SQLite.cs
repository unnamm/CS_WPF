using Configuration.Config;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class SQLite : Abstract.DBbase
    {
        public SQLite(IOptionsMonitor<DbSettings> option, ILogger<SQLite> logger) :
            base(new SqliteConnection($"Data Source={option.CurrentValue.Path}"), logger)
        { }

        public override async Task ConnectAsync(CancellationToken token = default)
        {
            await base.ConnectAsync(token);
            await MakeUserTableAsync(token);
        }

        Task<int> MakeUserTableAsync(CancellationToken token)
        {
            var query = "CREATE TABLE IF NOT EXISTS Users (Id TEXT PRIMARY KEY, Password TEXT NOT NULL)";
            return NonQueryAsync(query, token);
        }
    }
}
