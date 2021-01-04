namespace EasyDownloader.Downloader
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    // Download all format of picture
    internal class PictureDownloader : DownloaderBase
    {
        public DownloaderType Type 
        {
            get { return DownloaderType.Picture; }
        }

        public PictureDownloader() : base()
        {
        }

        public override void Download(TaskInfo taskInfo)
        {
            this.TaskInfo = taskInfo;
            this.TaskInfo.State = State.Downloading;
            Diagnostics.WriteDebugTrace($"Downloader. Downloading {this.TaskInfo.Url}", Diagnostics.DebugLevel.Debug);
            this.TaskInfo.DirPath = Config.ImagePath;

            try
            {
                HttpClient client = new HttpClient(this.ClientHandler, true);
                var response = client.GetAsync(this.TaskInfo.Url).Result;
                using (var fs = new FileStream(
                    this.TaskInfo.FilePath,
                    FileMode.CreateNew))
                {
                    response.Content.CopyToAsync(fs).Wait();
                }
            }
            catch (Exception ex)
            {
                Diagnostics.WriteDebugTrace($"Downloader. Error {this.TaskInfo.Url}", Diagnostics.DebugLevel.Critical);
                Diagnostics.WriteDebugTrace($"Error {ex.ToString()}", Diagnostics.DebugLevel.Exception);
            }
            

            this.TaskInfo.State = State.Completed;
            Diagnostics.WriteDebugTrace($"Downloader. Complete {this.TaskInfo.Url}", Diagnostics.DebugLevel.Debug);
        }
    }
}
