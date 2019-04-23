using Dna;
using Dna.AspNet;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace ChatWpf.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateBuildWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateBuildWebHostBuilder(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
                .UseDnaFramework(construct =>
                {
                    construct.AddFileLogger();
                })
                .UseStartup<Startup>();
        }
    }
}
