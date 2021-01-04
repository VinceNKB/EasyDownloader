namespace EasyDownloader
{
    using System;
    using System.Threading.Tasks;
    using System.Collections.Concurrent;
    using EasyDownloader.Downloader;
    using System.Threading;

    class DownloadManager
    {
        public static readonly int DEFAULT_DOWNLOADER_COUNT = 5;

        public int DownloaderCount { get; }


        public ConcurrentDictionary<int, Task> DownloadThreadingPool = new ConcurrentDictionary<int, Task>();

        public ConcurrentDictionary<int, DownloaderBase> DownloaderDict = new ConcurrentDictionary<int, DownloaderBase>();

        public TaskQueue TaskQueue { get; private set; }

        public DownloaderType Type { get; }

        public DownloadManager(TaskQueue queue, DownloaderType type, int downloadCount = 0)
        {
            if (queue == null)
            {
                throw new ArgumentNullException(nameof(queue));
            }

            this.Type = type;
            this.DownloaderCount = downloadCount > 0 ? downloadCount : DEFAULT_DOWNLOADER_COUNT;
            this.TaskQueue = queue;
            this.Initialize();
        }

        internal void Initialize()
        {
            for (int i = 0; i < this.DownloaderCount; i++)
            {
                int id = i;
                Task currentTask = Task.Run(() => this.MainTask(id));
                this.DownloadThreadingPool.AddOrUpdate(id, currentTask, (k, v) => currentTask);
                Diagnostics.WriteDebugTrace($"Start download task. Id={id}", Diagnostics.DebugLevel.Debug);
            }

            Task.Run(() =>
            {
                while (true)
                {
                    int length = this.TaskQueue.Length;
                    int downloadingCount = 0;
                    foreach (DownloaderBase downloader in DownloaderDict.Values)
                    {
                        if (downloader.TaskInfo != null && downloader.TaskInfo.State != State.Completed)
                        {
                            downloadingCount ++;
                        }
                    }

                    Diagnostics.WriteDebugTrace($"{Type}: {length} task in queue", Diagnostics.DebugLevel.Info);
                    Diagnostics.WriteDebugTrace($"{Type}: {downloadingCount} task in downloading", Diagnostics.DebugLevel.Info);

                    Thread.Sleep(5000);
                }
            });
        }

        public void MainTask(int taskId)
        {
            DownloaderBase downloader = DownloaderFactory.Create(this.Type);

            while (true)
            {
                TaskInfo taskInfo = this.TaskQueue.Take();
                Diagnostics.WriteDebugTrace($"TaskId={taskId}. Get task from queue. Url={taskInfo.Url}", Diagnostics.DebugLevel.Debug);
                this.DownloaderDict.AddOrUpdate(taskId, downloader, (k, v) => downloader);
                downloader.Download(taskInfo);
            }
        }

        public void StartTasks()
        {
        }

        public void PauseTasks()
        {
        }

        public void EndTasks()
        {
        }
    }
}
