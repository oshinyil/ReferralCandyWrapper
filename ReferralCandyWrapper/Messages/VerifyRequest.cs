using System.Collections.Generic;
using System.Linq;

namespace ReferralCandyWrapper.Messages
{
    public class VerifyRequest : BaseRequest
    {
        public VerifyRequest() : base("verify", "POST")
        {
        }

        public override string ToQueryString(string accessID, string secretKey)
        {
            var paramList = new List<string>();
            paramList.Add(string.Format("accessID={0}", accessID));
            paramList.Add(string.Format("timestamp={0}", GetUnixTimestamp()));

            var orderedParamString = string.Join("", paramList.OrderBy(p => p));
            var signature = GetSignature(string.Format("{0}{1}", secretKey, orderedParamString));
            var queryString = string.Format("{0}&signature={1}",
                string.Join("&", paramList),
                signature);

            return queryString;
        }
    }
}
