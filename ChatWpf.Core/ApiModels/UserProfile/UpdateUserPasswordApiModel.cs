namespace ChatWpf.Core.ApiModels.UserProfile
{
    public class UpdateUserPasswordApiModel
    {
        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }
    }
}
