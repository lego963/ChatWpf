using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Application;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.Chat.ChatMessage;

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

        public ICommand OpenMessageCommand { get; set; }

        public ChatListItemViewModel()
        {
            OpenMessageCommand = new RelayCommand(OpenMessage);
        }

        public void OpenMessage()
        {
            if (Name == "Jesse")
            {
                IoC.Base.IoC.Application.GoToPage(ApplicationPage.Login, new LoginViewModel
                {
                    Email = "jesse@helloworld.com"
                });
                return;
            }

            IoC.Base.IoC.Application.GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel
            {
                Items = new ObservableCollection<ChatMessageListItemViewModel>
                {
                    new ChatMessageListItemViewModel
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF00FF",
                        SenderName = "Luke",
                        SentByMe = true,
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "A received message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "A received message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF00FF",
                        SenderName = "Luke",
                        SentByMe = true,
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "A received message",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "A received message",
                        ImageAttachment = new ChatMessageListItemImageAttachmentViewModel
                        {
                            ThumbnailUrl = "http://anywhere"
                        },
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF0000",
                        SenderName = "Parnell",
                        SentByMe = false,
                    },
                }
            });
        }

    }
}
