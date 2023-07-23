using Azure.Storage.Queues.Models;
using AzureWebApp.Data;
using AzureWebApp.Models;
using AzureWebApp.Services.Queue;
using AzureWebApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO.Pipelines;
using System.Text.Json;

namespace AzureWebApp.Controllers
{
    public class ShoopingCartController : Controller
    {
        private readonly ILogger<ShoopingCartController> _logger;
        private AppDbContext _context;
        private const string QUEUE_NAME = "queue-cart-orders";
        private readonly IQueueService _queueService;
        public ShoopingCartController(ILogger<ShoopingCartController> logger, AppDbContext context , IQueueService queueService)
        {
            _logger = logger;
            _context = context;
            _queueService = queueService ?? throw new ArgumentNullException(nameof(queueService));
        }

        public async Task<IActionResult> Index()
        {
            var list = await _context.Products.Select(x => new ProductViewModel
            {
                ProductId = x.Id,
                ProductName = x.Name,
                Quantity = 0,
                Price = x.Price
            }).ToListAsync();

            return View(list);
        }
        private Product GetProductValue(int id)
        {
            var allProducts = ProductData.GetAllProducts();
            var product = allProducts.FirstOrDefault(y => y.Id == id);
            return product;
        }
        public IActionResult Create()
        {
            ViewBag.ProductList = ProductData.GetProductNames();
            return View();
        }

        //[HttpGet]
        //public async Task<IActionResult> CartAdd()
        //{
        //    var shoopingcart = await _context.ShoppingCarts.Include(x => x.ShoopingCartAddDetails).FirstOrDefaultAsync();
        //    var productIds = shoopingcart.ShoopingCartAddDetails.Select(y => y.ProductId);
        //    var products  =  await _context.Products.Where(x=> productIds.Contains(x.Id)).ToListAsync();     
        //    var model = new ShoopingCartViewModel();

        //    model.CustomerName = shoopingcart.CustomerName;
        //    model.ShippingAddress = shoopingcart.ShippingAddress;
        //    model.ShoopingCartAddDetails = shoopingcart.ShoopingCartAddDetails.Select(x => new ShoopingCartDetailsViewModel
        //    {
        //        Price = x.Price,
        //        ProductId = x.ProductId,
        //        Quantity = x.Quantity,
        //        Total = x.Total,
        //        ProductName = products.Where(y => y.Id == x.ProductId).FirstOrDefault().Name
        //    }).ToList();

            
        //    return View(model);
        //}

        [HttpPost]
        public async Task<IActionResult> Index(List<ProductViewModel> models)
        {
            var shoopingCart = new ShoopingCartViewModel();
            shoopingCart.CustomerMobile = "000-0000-000";
            shoopingCart.ShippingAddress = "North-Patenga, Chittagong";
            shoopingCart.CustomerName = "Mr.Patenga";

            shoopingCart.ShoopingCartDetails = models.Where(x => x.Quantity > 0).Select(x => 
                        new ShoopingCartDetailsViewModel
                        { ProductId = x.ProductId,
                            Quantity = x.Quantity,
                            Price = x.Price, 
                            Total = x.Quantity * x.Price 
                        }).ToList();
            var queueMessage = System.Text.Json.JsonSerializer.Serialize(shoopingCart);

            _queueService.SendMessage(QUEUE_NAME, queueMessage);

            return RedirectToAction(nameof(Index));
        }
    }
}