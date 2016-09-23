using Newtonsoft.Json;
using ReferralCandyWrapper.Extensions;
using ReferralCandyWrapper.Messages;
using System;
using System.IO;
using System.Net;

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
            var requestUriString = string.Format("{0}{1}.json", defaultApiUrl, request.GetMethodName());
            var webRequest = (HttpWebRequest)WebRequest.Create(requestUriString);
            webRequest.Method = request.GetHttpMethod();
            webRequest.ServicePoint.Expect100Continue = false;
            webRequest.ProtocolVersion = HttpVersion.Version10;

            if (webRequest.Method != WebRequestMethods.Http.Get)
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
                using (var requestWriter = new StreamWriter(webRequest.GetRequestStream()))
                {
                    requestWriter.Write(request.ToQueryString(accessID, secretKey));
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
    }
}
