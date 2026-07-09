using AutoMapper;
using Booksy.Common.Services;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Categories.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Categories.Commands;

/// <summary>
/// Handler for creating a new category
/// </summary>
public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CategoryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISlugService _slugService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateCategoryCommandHandler> _logger;

    public CreateCategoryCommandHandler(
        IUnitOfWork unitOfWork,
        ISlugService slugService,
        IMapper mapper,
        ILogger<CreateCategoryCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _slugService = slugService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<CategoryResponse> Handle(
        CreateCategoryCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new category with name: {CategoryName}", request.Name);

        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3 || request.Name.Length > 100)
        {
            throw new BusinessException("Category name must be between 3 and 100 characters");
        }

        // Check if category with same name already exists
        var existingCategory = await _unitOfWork.Categories.GetOneAsync(
            c => c.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
        
        if (existingCategory is not null)
        {
            _logger.LogWarning("Category with name '{CategoryName}' already exists", request.Name);
            throw new ConflictException($"A category with name '{request.Name}' already exists");
        }

        // Generate unique slug using ISlugService
        var slug = await _slugService.GenerateUniqueSlugAsync(
            _unitOfWork,
            request.Slug ?? request.Name,
            typeof(Category),
            cancellationToken: cancellationToken);

        // Create new category entity
        var category = new Category 
        { 
            Name = request.Name,
            Slug = slug
        };

        // Add to repository
        await _unitOfWork.Categories.AddAsync(category);
        
        // Save through UnitOfWork for centralized transaction management
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Category created successfully with ID: {CategoryId}, Slug: {Slug}", category.Id, category.Slug);

        // Map and return response
        return _mapper.Map<CategoryResponse>(category);
    }
}
