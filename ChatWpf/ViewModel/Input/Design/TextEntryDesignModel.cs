namespace ChatWpf.ViewModel.Input.Design
{
    public class TextEntryDesignModel : TextEntryViewModel
    {
        public static TextEntryDesignModel Instance => new TextEntryDesignModel();

        public TextEntryDesignModel()
        {
            Label = "Name";
            OriginalText = "Rodion Gyrbu";
            EditedText = "Editing";
        }
    }
}
