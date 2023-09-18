using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Implementation;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetECommerce.Services.Implementation
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;

        public CustomerService(ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
            _orderRepository = orderRepository;
        }

        public List<Customer> GetAllCustomers()
        {
            try
            {
                // Retrieve all customers from the repository
                var customers = _customerRepository.GetAllCustomers();

                // You can add additional business logic here if needed.

                return customers;
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception.
                throw new Exception($"File not found: {ex.FileName}", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exception (e.g., missing worksheet).
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (FormatException ex)
            {
                // Handle format exception (e.g., parsing errors).
                throw new Exception($"Data format error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions or log errors as needed.
                throw new Exception($"Error in CustomerService: {ex.Message}", ex);
            }
        }

        public void AddCustomer(Customer customer)
        {
            try
            {
                // Add the customer using the repository
                _customerRepository.AddCustomer(customer);

                // You can add additional logic here if needed after adding the customer.
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception.
                throw new Exception($"File not found: {ex.FileName}", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exception (e.g., missing worksheet).
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions or log errors as needed.
                throw new Exception($"Error adding customer: {ex.Message}", ex);
            }
        }

        public Dictionary<string, List<Customer>> GetCustomersGroupByCity()
        {
            try
            {
                var customers = _customerRepository.GetAllCustomers();

                // Group customers by city
                var groupedCustomers = customers
                    .GroupBy(c => c.Location)
                    .ToDictionary(group => group.Key, group => group.ToList());

                return groupedCustomers;
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception.
                throw new Exception($"File not found: {ex.FileName}", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exception (e.g., missing worksheet).
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions or log errors as needed.
                throw new Exception($"Error in GetCustomersGroupByCity: {ex.Message}", ex);
            }
        }

        public List<Customer> GetCustomersWithHighOrderTotal()
        {
            try
            {
                // Retrieve all customers from the repository
                var customers = _customerRepository.GetAllCustomers();

                // Retrieve all orders from the repository
                var orders = _orderRepository.GetAllOrders();

                // Group orders by customer ID and calculate total order amounts
                var orderTotals = orders
                    .GroupBy(order => order.CustomerId)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Sum(order => order.TotalAmount)
                    );

                // Filter customers from 5 locations whose orders total more than $50,000
                var filteredCustomers = customers
                    .Where(customer => orderTotals.ContainsKey(customer.Id) && orderTotals[customer.Id] > 50000)
                    .ToList();

                return filteredCustomers;
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception.
                throw new Exception($"File not found: {ex.FileName}", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exception (e.g., missing worksheet).
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (FormatException ex)
            {
                // Handle format exception (e.g., parsing errors).
                throw new Exception($"Data format error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions or log errors as needed.
                throw new Exception($"Error in GetCustomersWithHighOrderTotal: {ex.Message}", ex);
            }
        }

    }
}
