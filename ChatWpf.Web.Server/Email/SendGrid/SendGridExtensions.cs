using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatWpf.Core.IoC.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.Email.SendGrid
{
    public static class SendGridExtensions
    {
        /// <summary>
        /// Injects the <see cref="SendGridEmailSender"/> into the services to handle the 
        /// <see cref="IEmailSender"/> service
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSendGridEmailSender(this IServiceCollection services)
        {
            // Inject the SendGridEmailSender
            services.AddTransient<IEmailSender, SendGridEmailSender>();

            // Return collection for chaining
            return services;
        }
    }
}
