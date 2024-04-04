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
                new Product { Id = 1, Name = "Смартфон Samsung Galaxy S21", Price = 5 },
                new Product { Id = 2, Name = "Ноутбук HP Pavilion 15", Price = 10 },
                new Product { Id = 3, Name = "Телевізор LG OLED55CX", Price = 15 },
                new Product { Id = 4, Name = "Пилосос Xiaomi Mi Robot Vacuum", Price = 20 },
                new Product { Id = 5, Name = "Ігрова консоль Sony PlayStation 5", Price = 25 },
                new Product { Id = 6, Name = "Холодильник Bosch KGN39VL35", Price = 30 },
                new Product { Id = 7, Name = "Спортивний годинник Garmin Forerunner 945", Price = 40 },
                new Product { Id = 8, Name = "Книга \"Майстер та Маргарита\" Михаїла Булгакова", Price = 50 },
                new Product { Id = 9, Name = "Мультиварка REDMOND SkyCooker", Price = 60 },
                new Product { Id = 10, Name = "Блендер Philips Daily Collection", Price = 100 },

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
