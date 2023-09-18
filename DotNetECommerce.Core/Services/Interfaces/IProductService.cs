using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Core.Services.Interfaces
{
    public interface IProductService
    {
        List<Product> GetAllProducts();
        void AddProduct(Product product);
        // Define more service methods here as needed.
    }
}
