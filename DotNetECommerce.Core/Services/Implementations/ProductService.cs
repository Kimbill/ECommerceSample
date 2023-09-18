using DotNetECommerce.Core.Services.Interfaces;
using DotNetECommerce.Data.Repositories.Interfaces;
using DotNetECommerce.Model;
using System;
using System.Collections.Generic;
using System.IO;

namespace DotNetECommerce.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public List<Product> GetAllProducts()
        {
            try
            {
                // Retrieve all products from the repository
                var products = _productRepository.GetAllProducts();

                // You can add additional business logic here if needed.

                return products;
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
                throw new Exception($"Error in ProductService: {ex.Message}", ex);
            }
        }

        public void AddProduct(Product product)
        {
            try
            {
                // Add product to the repository
                _productRepository.AddProduct(product);

                // You can add additional business logic here if needed.
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
                throw new Exception($"Error in ProductService: {ex.Message}", ex);
            }
        }
    }
}
