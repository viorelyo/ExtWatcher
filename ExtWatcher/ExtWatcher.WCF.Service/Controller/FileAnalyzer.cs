using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Utils;
using System;
using System.IO;
using System.Security.AccessControl;
using System.Threading;

namespace ExtWatcher.WCF.Service.Controller
{
    public class FileAnalyzer
    {
        private string fileToBeAnalyzed;

        public void Prepare(FileEventArgs args)
        {
            fileToBeAnalyzed = Path.Combine(args.Folder, args.FileName);
            WaitReady();
            BlockFile();
        }

        public void Analyze()
        {
            bool isMalicious = SubmitFile();
            TakeAction(isMalicious);
        }

        /// <summary>
        /// Waits until a file can be opened with write permission
        /// Until file is full moved to a location
        /// </summary>
        private void WaitReady()
        {
            Logger.WriteToLog(String.Format("Waiting until file: '{0}' is available (full moved to location).", fileToBeAnalyzed));
            while (true)
            {
                try
                {
                    using (Stream stream = File.Open(fileToBeAnalyzed, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        if (stream != null)
                        {
                            Logger.WriteToLog(String.Format("File: '{0}' is ready.", fileToBeAnalyzed));
                            break;
                        }
                    }
                }
                catch (FileNotFoundException)
                {   
                }
                catch (IOException)
                {
                }
                catch (UnauthorizedAccessException)
                {
                }
                Thread.Sleep(500);
            }
        }

        private void BlockFile()
        {
            string adminUserName = Environment.UserName;

            File.SetAttributes(fileToBeAnalyzed, File.GetAttributes(fileToBeAnalyzed) | FileAttributes.Hidden);
            FileSecurity fs = File.GetAccessControl(fileToBeAnalyzed);
            FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            fs.AddAccessRule(fsa);
            File.SetAccessControl(fileToBeAnalyzed, fs);
        }

        private bool SubmitFile()
        {
            bool status = false;

            Logger.WriteToLog(String.Format("Submitting file: '{0}'", fileToBeAnalyzed));
            //try
            //{
            //    using (var httpClient = new HttpClient())
            //    {
            //        var fileStream = File.Open(fileToBeAnalyzed, FileMode.Open);
            //        var fileInfo = new FileInfo(fileToBeAnalyzed);

            //        var content = new MultipartFormDataContent();
            //        content.Headers.Add("filePath", fileToBeAnalyzed);
            //        content.Add(new StreamContent(fileStream), "\"file\"", String.Format("\"{0}\"", fileToBeAnalyzed + fileInfo.Extension));

            //        var task = httpClient.PostAsync(Constants.CloudAnalyzerURL, content)
            //            .ContinueWith(t =>
            //            {
            //                if (t.Status == TaskStatus.RanToCompletion)
            //                {
            //                    var response = t.Result;
            //                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
            //                    {
            //                        // TODO process response and return true / false depending on response
            //                        status = true;
            //                    }
            //                }

            //                fileStream.Dispose();
            //            });

            //        task.Wait();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Logger.WriteToLog("Unable to submit file.");
            //    Logger.WriteToLog(ex);
            //    return false;
            //}
            Thread.Sleep(5000);
            return status;
        }

        private void TakeAction(bool isFileMalicious)
        {
            string adminUserName = Environment.UserName;

            FileSecurity fs = File.GetAccessControl(fileToBeAnalyzed);
            FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            fs.RemoveAccessRule(fsa);
            File.SetAccessControl(fileToBeAnalyzed, fs);

            if (isFileMalicious)
            {
                Logger.WriteToLog(String.Format("File: '{0}' is malicious. Deleting it.", fileToBeAnalyzed));
                File.Delete(fileToBeAnalyzed);
            }
            else
            {
                Logger.WriteToLog(String.Format("File: '{0}' is benign. Unblocking it.", fileToBeAnalyzed));

                FileAttributes attr = File.GetAttributes(fileToBeAnalyzed) & ~FileAttributes.Hidden;
                File.SetAttributes(fileToBeAnalyzed, attr);
            }
        }
    }
}
