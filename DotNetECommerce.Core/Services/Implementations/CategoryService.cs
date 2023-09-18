using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;

namespace DotNetECommerce.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public List<Category> GetAllCategories()
        {
            try
            {
                var allCategories = _categoryRepository.GetAllCategories();

                // You can add business logic here, for example:
                // Filter categories, apply some transformations, or perform validation.
                var filteredCategories = allCategories.Where(category => category.Description
                .Contains("YourKeywordHere")).ToList();

                return filteredCategories;
            }
            catch (FileNotFoundException ex)
            {
                // Handle file not found exception.
                throw new Exception($"File not found: {ex.FileName}", ex);
            }
            catch (InvalidOperationException ex)
            {
                // Handle invalid operation exception (e.g., missing worksheet).
                throw new Exception($"Invalid operation: {ex.Message}", ex);
            }
            catch (FormatException ex)
            {
                // Handle format exception (e.g., parsing errors).
                throw new Exception($"Data format error: {ex.Message}", ex);
            }
            catch (Exception ex)
            {
                // Handle other exceptions or log errors as needed.
                throw new Exception($"Error in CategoryService: {ex.Message}", ex);
            }
        }
    }
}
