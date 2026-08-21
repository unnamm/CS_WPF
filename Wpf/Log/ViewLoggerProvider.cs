using Configuration.Config;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Collections.ObjectModel;

namespace Log
{
    /// <summary>
    /// ObservableCollection View List Log
    /// </summary>
    public class ViewLoggerProvider : ILoggerProvider, ISupportExternalScope, IEntrySink
    {
        private readonly IOptionsMonitor<Appsettings> _options;
        private Action<Action> _uiInvoker = action => action();
        private IExternalScopeProvider? _scopeProvider;
        public ObservableCollection<LogEntry> Logs { get; } = [];

        public ViewLoggerProvider(IOptionsMonitor<Appsettings> options)
        {
            _options = options;
        }

        public void SetInvoker(Action<Action> invoker) => _uiInvoker = invoker;

        public void Add(LogEntry log)
        {
            _uiInvoker(() =>
            {
                Logs.Add(log);
                while (Logs.Count > _options.CurrentValue.LogMaxValue)
                    Logs.RemoveAt(0);
            });
        }

        public void SetScopeProvider(IExternalScopeProvider scopeProvider) => _scopeProvider = scopeProvider;
        public ILogger CreateLogger(string categoryName) => new LoggerBase(categoryName, this, () => _scopeProvider);
        public void Dispose() { }
    }
}
