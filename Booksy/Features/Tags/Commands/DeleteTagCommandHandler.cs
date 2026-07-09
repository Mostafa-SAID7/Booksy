using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Tags.Commands
{
    /// <summary>
    /// Handler for deleting a tag
    /// </summary>
    public class DeleteTagCommandHandler : ICommandHandler<DeleteTagCommand, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<DeleteTagCommandHandler> _logger;

        public DeleteTagCommandHandler(
            IUnitOfWork unitOfWork,
            ILogger<DeleteTagCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<bool> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Attempting to delete tag with ID: {TagId}", request.Id);

            // Get the tag to delete
            var tag = await _unitOfWork.Tags.GetByIdAsync(request.Id);

            if (tag == null)
            {
                _logger.LogWarning("Tag not found with ID: {TagId}", request.Id);
                throw new NotFoundException($"Tag with ID {request.Id} not found");
            }

            // Check if tag has associated books
            var relatedBooks = await _unitOfWork.Books.GetAsync(
                b => b.Tags != null && b.Tags.Any(t => t.Id == request.Id));

            if (relatedBooks.Any())
            {
                _logger.LogWarning(
                    "Cannot delete tag {TagId} - it has {BookCount} associated books",
                    request.Id,
                    relatedBooks.Count());
                throw new BusinessException(
                    $"Cannot delete tag with ID {request.Id} because it has {relatedBooks.Count()} associated books. Please remove or reassign the books first.");
            }

            // Soft delete the tag
            tag.IsDeleted = true;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Tag deleted successfully with ID: {TagId}", request.Id);

            return true;
        }
    }
}
