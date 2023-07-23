using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SalesCartFunction;
using SalesCartFunction.Services;
using SalesCartFunction.ViewModels;

[assembly: FunctionsStartup(typeof(Startup))]
namespace SalesCartFunction
{
    public class Function1
    {
        public IShoopingCartService _shoopingCart;
        public Function1(IShoopingCartService shoopingCart) => _shoopingCart = shoopingCart;

        [FunctionName("Function1")]
        public void Run([QueueTrigger("queue-cart-orders")] string myQueueItem, ILogger log)
        {
            var model = JsonConvert.DeserializeObject<ShoopingCartViewModel>(myQueueItem);
            _shoopingCart.CreateOrder(model);
            log.LogInformation($"C# Queue trigger function processed: {myQueueItem}");
        }
    }
}
