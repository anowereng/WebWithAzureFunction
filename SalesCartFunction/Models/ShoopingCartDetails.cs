using System.ComponentModel.DataAnnotations.Schema;

namespace SalesCartFunction.Models
{
    public class ShoopingCartDetails
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public int Total { get; set; }

        public int ShoopingCartId { get; set; }
        [ForeignKey(nameof(ShoopingCartId))]
        public ShoopingCart ShoopingCart { get; set; }
    }
}
