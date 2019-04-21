using System.Collections.Generic;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Menu
{
    public class MenuViewModel : BaseViewModel
    {
        public List<MenuItemViewModel> Items { get; set; }
    }
}
