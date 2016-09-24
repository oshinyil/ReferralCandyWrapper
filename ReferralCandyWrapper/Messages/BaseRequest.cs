using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;

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

        public string GetMethodName()
        {
            return methodName;
        }

        public string GetHttpMethod()
        {
            return httpMethod;
        }

        protected internal static List<string> ToParamList(NameValueCollection nameValueCollection)
        {
            return (from key in nameValueCollection.AllKeys
                    from value in nameValueCollection.GetValues(key)
                    select string.Format("{0}={1}", key, value))
                    .ToList();
        }

        protected internal static string AppendSignature(string queryString, string signature)
        {
            return string.Format("{0}&signature={1}",
                queryString,
                signature);
        }

        protected internal static string GetSignature(List<string> paramList, string secretKey)
        {
            var orderedParamString = string.Join("", paramList.OrderBy(p => p));
            return Cryptography.GetMd5Hash(string.Format("{0}{1}", secretKey, orderedParamString));
        }

        protected internal abstract NameValueCollection GetNameValueCollection(string accessID);

        public string ToQueryString(string accessID, string secretKey)
        {
            var collection = GetNameValueCollection(accessID);
            var paramList = ToParamList(collection);
            var signature = GetSignature(paramList, secretKey);

            return AppendSignature(
                string.Join("&", paramList),
                signature);
        }
    }
}
