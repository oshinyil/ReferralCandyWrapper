using System.ComponentModel;

namespace ReferralCandyWrapper
{
    public enum HttpCode
    {
        [Description("API call was successful.")]
        Successful = 200,

        [Description("You've made an error in your request. See the message field in the response for details.")]
        ErrorRequest = 400,

        [Description("Authentication credentials provided were incorrect.")]
        IncorrectCredentials = 401,

        [Description("Unknown API method.")]
        UnknownMethod = 404,

        [Description("A temporary internal server error.")]
        InternalServerError = 500
    }
}
