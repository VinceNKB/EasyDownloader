namespace EasyDownloader
{
    using System;
    using System.IO;

    internal class TaskInfo
    {
        public Guid Id { get; set; }

        public string Url { get; set; }

        public string FileName { get; set; }

        public string DirPath { get; set; }

        public string FilePath 
        { 
            get
            {
                return Path.Combine(this.DirPath, this.FileName);
            }
        }

        public State State { get; set; }

        public long TotalSize { get; set; }

        public long DownloadedSize { get; set; }

        public string Ext { get; set; }

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
            this.FileName = this.GetFileName(url);
            this.Ext = this.GetExt(this.FileName);
            this.State = State.NotStart;
        }

        public TaskInfo(Guid id, string url, string fileName, string dirPath, State state)
        {
            this.Id = id;
            this.Url = url;
            this.FileName = fileName;
            this.DirPath = dirPath;
            this.State = state;
            this.Ext = this.GetExt(this.FileName);
        }

        public string GetFileName(string url)
        {
            Uri uri = new Uri(url);
            string[] segments = uri.Segments;
            return Util.GetTimestamp() + "_" + segments[segments.Length - 1];
        }

        public string GetExt(string fileName)
        {
            string[] texts = fileName.Split('.');
            return texts[texts.Length - 1];
        }
    }

    enum State
    {
        NotStart = 0, // In queue
        Downloading = 1, // In downloader
        Completed = 2 // Completed
    }
}
