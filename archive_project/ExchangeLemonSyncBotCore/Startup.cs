using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndClassLibrary;
using BlueLight.Main;
//using BackEndStandard;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeLemonSyncBotCore
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
            services.AddMvc();

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                // Lockout settings
                //options.Lockout.AllowedForNewUsers = true;
                //options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                //options.Lockout.MaxFailedAccessAttempts = 5;
                options.Password.RequireNonAlphanumeric = false;
            })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();


            var logConnString = Configuration.GetConnectionString("LogConnection");
            services.AddDbContext<LoggingContext>(options =>
#if DEBUG
            //   options.UseInMemoryDatabase("log")
            options.UseSqlServer(logConnString
               , b => b.MigrationsAssembly("ExchangeLemonCore"))
#else
                options.UseSqlServer (logConnString
                    , b => b.MigrationsAssembly ("ExchangeLemonCore"))
#endif
            );

            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                   //, b => b.MigrationsAssembly("BackEndStandard"))
                   , b => b.MigrationsAssembly("ExchangeLemonCore"))

            );

            DependencyHelper.Init(services);

            //services.AddTransient<ITransactionHubService, RemoteTransactionHubService> ();
            //services.AddSingleton<SignalFactory> ();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

// services.AddTransient<OrderItemCancelAllEvent> ();
// services.AddTransient<OrderItemCancelEvent> ();

// services.AddDbContext<ApplicationDbContext>(options =>
//options
////.UseLazyLoadingProxies()
//.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
////, b => b.MigrationsAssembly("BackEndStandard"))
//, b => b.MigrationsAssembly("ExchangeLemonCore"))

//);

// services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
////, b => b.MigrationsAssembly("BackEndStandard"))
//, b => b.MigrationsAssembly("ExchangeLemonCore"))

//);