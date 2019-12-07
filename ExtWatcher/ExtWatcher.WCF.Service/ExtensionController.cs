using ExtWatcher.Common.Utils;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service.Controller
{
    public class ExtensionController
    {
        private List<string> _supportedExtensions;

        public ExtensionController()
        {
            _supportedExtensions = new HashSet<string>();

            _supportedExtensions = ConfigurationManager.AppSettings.AllKeys
                                .Where(key => key.Equals("extwatcher:SupportedExtension"))
                                .Select(key => ConfigurationManager.AppSettings[key])
                                .

        }

        public bool IsExtensionSupported(string filePath)
        {
            Logger.WriteToLog(String.Format("Checking if File: {0} is PDF", filePath));
            string extension = Path.GetExtension(filePath);

            if (_supportedExtensions.Contains(filePath))
            {
                return true;
            }
            return false;
        }
    }
}
