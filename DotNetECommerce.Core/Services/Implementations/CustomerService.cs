using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Implementation;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Core.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public Dictionary<string, List<Customer>> GetCustomersGroupByCity()
        {
            var customers = _customerRepository.GetAllCustomers();

            var groupedCustomers = customers.GroupBy(customer => customer.City)
                .ToDictionary(group => group.Key, group => group.ToList());

            return groupedCustomers;
        }
    }
}
