using System;
using System.Collections.Specialized;

namespace ReferralCandyWrapper.Messages
{
    public class PurchaseRequest : BaseRequest
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string DiscountCode { get; set; }
        public DateTime OrderDateTime { get; set; }
        public string BrowserIP { get; set; }
        public string UserAgent { get; set; }
        public decimal InvoiceAmount { get; set; }
        public string CurrencyCode { get; set; }

        public PurchaseRequest() : base("purchase", "POST")
        {
        }

        protected internal override NameValueCollection GetNameValueCollection(string accessID)
        {
            var collection = new NameValueCollection();
            collection.Add("accessID", accessID ?? string.Empty);
            collection.Add("timestamp", Convert.ToString(Common.GetUnixTimestamp()));
            collection.Add("first_name", FirstName ?? string.Empty);
            collection.Add("last_name", LastName ?? string.Empty);
            collection.Add("email", Email ?? string.Empty);
            collection.Add("discount_code", DiscountCode ?? string.Empty);
            collection.Add("order_timestamp", Convert.ToString(Common.GetUnixTimestamp(OrderDateTime)));
            collection.Add("browser_ip", BrowserIP ?? string.Empty);
            collection.Add("user_agent", UserAgent ?? string.Empty);
            collection.Add("invoice_amount", InvoiceAmount.ToString("N2"));
            collection.Add("currency_code", CurrencyCode ?? string.Empty);

            return collection;
        }
    }
}
