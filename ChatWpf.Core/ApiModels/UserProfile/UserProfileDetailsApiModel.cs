using ChatWpf.Core.DataModels;

namespace ChatWpf.Core.ApiModels.UserProfile
{
    public class UserProfileDetailsApiModel
    {
        public string Token { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public LoginCredentialsDataModel ToLoginCredentialsDataModel()
        {
            return new LoginCredentialsDataModel
            {
                Email = Email,
                FirstName = FirstName,
                LastName = LastName,
                Username = Username,
                Token = Token
            };
        }
    }
}
