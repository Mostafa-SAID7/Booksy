using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Categories.Commands;

/// <summary>
/// Handler for deleting a category
/// </summary>
public class DeleteCategoryCommandHandler : ICommandHandler<DeleteCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger<DeleteCategoryCommandHandler> _logger;

    public DeleteCategoryCommandHandler(
        IUnitOfWork unitOfWork,
        ILogger<DeleteCategoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task Handle(
        DeleteCategoryCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Attempting to delete category with ID: {CategoryId}", request.Id);

        // Get existing category
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category is null)
        {
            _logger.LogWarning("Category not found with ID: {CategoryId}", request.Id);
            throw new NotFoundException("Category", request.Id);
        }

        // Check if category has associated books
        var relatedBooks = await _unitOfWork.Books.GetAsync(b => b.CategoryId == request.Id);
        if (relatedBooks.Any())
        {
            _logger.LogWarning(
                "Cannot delete category {CategoryId} - it has {BookCount} associated books",
                request.Id,
                relatedBooks.Count());
            throw new BusinessException(
                "Cannot delete a category that has associated books. Please remove or reassign the books first.");
        }

        // Delete the category
        _unitOfWork.Categories.Delete(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Successfully deleted category with ID: {CategoryId}", request.Id);
    }
}
