using ChatWpf.Core.DataModels;

namespace ChatWpf.Core.ViewModel.Menu.Design
{
    public class MenuItemDesignModel : MenuItemViewModel
    {
        public static MenuItemDesignModel Instance => new MenuItemDesignModel();

        public MenuItemDesignModel()
        {
            Text = "Hello World";
            Icon = IconType.File;
        }

    }
}
