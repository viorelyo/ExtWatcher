using ExtWatcher.Common;
using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Utils;
using System;
using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service.Controller
{
    public class FileAnalyzer
    {
        private string _fileToBeAnalyzed;
        private string _analyzerURL;

        public FileAnalyzer(string _cloudAnalyzerURL, FileEventArgs args)
        {
            _analyzerURL = _cloudAnalyzerURL;
            _fileToBeAnalyzed = Path.Combine(args.Folder, args.FileName);
            WaitReady();
        }

        public FileAnalysisStatus Analyze()
        {
            FileAnalysisStatus status = SubmitFile();
            TakeAction(status);
            return status;
        }

        /// <summary>
        /// Waits until a file can be opened with write permission
        /// Until file is full moved to a location
        /// </summary>
        private void WaitReady()
        {
            Logger.WriteToLog(String.Format("Waiting until file: '{0}' is available (full moved to location).", _fileToBeAnalyzed));
            while (true)
            {
                try
                {
                    using (Stream stream = File.Open(_fileToBeAnalyzed, FileMode.Open, FileAccess.ReadWrite, FileShare.ReadWrite))
                    {
                        if (stream != null)
                        {
                            Logger.WriteToLog(String.Format("File: '{0}' is ready.", _fileToBeAnalyzed));
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
                Thread.Sleep(1000);
            }
        }

        /// <summary>
        /// Set attributes for file as Hidden. (DEPRECATED: Blocks fullcontrol on file.)S
        /// </summary>
        private void BlockFile()
        {
            Logger.WriteToLog(String.Format("Blocking file: '{0}'.", _fileToBeAnalyzed));

            File.SetAttributes(_fileToBeAnalyzed, File.GetAttributes(_fileToBeAnalyzed) | FileAttributes.Hidden);

            // -------- DEPRECATED ------
            //string adminUserName = Environment.UserName;
            //FileSecurity fs = File.GetAccessControl(fileToBeAnalyzed);
            //FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            //fs.AddAccessRule(fsa);
            //File.SetAccessControl(fileToBeAnalyzed, fs);
        }

        private FileAnalysisStatus SubmitFile()
        {
            FileAnalysisStatus fileStatus = FileAnalysisStatus.Unknown;

            Logger.WriteToLog(String.Format("Creating submit request for file: '{0}'.", _fileToBeAnalyzed));
            try
            {
                using (var httpClient = new HttpClient(new HttpClientHandler() { UseDefaultCredentials = true }))
                {
                    // Open fileStream by blocking share of file to other processes
                    var fileStream = File.Open(_fileToBeAnalyzed, FileMode.Open, FileAccess.Read, FileShare.None);
                    var fileInfo = new FileInfo(_fileToBeAnalyzed);

                    var content = new MultipartFormDataContent();
                    content.Headers.Add("filePath", _fileToBeAnalyzed);
                    content.Add(new StreamContent(fileStream), "\"file\"", String.Format("{0}", _fileToBeAnalyzed));

                    BlockFile();

                    Logger.WriteToLog(String.Format("Submitting file: '{0}' to '{1}'.", _fileToBeAnalyzed, _analyzerURL));
                    Logger.WriteToLog(String.Format("Content: '{0}' to '{1}'.", content.Headers.ToString(), content.ToString()));
                    var task = httpClient.PostAsync(_analyzerURL, content)
                        .ContinueWith(t =>
                        {
                            if (t.Status == TaskStatus.RanToCompletion)
                            {
                                var response = t.Result;
                                Logger.WriteToLog(String.Format("Response of the submit request: '{0}'.", response.ToString()));

                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    string rawResponse = response.Content.ReadAsStringAsync().Result;
                                    Logger.WriteToLog(String.Format("Server response for file: '{0}' : '{1}'.", _fileToBeAnalyzed, rawResponse));

                                    string statusFromServer = rawResponse.Split(':')[1];
                                    if (statusFromServer.Contains(Constants.StatusFromServerBenign))
                                    { 
                                        fileStatus = FileAnalysisStatus.Benign;
                                    }
                                    else if (statusFromServer.Contains(Constants.StatusFromServerMalicious))
                                    {
                                        fileStatus = FileAnalysisStatus.Malicious;
                                    }
                                    else
                                    {
                                        fileStatus = FileAnalysisStatus.Unknown;
                                    }
                                }
                                else
                                {
                                    fileStatus = FileAnalysisStatus.Aborted;
                                }
                            }
                            else if (t.Status == TaskStatus.Faulted)
                            {
                                Logger.WriteToLog("Server unreachable.");
                                fileStream.Close();
                                fileStream.Dispose();

                                fileStatus = FileAnalysisStatus.Aborted;
                            }

                            fileStream.Close();
                            fileStream.Dispose();
                        });
                    task.Wait();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Unable to submit file.");
                Logger.WriteToLog(ex);

                return FileAnalysisStatus.Aborted;
            }

            return fileStatus;
        }

        private void TakeAction(FileAnalysisStatus analysisStatus)
        {
            Logger.WriteToLog(String.Format("Taking corresponding action on file: '{0}' with status '{1}'.", _fileToBeAnalyzed, FileAnalysisStatusExtension.ToString(analysisStatus)));

            // -------- DEPRECATED ------
            //string adminUserName = Environment.UserName;
            //FileSecurity fs = File.GetAccessControl(fileToBeAnalyzed);
            //FileSystemAccessRule fsa = new FileSystemAccessRule(adminUserName, FileSystemRights.FullControl, AccessControlType.Deny);
            //fs.RemoveAccessRule(fsa);
            //File.SetAccessControl(fileToBeAnalyzed, fs);

            if (analysisStatus == FileAnalysisStatus.Malicious)
            {
                Logger.WriteToLog(String.Format("File: '{0}' is malicious. Deleting it.", _fileToBeAnalyzed));
                File.Delete(_fileToBeAnalyzed);
            }
            else
            {
                Logger.WriteToLog(String.Format("Unblocking file: '{0}'.", _fileToBeAnalyzed));

                FileAttributes attr = File.GetAttributes(_fileToBeAnalyzed) & ~FileAttributes.Hidden;
                File.SetAttributes(_fileToBeAnalyzed, attr);
            }
        }
    }
}
