namespace ChatWpf.ViewModel.Chat.ChatList.Design
{
    public class ChatListItemDesignModel : ChatListItemViewModel
    {
        public static ChatListItemDesignModel Instance => new ChatListItemDesignModel();

        public ChatListItemDesignModel()
        {
            Initials = "RG";
            Name = "Rodion";
            Message = "хорошенький чатик";
            ProfilePictureRgb = "3099c5";
        }
    }
}
