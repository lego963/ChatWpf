using System.Collections.Generic;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Chat.ChatList
{
    public class ChatListViewModel : BaseViewModel
    {
        public List<ChatListItemViewModel> Items { get; set; }
    }
}
