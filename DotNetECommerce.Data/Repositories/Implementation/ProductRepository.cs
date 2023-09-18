using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class ProductRepository
    {
        private readonly string excelfilePath;

        public ProductRepository(string filePath)
        {
            excelfilePath = filePath;
        }

        public List<Product> GetAllProducts()
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            var products = new List<Product>();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
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

            return products;
        }

        public void AddProduct(Product product)
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            int nextRow = worksheet.Dimension.End.Row + 1;

            worksheet.Cells[nextRow, 1].Value = product.Id;
            worksheet.Cells[nextRow, 2].Value = product.SupplierId;
            worksheet.Cells[nextRow, 3].Value = product.CategoryId;

            package.Save();
        }
    }
}
