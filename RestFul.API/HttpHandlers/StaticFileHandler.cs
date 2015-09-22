using System;
using System.IO;
using System.Web;
using RestFul.API.Extensions;
using RestFul.API.Http;

namespace RestFul.API.HttpHandlers
{
    public class StaticFileHandler : HttpHandlerBase
    {
        public StaticFileHandler() : base(null) { }

        public override void ProcessRequest(AspNetRequest httpReq, AspNetResponse httpRes, string operationName)
        {
            /*下载要控制最大值*/

            if (httpRes.IsClosed) return;

            /*现只支持单个文件下载，以后会支持多文件打包下载*/

            var fileName = httpReq.HttpRequest.PhysicalPath;

            /*此处还要判断下载的文件是否为allow格式*/

            var file = new FileInfo(fileName);

            if (!file.Exists)
                throw new HttpException(404, string.Format("File '{0}' not exists", httpReq.PathInfo));

            try
            {
                var parts = fileName.Split('.');
                var fileExt = parts[parts.Length - 1];

                httpRes.ContentType = MimeTypes.GetMimeType(fileExt);
                httpRes.Response.TransmitFile(fileName);

            }
            catch (Exception)
            {
                throw new HttpException(403, "Forbidden, please contact Manager");
            }
        }
    }
}
