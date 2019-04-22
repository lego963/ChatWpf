using System.Collections.Generic;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Menu
{
    /// <summary>
    /// A view model for a menu
    /// </summary>
    public class MenuViewModel : BaseViewModel
    {
        /// <summary>
        /// The items in this menu
        /// </summary>
        public List<MenuItemViewModel> Items { get; set; }
    }
}
