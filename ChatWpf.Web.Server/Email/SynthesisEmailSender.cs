using System.Threading.Tasks;
using ChatWpf.Core.Email;
using ChatWpf.Web.Server.IoC;
using static Dna.FrameworkDI;

namespace ChatWpf.Web.Server.Email
{
    public static class SynthesisEmailSender
    {
        public static async Task<SendEmailResponse> SendUserVerificationEmailAsync(string displayName, string email, string verificationUrl)
        {
            return await Di.EmailTemplateSender.SendGeneralEmailAsync(new SendEmailDetails
            {
                IsHtml = true,
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
