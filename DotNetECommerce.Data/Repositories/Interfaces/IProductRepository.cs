using DotNetECommerce.Model;
using DotNetECommerce.Data.Repositories.Implementation;

namespace DotNetECommerce.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        void AddProduct(Product product);
        List<Product> GetAllProducts();

    }
}
