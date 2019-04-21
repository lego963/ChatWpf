using ChatWpf.Core.DataModels;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Menu
{
    public class MenuItemViewModel : BaseViewModel
    {
        public string Text { get; set; }

        public IconType Icon { get; set; }

        public MenuItemType Type { get; set; }
    }
}
