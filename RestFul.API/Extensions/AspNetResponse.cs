using System.Web;
using System.IO;

namespace RestFul.API.Extensions
{
    public class AspNetResponse
    {
        private readonly HttpResponse response;

        public AspNetResponse(HttpResponse response)
        {
            this.response = response;
        }

        public HttpResponse Response
        {
            get { return response; }
        }

        public int StatusCode
        {
            set { response.StatusCode = value; }
        }

        public string StatusDescription
        {
            set { this.response.StatusDescription = value; }
        }

        public string ContentType
        {
            get { return response.ContentType; }
            set { response.ContentType = value; }
        }

        public void AddHeader(string key, string value)
        {
            response.AddHeader(key, value);
        }

        public Stream OutputStream
        {
            get { return response.OutputStream; }
        }

        public void Write(string text)
        {
            response.Write(text);
        }

        public void Close()
        {
            this.IsClosed = true;
            response.ClearContent();
            response.End();
        }

        public void Flush()
        {
            response.Flush();
        }

        public void End()
        {
            this.IsClosed = true;
            response.ClearContent();
            response.End();
        }

        public bool IsClosed
        {
            get;
            private set;
        }
    }
}
