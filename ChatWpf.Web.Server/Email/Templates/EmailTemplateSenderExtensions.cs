using ChatWpf.Core.IoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.Email.Templates
{
    public static class EmailTemplateSenderExtensions
    {
        /// <summary>
        /// Injects the <see cref="EmailTemplateSender"/> into the services to handle the 
        /// <see cref="IEmailTemplateSender"/> service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddEmailTemplateSender(this IServiceCollection services)
        {
            // Inject the SendGridEmailSender
            services.AddTransient<IEmailTemplateSender, EmailTemplateSender>();

            // Return collection for chaining
            return services;
        }
    }
}
