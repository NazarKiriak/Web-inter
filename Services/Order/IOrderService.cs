using System.Collections.Generic;
using System.Threading.Tasks;
using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public interface IOrderService
    {
        Task<ResponseModel<Order>> GetOrderAsync(int id);
        Task<ResponseModel<IEnumerable<Order>>> GetAllOrdersAsync();
        Task<ResponseModel<Order>> AddOrderAsync(Order order);
        Task<ResponseModel<Order>> UpdateOrderAsync(int id, Order order);
        Task<ResponseModel<Order>> DeleteOrderAsync(int id);
    }
}
