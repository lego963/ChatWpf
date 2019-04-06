using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ChatWpf.Core.ViewModel.Base;
using ChatWpf.Core.ViewModel.PopupMenu;

namespace ChatWpf.Core.ViewModel.Chat.ChatMessage
{
    public class ChatMessageListViewModel : BaseViewModel
    {
        protected string mLastSearchText;

        protected string mSearchText;

        protected ObservableCollection<ChatMessageListItemViewModel> mItems;

        protected bool mSearchIsOpen;

        public ObservableCollection<ChatMessageListItemViewModel> Items
        {
            get => mItems;
            set
            {
                if (mItems == value)
                    return;

                mItems = value;

                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(mItems);
            }
        }

        public ObservableCollection<ChatMessageListItemViewModel> FilteredItems { get; set; }

        public string DisplayTitle { get; set; }

        public bool AttachmentMenuVisible { get; set; }

        public bool AnyPopupVisible => AttachmentMenuVisible;

        public ChatAttachmentPopupMenuViewModel AttachmentMenu { get; set; }

        public string PendingMessageText { get; set; }

        public string SearchText
        {
            get => mSearchText;
            set
            {
                if (mSearchText == value)
                    return;

                mSearchText = value;

                if (string.IsNullOrEmpty(SearchText))
                    Search();
            }
        }

        public bool SearchIsOpen
        {
            get => mSearchIsOpen;
            set
            {
                if (mSearchIsOpen == value)
                    return;

                mSearchIsOpen = value;

                if (!mSearchIsOpen)
                    SearchText = string.Empty;
            }
        }

        public ICommand AttachmentButtonCommand { get; set; }

        public ICommand PopupClickawayCommand { get; set; }

        public ICommand SendCommand { get; set; }

        public ICommand SearchCommand { get; set; }

        public ICommand OpenSearchCommand { get; set; }

        public ICommand CloseSearchCommand { get; set; }

        public ICommand ClearSearchCommand { get; set; }

        public ChatMessageListViewModel()
        {
            // Create commands
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            PopupClickawayCommand = new RelayCommand(PopupClickaway);
            SendCommand = new RelayCommand(Send);
            SearchCommand = new RelayCommand(Search);
            OpenSearchCommand = new RelayCommand(OpenSearch);
            CloseSearchCommand = new RelayCommand(CloseSearch);
            ClearSearchCommand = new RelayCommand(ClearSearch);

            // Make a default menu
            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        public void AttachmentButton()
        {
            // Toggle menu visibility
            AttachmentMenuVisible ^= true;
        }

        public void PopupClickaway()
        {
            // Hide attachment menu
            AttachmentMenuVisible = false;
        }

        public void Send()
        {
            if (string.IsNullOrEmpty(PendingMessageText))
                return;

            if (Items == null)
                Items = new ObservableCollection<ChatMessageListItemViewModel>();

            if (FilteredItems == null)
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>();

            // Fake send a new message
            var message = new ChatMessageListItemViewModel
            {
                Initials = "LM",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                SenderName = "Luke Malpass",
                NewItem = true
            };

            Items.Add(message);
            FilteredItems.Add(message);

            PendingMessageText = string.Empty;
        }

        public void Search()
        {
            if ((string.IsNullOrEmpty(mLastSearchText) && string.IsNullOrEmpty(SearchText)) ||
                string.Equals(mLastSearchText, SearchText))
                return;

            if (string.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(Items ?? Enumerable.Empty<ChatMessageListItemViewModel>());

                mLastSearchText = SearchText;

                return;
            }

            // TODO: Make more efficient search
            FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(
                Items.Where(item => item.Message.ToLower().Contains(SearchText)));

            mLastSearchText = SearchText;
        }

        public void ClearSearch()
        {
            if (!string.IsNullOrEmpty(SearchText))
                SearchText = string.Empty;
            else
                SearchIsOpen = false;
        }

        public void OpenSearch() => SearchIsOpen = true;

        public void CloseSearch() => SearchIsOpen = false;
    }
}
