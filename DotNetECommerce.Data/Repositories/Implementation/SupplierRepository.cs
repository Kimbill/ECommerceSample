using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly string excelFilePath;

        public SupplierRepository(string filePath)
        {
            excelFilePath = filePath;

            // Check if the file exists
            if (!File.Exists(excelFilePath))
            {
                throw new FileNotFoundException($"Excel file not found at: {excelFilePath}");
            }
        }

        public List<Supplier> GetAllSuppliers()
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException("Excel file does not contain a worksheet.");
                }

                var suppliers = new List<Supplier>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var supplier = new Supplier
                    {
                        Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        CompanyName = worksheet.Cells[row, 2].Value.ToString(),
                        ContactName = worksheet.Cells[row, 3].Value.ToString(),
                        // Add other properties based on your Excel columns
                    };

                    suppliers.Add(supplier);
                }

                return suppliers;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Error accessing data from '{excelFilePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching suppliers data: {ex.Message}", ex);
            }
        }

        // ...
    }
}
