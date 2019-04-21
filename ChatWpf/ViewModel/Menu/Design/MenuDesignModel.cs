using System.Collections.Generic;
using ChatWpf.Core.DataModels;

namespace ChatWpf.ViewModel.Menu.Design
{
    public class MenuDesignModel : MenuViewModel
    {
        public static MenuDesignModel Instance => new MenuDesignModel();

        public MenuDesignModel()
        {
            Items = new List<MenuItemViewModel>(new[]
            {
                new MenuItemViewModel { Type = MenuItemType.Header, Text = "Design time header..." },
                new MenuItemViewModel { Text = "Menu item 1", Icon = IconType.File },
                new MenuItemViewModel { Text = "Menu item 2", Icon = IconType.Picture },
            });
        }
    }
}
