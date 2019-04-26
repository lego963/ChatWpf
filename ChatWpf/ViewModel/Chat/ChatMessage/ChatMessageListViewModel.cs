using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using ChatWpf.ViewModel.Base;
using ChatWpf.ViewModel.PopupMenu;

namespace ChatWpf.ViewModel.Chat.ChatMessage
{
    public class ChatMessageListViewModel : BaseViewModel
    {
        private string _lastSearchText;

        private string _searchText;

        private ObservableCollection<ChatMessageListItemViewModel> _items;

        private bool _searchIsOpen;

        public ObservableCollection<ChatMessageListItemViewModel> Items
        {
            get => _items;
            set
            {
                if (_items == value)
                    return;

                _items = value;

                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(_items);
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
            get => _searchText;
            set
            {
                if (_searchText == value)
                    return;

                _searchText = value;

                if (string.IsNullOrEmpty(SearchText))
                    Search();
            }
        }

        public bool SearchIsOpen
        {
            get => _searchIsOpen;
            set
            {
                if (_searchIsOpen == value)
                    return;

                _searchIsOpen = value;

                if (!_searchIsOpen)
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
            AttachmentButtonCommand = new RelayCommand(AttachmentButton);
            PopupClickawayCommand = new RelayCommand(PopupClickaway);
            SendCommand = new RelayCommand(Send);
            SearchCommand = new RelayCommand(Search);
            OpenSearchCommand = new RelayCommand(OpenSearch);
            CloseSearchCommand = new RelayCommand(CloseSearch);
            ClearSearchCommand = new RelayCommand(ClearSearch);

            AttachmentMenu = new ChatAttachmentPopupMenuViewModel();
        }

        public void AttachmentButton()
        {
            AttachmentMenuVisible ^= true;
        }

        public void PopupClickaway()
        {
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

            var message = new ChatMessageListItemViewModel
            {
                Initials = "RG",
                Message = PendingMessageText,
                MessageSentTime = DateTime.UtcNow,
                SentByMe = true,
                SenderName = "Rodion Gyrbu",
                NewItem = true
            };

            Items.Add(message);
            FilteredItems.Add(message);

            PendingMessageText = string.Empty;
        }

        public void Search()
        {
            if (string.IsNullOrEmpty(_lastSearchText) && string.IsNullOrEmpty(SearchText) ||
                string.Equals(_lastSearchText, SearchText))
                return;

            if (string.IsNullOrEmpty(SearchText) || Items == null || Items.Count <= 0)
            {
                FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(Items ?? Enumerable.Empty<ChatMessageListItemViewModel>());

                _lastSearchText = SearchText;

                return;
            }

            // Find all items that contain the given text
            // TODO: Make more efficient search
            FilteredItems = new ObservableCollection<ChatMessageListItemViewModel>(
                Items.Where(item => item.Message.ToLower().Contains(SearchText)));

            _lastSearchText = SearchText;
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
