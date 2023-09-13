using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Core.Services.Implementations
{
    public class SalesReportService : ISalesReportService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;

        public SalesReportService(IOrderRepository orderRepository, IProductRepository productRepository, IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        //public List<Order>
    }
}
