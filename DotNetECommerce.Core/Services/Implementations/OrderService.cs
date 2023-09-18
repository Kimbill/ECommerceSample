using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetECommerce.Services.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;

        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                // Retrieve all orders from the repository
                var orders = _orderRepository.GetAllOrders();

                // You can add additional business logic here if needed.

                return orders;
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
                throw new Exception($"Error in OrderService: {ex.Message}", ex);
            }
        }
    }
}
