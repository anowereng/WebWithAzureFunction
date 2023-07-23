using AzureWebApp.Models;
using System.ComponentModel.DataAnnotations;

namespace AzureWebApp.ViewModels
{
    public class ShoopingCartViewModel
    {
        public string? CustomerName { get; set; }
        public string? CustomerMobile { get; set; }
        public string? ShippingAddress { get; set; }
        public List<ShoopingCartDetailsViewModel> ShoopingCartDetails { get; set; } = new List<ShoopingCartDetailsViewModel>();
    }

    public class ShoopingCartDetailsViewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public double Total { get; set; }
    }
}
