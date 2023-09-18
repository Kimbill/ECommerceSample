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
    public class CategoryRepository : ICategoryRepository
    {
        private readonly string excelFilePath;

        public CategoryRepository(string filePath)
        {
            excelFilePath = filePath;

            // Check if the file exists
            if (!File.Exists(excelFilePath))
            {
                throw new FileNotFoundException($"Excel file not found at: {excelFilePath}");
            }
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                using var package = new ExcelPackage(new FileInfo(excelFilePath));
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                {
                    throw new InvalidOperationException("Excel file does not contain a worksheet");
                }

                var categories = new List<Category>();

                for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
                {
                    try
                    {
                        var category = new Category
                        {
                            Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                            Name = worksheet.Cells[row, 2].Value.ToString(),
                            Description = worksheet.Cells[row, 3].Value.ToString(),

                        };

                        categories.Add(category);
                    }
                    catch (FormatException ex)
                    {
                        throw new FormatException($"Error parsing data in row {row} of '{excelFilePath}': {ex.Message}", ex);
                    }
                }

                return categories;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error reading data from '{excelFilePath}': {ex.Message}", ex);
            }

        }

        public void CreateExcelFile()
        {
            try
            {
                using var package = new ExcelPackage();
                var worksheet = package.Workbook.Worksheets.Add("Sheet1");

                // Add headers
                worksheet.Cells["A1"].Value = "Id";
                worksheet.Cells["B1"].Value = "Name";
                worksheet.Cells["C1"].Value = "Description";

                // Example data
                worksheet.Cells["A2"].Value = 1;
                worksheet.Cells["B2"].Value = "Category 1";
                worksheet.Cells["C2"].Value = "Description 1";

                // Save the Excel package to the specified file path
                package.SaveAs(new FileInfo(excelFilePath));
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Error creating Excel file '{excelFilePath}': {ex.Message}", ex);
            }
        }
    }
}
