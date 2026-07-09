using Booksy.Models.Enums;

namespace Booksy.Features.Orders.DTOs
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public TransactionStatus TransactionStatus { get; set; }
        public string? TransactionId { get; set; }
        public string? SessionId { get; set; }
        public List<OrderItemResponse> OrderItems { get; set; } = new List<OrderItemResponse>();
        public decimal TotalPrice => OrderItems.Sum(i => i.Price * i.Quantity);
    }
}
