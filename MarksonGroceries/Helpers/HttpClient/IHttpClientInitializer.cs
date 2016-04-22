using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MarksonGroceries.Helpers.HttpClient
{
    public interface IHttpClientInitializer
    {
        System.Net.Http.HttpClient GetHttpClient();
    }
}