using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly string excelFilePath;

        public OrderDetailRepository(string filePath)
        {
            excelFilePath = filePath;

            // Check if the file exists; if not, create it.
            if (!File.Exists(excelFilePath))
            {
                CreateOrderDetailDataFile(excelFilePath);
            }
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException($"Excel file '{excelFilePath}' does not contain a worksheet.");
                }

                var orderDetails = new List<OrderDetail>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    try
                    {
                        var orderDetail = new OrderDetail
                        {
                            Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                            OrderId = int.Parse(worksheet.Cells[row, 2].Value.ToString()),
                            ProductId = int.Parse(worksheet.Cells[row, 3].Value.ToString()),
                            UnitPrice = decimal.Parse(worksheet.Cells[row, 4].Value.ToString()),
                            Quantity = int.Parse(worksheet.Cells[row, 5].Value.ToString()),
                            Discount = decimal.Parse(worksheet.Cells[row, 6].Value.ToString()),
                        };

                        orderDetails.Add(orderDetail);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException($"Error parsing data in row {row} of '{excelFilePath}': {ex.Message}", ex);
                    }
                }

                return orderDetails;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading data from '{excelFilePath}': {ex.Message}", ex);
            }
        }

        private void CreateOrderDetailDataFile(string filePath)
        {
            try
            {
                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("OrderDetails");

                // Add headers
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "OrderId";
                worksheet.Cells["C1"].Value = "ProductId";
                worksheet.Cells["D1"].Value = "UnitPrice";
                worksheet.Cells["E1"].Value = "Quantity";
                worksheet.Cells["F1"].Value = "Discount";

                // Example data
                worksheet.Cells["A2"].Value = 1;
                worksheet.Cells["B2"].Value = 101;
                worksheet.Cells["C2"].Value = 201;
                worksheet.Cells["D2"].Value = 10.99;
                worksheet.Cells["E2"].Value = 5;
                worksheet.Cells["F2"].Value = 0.1;

                // Save the Excel package to the specified file path
                package.SaveAs(new FileInfo(filePath));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating Excel file '{filePath}': {ex.Message}", ex);
            }
        }
    }
}
