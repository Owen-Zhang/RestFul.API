using System;
using System.Text;
using System.Web;

namespace RestFul.API.Extensions
{
    public static class HttpRequestExtensions
    {
        private static string WebHostDirectoryName = "";

        public static string GetOperationName(this HttpRequest request)
        {
            var pathInfo = request.GetLastPathInfo();
            return GetOperationNameFromLastPathInfo(pathInfo);
        }

        public static string GetOperationNameFromLastPathInfo(string lastPathInfo)
        {
            if (string.IsNullOrEmpty(lastPathInfo)) return null;

            var operationName = lastPathInfo.Substring("/".Length);

            return operationName;
        }

        public static string GetLastPathInfo(this HttpRequest request)
        {
            var pathInfo = request.PathInfo;
            if (string.IsNullOrEmpty(pathInfo))
                pathInfo = GetLastPathInfoFromRawUrl(request.RawUrl);

            return pathInfo;
        }

        public static string GetLastPathInfoFromRawUrl(string rawUrl)
        {
            var pathInfo = rawUrl.IndexOf("?") != -1
                ? rawUrl.Substring(0, rawUrl.IndexOf("?"))
                : rawUrl;

            pathInfo = pathInfo.Substring(pathInfo.LastIndexOf("/"));

            return pathInfo;
        }

        public static string GetPathInfo(this HttpRequest request) {
            if (!string.IsNullOrEmpty(request.PathInfo))
                return request.PathInfo.TrimEnd('/');

            var appPath = string.IsNullOrEmpty(request.ApplicationPath)
                          ? WebHostDirectoryName
                          : request.ApplicationPath.TrimStart('/');

            var path = request.Path;
            return GetPathInfo(path, null, appPath);
        }

        public static string GetPathInfo(string fullPath, string mode, string appPath)
        {
            var pathInfo = ResolvePathInfoFromMappedPath(fullPath, mode);
            if (!string.IsNullOrEmpty(pathInfo)) return pathInfo;

            pathInfo = ResolvePathInfoFromMappedPath(fullPath, appPath);
            if (!string.IsNullOrEmpty(pathInfo)) return pathInfo;

            return fullPath;
        }

        public static string ResolvePathInfoFromMappedPath(string fullPath, string mappedPathRoot)
        {
            var sbPathInfo = new StringBuilder();
            var fullPathParts = fullPath.Split('/');
            var isMultipleRoot = string.IsNullOrEmpty(mappedPathRoot) ? false : (mappedPathRoot.Split('/').Length > 1);
            var pathRootFound = false;
            var mappedPart = string.Empty;
            foreach (var fullPathPart in fullPathParts)
            {
                if (pathRootFound)
                {
                    sbPathInfo.Append("/" + fullPathPart);
                }
                else
                {
                    if (isMultipleRoot)
                    {
                        mappedPart += (string.IsNullOrEmpty(mappedPart) ? string.Empty : "/") + fullPathPart;
                        if (string.Equals(mappedPart, mappedPathRoot, StringComparison.InvariantCultureIgnoreCase))
                        {
                            pathRootFound = true;
                        }
                    }
                    else
                    {
                        pathRootFound = string.Equals(fullPathPart, mappedPathRoot, StringComparison.InvariantCultureIgnoreCase);
                    }
                }
            }
            if (!pathRootFound) return null;

            var path = sbPathInfo.ToString();
            return path.Length > 1 ? path.TrimEnd('/') : "/";
        }

        public static string GetUrlHostName(this HttpRequest request)
        {
            try
            {
                return request.Url.Host;
            }
            catch {
                return request.UserHostName;
            }
        }
    }
}
