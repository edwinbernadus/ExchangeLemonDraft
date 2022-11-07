using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEndClassLibrary;
using BlueLight.Main;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PulseLogic;

namespace ExchangeServerGekkoCore
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


            //SetCompatibilityVersion (CompatibilityVersion.Version_2_1);
            var connString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<ApplicationContext>(options =>
               options.UseSqlServer(connString
                   //, b => b.MigrationsAssembly("BackEndStandard"))
                   , b => b.MigrationsAssembly("ExchangeLemonCore"))

            );

            // services.AddTransient<OrderItemCancelEvent> ();
            DependencyHelper.Init(services);
            //services.AddTransient<ITransactionHubService, RemoteTransactionHubService> ();
            //services.AddSingleton<SignalFactory> ();


            services.AddSingleton<SignalFactory>();
            //services.AddTransient<ITransactionHubService, RemoteTransactionHubService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // app.UseMvc ();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}