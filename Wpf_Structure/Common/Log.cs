using Common.Message;
using Common.Config;
using CommunityToolkit.Mvvm.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Log
    {
        public ObservableCollection<string> LogList { get; set; } = []; //print list ui

        private readonly DataYaml _config;

        private string _fileName = string.Empty;
        private DateTime _beforeDay;

        public Log(DataYaml config)
        {
            _config = config;
        }

        /// <summary>
        /// make folder, set folder path
        /// </summary>
        /// <param name="maxLine">LogList max count</param>
        private void Initialize()
        {
            _beforeDay = DateTime.Now;

            if (Directory.Exists(_config.LogFolderName) == false)
            {
                Directory.CreateDirectory(_config.LogFolderName);
            }

            _fileName = Path.Combine(_config.LogFolderName, _beforeDay.ToString("yyyy-MM-dd") + ".txt");
        }

        /// <summary>
        /// print textfile, print LogList array
        /// </summary>
        /// <param name="message"></param>
        public void Write(string message)
        {
            if (_beforeDay.Day != DateTime.Now.Day) //check next day
            {
                Initialize();
            }

            message = $"[{DateTime.Now:HH:mm:ss.f}] {message}";

            File.AppendAllText(_fileName, message + Environment.NewLine);

            WeakReferenceMessenger.Default.Send(new InvokeMessage(WriteUICollection, message));
        }

        private void WriteUICollection(string message)
        {
            LogList.Insert(0, message);
            if (LogList.Count > _config.LogMaxLine)
            {
                LogList.RemoveAt(LogList.Count - 1);
            }
        }
    }
}
