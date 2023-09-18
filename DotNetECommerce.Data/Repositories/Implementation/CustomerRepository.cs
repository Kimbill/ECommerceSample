using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class CustomerRepository : ICustomerRepository
{
    private readonly string excelFilePath;

    public CustomerRepository(string filePath)
    {
        excelFilePath = filePath;

        // Check if the file exists; if not, create it.
        if (!File.Exists(excelFilePath))
        {
            CreateCustomerDataFile(excelFilePath);
        }
    }

    public List<Customer> GetAllCustomers()
    {
        try
        {
            using var package = new ExcelPackage(new FileInfo(excelFilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException($"Excel file '{excelFilePath}' does not contain a worksheet.");
            }

            var customers = new List<Customer>();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                try
                {
                    var customer = new Customer
                    {
                        Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        CustomerName = worksheet.Cells[row, 2].Value.ToString(),
                        Location = worksheet.Cells[row, 3].Value.ToString(),
                    };

                    customers.Add(customer);
                }
                catch (FormatException ex)
                {
                    throw new FormatException($"Error parsing data in row {row} of '{excelFilePath}': {ex.Message}", ex);
                }
            }

            return customers;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error reading data from '{excelFilePath}': {ex.Message}", ex);
        }
    }

    public void AddCustomer(Customer customer)
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

            worksheet.Cells[nextRow, 1].Value = customer.Id;
            worksheet.Cells[nextRow, 2].Value = customer.CustomerName;
            worksheet.Cells[nextRow, 3].Value = customer.Location;

            package.Save();
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error adding customer data to '{excelFilePath}': {ex.Message}", ex);
        }
    }

    private void CreateCustomerDataFile(string filePath)
    {
        try
        {
            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Customers");

            // Add headers
            worksheet.Cells["A1"].Value = "Id";
            worksheet.Cells["B1"].Value = "CustomerName";
            worksheet.Cells["C1"].Value = "Location";

            // Example data
            worksheet.Cells["A2"].Value = 1;
            worksheet.Cells["B2"].Value = "Example Customer";
            worksheet.Cells["C2"].Value = "Example Location";

            // Save the Excel package to the specified file path
            package.SaveAs(new FileInfo(filePath));
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Error creating Excel file '{filePath}': {ex.Message}", ex);
        }
    }
}
