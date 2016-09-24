using System;
using System.Collections.Specialized;

namespace ReferralCandyWrapper.Messages
{
    public class VerifyRequest : BaseRequest
    {
        public VerifyRequest() : base("verify", "GET")
        {
        }
        
        protected internal override NameValueCollection GetNameValueCollection(string accessID)
        {
            var collection = new NameValueCollection();
            collection.Add("accessID", accessID);
            collection.Add("timestamp", Convert.ToString(Common.GetUnixTimestamp()));

            return collection;
        }
    }
}
