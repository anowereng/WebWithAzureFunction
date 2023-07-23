using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SalesCartFunction;
using SalesCartFunction.Models;
using SalesCartFunction.ViewModels;
using System;
using System.Linq;

namespace SalesCartFunction.Services
{
    public class ShoopingCartService : IShoopingCartService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        public ILogger _log;
        public ShoopingCartService(IServiceScopeFactory serviceScopeFactory, ILogger log)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _log = log; 
        }
        public void CreateOrder(ShoopingCartViewModel model)
        {
            try
            {
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    var order = new Order();
                    order.Date = DateTime.Now.Date;
                    order.OrderNo = Guid.NewGuid().ToString();
                    order.CustomerName = model.CustomerName;
                    order.CustomerMobile = model.CustomerMobile;
                    order.Total = model.ShoopingCartDetails.Sum(x => x.Total);
                    order.OrderDetails = model.ShoopingCartDetails.Select(x => new OrderDetails
                    {
                        ProductId = x.ProductId,
                        Quantity = x.Quantity,
                        Price = x.Price,
                        Total = x.Total
                    }).ToList();
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
            }
        }
    }
}
