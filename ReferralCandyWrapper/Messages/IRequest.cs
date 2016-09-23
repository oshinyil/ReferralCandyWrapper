namespace ReferralCandyWrapper.Messages
{
    internal interface IRequest
    {
        string GetMethodName();
        string GetHttpMethod();
        string ToQueryString(string accessID, string secretKey);
    }
}
