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
        public static DownloaderBase Create(DownloaderType type, TaskInfo taskInfo)
        {
            switch (type)
            {
                case DownloaderType.Default:
                    return new PictureDownloader(taskInfo);
                case DownloaderType.Picture:
                    return new PictureDownloader(taskInfo);
                case DownloaderType.Video:
                    return new StreamDownloader(taskInfo);
                default:
                    return new PictureDownloader(taskInfo);
            }
        }
    }
}
