using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly string excelfilePath;

        public CustomerRepository(string filePath)
        {
            excelfilePath = filePath;
        }

        public List<Customer> GetAllCustomers()
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            var customers = new List<Customer>();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var customer = new Customer
                {
                    Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                    CustomerName = worksheet.Cells[row, 2].Value.ToString(),
                    Location = worksheet.Cells[row, 3].Value.ToString(),

                };

                customers.Add(customer);

            }

            return customers;
        }

        public void AddCustomer(Customer customer)
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            int nextRow = worksheet.Dimension.End.Row + 1;

            worksheet.Cells[nextRow, 1].Value = customer.Id;
            worksheet.Cells[nextRow, 2].Value = customer.CustomerName;
            worksheet.Cells[nextRow, 3].Value = customer.Location;

            package.Save();
        }
    }
}
