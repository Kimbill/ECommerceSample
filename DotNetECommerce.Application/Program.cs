using DotNetECommerce.Data.Repositories.Implementation;
using DotNetECommerce.Model;
using DotNetECommerce.Services.Implementation;
using System;
using System.Collections.Generic;

namespace DotNetECommerce.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create instances of repositories and services manually.
            var categoryRepository = new CategoryRepository("CategoryFile.xlsx");
            var categoryService = new CategoryService(categoryRepository);

            var orderRepository = new OrderRepository("OrderFile.xlsx");
            var orderService = new OrderService(orderRepository);

            var customerRepository = new CustomerRepository("CustomerFile.xlsx");
            var customerService = new CustomerService(customerRepository, orderRepository);

            var salesReportRepository = new SalesReportRepository(orderRepository, null, null, "Sales_path.xlsx");
            var salesReportService = new SalesReportService(salesReportRepository);

            var productRepository = new ProductRepository("ProductFile.xlsx");
            var productService = new ProductService(productRepository);

            // Example: Calling methods from the services.
            var categories = categoryService.GetAllCategories();
            var customers = customerService.GetAllCustomers();
            var orders = orderService.GetAllOrders();

            var salesReports = salesReportService.GenerateSalesReports();
            var top5Deals = salesReportService.GetTop5Deals();

            var products = productService.GetAllProducts();

            // Example: Displaying results.
            Console.WriteLine("Categories:");
            foreach (var category in categories)
            {
                Console.WriteLine($"{category.Id}: {category.Name}");
            }

            Console.WriteLine("\nCustomers:");
            foreach (var customer in customers)
            {
                Console.WriteLine($"{customer.Id}: {customer.CustomerName}");
            }

            Console.WriteLine("\nOrders:");
            foreach (var order in orders)
            {
                Console.WriteLine($"{order.Id}: {order.OrderDate}");
            }

            Console.WriteLine("\nSales Reports:");
            foreach (var report in salesReports)
            {
                Console.WriteLine("Sales Report:");
                foreach (var entry in report)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nTop 5 Deals:");
            foreach (var deal in top5Deals)
            {
                Console.WriteLine("Top 5 Deal:");
                foreach (var entry in deal)
                {
                    Console.WriteLine($"{entry.Key}: {entry.Value}");
                }
                Console.WriteLine();
            }

            Console.WriteLine("\nProducts:");
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}: {product.Name}");
            }
        }
    }
}
