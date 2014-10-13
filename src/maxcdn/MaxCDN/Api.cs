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
        private int _requestTimeout = 30;

        private const string _MaxCDNBaseAddress = "https://rws.maxcdn.com";

        public Api(string alias, string consumerKey, string consumerSecret, int requestTimeout = 30)
        {
            _consumerKey = consumerKey;
            _alias = alias;
            _consumerSecret = consumerSecret;
            _requestTimeout = requestTimeout;
        }

        public dynamic Get(string url, bool debug = false)
        {            
            var requestUrl = GenerateOAuthRequestUrl(url, "GET");

            var request = new ApiWebClient(_requestTimeout);            
            var response = request.DownloadString(requestUrl);

            var result = response;
            if (debug)
                DumpObject(result);

            return result;
        }

        //DELETE requests handler
        public bool Delete(string url)
        {
            var response = GetWebResponse(url, "DELETE");
            return ((HttpWebResponse) response).StatusCode == HttpStatusCode.OK;
        }
        //PURGE-DELETE requests handler
        public bool Purge(string url, string uri)
        {
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

            var response = GetWebResponse(url + "?" + encfinal.Replace("%3D", "=").Replace("%26", "&"), "DELETE");

            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
        }

        //API Response inerpreter
        private WebResponse GetWebResponse(string url, string method)
        {
            var requestUrl = GenerateOAuthRequestUrl(url, method);

            var request = WebRequest.Create(requestUrl);
            request.Method = method;

            var response = request.GetResponse();
            return response;
        }

        //PUT requests handler
        public bool Put(string url, dynamic data)
        {
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

            var requestUrl = GenerateOAuthRequestUrl(url, "PUT");
            var request = WebRequest.Create(requestUrl);
            request.Method = "PUT";
            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;
            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();
            var response = request.GetResponse();
            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
        }
        //POST request handler
        public bool Post(string url, dynamic data)
        {
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


            var requestUrl = GenerateOAuthRequestUrl(url, "POST");

            var request = WebRequest.Create(requestUrl);
            request.Method = "POST";

            byte[] byteArray = Encoding.UTF8.GetBytes(data);
            request.ContentType = "application/json";
            request.ContentLength = byteArray.Length;

            var dataStream = request.GetRequestStream();
            dataStream.Write(byteArray, 0, byteArray.Length);
            dataStream.Close();

            var response = request.GetResponse();

            return ((HttpWebResponse)response).StatusCode == HttpStatusCode.OK;
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
            private int _requestTimeout = 30;

            public ApiWebClient(int requestTimeout = 30)
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
    }
}
