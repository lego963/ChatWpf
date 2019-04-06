using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace ChatWpf.Web.Server.IoC
{
    public static class IoCContainer
    {
        public static ServiceProvider Provider { get; set; }
    }
}
