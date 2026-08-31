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
        readonly PasswordHasher _hasher = new();

        public SQLite(IOptionsMonitor<DbSettings> option, ILogger<SQLite> logger) :
            base(new SqliteConnection($"Data Source={option.CurrentValue.Path}"), logger)
        { }

        public override async Task ConnectAsync(CancellationToken token = default)
        {
            await base.ConnectAsync(token);
            await MakeUserTableAsync(token);
        }

        async Task MakeUserTableAsync(CancellationToken token)
        {
            await NonQueryAsync("CREATE TABLE IF NOT EXISTS Users (Id TEXT PRIMARY KEY, Password TEXT NOT NULL)", token);

            await EnsureColumnAsync("Users", "Rank", "TEXT", token);
            await EnsureColumnAsync("Users", "RoleId", "INTEGER REFERENCES Roles(Id)", token);
        }

        public async Task<bool> IsUserExist(string id)
        {
            var existing = await ReaderAsync("SELECT Id FROM Users WHERE Id = @Id",
                new Dictionary<string, object?> { ["@Id"] = id });
            return existing.Count > 0;
        }

        public Task<int> InsertUser(string id, string password) =>
            NonQueryAsync("INSERT INTO Users (Id, Password) VALUES (@Id, @Password)",
                new Dictionary<string, object?> { ["@Id"] = id, ["@Password"] = _hasher.Hash(password) });

        public async Task<bool> ValidateUser(string id, string password)
        {
            var rows = await ReaderAsync("SELECT Password FROM Users WHERE Id = @Id",
                new Dictionary<string, object?> { ["@Id"] = id });

            if (rows.Count == 0)
                return false;

            var storedHash = (string)rows[0][0];
            return _hasher.Verify(password, storedHash);
        }
    }
}
