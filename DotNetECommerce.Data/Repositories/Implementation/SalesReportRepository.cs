using DotNetECommerce.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class SalesReportRepository : ISalesReportRepository
    {
        private readonly string excelFilePath;
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly IProductRepository _productRepository;

        public SalesReportRepository(IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository, IProductRepository productRepository, string filePath = "Sales_path.xlsx")
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
            excelFilePath = filePath;
        }

        public List<Dictionary<string, object>> GenerateSalesReports()
        {
            // Fetch necessary data from repositories
            var orders = _orderRepository.GetAll();
            var orderDetails = _orderDetailRepository.GetAll();
            var products = _productRepository.GetAll();

            // Process data to generate sales reports
            var salesReports = new List<Dictionary<string, object>>();

            foreach (var order in orders)
            {
                var salesReportRow = new Dictionary<string, object>();
                salesReportRow["OrderID"] = order.Id;
                salesReportRow["CustomerName"] = order; // Assuming you have a Customer property on Order
                                                        // Add more columns and data as needed...

                salesReports.Add(salesReportRow);
            }

            return salesReports;
        }

        // Implement other repository methods if needed
    }
}
