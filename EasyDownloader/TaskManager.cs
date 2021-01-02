namespace EasyDownloader
{
    using System;
    using System.Collections.Generic;
    using EasyDownloader.Downloader;

    class TaskManager
    {
        private static TaskManager instance;

        private static Object lockObject = new Object();

        public Dictionary<DownloaderType, DownloadManager> DownloadManagerPool = new Dictionary<DownloaderType, DownloadManager>();

        public static TaskManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (lockObject)
                    {
                        if (instance == null)
                        {
                            instance = new TaskManager();
                        }
                    }
                }

                return instance;
            }
        }

        private TaskManager()
        {
            this.DownloadManagerPool.Add(DownloaderType.Picture, new DownloadManager(new TaskQueue(), DownloaderType.Picture));
            this.DownloadManagerPool.Add(DownloaderType.Video, new DownloadManager(new TaskQueue(), DownloaderType.Video));
        }

        public void AddImageTask(string url)
        {
            this.DownloadManagerPool[DownloaderType.Picture].TaskQueue.Add(new TaskInfo(url));
        }

        public void AddVideoTask(string url)
        {
            this.DownloadManagerPool[DownloaderType.Video].TaskQueue.Add(new TaskInfo(url));
        }
    }
}
