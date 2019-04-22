using System.Threading.Tasks;
using ChatWpf.Core.Email;

namespace ChatWpf.Core.DI.Interfaces
{
    public interface IEmailTemplateSender
    {
        Task<SendEmailResponse> SendGeneralEmailAsync(SendEmailDetails details, string title, string content1, string content2, string buttonText, string buttonUrl);
    }
}
