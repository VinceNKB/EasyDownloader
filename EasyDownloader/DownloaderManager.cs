namespace EasyDownloader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    using EasyDownloader.Downloader;

    class DownloaderManager
    {
        public static readonly int DEFAULT_DOWNLOADER_COUNT = 3;

        private ConcurrentDictionary<IDownloader, Task> downloaderPool;

        public int DownloaderCount { get; }

        public TaskQueue Queue { get; private set; }

        public DownloaderManager(TaskQueue queue, int downloadCount = 0)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            this.DownloaderCount = downloadCount > 0 ? downloadCount : DEFAULT_DOWNLOADER_COUNT;
            this.Queue = queue;
        }
    }
}
