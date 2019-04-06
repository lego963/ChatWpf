using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using ChatWpf.Animation;
using ChatWpf.Core.ViewModel.Chat.ChatMessage;

namespace ChatWpf.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : BasePage<ChatMessageListViewModel>
    {
        public ChatPage() : base()
        {
            InitializeComponent();
        }

        public ChatPage(ChatMessageListViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }

        protected override void OnViewModelChanged()
        {
            if (ChatMessageList == null)
                return;

            var storyboard = new Storyboard();
            storyboard.AddFadeIn(1, from: true);
            storyboard.Begin(ChatMessageList);

            MessageText.Focus();
        }

        private void MessageText_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            var textbox = sender as TextBox;

            if (e.Key == Key.Enter)
            {
                if (Keyboard.Modifiers.HasFlag(ModifierKeys.Control))
                {
                    var index = textbox.CaretIndex;

                    textbox.Text = textbox.Text.Insert(index, Environment.NewLine);

                    textbox.CaretIndex = index + Environment.NewLine.Length;

                    e.Handled = true;
                }
                else
                    ViewModel.Send();

                e.Handled = true;
            }
        }
    }
}