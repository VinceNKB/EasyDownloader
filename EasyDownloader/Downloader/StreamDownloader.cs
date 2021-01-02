namespace EasyDownloader.Downloader
{
    // Download large data, such as video
    class StreamDownloader : DownloaderBase
    {
        public DownloaderType Type 
        {
            get { return DownloaderType.Video; }
        }

        public StreamDownloader(TaskInfo taskInfo) : base(taskInfo)
        {
        }

        public override void Download()
        {
            Diagnostics.WriteDebugTrace($"Downloader. Downloading ${this.TaskInfo.Url}", Diagnostics.DebugLevel.Debug);
            Diagnostics.WriteDebugTrace("NotImplementedException", Diagnostics.DebugLevel.Exception);
            //throw new NotImplementedException();
        }
    }
}
