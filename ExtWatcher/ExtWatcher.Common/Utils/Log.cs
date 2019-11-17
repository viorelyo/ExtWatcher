using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.Common.Utils
{
    public class Log
    {
        private string _logMessage;
        private DateTime _logTime;

        public Log(string message)
        {
            _logMessage = message;
            _logTime = DateTime.Now;
        }

        public string GetMessage()
        {
            return _logMessage;
        }

        public string GetTime()
        {
            return _logTime.ToString("HH:mm:ss.fff");
        }

        public string GetDate()
        {
            return _logTime.ToString("dd-MM-yyyy");
        }
    }
}
