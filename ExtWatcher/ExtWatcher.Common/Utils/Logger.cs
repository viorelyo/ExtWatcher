using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ExtWatcher.Common.Utils
{
    public class Logger
    {
        private static Queue<Log> _logQueue;

        private const string _defaultLogPath = @"C:\ExtWatcher";
        private const string _defaultLogFile = "file.log";
        private const int _defaultFlushAtAge = 10;
        private const int _defaultFlushAtQty = 10;

        private static string _logPath;
        private static string _logFile;
        private static int _flushAtAge;
        private static int _flushAtQty;

        private static DateTime _lastFlushedAt;

        static Logger()
        {
            _logQueue = new Queue<Log>();
            _lastFlushedAt = DateTime.Now;

            bool shouldConfigureDefault = false;
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                _logPath = appSettings["logger:LogPath"] ?? _defaultLogPath;
                _logFile = appSettings["logger:LogFile"] ?? _defaultLogFile;
                _flushAtAge = int.Parse(appSettings["logger:FlushAtAge"] ?? _defaultFlushAtAge.ToString());
                _flushAtQty = int.Parse(appSettings["logger:FlushAtQty"] ?? _defaultFlushAtQty.ToString());
            }
            catch (ConfigurationErrorsException)
            {
                shouldConfigureDefault = true;
            }
            catch (FormatException)
            {
                shouldConfigureDefault = true;
            }

            if (shouldConfigureDefault)
            {
                _logPath = _defaultLogPath;
                _logFile = _defaultLogFile;
                _flushAtAge = _defaultFlushAtAge;
                _flushAtQty = _defaultFlushAtQty;
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
                string path = Path.Combine(_logPath, entry.GetDate() + "_" + _logFile);

                FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write);
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(String.Format(@"[{0} {1}] - {2}",
                        entry.GetDate(),
                        entry.GetTime(),
                        entry.GetMessage()));
                }
                stream.Close();
            }
        }
    }
}
