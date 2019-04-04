﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.PopupMenu;

namespace ChatWpf.Core.ViewModel.Chat.ChatMessage
{
    public class ChatMessageListViewModel : BaseViewModel
    {
        public ObservableCollection<ChatMessageListItemViewModel> Items { get; set; }

        public bool AttachmentMenuVisible { get; set; }

        public bool AnyPopupVisible => AttachmentMenuVisible;

        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

        public string PendingMessageText { get; set; }

        public ICommand AttachmentButtonCommand { get; set; }

        public ICommand PopupClickawayCommand { get; set; }

        public ICommand SendCommand { get; set; }

        public ChatMessageListViewModel()
        {
            // Create commands
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            PopupClickawayCommand = new RelayCommand(PopupClickaway);
            SendCommand = new RelayCommand(Send);

            // Make a default menu
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        public void AttachmentButton()
        {
            // Toggle menu visibility
            AttachmentMenuVisible ^= true;
        }

        public void PopupClickaway()
        {
            // Hide attachment menu
            AttachmentMenuVisible = false;
        }

        public void Send()
        {
            if (Items == null)
                Items = new ObservableCollection<ChatMessageListItemViewModel>();

            // Fake send a new message
            Items.Add(new ChatMessageListItemViewModel
            {
                Initials = "LM",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                SenderName = "Luke Malpass",
                NewItem = true
            });

            // Clear the pending message text
            PendingMessageText = string.Empty;
        }
    }
}
