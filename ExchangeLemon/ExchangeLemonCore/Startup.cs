using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Bugsnag.AspNet.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BackEndClassLibrary;
using BlueLight.Main;
using ExchangeLemonCore.Services;

// using Microsoft.ApplicationInsights.AspNetCore;
// using Microsoft.ApplicationInsights.Extensibility;
//using Microsoft.ApplicationInsights.SnapshotCollector;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ExchangeLemonCore.Controllers;
using Microsoft.Net.Http.Headers;

// using Coravel;
// using Coravel.Scheduling.Schedule.Interfaces;
//using ExchangeSerilog;
using DebugWorkplace;
using BotWalletWatcher;
using MediatR;
using MediatR.Pipeline;
using PulseLogic;
using System.Reflection;
using Hangfire;
using Hangfire.MemoryStorage;
// using Microsoft.ApplicationInsights.Extensibility.Implementation;
using ExchangeSerilog;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ElmahCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.HttpOverrides;

namespace ExchangeLemonCore
{
    public class Startup
    {
#if DEBUG
        private bool isMigrate = false;
#else
        private bool isMigrate = false;
#endif

        bool isDevModeMemoryRepository = false;
        bool isSaveLogSqlToFile = false;


        //private class SnapshotCollectorTelemetryProcessorFactory : ITelemetryProcessorFactory
        //{
        //    private readonly IServiceProvider _serviceProvider;

        //    public SnapshotCollectorTelemetryProcessorFactory(IServiceProvider serviceProvider) =>
        //        _serviceProvider = serviceProvider;

        //    public ITelemetryProcessor Create(ITelemetryProcessor next)
        //    {
        //        var snapshotConfigurationOptions = _serviceProvider.GetService<IOptions<SnapshotCollectorConfiguration>>();
        //        return new SnapshotCollectorTelemetryProcessor(next, configuration: snapshotConfigurationOptions.Value);
        //    }
        //}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        //public IServiceCollection services { get; private set; }

        //public ServiceProvider Services { get; private set; }
        //public IServiceCollection Services { get;  set; }


        //void ConfigureCosmos()
        //{
        //    var serviceEndpoint = "https://waterbearcosmos.documents.azure.com:443/";
        //    var databaseName = "orange-one";
        //    string authKey = "QRuQAXtVKxo43hsPPhVjVC8FWxOKvvyvmWKgQDTbdngVAqDoacG4jNNZuVYHHkVaMiCRdVAz0KOV49BUeWE04w==";

        //    var services = new ServiceCollection();

        //    // services.AddDbContext<BooksContext>(options => options.UseCosmosSql(configSection["ServiceEndpoint"], configSection["AuthKey"], configSection["DatabaseName"]));
        //    services.AddDbContextPool<CosmosContext>(options => options.UseCosmosSql(serviceEndpoint,
        //    authKey, databaseName));
        //}

        public void ConfigureServices(IServiceCollection services)
        {
            // services.Configure<DevModeConfig>(Configuration.GetSection("DevMode"));
            // services.AddSingleton(resolver =>
            // {
            //     var item = resolver.GetRequiredService<IOptions<DevModeConfig>>().Value;
            //     isDevModeMemoryRepository = item.IsDevModeMemoryRepository;
            //     return item;
            // });

            services.AddCors(options =>
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader()
                            .AllowCredentials();
                    }
                )
            );

            services.AddHttpClient();
            services.AddMemoryCache();
            // services.AddSignalR();
            // services.AddSignalR();

            services.AddElmah();
            services.AddSignalR()
                .AddMessagePackProtocol();
            //services.Configure<SnapshotCollectorConfiguration>(Configuration.GetSection(nameof(SnapshotCollectorConfiguration)));

            //services.AddSingleton<ITelemetryProcessorFactory>(sp => new SnapshotCollectorTelemetryProcessorFactory(sp));


            var connString = Configuration.GetConnectionString("DefaultConnection");
            var logConnString = Configuration.GetConnectionString("LoggingConnection");

            var sourceConnString = Configuration.GetConnectionString("SourceConnection");
            services.AddDbContextPool<SourceContext>(options =>
                options.UseSqlServer(sourceConnString)
            );


            ConfigureLoggingContext(logConnString, services);
            ConfigureApplicationContext(connString, services);


            services.AddDbContextPool<DashboardContext>(options =>
                options.UseInMemoryDatabase("dashboard")
            );

            services.AddDbContextPool<GraphDbContext>(options =>
                options.UseInMemoryDatabase("graph_db")
            );

            services.AddDbContextPool<LoggingExtContext>(options =>
                options.UseSqlServer(logConnString
                    , b => b.MigrationsAssembly("ExchangeLemonCore"))
            );


            services.AddDbContextPool<BlockContext>(options =>
                options.UseSqlServer(logConnString
                    , b => b.MigrationsAssembly("ExchangeLemonCore"))
            );


            // services.AddDbContextPool<EmployeeContext>(options => options.UseSqlServer(connection));
            //services.AddDbContextPool<ApplicationContext>(options =>


            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                })
                .AddEntityFrameworkStores<ApplicationContext>()
                .AddDefaultTokenProviders();
            
            ConfigureExternalIdentity(services);
            
            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            //services.AddMvc();
            services.AddHangfire(x => x.UseMemoryStorage());

            services.AddBugsnag(configuration => { configuration.ApiKey = "87693e2d51ea8347697cd144f6736073"; });


            //ConfigureCosmos();
            //ConfigureCosmos.Execute(services);

            DependencyHelper.Init(services);
            AddDependency(services);
            

            var s2 = services.BuildServiceProvider();

            var s3 = s2.GetService<RpcBackEnd>();
            if (isMigrate == false)
            {
                s3.InitBackend();
            }

            services.AddMvc().AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new Newtonsoft.Json.Serialization.DefaultContractResolver();
            });
            //InitRpcBackEndExt(s2);

            //var s3 = s2.GetService<IHttpPostService>();
            //InitRpcBackEnd(s3);

            //this.services =  services;
        }

        //[Obsolete]
        //void InitRpcBackEnd(IHttpPostService httpPostService)
        //{
        //    //var n = new HttpPostService();
        //    //var rpcBackEnd = new RpcBackEnd(httpPostService);
        //    //rpcBackEnd.InitBackend();
        //}

        //private void InitRpcBackEndExt(ServiceProvider s2)
        //{
        //    var s3 = s2.GetService<RpcBackEnd>();
        //    s3.InitBackend();
        //}


        private static void InitLinuxExt(IApplicationBuilder app)
        {
            var forwardOptions = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
                RequireHeaderSymmetry = false
            };

            forwardOptions.KnownNetworks.Clear();
            forwardOptions.KnownProxies.Clear();

// ref: https://github.com/aspnet/Docs/issues/2384
            app.UseForwardedHeaders(forwardOptions);
        }

        private void AddDependency(IServiceCollection services)
        {
            services.AddMediatR(typeof(SendMoneyTestCommand).GetTypeInfo().Assembly);

            services.AddTransient<IHttpPostService, HttpPostService>();
            //services.AddTransient<RpcBackEnd>();
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddSingleton<RpcBackEnd>();
            services.AddTransient<ILogHubService, LogHubService>();
            services.AddTransient<IBitcoinGetBalance, BitcoinGetBalance>();
            //services.AddSingleton<SignalFactory>();
            //services.AddTransient<ITransactionHubService, RemoteTransactionHubService>();
            services.AddTransient<ITransactionHubService, SelfTransactionHubService>();
            services.AddTransient<ILogHelperMvc, SeriLogHelperMvc>();
            services.AddTransient<SerilogHelper>();
            services.AddTransient<FakeAccountService>();
            services.AddTransient<IFileService, FileService>();
            services.AddTransient<ReplayFileService>();
            services.AddTransient<IReplayPlayerService, ReplayPlayerService>();
            services.AddTransient<IReplayValidationService, ReplayValidationService>();
            services.AddTransient<IValidationCancelOrderAllService, ValidationCancelOrderAllService>();
            services.AddTransient<EnsureContextService>();

            //services.AddTransient<BlockChainDownloadServiceExt>();
            services.AddTransient<PulseService>();
            //services.AddTransient<PulseHub>();
            //services.AddTransient<IUnitOfWorkService, UnitOfWorkService>();
            //services.AddScheduler(scheduler =>
            //    {
            //        SchedulerSubmitJob(scheduler);
            //    }
            //);

            //services.AddElmah();
            services.AddTransient<ImageUrlService>();
            // services.AddTransient<ServiceBusService>();
            // services.AddTransient<LogicBusService>();
            // services.AddTransient<ICustomTelemetryService, CustomTelemetryService>();
            services.AddTransient<ICustomTelemetryService, BackEndClassLibrary.DummyTelemetryService>();
            services.AddTransient<ReceiveLogCaptureService>();
            // 
            // services.AddTransient<BtcCloudService>();

            services.AddTransient<WatcherHub>();
            services.AddTransient<BlockChainPersistantService>();
            // services.AddTransient<BlockChainPositionService>();
            //services.AddTransient<INotificationWatcherService, NotificationWatcherServerService>();
            // DependencyWatcherHelper.Populate(services);

            services.AddSignalR()
                .AddMessagePackProtocol();


            //services.AddTransient<IBtcCloudServiceRegisterNotification, BtcCloudService>();
            services.AddTransient<IBtcServiceSendMoney, BtcServiceServerSendMoney>();
        }

        private static void ConfigureExternalIdentity(IServiceCollection services)
        {
            services.AddAuthentication()
                // .AddApple(options =>
                // {
                //     options.ClientId = Configuration["Apple:ClientId"];
                //     options.KeyId = Configuration["Apple:KeyId"];
                //     options.TeamId = Configuration["Apple:TeamId"];
                //     options.UsePrivateKey(
                //         (keyId) => HostingEnvironment.ContentRootFileProvider.GetFileInfo($"AuthKey_{keyId}.p8"));
                // })
                .AddGoogle(options =>
                {
                    // https://console.developers.google.com/apis/credentials?folder=&organizationId=&project=litebed-9aa4d
                    // https://console.developers.google.com/apis/credentials/oauthclient/987429144757-qu214tn9v6d76e0cg5kfdc4nqv1dkcaf.apps.googleusercontent.com?project=litebed-9aa4d

                    options.ClientId = "987429144757-qu214tn9v6d76e0cg5kfdc4nqv1dkcaf.apps.googleusercontent.com";
                    options.ClientSecret = "sECaoM-3vFPzB10KAFOGl1qI";
                    options.Scope.Add("email");
                    options.Scope.Add("profile");

                    // options.Scope.Add("https://www.googleapis.com/auth/user.birthday.read");
                    // options.Scope.Add("https://www.googleapis.com/auth/user.gender.read");

                    // options.Scope.Add("https://www.googleapis.com/auth/userinfo.profile");
                    // options.Scope.Add("https://www.googleapis.com/auth/user.phonenumbers.read");
                    // options.Scope.Add("https://www.googleapis.com/auth/contacts.readonly");


                    options.ClaimActions.MapJsonKey("urn:google:picture", "picture", "url");
                    options.ClaimActions.MapJsonKey("urn:google:locale", "locale", "string");
                    options.Events.OnCreatingTicket = ctx =>
                    {
                        var p = ctx.Properties.GetTokens().ToList();
                        return Task.CompletedTask;
                    };
                    // options.Events.OnRedirectToAuthorizationEndpoint = ctx =>
                    // {
                    //     var p = ctx.Properties.GetTokens().ToList();
                    //     return Task.CompletedTask;
                    // };
                    options.Events.OnRemoteFailure = ctx =>
                    {
                        var p = ctx.Properties.GetTokens().ToList();
                        return Task.CompletedTask;
                    };
                    options.Events.OnTicketReceived = ctx =>
                    {
                        var p = ctx.Properties.GetTokens().ToList();
                        return Task.CompletedTask;
                    };
                    options.SaveTokens = true;
                })
                // .AddFacebook(options =>
                // {
                //     // https://developers.facebook.com/apps/1811427865834338/fb-login/settings/
                //
                //     options.ClientId = "1811427865834338";
                //     options.ClientSecret = "66e936bcff58521e480dd1dad71a812a";
                //     options.Scope.Add("email");
                //     options.Scope.Add("public_profile");
                //
                //     // options.Scope.Add("user_birthday");
                //     // options.Scope.Add("user_photos");
                //     // options.Scope.Add("user_gender");
                //
                //     // options.Scope.Add("user_location");
                //
                //
                //     options.SaveTokens = true;
                // })
                
                
                // .AddInstagram(
                //     options =>
                //     {
                //         // options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;
                //         
                //         
                //         options.ClientId = "594946004743412";
                //         options.ClientSecret = "204c92f85d14e568721c7fa0caa7f88d";
                //         // options.ClientId = "587326888630400";
                //         // options.ClientSecret = "1203b2a941c8c367f7047b3516fc067e";
                //         options.Scope.Add("email");
                //         options.SaveTokens = true;
                //     });
                ;
        }

        private void ConfigureApplicationContext(string connString, IServiceCollection services)
        {
            var factoryDbContext = GenerateFactoryDbContext();

#if DEBUG


            services.AddDbContextPool<ApplicationContext>(options =>
            {
                if (isDevModeMemoryRepository)
                {
                    options.UseInMemoryDatabase("data");
                }
                else
                {
                    if (isSaveLogSqlToFile)
                    {
                        options.UseLoggerFactory(factoryDbContext);
                    }

                    options.UseSqlServer(connString
                        , b => b.MigrationsAssembly("ExchangeLemonCore"));

                    //           services.AddDbContext<ApplicationDbContext>(options =>
                    //options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")),
                    //ServiceLifetime.Transient);
                }
            }).AddUnitOfWork<ApplicationContext>();

#else
            services.AddDbContextPool<ApplicationContext>(options =>
              options.UseSqlServer(connString, b => b.MigrationsAssembly("ExchangeLemonCore")))
              .AddUnitOfWork<ApplicationContext>();
#endif
        }


        private void ConfigureLoggingContext(string logConnString, IServiceCollection services)
        {
            LoggingContext.connStringLog = logConnString;
#if DEBUG
            var factoryLogContext = GenerateFactoryLogContext();

#endif


            services.AddDbContextPool<LoggingContext>(options =>
#if DEBUG
                {
                    // options.UseInMemoryDatabase("log");

                    if (isDevModeMemoryRepository)
                    {
                        options.UseInMemoryDatabase("log");
                    }
                    else
                    {
                        if (isSaveLogSqlToFile)
                        {
                            options.UseLoggerFactory(factoryLogContext);
                        }

                        options.UseSqlServer(logConnString
                            , b => b.MigrationsAssembly("ExchangeLemonCore"));
                    }
                }
#else
             //options.UseInMemoryDatabase("log")
             options.UseSqlServer(logConnString
                 , b => b.MigrationsAssembly("ExchangeLemonCore"))
#endif
            );
        }

        // private void SchedulerSubmitJob(IScheduler scheduler)
        // {
        //     var stopWatchHelper = new StopWatchHelper("server_time");
        //     stopWatchHelper.Start();

        //     scheduler.ScheduleAsync(async () =>
        //     {
        //         await Task.Delay(500);
        //         var duration = stopWatchHelper.Save("server-time");
        //         var totalMinutes = duration.TotalMinutes;
        //         Console.WriteLine($"Duration total minutes: {totalMinutes}");
        //     })
        //             .EveryMinute();

        //     scheduler.Schedule(
        //         () => Console.WriteLine("Run at 1pm utc during week days.")
        //     )
        //     .DailyAt(13, 00)
        //     .Weekday();
        // }

        private LoggerFactory GenerateFactoryLogContext()
        {
            var factoryLogContext = new LoggerFactory();
            {
                var logger = factoryLogContext;
                var filePath = @"Logs\\LogContext.log";
                logger.AddFile(filePath);
            }
            return factoryLogContext;
        }

        private LoggerFactory GenerateFactoryDbContext()
        {
            var factoryDbContext = new LoggerFactory();
            {
                var logger = factoryDbContext;

                var filePath = @"Logs\\DbContext.log";
                logger.AddFile(filePath);
            }

            return factoryDbContext;
        }

        //public static IServiceProvider __serviceProvider;

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory,
            IApplicationLifetime applicationLifetime)
        {
            Console.WriteLine("[woot] - init");
            applicationLifetime.ApplicationStopping.Register(() =>
            {
                Console.WriteLine("[woot] - finish1");
                RpcBackEnd.connection?.Close();
                Debug.WriteLine("close");
                Console.WriteLine("[woot] - finish2");
                // server is not going to shutdown
                // until the callback is done
            });

            // ApplicationLogging.ConfigureLogger(loggerFactory);
            // ApplicationLogging.LoggerFactory = loggerFactory;

            if (env.IsDevelopment())
            {
                // app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();


                // TelemetryConfiguration.Active.DisableTelemetry = true;
                // TelemetryDebugWriter.IsTracingDisabled = true;
            }
            else
            {
                app.UseDatabaseErrorPage();
                app.UseDeveloperExceptionPage();
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseCors("AllowAllOrigins");
            // app.UseHealthCheck("/hc");


            // app.UseHttpsRedirection();
            InitLinuxExt(app);
            app.UseElmah();
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = ctx =>
                {
                    const long durationInSeconds = 60 * 60 * 24 * 30 * 12;
                    ctx.Context.Response.Headers[HeaderNames.CacheControl] =
                        "public,max-age=" + durationInSeconds;
                }
            });

            app.UseAuthentication();


            app.UseSignalR((options) =>
            {
                options.MapHub<TransactionHub>("/signal/transaction");
                options.MapHub<StreamHub>("/signal/stream");
                options.MapHub<WatcherHub>("/signal/watcher");
                options.MapHub<LogHub>("/signal/log");
                options.MapHub<PulseHub>("/signal/pulse");
                options.MapHub<ChatHub>("/chatHub");
            });
            //});

            // app.UseProblemDetails ();
            // app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            //#if DEBUG
            //app.UseMiddleware<RequestResponseLoggingMiddleware>();
            if (FeatureRepo.UseRequestWebLog)
            {
                app.UseMiddleware<LogRequestMiddleware>();
                app.UseMiddleware<LogResponseMiddleware>();
            }

            //#endif
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            //app.UseElmah();

            //GlobalConfiguration.Configuration.UseMemoryStorage();

            app.UseHangfireDashboard();
            app.UseHangfireServer();
            OrderItemMainService.BackgroundNotificationExecutor =
                new Action<OrderItemNotificationService>((notificationService) =>
                {
                    var jobId = BackgroundJob.Enqueue(
                        () => notificationService.HandleNotification().Wait());
                });
        }
    }
}