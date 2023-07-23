using AzureWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AzureWebApp.Data
{
    public static class ProductData
    {
        public static List<Product> GetAllProducts()
        {
            return new List<Product>
            {
                new Product { Id = 1, Name ="Khaas Coconut", Quantity = 0 , Price =  250},
                new Product { Id = 2, Name ="Khaas Green Coconut", Quantity = 0, Price =  210 },
                new Product { Id = 3, Name ="Duck", Quantity = 0, Price =  190 },
                new Product { Id = 4, Name ="Chicken", Quantity = 0, Price =  290 },
            };
        }
        public static List<SelectListItem> GetProductNames()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "1", Text ="Khaas Coconut" },
                new SelectListItem { Value = "2", Text ="Khaas Green Coconut"},
                new SelectListItem { Value = "3", Text ="Duck"},
                new SelectListItem { Value = "4", Text ="Chicken"},
            };
        }
    }
}
