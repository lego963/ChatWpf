using System.Collections.Generic;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Chat
{
    public class ChatListViewModel : BaseViewModel
    {
        public List<ChatListItemViewModel> Items { get; set; }
    }
}
