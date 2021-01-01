namespace EasyDownloader.Downloader
{
    interface IDownloader
    {
        /// <summary>
        /// Download from internet to local file.
        /// </summary>
        /// <param name="url">Url of resource to be downloaded</param>
        /// <param name="toPath">Local path where data is stored</param>
        void FromUrl(string url, string toPath);
    }
}
