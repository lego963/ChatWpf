namespace ChatWpf.Core
{
    public class ChatListItemDesignModel : ChatListItemViewModel
    {
        public static ChatListItemDesignModel Instance => new ChatListItemDesignModel();

        public ChatListItemDesignModel()
        {
            Initials = "LM";
            Name = "Luke";
            Message = "This chat app is awesome! I bet it will be fast too";
            ProfilePictureRgb = "3099c5";
        }

    }
}
