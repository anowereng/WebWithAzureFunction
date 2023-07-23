using System.ComponentModel.DataAnnotations;

namespace AzureWebApp.ViewModels
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string ProductName { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }
    }
}
