using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitoring.Common.Contract
{
    public delegate void FileEventHandler(object sender, FileEventArgs e);

    [Serializable]
    [DataContract]
    public class FileEventArgs : EventArgs
    {
        public static FileEventArgs Create(FileSystemEventArgs args, string folder)
        {
            return new FileEventArgs()
            {
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
        public Guid Id { get; private set; }
    }
}
