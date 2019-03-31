using System.Collections.Generic;
using System.Windows.Input;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.PopupMenu;

namespace ChatWpf.Core.ViewModel.Chat.ChatMessage
{
    public class ChatMessageListViewModel : BaseViewModel
    {
        public List<ChatMessageListItemViewModel> Items { get; set; }

        public bool AttachmentMenuVisible { get; set; }

        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

        public ICommand AttachmentButtonCommand { get; set; }

        public ChatMessageListViewModel()
        {
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);

            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        public void AttachmentButton()
        {
            AttachmentMenuVisible ^= true;
        }
    }
}
