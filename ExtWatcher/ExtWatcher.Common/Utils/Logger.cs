﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.Common.Utils
{
    public class Logger
    {
        private static Logger _instance;
        private static Queue<Log> _logQueue;

        private static string _logPath = ConfigurationManager.AppSettings["LogPath"];
        private static string _logFile = ConfigurationManager.AppSettings["LogFile"];
        private static int _flushAtAge = int.Parse(ConfigurationManager.AppSettings["FlushAtAge"]);
        private static int _flushAtQty = int.Parse(ConfigurationManager.AppSettings["FlushAtQty"]);

        private static DateTime _lastFlushedAt;
        
        private Logger() { }

        public static Logger GetInstance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Logger();
                    _logQueue = new Queue<Log>();
                    _lastFlushedAt = DateTime.Now;
                }
                return _instance;
            }
        }

        public static void WriteToLog(string message)
        {
            lock (_logQueue)
            {
                Log log = new Log(message);
                _logQueue.Enqueue(log);

                // Check if should flush
                if (_logQueue.Count >= _flushAtQty || CheckTimeToFlush())
                {
                    FlushLogToFile();
                }
            }
        }

        public static void WriteToLog(Exception e)
        {
            lock (_logQueue)
            {
                Log msg = new Log(e.Source.ToString().Trim() + " " + e.Message.ToString().Trim());
                Log stack = new Log("Stack: " + e.StackTrace.ToString().Trim());
                _logQueue.Enqueue(msg);
                _logQueue.Enqueue(stack);

                if (_logQueue.Count >= _flushAtQty || CheckTimeToFlush())
                {
                    FlushLogToFile();
                }
            }
        }

        /// <summary>
        /// Force flush the log queue
        /// </summary>
        public static void ForceFlush()
        {
            FlushLogToFile();
        }

        /// <summary>
        /// Check if is it time to flush to file
        /// </summary>
        /// <returns></returns>
        private static bool CheckTimeToFlush()
        {
            TimeSpan time = DateTime.Now - _lastFlushedAt;
            if (time.TotalSeconds >= _flushAtAge)
            {
                _lastFlushedAt = DateTime.Now;
                return true;
            }
            return false;
        }

        private static void FlushLogToFile()
        {
            if (!Directory.Exists(_logPath))
            {
                Directory.CreateDirectory(_logPath);
            }

            while (_logQueue.Count > 0)
            {
                Log entry = _logQueue.Dequeue();
                string path = _logPath + entry.GetDate() + "_" + _logFile;

                FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(String.Format(@"[{0} {1}] - {2}", 
                        entry.GetDate(), 
                        entry.GetTime(),
                        entry.GetMessage()));
                    stream.Close();
                }
            }
        }
    }
}
