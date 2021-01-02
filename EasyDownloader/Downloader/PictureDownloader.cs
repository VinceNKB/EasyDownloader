using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDownloader.Downloader
{
    // Download all format of picture
    internal class PictureDownloader : DownloaderBase
    {
        public DownloaderType Type 
        {
            get { return DownloaderType.Picture; }
        }

        public PictureDownloader(TaskInfo taskInfo) : base(taskInfo)
        {
        }

        public override void Download()
        {
            Diagnostics.WriteDebugTrace($"Downloader. Downloading ${this.TaskInfo.Url}", Diagnostics.DebugLevel.Debug);
            Diagnostics.WriteDebugTrace("NotImplementedException", Diagnostics.DebugLevel.Exception);
            //throw new NotImplementedException();
        }
    }
}
