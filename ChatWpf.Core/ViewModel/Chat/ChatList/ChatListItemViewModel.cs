using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Chat.ChatList
{
    public class ChatListItemViewModel : BaseViewModel
    {
        public string Name { get; set; }

        public string Message { get; set; }

        public string Initials { get; set; }

        public string ProfilePictureRgb { get; set; }

        public bool NewContentAvailable { get; set; }

        public bool IsSelected { get; set; }

    }
}
