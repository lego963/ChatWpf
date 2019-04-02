using System.Collections.Generic;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Chat.Menu
{
    public class MenuViewModel:BaseViewModel
    {
        public List<MenuItemViewModel> Items { get; set; }
    }
}
