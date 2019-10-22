using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitoring
{
    public class Logger
    {
        private static readonly Lazy<Logger> Lazy = new Lazy<Logger>(() => new Logger());
        public static Logger Instance
        {
            get { return Lazy.Value; }
        }

        internal Logger()
        {
            LoggerFileName = "example";
            LoggerFileExtension = ".log";
        }

        public StreamWriter Writer { get; set; }

        public string LoggerPath
        {
            get { return _LoggerPath ?? (_LoggerPath = AppDomain.CurrentDomain.BaseDirectory); }
            set { _LoggerPath = value; }
        } private string _LoggerPath;

        public string LoggerFileName { get; set; }

        public string LoggerFileExtension { get; set; }

        public string LoggerFile { get { return LoggerFileName + LoggerFileExtension; } }

        public string LoggerFullPath { get { return Path.Combine(LoggerPath, LoggerFile); } }

        public bool LoggerExists { get { return File.Exists(LoggerFullPath); } }

        public void WriteLineToLogger(string msg)
        {
            WriteToLogger(msg + Environment.NewLine);
        }

        public void WriteToLogger(string msg)
        {
            if (!Directory.Exists(LoggerPath))
            {
                Directory.CreateDirectory(LoggerPath);
            }
            if (Writer == null)
            {
                Writer = new StreamWriter(LoggerFullPath, true);
            }

            Writer.Write(msg);
            Writer.Flush();
        }

        public static void WriteLine(string msg)
        {
            Instance.WriteLineToLogger(msg);
        }

        public static void Write(string msg)
        {
            Instance.WriteToLogger(msg);
        }
    }
}
