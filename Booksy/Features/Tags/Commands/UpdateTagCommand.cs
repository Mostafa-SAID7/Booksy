using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Commands
{
    /// <summary>
    /// Command for updating an existing tag
    /// </summary>
    public class UpdateTagCommand : ICommand<TagResponse>
    {
        /// <summary>
        /// Tag ID to update
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Updated tag details
        /// </summary>
        public TagUpdateRequest Request { get; set; } = null!;

        public UpdateTagCommand(Guid id, TagUpdateRequest request)
        {
            Id = id;
            Request = request;
        }
    }
}
