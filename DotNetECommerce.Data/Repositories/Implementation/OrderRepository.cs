using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private readonly string excelFilePath;

        public OrderRepository(string filePath)
        {
            excelFilePath = filePath;

            // Check if the file exists; if not, create it.
            if (!File.Exists(excelFilePath))
            {
                CreateOrderDataFile(excelFilePath);
            }
        }

        public List<Order> GetAllOrders()
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException($"Excel file '{excelFilePath}' does not contain a worksheet.");
                }

                var orders = new List<Order>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    try
                    {
                        var order = new Order
                        {
                            Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                            OrderDate = DateTime.Parse(worksheet.Cells[row, 2].Value.ToString()),
                            CustomerId = int.Parse(worksheet.Cells[row, 3].Value.ToString()),
                            ShipperId = int.Parse(worksheet.Cells[row, 4].Value.ToString()),
                        };

                        orders.Add(order);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException($"Error parsing data in row {row} of '{excelFilePath}': {ex.Message}", ex);
                    }
                }

                return orders;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading data from '{excelFilePath}': {ex.Message}", ex);
            }
        }

        private void CreateOrderDataFile(string filePath)
        {
            try
            {
                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Orders");

                // Add headers
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "OrderDate";
                worksheet.Cells["C1"].Value = "CustomerId";
                worksheet.Cells["D1"].Value = "ShipperId";

                // Example data
                worksheet.Cells["A2"].Value = 1;
                worksheet.Cells["B2"].Value = DateTime.Now;
                worksheet.Cells["C2"].Value = 101;
                worksheet.Cells["D2"].Value = 201;

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