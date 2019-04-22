using System.Threading.Tasks;
using ChatWpf.Core.Email;

namespace ChatWpf.Core.DI.Interfaces
{
    /// <summary>
    /// A service that handles sending emails on behalf of the caller
    /// </summary>
    public interface IEmailSender
    {
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
