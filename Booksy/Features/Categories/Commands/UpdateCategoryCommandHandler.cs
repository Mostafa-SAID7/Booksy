using Booksy.Common.Services;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Categories.Commands;

/// <summary>
/// Handler for updating an existing category
/// </summary>
public class UpdateCategoryCommandHandler : ICommandHandler<UpdateCategoryCommand>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISlugService _slugService;
    private readonly ILogger<UpdateCategoryCommandHandler> _logger;

    public UpdateCategoryCommandHandler(
        IUnitOfWork unitOfWork,
        ISlugService slugService,
        ILogger<UpdateCategoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _slugService = slugService;
        _logger = logger;
    }

    public async Task Handle(
        UpdateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating category with ID: {CategoryId}, new name: {CategoryName}", request.Id, request.Name);

        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3 || request.Name.Length > 100)
        {
            throw new BusinessException("Category name must be between 3 and 100 characters");
        }

        // Get existing category
        var category = await _unitOfWork.Categories.GetByIdAsync(request.Id);
        if (category is null)
        {
            _logger.LogWarning("Category not found with ID: {CategoryId}", request.Id);
            throw new NotFoundException("Category", request.Id);
        }

        // Check if another category with the same name exists - use GetOneAsync instead of GetAllAsync
        var existingCategory = await _unitOfWork.Categories.GetOneAsync(c => 
            c.Id != request.Id && 
            c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
        
        if (existingCategory is not null)
        {
            _logger.LogWarning("Category with name '{CategoryName}' already exists", request.Name);
            throw new ConflictException($"A category with name '{request.Name}' already exists");
        }

        // Update the category
        category.Name = request.Name;

        // Update or regenerate slug using ISlugService
        var newSlug = await _slugService.GenerateUniqueSlugAsync(
            _unitOfWork,
            request.Slug ?? request.Name,
            typeof(Category),
            request.Id,
            cancellationToken);

        category.Slug = newSlug;

        _unitOfWork.Categories.Update(category);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Category updated successfully with ID: {CategoryId}, new slug: {Slug}", request.Id, category.Slug);
    }
}
