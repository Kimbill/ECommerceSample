using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class OrderDetailRepository
    {
        private readonly string excelfilePath;

        public OrderDetailRepository(string filePath)
        {
            excelfilePath = filePath;
        }

        public List<OrderDetail> GetAllOrderDetails()
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            var orderDetails = new List<OrderDetail>();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
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

            return orderDetails;
        }
    }
}
