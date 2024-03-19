using RESTwebAPI.Models;

namespace RESTwebAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly List<Order> _orders;

        public OrderService()
        {
            _orders = new List<Order>
            {
                new Product { Id = 1, Name = "Холодильник LG", Price = 35000 },
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
        public async Task<ResponseModel<Order>> AddOrderAsync(Order order)
        {
            order.OrderId = _orders.Any() ? _orders.Max(p => p.OrderId) + 1 : 1;
            _orders.Add(order);
            return new ResponseModel<Order>
            {
                Data = order,
                Success = true,
                Message = "Order added successfully."
            };
        }

        public async Task<ResponseModel<Order>> DeleteOrderAsync(int id)
        {
            var orderToRemove = _orders.FirstOrDefault(p => p.OrderId == id);
            if (orderToRemove == null)
            {
                return new ResponseModel<Order>
                {
                    Data = null,
                    Success = false,
                    Message = $"Order with id {id} not found."
                };
            }
            _orders.Remove(orderToRemove);
            return new ResponseModel<Order>
            {
                Data = orderToRemove,
                Success = true,
                Message = $"Order with id {id} deleted successfully."
            };
        }

        public async Task<ResponseModel<IEnumerable<Order>>> GetAllOrdersAsync()
        {
            return new ResponseModel<IEnumerable<Order>>
            {
                Data = _orders,
                Success = true,
                Message = "Successfully retrieved all orders."
            };
        }

        public async Task<ResponseModel<Order>> GetOrderAsync(int id)
        {
            var order = _orders.FirstOrDefault(p => p.OrderId == id);
            if (order == null)
            {
                return new ResponseModel<Order>
                {
                    Data = null,
                    Success = false,
                    Message = $"Order with id {id} not found."
                };
            }
            return new ResponseModel<Order>
            {
                Data = order,
                Success = true,
                Message = $"Successfully retrieved order with id {id}."
            };
        }

        public async Task<ResponseModel<Order>> UpdateOrderAsync(int id, Order order)
        {
            var existingOrder = _orders.FirstOrDefault(p => p.OrderId == id);
            if (existingOrder == null)
            {
                return new ResponseModel<Order>
                {
                    Data = null,
                    Success = false,
                    Message = $"Order with id {id} not found."
                };
            }
            existingOrder.OrderName = order.OrderName;
            existingOrder.TotalAmount= order.TotalAmount;
            return new ResponseModel<Order>
            {
                Data = existingOrder,
                Success = true,
                Message = $"Order with id {id} updated successfully."
            };
        }
    }
}
