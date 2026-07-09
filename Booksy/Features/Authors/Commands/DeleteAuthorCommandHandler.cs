using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Authors.Commands;

/// <summary>
/// Handler for deleting an author
/// </summary>
public class DeleteAuthorCommandHandler : ICommandHandler<DeleteAuthorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteAuthorCommandHandler> _logger;

    public DeleteAuthorCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteAuthorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        DeleteAuthorCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete author with ID: {AuthorId}", request.Id);

        // Get existing author
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
        if (author == null)
        {
            _logger.LogWarning("Author not found with ID: {AuthorId}", request.Id);
            throw new NotFoundException($"Author with ID {request.Id} not found");
        }

        // Check if author has associated books
        var relatedBooks = await _unitOfWork.Books.GetAsync(b => b.AuthorId == request.Id);
        if (relatedBooks.Any())
        {
            _logger.LogWarning(
                "Cannot delete author {AuthorId} - they have {BookCount} associated books",
                request.Id,
                relatedBooks.Count());
            throw new BusinessException(
                $"Cannot delete author with ID {request.Id} because they have {relatedBooks.Count()} associated books. Please remove or reassign the books first.");
        }

        // Delete author through UnitOfWork
        _unitOfWork.Authors.Delete(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Author deleted successfully with ID: {AuthorId}", request.Id);

        return Unit.Value;
    }
}