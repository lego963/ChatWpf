using System;
using System.Text;
using ChatWpf.Web.Server.Data;
using ChatWpf.Web.Server.Email.SendGrid;
using ChatWpf.Web.Server.Email.Templates;
using Dna;
using Dna.AspNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace ChatWpf.Web.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSendGridEmailSender();

            services.AddEmailTemplateSender();

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Framework.Construction.Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().
                AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = Framework.Construction.Configuration["Jwt:Issuer"],
                        ValidAudience = Framework.Construction.Configuration["Jwt:Audience"],

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Framework.Construction.Configuration["Jwt:SecretKey"]))
                    };
                });

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;

                options.User.RequireUniqueEmail = true;
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/login";

                options.ExpireTimeSpan = TimeSpan.FromSeconds(1500);
            });

            services.AddMvc(options => { }).SetCompatibilityVersion(CompatibilityVersion.Version_2_1); 
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            app.UseDnaFramework();

            app.UseAuthentication();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");

                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseCookiePolicy();

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{moreInfo?}");

                routes.MapRoute(
                    name: "aboutPage",
                    template: "more",
                    defaults: new { controller = "About", action = "TellMeMore" });
            });

            serviceProvider.GetService<ApplicationDbContext>().Database.EnsureCreated();
        }
    }
}
