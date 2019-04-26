using System;
using System.Collections.ObjectModel;

namespace ChatWpf.ViewModel.Chat.ChatMessage.Design
{
    public class ChatMessageListDesignModel : ChatMessageListViewModel
    {
        public static ChatMessageListDesignModel Instance => new ChatMessageListDesignModel();

        public ChatMessageListDesignModel()
        {
            DisplayTitle = "Parnell";

            Items = new ObservableCollection<ChatMessageListItemViewModel>
            {
                new ChatMessageListItemViewModel
                {
                    SenderName = "Vladimir",
                    Initials = "VV",
                    Message = "FAST",
                    ProfilePictureRgb = "00d405",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    SentByMe = false
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Rodion",
                    Initials = "RG",
                    Message = "Слушаю, есть предложения?",
                    ProfilePictureRgb = "3099c5",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                    SentByMe = true
                },
                new ChatMessageListItemViewModel
                {
                    SenderName = "Vladimir",
                    Initials = "VV",
                    Message = "Нужен человек для одного мероприятия, ты как?",
                    ProfilePictureRgb = "00d405",
                    MessageSentTime = DateTimeOffset.UtcNow,
                    SentByMe = false
                },
                new ChatMessageListItemViewModel
                {
                SenderName = "Rodion",
                Initials = "RG",
                Message = "Место и время",
                ProfilePictureRgb = "3099c5",
                MessageSentTime = DateTimeOffset.UtcNow,
                MessageReadTime = DateTimeOffset.UtcNow.Subtract(TimeSpan.FromDays(1.3)),
                SentByMe = true
                }
            };
        }
    }
}
