using System.ComponentModel.DataAnnotations;

namespace AzureWebApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string OrderNo { get; set; }
        public double Total { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public List<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
