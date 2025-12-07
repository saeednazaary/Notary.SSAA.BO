

namespace Notary.SSAA.BO.SharedKernel.SharedModels
{
    public class CachedToken
    {
        public string AccessToken { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
    }
}
