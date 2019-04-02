namespace ChatWpf.Core.ViewModel.Dialogs.Design
{
    public class MessageBoxDialogDesignModel : MessageBoxDialogViewModel
    {
        public static MessageBoxDialogDesignModel Instance => new MessageBoxDialogDesignModel();

        public MessageBoxDialogDesignModel()
        {
            OkText = "OK";
            Message = "Design time messages are fun :)";
        }

    }
}
