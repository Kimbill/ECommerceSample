using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class ProductRepository : IProductRepository
    {
        private readonly string excelFilePath;

        public ProductRepository(string filePath)
        {
            excelFilePath = filePath;

            // Check if the file exists; if not, create it.
            if (!File.Exists(excelFilePath))
            {
                CreateProductDataFile(excelFilePath);
            }
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException($"Excel file '{excelFilePath}' does not contain a worksheet.");
                }

                var products = new List<Product>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    try
                    {
                        var product = new Product
                        {
                            Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                            Name = worksheet.Cells[row, 2].Value.ToString(),
                            UnitPrice = decimal.Parse(worksheet.Cells[row, 3].Value.ToString()),
                            CategoryId = int.Parse(worksheet.Cells[row, 4].Value.ToString()),
                            SupplierId = int.Parse(worksheet.Cells[row, 5].Value.ToString()),
                        };

                        products.Add(product);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException($"Error parsing data in row {row} of '{excelFilePath}': {ex.Message}", ex);
                    }
                }

                return products;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading data from '{excelFilePath}': {ex.Message}", ex);
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException($"Excel file '{excelFilePath}' does not contain a worksheet.");
                }

                int nextRow = worksheet.Dimension.End.Row + 1;

                worksheet.Cells[nextRow, 1].Value = product.Id;
                worksheet.Cells[nextRow, 2].Value = product.Name;
                worksheet.Cells[nextRow, 3].Value = product.UnitPrice;
                worksheet.Cells[nextRow, 4].Value = product.CategoryId;
                worksheet.Cells[nextRow, 5].Value = product.SupplierId;

                package.Save();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error writing data to '{excelFilePath}': {ex.Message}", ex);
            }
        }

        private void CreateProductDataFile(string filePath)
        {
            try
            {
                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Products");

                // Add headers
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Name";
                worksheet.Cells["C1"].Value = "UnitPrice";
                worksheet.Cells["D1"].Value = "CategoryId";
                worksheet.Cells["E1"].Value = "SupplierId";

                // Example data
                worksheet.Cells["A2"].Value = 1;
                worksheet.Cells["B2"].Value = "Product 1";
                worksheet.Cells["C2"].Value = 10.99;
                worksheet.Cells["D2"].Value = 101;
                worksheet.Cells["E2"].Value = 201;

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
