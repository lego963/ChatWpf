using ChatWpf.Web.Server.Data;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.IoC
{
    public static class IoC
    {
        public static ApplicationDbContext ApplicationDbContext => IoCContainer.Provider.GetService<ApplicationDbContext>();
    }
}