namespace Booksy.Models.DTOs.Response.Reviews
{
    public class ReviewResponse
    {
        public int Id { get; set; }
        public string ReviewerName { get; set; } = string.Empty;
        public string Comment { get; set; } = string.Empty;
        public int Rating { get; set; }
    }
}
