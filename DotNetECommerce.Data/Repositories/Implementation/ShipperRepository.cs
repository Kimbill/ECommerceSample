using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DotNetECommerce.Data.Repositories.Implementation
{
    public class ShipperRepository : IShipperRepository
    {
        private readonly string excelFilePath;

        public ShipperRepository(string filePath)
        {
            excelFilePath = filePath;

            // Check if the file exists
            if (!File.Exists(excelFilePath))
            {
                throw new FileNotFoundException($"Excel file not found at: {excelFilePath}");
            }
        }

        public List<Shipper> GetAllShippers()
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException("Excel file does not contain a worksheet.");
                }

                var shippers = new List<Shipper>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    var shipper = new Shipper
                    {
                        Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                        ShipperName = worksheet.Cells[row, 2].Value.ToString(),
                        PhoneNumber = worksheet.Cells[row, 3].Value.ToString(),
                    };

                    shippers.Add(shipper);
                }

                return shippers;
            }
            catch (FileNotFoundException ex)
            {
                throw new FileNotFoundException($"Error accessing data from '{excelFilePath}': {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error fetching shippers data: {ex.Message}", ex);
            }
        }

        // ...
    }
}
