using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        List<Category> GetAllCategories();
        // Define more service methods here as needed.
    }
}
