using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class CustomerRepository
    {
        private readonly string excelfilePath;

        public CustomerRepository(string filePath)
        {
            excelfilePath = filePath;
        }

        public List<Customer> GetAllCustomers()
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
        }
    }
}
