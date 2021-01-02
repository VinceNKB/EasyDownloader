using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDownloader
{
    internal class TaskInfo
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public State State { get; set; }

        public long TotalSize { get; set; }

        public long DownloadedSize { get; set; }

        public double DownloadedPercent 
        { 
            get { return (double)DownloadedSize / TotalSize; } 
        }

        public double Speed { get; }

        public TaskInfo(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            this.Id = Guid.NewGuid();
            this.Url = url;
            this.State = State.NotStart;
        }

        public TaskInfo(Guid id, string url, string fileName, string filePath, State state)
        {
            this.Id = id;
            this.Url = url;
            this.FileName = fileName;
            this.FilePath = filePath;
            this.State = state;
        }
    }

    enum State
    {
        NotStart = 0, // In queue
        Downloading = 1, // In downloader
        Completed = 2 // Completed
    }
}
