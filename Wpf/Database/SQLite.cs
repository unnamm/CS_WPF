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
    }
}
