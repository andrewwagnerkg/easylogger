using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Logger
{
    class HttpLogger:IHttpLogger
    {
        private int Timeout = 30000;
        public string Url { get; set; }
        public bool IsPOST { get; set; }

        public HttpLogger(string Url, bool IsPost)
        {
            this.Url = Url;
            this.IsPOST = IsPost;
        }

        public void Write<T>(string text) where T : class
        {
            if (!IsPOST)
            {
                try
                {
                    WebRequest request = WebRequest.Create($"{Url}?log={typeof(T).Name}|{text}");
                    request.Method = "GET";
                    request.Timeout = Timeout;
                    WebResponse response = request.GetResponse();
                    string result = string.Empty;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                try
                {
                    WebRequest request = WebRequest.Create(Url);
                    request.Timeout = Timeout;
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    byte[] data = Encoding.UTF8.GetBytes($"log={typeof(T).Name}|{text}");
                    request.ContentLength = data.Length;
                    using (Stream stream = request.GetRequestStream())
                    {
                        stream.Write(data, 0, data.Length);
                    }
                    WebResponse response = request.GetResponse();
                    string result = string.Empty;
                    using (Stream stream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            result = reader.ReadToEnd();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
