using DotNetECommerce.Data.Repositories.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

            // Check if the file exists
            if (!File.Exists(excelFilePath))
            {
                throw new FileNotFoundException($"Excel file not found at: {excelFilePath}");
            }
        }

        public List<Dictionary<string, object>> GenerateSalesReports()
        {
            try
            {
                // Fetch necessary data from repositories
                var orders = _orderRepository.GetAll();
                //var orderDetails = _orderDetailRepository.GetAll();
                //var products = _productRepository.GetAll();

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
                CreateExcelFile(salesReports);

                return salesReports;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Error accessing data from '{excelFilePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error generating sales reports: {ex.Message}", ex);
            }
        }

        // Implement other repository methods if needed

        private void CreateExcelFile(List<Dictionary<string, object>> salesReports)
        {
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("SalesReport");

                // Add headers
                var headers = salesReports.First().Keys.ToArray();
                for (var col = 0; col < headers.Length; col++)
                {
                    worksheet.Cells[1, col + 1].Value = headers[col];
                }

                // Add data
                var row = 2;
                foreach (var report in salesReports)
                {
                    var col = 1;
                    foreach (var key in headers)
                    {
                        worksheet.Cells[row, col].Value = report[key];
                        col++;
                    }
                    row++;
                }

                // Save the Excel file
                File.WriteAllBytes(excelFilePath, package.GetAsByteArray());
            }
        }

    }
}
