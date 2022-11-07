using System.Data.SqlClient;
using System.Threading.Tasks;
//using ExchangeLemonCore.Data;
// using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Http;
//using BackEndStandard;

namespace ExchangeLemonCore
{
    public class HealthCheckMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path;

        public HealthCheckMiddleware(RequestDelegate next, string path)
        {
            _next = next;
            _path = path;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.Path.Value == _path)
            {
                try
                {
                    // using (var db = new SqlConnection("Database=Oops"))
                    // {
                    //     await db.OpenAsync();
                    //     db.Close();
                    // }

                    context.Response.StatusCode = 200;
                    context.Response.ContentLength = 2;
                    await context.Response.WriteAsync("UP");
                }
                catch (SqlException)
                {
                    context.Response.StatusCode = 400;
                    context.Response.ContentLength = 20;
                    await context.Response.WriteAsync("SQL Connection Error");
                }
            }
            else
            {
                await this._next(context);
            }
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