using ExtWatcher.Common.Utils;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace ExtWatcher.WCF.Service.Controller
{
    internal class ExtensionController
    {
        private List<string> _supportedExtensions;

        public ExtensionController()
        {
            _supportedExtensions = new List<string>();

            try
            {
                _supportedExtensions = new List<string>(ConfigurationManager.AppSettings["extwatcher:SupportedExtensions"].Split(new char[] { ';' }));
            }
            catch (ConfigurationErrorsException e)
            {
                Logger.WriteToLog("Could not extract supported extensions from config file.");
                Logger.WriteToLog(e);
            }
        }

        public bool IsExtensionSupported(string filePath)
        {
            string extension = Path.GetExtension(filePath);

            return _supportedExtensions.Contains(extension);
        }
    }
}
