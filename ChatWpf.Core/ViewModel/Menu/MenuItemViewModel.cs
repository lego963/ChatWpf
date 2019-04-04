using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Menu
{
    public class MenuItemViewModel:BaseViewModel
    {
        public string Text { get; set; }

        public IconType Icon { get; set; }

        public MenuItemType Type { get; set; }
    }
}
