using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectoryMonitoring.Controller
{
    public class ExtensionController
    {
        public static bool IsFilePDF(string filePath)
        {
            Logger.WriteLine(String.Format("Checking if File: {0} is PDF", filePath));
            string extension = Path.GetExtension(filePath);
            if (extension == ".pdf")
            {
                return true;
            }
            return false;
        }
    }
}
