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
        private readonly string excelfilePath;

        public CategoryRepository(string filePath)
        {
            excelfilePath = filePath;
        }

        public List<Category> GetAllCategoriess()
        {
            using var package = new ExcelPackage(new FileInfo(excelfilePath));
            var worksheet = package.Workbook.Worksheets.FirstOrDefault();

            if (worksheet == null)
            {
                throw new InvalidOperationException("Excel file does not contain a worksheet");
            }

            var categories = new List<Category>();

            for (int row = 2; row <= worksheet.Dimension.End.Row; row++)
            {
                var category = new Category
                {
                    Id = int.Parse(worksheet.Cells[row, 1].Value.ToString()),
                    Name = worksheet.Cells[row, 2].Value.ToString(),
                    Description = worksheet.Cells[row, 3].Value.ToString(),

                };

                categories.Add(category);

            }

            return categories;
        }
    }
}
