namespace Ordering.Application.Responses
{
    public class OrderResponse
    {
        public Guid Id { get; set; } 
        public string UserId { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }
}
