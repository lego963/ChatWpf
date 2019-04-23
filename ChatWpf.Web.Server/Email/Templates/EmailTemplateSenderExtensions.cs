using ChatWpf.Core.DI.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.Email.Templates
{
    public static class EmailTemplateSenderExtensions
    {
        public static IServiceCollection AddEmailTemplateSender(this IServiceCollection services)
        {
            services.AddTransient<IEmailTemplateSender, EmailTemplateSender>();

            return services;
        }
    }
}
