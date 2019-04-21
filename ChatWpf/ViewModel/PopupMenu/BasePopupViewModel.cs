using ChatWpf.Core.DataModels;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.PopupMenu
{
    public class BasePopupViewModel : BaseViewModel
    {
        public string BubbleBackground { get; set; }

        public ElementHorizontalAlignment ArrowAlignment { get; set; }

        public BaseViewModel Content { get; set; }

        public BasePopupViewModel()
        {
            // Set default values
            // TODO: Move colors into Core and make use of it here
            BubbleBackground = "ffffff";
            ArrowAlignment = ElementHorizontalAlignment.Left;
        }

    }
}
