using System.Collections.Generic;
using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public interface IProductService
    {
        Task<ResponseModel<Product>> GetProductAsync(int id);
        Task<ResponseModel<IEnumerable<Product>>> GetAllProductsAsync();
        Task<ResponseModel<Product>> AddProductAsync(Product product);
        Task<ResponseModel<Product>> UpdateProductAsync(int id, Product product);
        Task<ResponseModel<Product>> DeleteProductAsync(int id);
    }
}
