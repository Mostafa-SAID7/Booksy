using Booksy.Common.Services;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;
using MediatR;

namespace Booksy.Features.Authors.Commands;

/// <summary>
/// Handler for updating an author
/// </summary>
public class UpdateAuthorCommandHandler : ICommandHandler<UpdateAuthorCommand, Unit>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISlugService _slugService;
    private readonly ILogger<UpdateAuthorCommandHandler> _logger;

    public UpdateAuthorCommandHandler(
        IUnitOfWork unitOfWork,
        ISlugService slugService,
        ILogger<UpdateAuthorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _slugService = slugService;
        _logger = logger;
    }

    public async Task<Unit> Handle(
        UpdateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Updating author with ID: {AuthorId}, new name: {AuthorName}", request.Id, request.Name);

        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3 || request.Name.Length > 100)
        {
            throw new BusinessException("Author name must be between 3 and 100 characters");
        }

        // Get existing author
        var author = await _unitOfWork.Authors.GetByIdAsync(request.Id);
        if (author == null)
        {
            _logger.LogWarning("Author not found with ID: {AuthorId}", request.Id);
            throw new NotFoundException($"Author with ID {request.Id} not found");
        }

        // Check if new name conflicts with another author - use GetOneAsync
        var existingAuthor = await _unitOfWork.Authors.GetOneAsync(a => 
            a.Id != request.Id && 
            a.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
        
        if (existingAuthor is not null)
        {
            _logger.LogWarning("Author with name '{AuthorName}' already exists", request.Name);
            throw new ConflictException($"An author with name '{request.Name}' already exists");
        }

        // Update author
        author.Name = request.Name;
        author.Bio = request.Bio;

        // Update or regenerate slug using ISlugService
        var newSlug = await _slugService.GenerateUniqueSlugAsync(
            _unitOfWork,
            request.Slug ?? request.Name,
            typeof(Author),
            request.Id,
            cancellationToken);

        author.Slug = newSlug;

        // Save changes through UnitOfWork
        _unitOfWork.Authors.Update(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Author updated successfully with ID: {AuthorId}, new slug: {Slug}", request.Id, author.Slug);

        return Unit.Value;
    }
}