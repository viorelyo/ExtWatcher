using ExtWatcher.Common;
using ExtWatcher.Common.Contract;
using ExtWatcher.Common.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ExtWatcher.WCF.Service.Controller
{
    public class FileAnalyzer
    {
        private string fileToBeAnalyzed;
        private string originalFilePath;

        public void Prepare(FileEventArgs args)
        {
            originalFilePath = Path.Combine(args.Folder, args.FileName);
            PutFileInQuarantine();
        }

        public void Analyze()
        {
            bool isMalicious = SubmitFile();
            TakeAction(isMalicious);
        }

        private void PutFileInQuarantine()
        {
            fileToBeAnalyzed = Path.Combine(Constants.QuarantineFolderPath, Path.GetFileName(originalFilePath));
            File.Move(originalFilePath, fileToBeAnalyzed);
        }

        private bool SubmitFile()
        {
            bool status = false;

            Logger.WriteToLog(String.Format("Submitting file: '{0}'", fileToBeAnalyzed));
            try
            {
                using (var httpClient = new HttpClient())
                {
                    var fileStream = File.Open(fileToBeAnalyzed, FileMode.Open);
                    var fileInfo = new FileInfo(fileToBeAnalyzed);

                    var content = new MultipartFormDataContent();
                    content.Headers.Add("filePath", fileToBeAnalyzed);
                    content.Add(new StreamContent(fileStream), "\"file\"", String.Format("\"{0}\"", fileToBeAnalyzed + fileInfo.Extension));

                    var task = httpClient.PostAsync(Constants.CloudAnalyzerURL, content)
                        .ContinueWith(t =>
                        {
                            if (t.Status == TaskStatus.RanToCompletion)
                            {
                                var response = t.Result;
                                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                                {
                                    // TODO process response and return true / false depending on response
                                    status = true;
                                }
                            }

                            fileStream.Dispose();
                        });

                    task.Wait();
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog("Unable to submit file.");
                Logger.WriteToLog(ex);
                return false;
            }

            return status;
        }

        private void TakeAction(bool isFileMalicious)
        {
            if (isFileMalicious)
            {
                Logger.WriteToLog(String.Format("File: '{0}' is malicious. Deleting it.", originalFilePath));
                File.Delete(fileToBeAnalyzed);
            }
            else
            {
                Logger.WriteToLog(String.Format("File: '{0}' is benign. Removing it from Quarantine.", originalFilePath));
                File.Move(fileToBeAnalyzed, originalFilePath);
            }
        }
    }
}
