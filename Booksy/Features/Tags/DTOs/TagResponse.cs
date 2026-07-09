namespace Booksy.Features.Tags.DTOs
{
    /// <summary>
    /// DTO for tag response in API
    /// </summary>
    public class TagResponse
    {
        /// <summary>
        /// Tag ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Tag name
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL-friendly slug
        /// </summary>
        public string Slug { get; set; } = string.Empty;

        /// <summary>
        /// Tag description
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// Number of books associated with this tag
        /// </summary>
        public int BookCount { get; set; }

        /// <summary>
        /// Created date
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Updated date (nullable)
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
