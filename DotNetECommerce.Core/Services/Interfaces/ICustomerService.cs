using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Core.Services.Interfaces
{
    public interface ICustomerService
    {
        List<Customer> GetAllCustomers();
        void AddCustomer(Customer customer);
        Dictionary<string, List<Customer>> GetCustomersGroupByCity();
        List<Customer> GetCustomersWithHighOrderTotal();
        // Define more service methods here as needed.
    }
}
