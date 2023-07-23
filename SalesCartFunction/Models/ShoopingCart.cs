using System.Collections.Generic;

namespace SalesCartFunction.Models
{
    public class ShoopingCart
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string CustomerMobile { get; set; }
        public string ShippingAddress { get; set; }
        public List<ShoopingCartDetails> ShoopingCartDetails { get; set; } = new List<ShoopingCartDetails>();
    }
}
