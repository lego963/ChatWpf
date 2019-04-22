using System;
using System.Globalization;
using ChatWpf.Controls.Chat.ChatList;
using ChatWpf.Core.DataModels;

namespace ChatWpf.ValueConverter
{
    public class SideMenuContentConverter : BaseValueConverter<SideMenuContentConverter>
    {
        private readonly ChatListControl _chatListControl = new ChatListControl();

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var sideMenuType = (SideMenuContent)value;

            switch (sideMenuType)
            {
                case SideMenuContent.Chat:
                    return _chatListControl;

                default:
                    return "No UI yet, sorry :)";
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
