using ReferralCandyWrapper;
using ReferralCandyWrapper.Messages;
using System;

namespace ReferralCandyConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var referralCandy = new ReferralCandy("ACCESS_ID", "API_SECRET_KEY");
            var response = referralCandy.Verify(new VerifyRequest());
        }
    }
}
