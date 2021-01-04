namespace EasyDownloader.Downloader
{
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;

    // Download large data, such as video
    class StreamDownloader : DownloaderBase
    {
        public DownloaderType Type 
        {
            get { return DownloaderType.Video; }
        }

        public StreamDownloader() : base()
        {
        }

        public override void Download(TaskInfo taskInfo)
        {
            this.TaskInfo = taskInfo;
            this.TaskInfo.State = State.Downloading;
            Diagnostics.WriteDebugTrace($"Downloader. Downloading {this.TaskInfo.Url}", Diagnostics.DebugLevel.Debug);
            this.TaskInfo.DirPath = Config.VideoPath;

            HttpClient client = new HttpClient(this.ClientHandler, true);
            var response = client.GetAsync(this.TaskInfo.Url).Result;
            using (var fs = new FileStream(
                this.TaskInfo.FilePath,
                FileMode.CreateNew))
            {
                response.Content.CopyToAsync(fs).Wait();
            }

            this.TaskInfo.State = State.Completed;
            Diagnostics.WriteDebugTrace($"Downloader. Complete {this.TaskInfo.Url}", Diagnostics.DebugLevel.Debug);
        }
    }
}
