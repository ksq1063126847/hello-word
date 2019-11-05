using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace WebApplication2.ServicesClass
{
    public class MyAsyncMethods
    {
        public async static Task<long?> GetPageLength()
        {
            HttpClient httpClient = new HttpClient();
            var response = await  httpClient.GetAsync(@"https://www.baidu.com/");
            return response.Content.Headers.ContentLength;
        }

    }
}