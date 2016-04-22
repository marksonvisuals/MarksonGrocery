using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;

namespace MarksonGroceries.Helpers.HttpClient
{
    public class HttpClientInitializer : IHttpClientInitializer
    {
        public System.Net.Http.HttpClient GetHttpClient()
        {
            var httpClient = new System.Net.Http.HttpClient()
            {
                BaseAddress = new Uri(System.Configuration.ConfigurationManager.AppSettings["baseUrl"])
            };

            return httpClient;
        }
    }
}