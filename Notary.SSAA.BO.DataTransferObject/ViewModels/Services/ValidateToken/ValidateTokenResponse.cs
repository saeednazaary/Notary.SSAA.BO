

using System.Net;

namespace SSAA.Notary.DataTransferObject.ViewModels.Services.ValidateToken
{
    public class ValidateTokenResponse : ResponseModel
    {

        public TokenData TokenData { get; set; }

    }
    public class ResponseModel
    {
        public string UserId { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public HttpStatusCode HttpStatusCode { get; set; }
    }

    public class TokenData
    {
        public int exp { get; set; }
        public int iat { get; set; }
        public string jti { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
        public string sub { get; set; }
        public string typ { get; set; }
        public string azp { get; set; }
        public string session_state { get; set; }
        public string name { get; set; }
        public string given_name { get; set; }
        public string family_name { get; set; }
        public string preferred_username { get; set; }
        public string email { get; set; }
        public bool email_verified { get; set; }
        public string gender { get; set; }
        public string birthdate { get; set; }
        public string acr { get; set; }
        public List<string> allowedorigins { get; set; }
        public string scope { get; set; }
        public string sid { get; set; }
        public string client_id { get; set; }
        public string username { get; set; }
        public bool active { get; set; }
        public string phoneNumber { get; set; }
    }
}
