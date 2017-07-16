using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using System.Reflection;
using DotNetify;
using DotNetify.Security;
using DataAccess.Repositories;
using Domain.Repository.Interfaces;
using Domain.Service.Interfaces;
using Services;

namespace WebApp
{
   public class Startup
   {
      public Startup(IHostingEnvironment env)
      {
         var builder = new ConfigurationBuilder()
             .SetBasePath(env.ContentRootPath)
             .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
             .AddEnvironmentVariables();
         Configuration = builder.Build();
      }

      public IConfigurationRoot Configuration { get; }


      // This method gets called by the runtime. Use this method to add services to the container.
      public void ConfigureServices(IServiceCollection services)
      {
         // Add framework services.
         services.AddMvc();

         services.AddCors(options =>
         {
            options.AddPolicy("CorsPolicy",
                builder => builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
         });

         // Add authentication middleware and inform .NET Core MVC what scheme we'll be using.
         services.AddAuthentication(options => options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme);

         // SignalR and Memory Cache are required by dotNetify.
         services.AddSignalR(options => options.Hubs.EnableDetailedErrors = true);
         services.AddMemoryCache();

         services.AddTransient<IMenuRepository, MenuRepository>();
         services.AddTransient<IMenuService, MenuService>();
         services.AddTransient<IShoppingCartService, ShoppingCartService>();
         services.AddTransient<IAccountService, AccountService>();
         services.AddSingleton<IUserCache, UserCache>();

         services.AddDotNetify();

         services.AddScoped<ClaimsPrincipal>(p => p.GetRequiredService<IPrincipalAccessor>().Principal as ClaimsPrincipal);

         // Find the assembly "ViewModels" and register it to DotNetify.VMController.
         // This will cause all the classes inside the assembly that inherits from DotNetify.BaseVM to be known as view models.
         var vmAssembly = Assembly.Load(new AssemblyName("ViewModels"));
         if (vmAssembly != null)
            VMController.RegisterAssembly(vmAssembly);
      }

      // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
      public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
      {
         loggerFactory.AddConsole(Configuration.GetSection("Logging"));
         loggerFactory.AddDebug();

         app.UseCookieAuthentication(new CookieAuthenticationOptions
         {
            LoginPath = "/login",
            AuthenticationScheme = CookieAuthenticationDefaults.AuthenticationScheme,
            AutomaticAuthenticate = true,
            AutomaticChallenge = true
         });

         app.UseGoogleAuthentication(new GoogleOptions()
         {
            ClientId = "378148702389-b804945knu3j8smj9202htuse2phfl74.apps.googleusercontent.com",
            ClientSecret = "C0n9v98_8pa0bYyQ94p9_Rcr"
         });

         // Configure SignalR to use web sockets and to allow cross-origin to support mobile app.
         app.UseWebSockets();
         app.Map("/signalr", map =>
         {
            map.UseCors("CorsPolicy");
            map.RunSignalR();
         });

         app.UseStaticFiles();

         app.UseMvc(routes =>
         {
            routes.MapRoute(
                   name: "default",
                   template: "{controller=Home}/{action=Index}/{id?}");
         });

         app.UseDotNetify();
      }
   }
}
