using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesSummaryFunction;
using SalesSummaryFunction.Services;
using System.Xml.Linq;


namespace SalesSummaryFunction
{
    public class Startup : FunctionsStartup
    {
        public IConfiguration Configuration { get; }
        private string _conn;
        public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder builder)
        {
            var config = builder.ConfigurationBuilder.Build();
            var appConfigConnection = config.GetConnectionString(nameof(AppDbContext));
            _conn = appConfigConnection;
        }

        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddDbContextPool<AppDbContext>(options =>
            {
                options.UseSqlServer(_conn,
                    sqlServerOptions => sqlServerOptions.CommandTimeout(1200));
            });
            builder.Services.AddScoped<DbContext, AppDbContext>();
            builder.Services.AddTransient<IMailSenderService, SendGridMailService>();
            builder.Services.AddHttpContextAccessor();
            builder.Services.BuildServiceProvider();
        }
    }
}
