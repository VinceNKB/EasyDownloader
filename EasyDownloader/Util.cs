using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyDownloader
{
    class Util
    {
        static HashSet<string> imageSuffix = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "jpg", "bmp", "jpeg", "png", "gif"
        };

        static HashSet<string> videoSuffix = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "avi", "baimov", "rmvb", "rm", "flv", "mp4", "3gp"
        };


        public static bool IsValidUrl(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            Uri uriResult;
            bool result = Uri.TryCreate(text, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            return result;
        }

        public static bool IsImageUrl(string url, bool validateUrlFormat = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            if (validateUrlFormat && !Util.IsValidUrl(url))
            {
                return false;
            }

            string[] pieces = url.Split('.');
            return pieces != null && pieces.Length > 0 && imageSuffix.Contains(pieces[pieces.Length - 1]);
        }

        public static bool IsVideoUrl(string url, bool validateUrlFormat = false)
        {
            if (string.IsNullOrEmpty(url))
            {
                return false;
            }

            if (validateUrlFormat && !Util.IsValidUrl(url))
            {
                return false;
            }

            string[] pieces = url.Split('.');
            return pieces != null && pieces.Length > 0 && videoSuffix.Contains(pieces[pieces.Length - 1]);
        }

        public static String GetTimestamp()
        {
            return DateTime.Now.ToString("yyyyMMddHHmmssf");
        }
    }
}
