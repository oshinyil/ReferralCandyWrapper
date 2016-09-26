# ReferralCandy C# API Client
Microsoft .NET Framework 4.0

## Usage
### Initialization
Initialize an object with your ReferralCandy credentials

	IReferralCandy referralCandy = new ReferralCandy("ACCESS_ID", "API_SECRET_KEY");

### Verification
Verify your credentials.

	var response = referralCandy.Verify(new VerifyRequest());
	Console.WriteLine("HTTP Code: {0}", response.HttpCode);
	Console.WriteLine("Message: {0}", response.Message);

### API Methods
The ReferralCandy API client will perform the authentication steps for you. This means that you would not be required to pass in the 'timestamp', 'accessID', 'apiSecretKey', and 'signature' parameters. But, you need to pass a request object as an argument.

[API endpoints](http://www.referralcandy.com/api) are available as methods in the ReferralCandy API client.

API responses are wrapped in a response object.

e.g.

	var purchaseRequest = new PurchaseRequest
	{
		FirstName = "Alex",
		LastName = "Morgan",
		Email = "alexmorgan@company.com",
		CurrencyCode = "USD",
		BrowserIP = "172.0.0.1",
		UserAgent = "Chrome",
		OrderDateTime = DateTime.Now,
		InvoiceAmount = 1.00M,
		ExternalReferenceID = "A123"
	};

	var response = referralCandy.Purchase(purchaseRequest);

	Console.WriteLine("HTTP Code: {0}", response.HttpCode);
	Console.WriteLine("Message: {0}", response.Message);
	Console.WriteLine("Referral Corner URL: {0}", response.ReferralCornerUrl);
	
## Credits
* [API documentation](http://www.referralcandy.com/api)
* [Python wrapper for ReferralCandy API](https://github.com/ReferralCandy/referral_candy_python)
* [Ruby wrapper for ReferralCandy API](https://github.com/ReferralCandy/referral_candy)
