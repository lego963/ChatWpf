using System.Threading.Tasks;
using ChatWpf.Core.Email;

namespace ChatWpf.Core.IoC.Interfaces
{
    public interface IEmailSender
    {
        /// <summary>
        /// Sends an email message with the given information
        /// </summary>
        /// <param name="details">The details about the email to send</param>
        /// <returns></returns>
        Task<SendEmailResponse> SendEmailAsync(SendEmailDetails details);
    }
}
