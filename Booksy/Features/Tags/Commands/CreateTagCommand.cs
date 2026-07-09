using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;

namespace Booksy.Features.Tags.Commands
{
    /// <summary>
    /// Command for creating a new tag
    /// </summary>
    public class CreateTagCommand : ICommand<TagResponse>
    {
        /// <summary>
        /// Tag creation details
        /// </summary>
        public TagCreateRequest Request { get; set; } = null!;

        public CreateTagCommand(TagCreateRequest request)
        {
            Request = request;
        }
    }
}
