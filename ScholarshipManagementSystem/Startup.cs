using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Repository.Data;
using DAL.Models;
using ScholarshipManagementSystem.Permission;
using ScholarshipManagementSystem.Services;
using ScholarshipManagementSystem.Models;
using ScholarshipManagementSystem.Common;

namespace ScholarshipManagementSystem
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(opt =>
            {
                opt.Password.RequiredLength = 7;                
                opt.Password.RequireDigit = false;
                opt.Password.RequireUppercase = false;
                opt.User.RequireUniqueEmail = true;
                opt.SignIn.RequireConfirmedEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultUI()
            .AddDefaultTokenProviders();
            services.Configure<DataProtectionTokenProviderOptions>(opts => opts.TokenLifespan = System.TimeSpan.FromHours(24));
            services.AddControllersWithViews();
            string smtpServer = Configuration.GetSection("MailSettings:Host").Value;
            int.TryParse(Configuration.GetSection("MailSettings:Port").Value, out int port);            
            string password = Configuration.GetSection("MailSettings:Password").Value;
            string displayName = Configuration.GetSection("MailSettings:DisplayName").Value;
            string email = Configuration.GetSection("MailSettings:Mail").Value;

            EmailSender emailSender = new EmailSender(smtpServer, port, password, displayName, email);
            services.AddSingleton<IEmailSender>(emailSender);
            services.AddTransient<reCaptchaService>();
            services.AddSession();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseRouting();
            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NDczNTU0QDMxMzkyZTMyMmUzMEh0QU9pd2FiUWJ5ZndJRFNQNjFTTWJwS2kySFVwL3dIY0k5L29KSWFtREU9;NDczNTU1QDMxMzkyZTMyMmUzMEJsZWdlZmtoN0EwOEkxUng2aUdZUjB1RW03VGozdzk3Zk54VWdibnAzVHc9");
        }
    }
}