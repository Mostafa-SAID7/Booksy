using System;
using System.ComponentModel.DataAnnotations;
using Booksy.Models.Entities.Common;
using Booksy.Models.Entities.Users;
using Booksy.Models.Entities.Books;
using Booksy.Models.Enums;

namespace Booksy.Models.Entities.Books
{
    public class Review : BaseEntity, IAuditableEntity, ISoftDeletable
    {
        [Required]
        public int BookId { get; set; }
        public Book Book { get; set; } = null!;

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; } = null!;

        [Required]
        [Range(1, 5)]
        public int Rating { get; set; }  // e.g., 1-5 stars

        [MaxLength(1000)]
        public string? Comment { get; set; }

        public bool IsDeleted { get; set; } = false;

        // Optional: Review status, e.g., Pending, Approved, Rejected
        public ReviewStatus Status { get; set; } = ReviewStatus.Pending;
        public string? ReviewerName { get;  set; }
    }
}
