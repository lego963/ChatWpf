using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ChatWpf.Core.DataModels;
using ChatWpf.ViewModel.Base;
using ChatWpf.ViewModel.Chat.ChatMessage;

namespace ChatWpf.ViewModel.Chat.ChatList
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
            DI.Di.ViewModelApplication.GoToPage(ApplicationPage.Chat, new ChatMessageListViewModel
            {
                DisplayTitle = "Vladimir, Me",

                Items = new ObservableCollection<ChatMessageListItemViewModel>
                {
                    new ChatMessageListItemViewModel
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF00FF",
                        SenderName = "Rodion",
                        SentByMe = true
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "Дорогой, выезжай к ГЧ",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF0000",
                        SenderName = "Vladimir",
                        SentByMe = false
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = "Буду ждать тебя у главного входа",
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF0000",
                        SenderName = "Vladimir",
                        SentByMe = false
                    },
                    new ChatMessageListItemViewModel
                    {
                        Message = Message,
                        Initials = Initials,
                        MessageSentTime = DateTime.UtcNow,
                        ProfilePictureRgb = "FF00FF",
                        SenderName = "Rodion",
                        SentByMe = true
                    }
                }
            });
        }
    }
}
