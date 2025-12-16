namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Services.ClientLogin
{
    public class ClientLoginViewModel
    {
        public ClientLoginViewModel()
        {
            Credential = new CredentialLogin();
            UserAttributes = new UserAttributes();
            UserBranchAccesses = new UserBranchAccesses();
        }

        public CredentialLogin Credential { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public int HttpStatusCode { get; set; }
        public object Claims { get; set; }
        public UserAttributes UserAttributes { get; set; }
        public UserBranchAccesses UserBranchAccesses { get; set; }
    }

    public class CredentialLogin
    {
        public string AccessToken { get; set; }
        public string RefreshTokenId { get; set; }
        public DateTime ExpireDate { get; set; }
    }

    public class UserAttributes
    {
        public string DisplayName { get; set; }
        public string UserId { get; set; }
        public string LastModifiedDateTime { get; set; }
    }

    public class UserBranchAccesses
    {
        public List<BranchAccess> BranchAccesses { get; set; } = new();
    }

    public class BranchAccess
    {
        public string BranchAccessId { get; set; }
        public string BranchId { get; set; }
        public string BranchCode { get; set; }
        public string BranchName { get; set; }
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string DisplayName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
    }

}
