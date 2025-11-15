using Ordering.Core.Common;

namespace Ordering.Core.Entities
{
    public class Order : EntityBase
    {
        public string UserId { get; set; } = string.Empty;
        public string Product { get; set; } = string.Empty;
        public int Quantity{ get; set; }
        public decimal Price { get; set; }
    }
}
