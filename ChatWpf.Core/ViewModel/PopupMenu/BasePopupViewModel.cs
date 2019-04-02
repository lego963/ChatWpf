using ChatWpf.Core.DataModels;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.PopupMenu
{
    public class BasePopupViewModel : BaseViewModel
    {
        public string BubbleBackground { get; set; }

        public BaseViewModel Content { get; set; }

        public ElementHorizontalAlignment ArrowAlignment { get; set; }

        public BasePopupViewModel()
        {
            // TODO: Move colors into Core and make use of it here
            BubbleBackground = "ffffff";
            ArrowAlignment = ElementHorizontalAlignment.Left;
        }

    }
}