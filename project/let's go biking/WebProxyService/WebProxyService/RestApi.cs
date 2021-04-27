using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebProxyService
{
    class RestApi
    {
        private string response;

        public RestApi(string uri)
        {
            System.Diagnostics.Debug.WriteLine("using rest api to perform a new request");
            System.Diagnostics.Debug.WriteLine("uri : " + uri);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                this.response = reader.ReadToEnd();
            }
        }

        public string getResponse()
        {
            return response;
        }

    }

}
