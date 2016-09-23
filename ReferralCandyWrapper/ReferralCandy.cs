using ReferralCandyWrapper.Messages;
using System;

namespace ReferralCandyWrapper
{
    public class ReferralCandy : IReferralCandy
    {
        private readonly HttpClient httpClient;

        public ReferralCandy(string accessID, string secretKey)
        {
            this.httpClient = new HttpClient(accessID, secretKey);
        }

        public Response Verify(VerifyRequest request)
        {
            return httpClient.ProcessRequest(request);
        }

        public Response Purchase()
        {
            throw new NotImplementedException();
        }

    }
}
