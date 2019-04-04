using ChatWpf.Core.DataModels;

namespace ChatWpf.Core.Icons
{
    public static class IconTypeExtensions
    {
        public static string ToFontAwesome(this IconType type)
        {
            // Return a FontAwesome string based on the icon type
            switch (type)
            {
                case IconType.File:
                    return "\uf0f6";

                case IconType.Picture:
                    return "\uf1c5";

                // If none found, return null
                default:
                    return null;
            }
        }
    }
}
