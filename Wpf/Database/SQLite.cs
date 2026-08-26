using Configuration.Config;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Database
{
    public class SQLite : Abstract.Database
    {
        public SQLite(IOptionsMonitor<DbSettings> option) : base(new SqliteConnection($"Data Source={option.CurrentValue.Path}")) { }
    }
}
