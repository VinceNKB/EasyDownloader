using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDownloader.Downloader
{
    // Download all format of picture
    class DownloaderFactory
    {
        public static DownloaderBase Create(DownloaderType type)
        {
            switch (type)
            {
                case DownloaderType.Default:
                    return new PictureDownloader();
                case DownloaderType.Picture:
                    return new PictureDownloader();
                case DownloaderType.Video:
                    return new StreamDownloader();
                default:
                    return new PictureDownloader();
            }
        }
    }
}
