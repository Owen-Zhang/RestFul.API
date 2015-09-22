using System.Collections.Generic;

namespace RestFul.API.Http
{
    public static class MimeTypes
    {
        private static List<string> allowFileType = new List<string>{ 
            "jpeg", "gif", "png", "bmp", "js", "css", "xls", "txt", "doc", "ppt", "pptx"
        };

        public static bool IsAllowDownLoadFileType(string fileType)
        {
            return
                allowFileType.Contains(fileType);
        }

        public static string GetMimeType(string fileExt)
        {
            switch (fileExt)
            {
                case "jpeg":
                case "gif":
                case "png":
                case "tiff":
                case "bmp":
                    return "image/" + fileExt;

                case "jpg":
                    return "image/jpeg";

                case "tif":
                    return "image/tiff";

                case "htm":
                case "html":
                case "shtml":
                    return "text/html";

                case "js":
                    return "text/javascript";

                case "csv":
                case "css":
                case "sgml":
                    return "text/" + fileExt;

                case "txt":
                    return "text/plain";

                case "wav":
                    return "audio/wav";

                case "mp3":
                    return "audio/mpeg3";

                case "mid":
                    return "audio/midi";

                case "qt":
                case "mov":
                    return "video/quicktime";

                case "mpg":
                    return "video/mpeg";

                case "avi":
                    return "video/" + fileExt;

                case "rtf":
                    return "application/" + fileExt;

                case "xls":
                    return "application/x-excel";

                case "doc":
                    return "application/msword";

                case "ppt":
                    return "application/powerpoint";

                case "gz":
                case "tgz":
                    return "application/x-compressed";

                default:
                    return "application/" + fileExt;
            }
        }
    }
}
