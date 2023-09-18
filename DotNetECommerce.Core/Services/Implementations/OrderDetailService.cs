using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Services.Implementation
{
    public class OrderDetailService : IOrderDetailService
    {
        private readonly IOrderDetailRepository _orderDetailRepository;

        public OrderDetailService(IOrderDetailRepository orderDetailRepository)
        {
            _orderDetailRepository = orderDetailRepository;
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            try
            {
                // Retrieve all order details from the repository
                var orderDetails = _orderDetailRepository.GetAllOrderDetails();

                // You can add additional business logic here if needed.

                return orderDetails;
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
                throw new Exception($"Error in OrderDetailService: {ex.Message}", ex);
            }
        }
    }
}