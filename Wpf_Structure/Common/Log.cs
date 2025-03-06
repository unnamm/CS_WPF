using Common.Config;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class Log
    {
        public ObservableCollection<string> LogList { get; set; } = []; //print list ui

        private readonly int _maxLine; //print list max line
        private readonly string _folderPath;

        private string _fileName = string.Empty;
        private DateTime _beforeDay;

        public Log(DataConfig config)
        {
            _maxLine = config.LogMaxLine;
            _folderPath = config.LogFolderName;
        }

        /// <summary>
        /// make folder, set folder path
        /// </summary>
        /// <param name="maxLine">LogList max count</param>
        private void Initialize()
        {
            _beforeDay = DateTime.Now;

            if (Directory.Exists(_folderPath) == false)
            {
                Directory.CreateDirectory(_folderPath);
            }

            _fileName = Path.Combine(_folderPath, _beforeDay.ToString("yyyy-MM-dd") + ".txt");
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

            LogList.Insert(0, message); //insert first
            if (LogList.Count > _maxLine)
            {
                LogList.RemoveAt(LogList.Count - 1);
            }
            File.AppendAllText(_fileName, message + Environment.NewLine);
        }
    }
}
