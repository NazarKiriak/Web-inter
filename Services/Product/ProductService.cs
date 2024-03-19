using System.Collections.Generic;
using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly List<Product> _products;

        public ProductService()
        {
            _products = new List<Product>
            {
                new Product { Id = 1, Name = "Холодильник LG 536", Price = 35000 },
                new Product { Id = 2, Name = "Ноутбук Aser Aspeire 5", Price = 17890 },
                new Product { Id = 3, Name = "Ігровий монітор Samsung A535", Price = 15000 },
                new Product { Id = 4, Name = "Чохол iPhone 14 Pro", Price = 20 },
                new Product { Id = 5, Name = "Комп'ютер MSI AD4536", Price = 55000 },
                new Product { Id = 6, Name = "Навушники Logitech 43", Price = 3000 },
                new Product { Id = 7, Name = "Колонка JBL 4", Price = 4000 },
                new Product { Id = 8, Name = "Флешка Samsung 512Gb", Price = 500 },
                new Product { Id = 9, Name = "Квадракоптер DJI Mavic 3 Pro", Price = 60000 },
                new Product { Id = 10, Name = "Ліцензія Windows 10 Pro", Price = 4000 },
            };
        }
        public async Task<ResponseModel<Product>> AddProductAsync(Product product)
        {
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
            return new ResponseModel<Product>
            {
                Data = product,
                Success = true,
                Message = "Product added successfully."
            };
        }

        public async Task<ResponseModel<Product>> DeleteProductAsync(int id)
        {
            var productToRemove = _products.FirstOrDefault(p => p.Id == id);
            if (productToRemove == null)
            {
                return new ResponseModel<Product>
                {
                    Data = null,
                    Success = false,
                    Message = $"Product with id {id} not found."
                };
            }
            _products.Remove(productToRemove);
            return new ResponseModel<Product>
            {
                Data = productToRemove,
                Success = true,
                Message = $"Product with id {id} deleted successfully."
            };
        }

        public async Task<ResponseModel<IEnumerable<Product>>> GetAllProductsAsync()
        {
            return new ResponseModel<IEnumerable<Product>>
            {
                Data = _products,
                Success = true,
                Message = "Successfully retrieved all products."
            };
        }

        public async Task<ResponseModel<Product>> GetProductAsync(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return new ResponseModel<Product>
                {
                    Data = null,
                    Success = false,
                    Message = $"Product with id {id} not found."
                };
            }
            return new ResponseModel<Product>
            {
                Data = product,
                Success = true,
                Message = $"Successfully retrieved product with id {id}."
            };
        }

        public async Task<ResponseModel<Product>> UpdateProductAsync(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
            {
                return new ResponseModel<Product>
                {
                    Data = null,
                    Success = false,
                    Message = $"Product with id {id} not found."
                };
            }
            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return new ResponseModel<Product>
            {
                Data = existingProduct,
                Success = true,
                Message = $"Product with id {id} updated successfully."
            };
        }
    }
}
