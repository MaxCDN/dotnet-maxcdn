using System;
using System.ComponentModel;
using System.Net;
using System.Web;
using System.Text;

namespace MaxCDN
{
    public class Api
    {
        private string _consumerKey = "";
        private string _consumerSecret = "";
        private string _alias = "";
        private int _requestTimeout = 180;

        private const string _MaxCDNBaseAddress = "https://rws.maxcdn.com";

        public Api(string alias, string consumerKey, string consumerSecret, int requestTimeout = 180)
        {
            _consumerKey = consumerKey;
            _alias = alias;
            _consumerSecret = consumerSecret;
            _requestTimeout = requestTimeout;
        }

        public dynamic Get(string url, bool debug = false)
        {
            if (debug)
            {
                Console.Write("Running garbage collector\n");
            }
            clear(debug);
            if (debug)
            {
                Console.Write("Done\n\nGenerating OAuth request URL for " + url + "\n");
            }
            var requestUrl = GenerateOAuthRequestUrl(url, "GET");
            if (debug)
            {
                Console.Write("Done: \n" + requestUrl + "\n\nCreating request\n");
            }
            var request = new ApiWebClient(_requestTimeout);
            if (debug)
            {
                Console.Write("Done: \n" + request + "\n Defining User Agent for API request\n");
            }
            request.Headers.Add("user-agent", "MaxCDN dot-net API Client");
            if (debug)
            {
                Console.Write("Done:\nUser Agent = MaxCDN dot-net API Client\n\nMaking the GET request\n");
            }
            var response = request.DownloadString(requestUrl);
            if (debug)
            {
                DumpObject(response);
            }
            return response;
        }

        //DELETE requests handler
        public bool Delete(string url, bool debug = false)
        {
            if (debug)
            {
                Console.Write("Running garbage collector\n");
            }
            clear(debug);
            if (debug)
            {
                Console.Write("Done\n\n");
            }
            var response = GetWebResponse(url, "DELETE");
            if (debug)
            {
                Console.Write("Done:\n" + response + "\n\n");
                DumpObject(response);
            }
            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
        }
        //PURGE-DELETE requests handler
        public bool Purge(string url, string uri, bool debug = false)
        {
            if (debug)
            {
                Console.Write("Running garbage collector\n");
            }
            clear(debug);
            if (debug)
            {
                Console.Write("Done\n\nEncoding request URi\n");
            }
            char[] enc = System.Web.HttpUtility.UrlEncode(uri).ToCharArray();

            for (int i = 0; i < enc.Length - 2; i++)
            {
                if (enc[i] == '%')
                {
                    enc[i + 1] = char.ToUpper(enc[i + 1]);
                    enc[i + 2] = char.ToUpper(enc[i + 2]);
                }
            }

            string encfinal = new string(enc);

            if (debug)
            {
                Console.Write("Done:\n" + encfinal + "\n\nMaking the DELETE request\n");
            }

            var response = GetWebResponse(url + "?" + encfinal.Replace("file%3D", "file=").Replace("%5D%3D", "%5D=").Replace("%26file%5B", "&file%5B"), "DELETE");
            if (debug)
            {
                Console.Write("Done:\n");
                DumpObject(response);
            }
            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
        }

        //PUT requests handler
        public bool Put(string url, dynamic data, bool debug = false)
        {
            if (debug)
            {
                Console.Write("Running garbage collector\n");
            }
            clear(debug);
            if (debug)
            {
                Console.Write("Done\n\nEncoding request URi\n");
            }
            char[] enc = System.Web.HttpUtility.UrlEncode(data).ToCharArray();

            for (int i = 0; i < enc.Length - 2; i++)
            {
                if (enc[i] == '%')
                {
                    enc[i + 1] = char.ToUpper(enc[i + 1]);
                    enc[i + 2] = char.ToUpper(enc[i + 2]);
                }
            }

            string encfinal = new string(enc);
            data = encfinal;
            data = data.Replace("%3D", "=").Replace("%26", "&");

            if (debug)
            {
                Console.Write("Done:\n" + encfinal + "\n\nMaking the PUT request\n Generating OAuth request for: " + url + "\n");
            }

            var requestUrl = GenerateOAuthRequestUrl(url, "PUT");
            if (debug)
            {
                Console.Write(" Done:\n" + requestUrl + "\n  Creating API Web client request\n");
            }
            var request = WebRequest.Create(requestUrl);
            if (debug)
            {
                Console.Write(" Done:\n" + request + "\n  Defining request method (PUT)\n");
            }
            request.Method = "PUT";
            if (debug)
            {
                Console.Write(" Done\n  Reading response\n");
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            var response = request.GetResponse();
            if (debug)
            {
                DumpObject(response);
            }
            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
        }
        //POST request handler
        public bool Post(string url, dynamic data, bool debug = false)
        {
            if (debug)
            {
                Console.Write("Running garbage collector\n");
            }
            clear(debug);
            if (debug)
            {
                Console.Write("Done\n\nEncoding request URi\n");
            }
            char[] enc = System.Web.HttpUtility.UrlEncode(data).ToCharArray();

            for (int i = 0; i < enc.Length - 2; i++)
            {
                if (enc[i] == '%')
                {
                    enc[i + 1] = char.ToUpper(enc[i + 1]);
                    enc[i + 2] = char.ToUpper(enc[i + 2]);
                }
            }

            string encfinal = new string(enc);
            data = encfinal;
            data = data.Replace("%3D", "=").Replace("%26", "&");

            if (debug)
            {
                Console.Write("Done:\n" + data + "\nGenerating OAuth request URi\n\n");
            }

            var requestUrl = GenerateOAuthRequestUrl(url, "POST");
            if (debug)
            {
                Console.Write("Done\n\nMaking the POST request with " + requestUrl + "\n\n");
            }
            var request = WebRequest.Create(requestUrl);
            if (debug)
            {
                Console.Write("Done:\n" + request + "\n\nDefining request method (POST)\n");
            }
            request.Method = "POST";
            if (debug)
            {
                Console.Write("Done\n\nReading response\n");
            }
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse();
            if (debug)
            {
                DumpObject(response);
            }
            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
        }

        //API Response inerpreter
        private WebResponse GetWebResponse(string url, string method)
        {
            var requestUrl = GenerateOAuthRequestUrl(url, method);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(requestUrl);
            request.Method = method;
            request.UserAgent = "MaxCDN dot-net API Client";
            var response = request.GetResponse();
            return response;
        }

        //Generate request url

        private string GenerateOAuthRequestUrl(string url, string method)
        {
            Uri uri;
            Uri.TryCreate(_MaxCDNBaseAddress + "/" + _alias + url, UriKind.Absolute, out uri);

            var normalizedUrl = "";
            var normalizedParams = "";

            var oAuth = new OAuthBase();
            var nonce = oAuth.GenerateNonce();
            var timeStamp = oAuth.GenerateTimeStamp();
            var sig =
                HttpUtility.UrlEncode(oAuth.GenerateSignature(uri, _consumerKey, _consumerSecret, "", "", method, timeStamp,
                                                              nonce, OAuthBase.SignatureTypes.HMACSHA1, out normalizedUrl,
                                                              out normalizedParams));
            var requestUrl = normalizedUrl + "?" + normalizedParams + "&oauth_signature=" + sig;
            return requestUrl.Replace("\"", "");
        }

        private void DumpObject(dynamic o)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(o))
            {
                string name = descriptor.Name;
                object value = descriptor.GetValue(o);
                Console.Write("{0}={1} ", name, value);
            }

            Console.WriteLine();
        }

        //Initialize WebClient
        private class ApiWebClient : WebClient
        {
            private int _requestTimeout = 180;

            public ApiWebClient(int requestTimeout = 180)
            {
                _requestTimeout = requestTimeout;
            }

            protected override WebRequest GetWebRequest(Uri address)
            {
                WebRequest w = base.GetWebRequest(address);
                w.Timeout = _requestTimeout * 1000;
                return w;
            }
        }
        public void clear(bool debug)
        {
            if (debug)
            {
                Console.WriteLine("Total Memory Used: {0}\n", GC.GetTotalMemory(true));
                Console.WriteLine("Garbage Count for current generation collection: {0}\n", GC.CollectionCount(GC.GetGeneration(0)));
            }
            GC.Collect();
        }
    }
}
