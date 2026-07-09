using System.ComponentModel.DataAnnotations;

namespace Booksy.Features.Tags.DTOs
{
    /// <summary>
    /// DTO for updating a tag
    /// </summary>
    public class TagUpdateRequest
    {
        /// <summary>
        /// Tag name (required, 1-100 characters)
        /// </summary>
        [Required(ErrorMessage = "Tag name is required")]
        [MaxLength(100, ErrorMessage = "Tag name cannot exceed 100 characters")]
        [MinLength(1, ErrorMessage = "Tag name cannot be empty")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL-friendly slug (optional - can be auto-generated)
        /// </summary>
        [MaxLength(120, ErrorMessage = "Tag slug cannot exceed 120 characters")]
        public string? Slug { get; set; }

        /// <summary>
        /// Tag description (optional)
        /// </summary>
        [MaxLength(500, ErrorMessage = "Tag description cannot exceed 500 characters")]
        public string? Description { get; set; }
    }
}
