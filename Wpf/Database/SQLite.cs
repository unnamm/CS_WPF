using Configuration.Config;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
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
            await MakeRoleTableAsync(token);
            await MakeRolePermissionTableAsync(token);
            await MakeUserTableAsync(token);
        }

        Task<int> MakeRoleTableAsync(CancellationToken token)
        {
            var query = """
                CREATE TABLE IF NOT EXISTS Roles (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL UNIQUE,
                    Level INTEGER NOT NULL)
                """;
            return NonQueryAsync(query, token);
        }

        Task<int> MakeRolePermissionTableAsync(CancellationToken token)
        {
            var query = """
                CREATE TABLE IF NOT EXISTS RolePermissions (
                    RoleId INTEGER NOT NULL REFERENCES Roles(Id),
                    PermissionCode TEXT NOT NULL,
                    PRIMARY KEY (RoleId, PermissionCode))
                """;
            return NonQueryAsync(query, token);
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

        public async Task<List<UserInfo>> GetUsers()
        {
            var rows = await ReaderAsync("SELECT Id, Rank, RoleId FROM Users", null);
            return rows.Select(r => new UserInfo(
                (string)r[0],
                r[1] as string,
                r[2] is long roleId ? (int)roleId : null)).ToList();
        }

        public async Task<List<RoleInfo>> GetRoles()
        {
            var rows = await ReaderAsync("SELECT Id, Name, Level FROM Roles ORDER BY Level", null);
            return rows.Select(r => new RoleInfo((int)(long)r[0], (string)r[1], (int)(long)r[2])).ToList();
        }

        public Task<int> UpdateUserAsync(string id, string? rank, int? roleId) =>
            NonQueryAsync("UPDATE Users SET Rank = @Rank, RoleId = @RoleId WHERE Id = @Id",
                new Dictionary<string, object?> { ["@Id"] = id, ["@Rank"] = rank, ["@RoleId"] = roleId });
    }
}
