using System;
using System.IO;
using System.Runtime.Serialization;

namespace ExtWatcher.Common.Contract
{
    public delegate void FileEventHandler(object sender, FileEventArgs e);

    public enum FileAnalysisStatus
    {
        Unknown,
        Malicious,
        Benign,
        Aborted
    }

    public static class FileAnalysisStatusExtension
    {
        public static string ToString(this FileAnalysisStatus status)
        {
            switch (status)
            {
                case FileAnalysisStatus.Unknown:
                    return "Unknown";
                case FileAnalysisStatus.Malicious:
                    return "Malicious";
                case FileAnalysisStatus.Benign:
                    return "Benign";
                case FileAnalysisStatus.Aborted:
                    return "Aborted";
                default:
                    return "Unknown";
            }
        }
    }

    [Serializable]
    [DataContract]
    public class FileEventArgs : EventArgs
    {
        public static FileEventArgs Create(FileSystemEventArgs args, string folder)
        {
            return new FileEventArgs()
            {
                AnalysisStatus = FileAnalysisStatus.Unknown,
                Folder = folder,
                Date = DateTime.Now,
                FileName = args.Name,
                ChangeType = args.ChangeType,
                Id = Guid.NewGuid()
            };
        }

        [DataMember]
        public WatcherChangeTypes ChangeType { get; private set; }

        [DataMember]
        public DateTime Date { get; private set; }

        [DataMember]
        public string FileName { get; private set; }

        [DataMember]
        public string Folder { get; private set; }

        [DataMember]
        public FileAnalysisStatus AnalysisStatus { get; set; }

        [DataMember]
        public Guid Id { get; private set; }
    }
}
