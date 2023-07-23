using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SalesSummaryFunction;
using SalesSummaryFunction.Models;
using SalesSummaryFunction.Services;
using static System.Formats.Asn1.AsnWriter;
[assembly: FunctionsStartup(typeof(Startup))]

namespace SalesSummaryFunction
{
    public class Function1
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public IMailSenderService _mailService;
        private readonly IConfiguration _config;
        public Function1(IServiceScopeFactory serviceScopeFactory, IMailSenderService mailService, IConfiguration config)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _mailService = mailService;
            _config =  config;
        }


        [FunctionName("Function1")]
        public async Task RunAsync([TimerTrigger("*/60 * * * * *", RunOnStartup = true)] TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            await SaleSummaryAsync();
        }

        public async Task SaleSummaryAsync()
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var totalSales = _context.Orders.Where(x => x.Date == DateTime.Now.Date).Sum(x => x.Total);
                    var emailText = GetMailText(totalSales);
                    // temp comment for unnecessary mail sending
                    var toEmailAddress = _config.GetValue<string>("sendGridToEmailAddress");
                    await _mailService.SendAsync(toEmailAddress, emailText.Item1, emailText.Item2);
                }
            }
            catch (Exception ex)
            {
            }
        }

        public Tuple<string, string> GetMailText(double salesAmount)
        {
            string subject = $"Sales Summary ({DateTime.Now.Date})";
            string content = $"Hello , Good Day <br/> Today Total Sales : <strong>({salesAmount})</strong>";

            return new Tuple<string, string>(subject, content);
        }
    }
}
