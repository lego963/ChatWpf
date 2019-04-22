using System.Threading.Tasks;
using ChatWpf.ViewModel.Base;

namespace ChatWpf.ViewModel.Chat.ChatMessage
{
    public class ChatMessageListItemImageAttachmentViewModel : BaseViewModel
    {
        private string _thumbnailUrl;

        public string Title { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string ThumbnailUrl
        {
            get => _thumbnailUrl;
            set
            {
                if (value == _thumbnailUrl)
                    return;

                _thumbnailUrl = value;

                // TODO: Download image from website
                //       Save file to local storage/cache
                //       Set LocalFilePath value
                //
                //       For now, just set the file path directly
                Task.Delay(2000).ContinueWith(t => LocalFilePath = "/Images/Samples/rusty.jpg");
            }
        }

        public string LocalFilePath { get; set; }

        public bool ImageLoaded => LocalFilePath != null;
    }
}
