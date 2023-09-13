using DotNetECommerce.Core.Services.Implementations;
using DotNetECommerce.Data.Repositories.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DotNetECommerce.Application
{
    public class Program
    {
        static void Main(string[] args)
        {
            var customerRepository = new CustomerRepository();
            var orderRepository = new OrderRepository();
            var orderDetailRepository = new OrderDetailRepository();
            var productRepository = new ProductRepository();
            var salesReportService = new SalesReportService();
            var customerService = new CustomerService();


            var salesReports = salesReportService.GenerateSalesReports();

            var top5Reports = salesReports.Take(5);
            foreach (var report in top5Reports)
            {
                Console.WriteLine($"Sales Report:");
                foreach (var entry in report.Entries)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }
                Console.WriteLine();
            }

            var top5Customers = customerService.GetCustomersGroupByCity();

            var top5CustomersCount = top5Customers.Count();
            foreach (var customer in top5Customers)
            {
                Console.WriteLine();
            }



        }
    }
}
