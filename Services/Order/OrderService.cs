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
                new Order { OrderId = 1, OrderName = "Смартфон Samsung Galaxy S21", TotalAmount = 50 },
                new Order { OrderId = 2, OrderName = "Ноутбук HP Pavilion 15", TotalAmount = 100 },
                new Order { OrderId = 3, OrderName = "Телевізор LG OLED55CX", TotalAmount = 150 },
                new Order { OrderId = 4, OrderName = "Пилосос Xiaomi Mi Robot Vacuum", TotalAmount = 200 },
                new Order { OrderId = 5, OrderName = "Ігрова консоль Sony PlayStation 5", TotalAmount = 250 },
                new Order { OrderId = 6, OrderName = "Холодильник Bosch KGN39VL35", TotalAmount = 300 },
                new Order { OrderId = 7, OrderName = "Спортивний годинник Garmin Forerunner 945", TotalAmount = 400 },
                new Order { OrderId = 8, OrderName = "Книга \"Майстер та Маргарита\" Михаїла Булгакова", TotalAmount = 500 },
                new Order { OrderId = 9, OrderName = "Мультиварка REDMOND SkyCooker", TotalAmount = 600 },
                new Order { OrderId = 10, OrderName = "Блендер Philips Daily Collection", TotalAmount = 1000 },
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
