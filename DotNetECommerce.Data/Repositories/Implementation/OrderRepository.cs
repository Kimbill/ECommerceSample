using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class OrderRepository
    {
        private readonly string excelfilePath;

        public OrderRepository(string filePath)
        {
            excelfilePath = filePath;
        }

        public List<Order> GetAllOrders()
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            var orders = new List<Order>();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
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

            return orders;
        }
    }
}
