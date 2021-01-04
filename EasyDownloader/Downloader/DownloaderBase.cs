namespace EasyDownloader.Downloader
{
    using System.Net;
    using System.Net.Http;
    using MihaZupan;

    abstract class DownloaderBase
    {
        public HttpClientHandler ClientHandler { get; set; }

        public TaskInfo TaskInfo { get; set; }

        public DownloaderBase()
        {
            HttpToSocks5Proxy proxy = new HttpToSocks5Proxy(Config.ProxyHost, Config.ProxyPort);
            this.ClientHandler = new HttpClientHandler { Proxy = proxy };
            //clientHandler.Proxy.Credentials = new NetworkCredential(PROXY_UID, PROXY_PWD, PROXY_DMN);
        }

        public abstract void Download(TaskInfo taskInfo);
    }

    enum DownloaderType
    {
        Default = 0,
        Picture = 1,
        Video = 2
    }
}
