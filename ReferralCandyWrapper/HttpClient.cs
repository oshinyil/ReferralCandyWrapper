using Newtonsoft.Json;
using ReferralCandyWrapper.Extensions;
using ReferralCandyWrapper.Messages;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace ReferralCandyWrapper
{
    internal class HttpClient
    {
        private const string defaultApiUrl = "https://my.referralcandy.com/api/v1/";
        private readonly string accessID;
        private readonly string secretKey;

        public HttpClient(string accessID, string secretKey)
        {
            this.accessID = accessID;
            this.secretKey = secretKey;
        }

        public Response ProcessRequest(IRequest request)
        {
            HttpStatusCode httpStatusCode;
            Response response = null;

            var responseString = GetResponseString(request, out httpStatusCode);
            response = JsonConvert.DeserializeObject<Response>(responseString);
            if (response != null)
            {
                response.HttpCode = (int)httpStatusCode;
                response.Message = ((HttpCode)response.HttpCode).Description();
            }

            return response;
        }

        private string GetResponseString(IRequest request, out HttpStatusCode statusCode)
        {
            var webRequest = GenerateHttpWebRequest(request);
            var webResponse = GetHttpWebResponse(webRequest);

            string responseString;
            using (var streamReader = new StreamReader(webResponse.GetResponseStream()))
            {
                responseString = streamReader.ReadToEnd();
            }

            statusCode = webResponse.StatusCode;
            return responseString;
        }

        private HttpWebRequest GenerateHttpWebRequest(IRequest request)
        {
            var requestUriString = string.Format("{0}{1}.json{2}", 
                defaultApiUrl, 
                request.GetMethodName(),
                request.GetHttpMethod() == "GET" ? AppendQueryString(request) : string.Empty);

            var webRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            webRequest.Method = request.GetHttpMethod();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.ProtocolVersion = HttpVersion.Version10;

            if (webRequest.Method != WebRequestMethods.Http.Get)
            {
                var byteArray = Encoding.UTF8.GetBytes(request.ToQueryString(accessID, secretKey));
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.ContentLength = byteArray.Length;

                using (var stream = webRequest.GetRequestStream())
                {
                    stream.Write(byteArray, 0, byteArray.Length);
                }
            }

            return webRequest;
        }

        private static HttpWebResponse GetHttpWebResponse(HttpWebRequest webRequest)
        {
            try
            {
                return (HttpWebResponse)webRequest.GetResponse();
            }
            catch (Exception exception)
            {
                if (exception.GetType() == typeof(WebException))
                {
                    var webException = (WebException)(exception);
                    if (webException.Response == null)
                    {
                        throw;
                    }
                    return (HttpWebResponse)webException.Response;
                }
                else
                {
                    throw;
                }
            }
        }

        private string AppendQueryString(IRequest request)
        {
            return string.Format("?{0}", request.ToQueryString(accessID, secretKey));
        }
    }
}
