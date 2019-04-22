using ChatWpf.Core.DataModels;

namespace ChatWpf.Core.Icons
{
    public static class IconTypeExtensions
    {
        public static string ToFontAwesome(this IconType type)
        {
            switch (type)
            {
                case IconType.File:
                    return "\uf0f6";

                case IconType.Picture:
                    return "\uf1c5";

                default:
                    return null;
            }
        }
    }
}
