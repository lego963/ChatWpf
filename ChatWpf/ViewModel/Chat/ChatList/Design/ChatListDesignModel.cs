using System.Collections.Generic;

namespace ChatWpf.ViewModel.Chat.ChatList.Design
{
    public class ChatListDesignModel : ChatListViewModel
    {
        public static ChatListDesignModel Instance => new ChatListDesignModel();

        public ChatListDesignModel()
        {
            Items = new List<ChatListItemViewModel>
            {
                new ChatListItemViewModel
                {
                    Name = "Darya",
                    Initials = "DS",
                    Message = "Привет, как дела?",
                    ProfilePictureRgb = "3099c5",
                    NewContentAvailable = true
                },
                new ChatListItemViewModel
                {
                    Name = "Semen",
                    Initials = "SL",
                    Message = "HALO, HALO",
                    ProfilePictureRgb = "fe4503"
                },
                new ChatListItemViewModel
                {
                    Name = "Anastasiya",
                    Initials = "AG",
                    Message = "Привет, нужна твоя консультация. Ответь, как будешь свободен.",
                    ProfilePictureRgb = "00d405",
                    IsSelected = true
                },
                new ChatListItemViewModel
                {
                    Name = "Egor",
                    Initials = "EZ",
                    Message = "Есть предложение, отпиши, как только сможешь!",
                    ProfilePictureRgb = "00d405",
                    IsSelected = true
                },
                new ChatListItemViewModel
                {
                    Name = "Vladimir",
                    Initials = "VV",
                    Message = "FAST",
                    ProfilePictureRgb = "00d405",
                    IsSelected = true
                },
                new ChatListItemViewModel
                {
                    Name = "Kristina",
                    Initials = "KK",
                    Message = "Какие планы на выходные?",
                    ProfilePictureRgb = "00d405",
                    IsSelected = true
                }
            };
        }
    }
}
