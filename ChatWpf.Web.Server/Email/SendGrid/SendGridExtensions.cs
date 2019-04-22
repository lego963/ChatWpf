using ChatWpf.Core.DI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.Email.SendGrid
{
    public static class SendGridExtensions
    {
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            return services;
        }
    }
}
