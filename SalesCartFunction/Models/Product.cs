﻿using System.ComponentModel.DataAnnotations;

namespace SalesCartFunction.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

    }
}
