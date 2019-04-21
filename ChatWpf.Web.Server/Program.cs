﻿using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Dna;
using Dna.AspNet;

namespace ChatWpf.Web.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args)
        {
            return WebHost.CreateDefaultBuilder()
                // Add Dna Framework
                .UseDnaFramework(construct =>
                {
                    // Configure framework

                    // Add file logger
                    construct.AddFileLogger();
                })
                .UseStartup<Startup>()
                .Build();
        }
    }
}