using Booksy.Core.Interfaces;

namespace Booksy.Features.Tags.Commands
{
    /// <summary>
    /// Command for deleting a tag
    /// </summary>
    public class DeleteTagCommand : ICommand<bool>
    {
        /// <summary>
        /// Tag ID to delete
        /// </summary>
        public Guid Id { get; set; }

        public DeleteTagCommand(Guid id)
        {
            Id = id;
        }
    }
}
