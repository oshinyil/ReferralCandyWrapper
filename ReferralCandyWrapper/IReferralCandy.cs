using ReferralCandyWrapper.Messages;

namespace ReferralCandyWrapper
{
    public interface IReferralCandy
    {
        Response Verify(VerifyRequest request);
        Response Purchase();
    }
}
