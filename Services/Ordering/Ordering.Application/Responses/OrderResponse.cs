namespace Ordering.Application.Responses
{
    public class OrderResponse
    {
        public string Id { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Message { get; set; }= string.Empty;
    }
}
