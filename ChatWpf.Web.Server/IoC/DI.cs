using ChatWpf.Core.DI.Interfaces;
using ChatWpf.Web.Server.Data;
using Dna;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.IoC
{
    public static class Di
    {
        public static ApplicationDbContext ApplicationDbContext => Framework.Provider.GetService<ApplicationDbContext>();

        public static IEmailSender EmailSender => Framework.Provider.GetService<IEmailSender>();

        public static IEmailTemplateSender EmailTemplateSender => Framework.Provider.GetService<IEmailTemplateSender>();
    }
}
