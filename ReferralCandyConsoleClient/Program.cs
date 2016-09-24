using ReferralCandyWrapper;
using ReferralCandyWrapper.Messages;
using System;

namespace ReferralCandyConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IReferralCandy referralCandy = new ReferralCandy("ACCESS_ID", "API_SECRET_KEY");

            Console.WriteLine("Verify...");

            var response = referralCandy.Verify(new VerifyRequest());

            Console.WriteLine("HTTP Code: {0}", response.HttpCode);
            Console.WriteLine("Message: {0}", response.Message);


            Console.WriteLine("Purchase...");

            var purchaseRequest = new PurchaseRequest
            {
                FirstName = "Alex",
                LastName = "Morgan",
                Email = "alexmorgan@company.com",
                CurrencyCode = "USD",
                BrowserIP = "172.0.0.1",
                UserAgent = "Chrome",
                OrderDateTime = DateTime.Now,
                InvoiceAmount = 1.00M
            };

            response = referralCandy.Purchase(purchaseRequest);

            Console.WriteLine("HTTP Code: {0}", response.HttpCode);
            Console.WriteLine("Message: {0}", response.Message);
            Console.WriteLine("Referral Corner URL: {0}", response.ReferralCornerUrl);

            Console.ReadLine();
        }
    }
}
