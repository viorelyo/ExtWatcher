using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace HelpTools
{
    class Program
    {
        static void Main(string[] args)
        {
            string fileLocation = @"C:\Users\viorel\Desktop\bitdefender_ts_2019_userguide_en.pdf";

            RemoveTrashFile(fileLocation);
        }

        private static void RemoveTrashFile(string fileLocation)
        {
            string adminUserName = Environment.UserName;

            FileSecurity fs = File.GetAccessControl(fileLocation);
            FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            fs.RemoveAccessRule(fsa);
            File.SetAccessControl(fileLocation, fs);

            File.Delete(fileLocation);
        }
    }
}
