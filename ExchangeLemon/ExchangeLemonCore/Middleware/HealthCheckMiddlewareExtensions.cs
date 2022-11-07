using Microsoft.AspNetCore.Builder;
//using ExchangeLemonCore.Data;
// using Hellang.Middleware.ProblemDetails;
//using BackEndStandard;

namespace ExchangeLemonCore
{
    public static class HealthCheckMiddlewareExtensions
    {
        public static IApplicationBuilder UseHealthCheck(this IApplicationBuilder builder, string path)
        {
            return builder.UseMiddleware<HealthCheckMiddleware>(path);
        }
    }

}

// services.AddTransient<OrderListInquiryService> ();
// services.AddTransient<FunctionBitcoinService> ();
// services.AddTransient<GraphGeneratorService> ();
// services.AddTransient<BitcoinAddressService> ();

// services.AddTransient<OrderItemCancelAllEvent> ();
// services.AddTransient<OrderItemCancelEvent> ();

// services.AddTransient<IContextSaveService, ContextSaveService> ();

// services.AddTransient<RepoBitcoin> ();

//GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
//GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Formatting = Formatting.Indented;

//config.Formatters.JsonFormatter.SupportedMediaTypes
// .Add(new MediaTypeHeaderValue("text/html"));

//services.AddDbContext<ApplicationDbContext>(options =>
//  options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
//  //, b => b.MigrationsAssembly("BackEndStandard"))
//  , b => b.MigrationsAssembly("ExchangeLemonCore"))

//  );

//services.AddDbContext<LemonDbContext>(options =>
//    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
//    //, b => b.MigrationsAssembly("BackEndStandard"))
//    , b => b.MigrationsAssembly("ExchangeLemonCore"))

//    );

// services.AddDbContext<ApplicationDbContext>(options =>
//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

// services.AddDbContext<LemonDbContext>(options =>
//options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

//options.UseSqlServer(connection, b => b.MigrationsAssembly("ExchangeLemonCore")).

//services.AddIdentity<ApplicationUser, IdentityRole>()
//    .AddEntityFrameworkStores<DBContext>()
//    .AddDefaultTokenProviders();