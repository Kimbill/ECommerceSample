using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();

    }
}
