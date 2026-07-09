using System;

namespace Booksy.Models.Entities.Common
{
    /// <summary>
    /// Base entity with GUID primary key for distributed systems and uniqueness
    /// </summary>
    public abstract class BaseEntity
    {
        /// <summary>
        /// Primary key using GUID for global uniqueness
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Timestamp when entity was created
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Timestamp when entity was last updated
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
