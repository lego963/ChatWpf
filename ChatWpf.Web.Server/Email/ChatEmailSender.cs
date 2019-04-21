using System.Threading.Tasks;
using ChatWpf.Core.Email;
using ChatWpf.Web.Server.IoC;
using static Dna.FrameworkDI;

namespace ChatWpf.Web.Server.Email
{
    public static class ChatEmailSender
    {
        /// <summary>
        /// Sends a verification email to the specified user
        /// </summary>
        /// <param name="displayName">The users display name (typically first name)</param>
        /// <param name="email">The users email to be verified</param>
        /// <param name="verificationUrl">The URL the user needs to click to verify their email</param>
        /// <returns></returns>
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await DI.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
                {
                    IsHTML = true,
                    FromEmail = Configuration["SynthesisSettings:SendEmailFromEmail"],
                    FromName = Configuration["SynthesisSettings:SendEmailFromName"],
                    ToEmail = email,
                    ToName = displayName,
                    Subject = "Verify Your Email - Synthesis"
            },
                "Verify Email",
                $"Hi {displayName ?? "stranger"},",
                "Thanks for creating an account with us.<br/>To continue please verify your email with us.",
                "Verify Email",
                verificationUrl
            );
        }
    }
}