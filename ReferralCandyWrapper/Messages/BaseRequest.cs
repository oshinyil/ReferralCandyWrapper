using System;

namespace ReferralCandyWrapper.Messages
{
    public abstract class BaseRequest : IRequest
    {
        protected readonly string methodName;
        protected readonly string httpMethod;

        public BaseRequest(string methodName, string httpMethod)
        {
            this.methodName = methodName;
            this.httpMethod = httpMethod;
        }

        protected static int GetUnixTimestamp()
        {
            return (int)(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1))).TotalSeconds;
        }

        protected static string GetSignature(string queryString)
        {
            return Cryptography.GetMd5Hash(queryString);
        }

        public string GetMethodName()
        {
            return methodName;
        }

        public string GetHttpMethod()
        {
            return httpMethod;
        }

        public abstract string ToQueryString(string accessID, string secretKey);
    }
}
