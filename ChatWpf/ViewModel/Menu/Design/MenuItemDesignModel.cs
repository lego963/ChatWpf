using ChatWpf.Core.DataModels;

namespace ChatWpf.ViewModel.Menu.Design
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
