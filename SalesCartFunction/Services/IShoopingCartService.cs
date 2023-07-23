using SalesCartFunction.Models;
using SalesCartFunction.ViewModels;

namespace SalesCartFunction.Services
{
    public interface IShoopingCartService
    {
       public void CreateOrder(ShoopingCartViewModel model);
    }
}
