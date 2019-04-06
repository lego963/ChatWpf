using System.Threading.Tasks;
using ChatWpf.Core.ViewModel.Base;

namespace ChatWpf.Core.ViewModel.Chat.ChatMessage
{
    public class ChatMessageListItemImageAttachmentViewModel : BaseViewModel
    {
        private string mThumbnailUrl;

        public string Title { get; set; }

        public string FileName { get; set; }

        public long FileSize { get; set; }

        public string ThumbnailUrl
        {
            get => mThumbnailUrl;
            set
            {
                // If value hasn't changed, return
                if (value == mThumbnailUrl)
                    return;

                // Update value
                mThumbnailUrl = value;

                // TODO: Download image from website
                //       Save file to local storage/cache
                //       Set LocalFilePath value
                //
                //       For now, just set the file path directly
                System.Threading.Tasks.Task.Delay(2000).ContinueWith(t => LocalFilePath = "/Images/Samples/user.png");
            }
        }

        public string LocalFilePath { get; set; }

        public bool ImageLoaded => LocalFilePath != null;
    }
}
