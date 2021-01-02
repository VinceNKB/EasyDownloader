namespace EasyDownloader.Downloader
{
    abstract class DownloaderBase
    {
        public TaskInfo TaskInfo { get; set; }

        public DownloaderBase(TaskInfo taskInfo)
        {
            this.TaskInfo = taskInfo;
        }

        public abstract void Download();
    }

    enum DownloaderType
    {
        Default = 0,
        Picture = 1,
        Video = 2
    }
}
