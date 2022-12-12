using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskManagerWPF.Services.Web
{
    public class HttpClientService
    {
        public static HttpClient HttpClient { get; set; }

        public HttpClientService()
        {
            HttpClient = new();
        }
    }
}
