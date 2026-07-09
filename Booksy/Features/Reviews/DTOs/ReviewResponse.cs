namespace Booksy.Features.Reviews.DTOs
{
    public class ReviewResponse
    {
        public Guid Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
        public int Status { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
}
