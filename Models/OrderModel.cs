namespace RESTwebAPI.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        public string OrderName { get; set; }
        public decimal TotalAmount { get; set; }
    }
}