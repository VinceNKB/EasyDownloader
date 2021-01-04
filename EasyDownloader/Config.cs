using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDownloader
{
    class Config
    {
        public static string ImagePath { get; set; } = @"F:\download\note\pp3\image";

        public static string VideoPath { get; set; } = @"F:\download\note\pp3\video";

        public static string ProxyHost { get; set; } = "127.0.0.1";

        public static int ProxyPort { get; set; } = 10808;
    }
}
