using AutoMapper;
using Booksy.Common.Services;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Authors.Commands;

/// <summary>
/// Handler for creating a new author
/// </summary>
public class CreateAuthorCommandHandler : ICommandHandler<CreateAuthorCommand, AuthorResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ISlugService _slugService;
    private readonly IMapper _mapper;
    private readonly ILogger<CreateAuthorCommandHandler> _logger;

    public CreateAuthorCommandHandler(
        IUnitOfWork unitOfWork,
        ISlugService slugService,
        IMapper mapper,
        ILogger<CreateAuthorCommandHandler> logger)
    {
        _unitOfWork = unitOfWork;
        _slugService = slugService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<AuthorResponse> Handle(
        CreateAuthorCommand request,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation("Creating new author with name: {AuthorName}", request.Name);

        // Validate input
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Length < 3 || request.Name.Length > 100)
        {
            throw new BusinessException("Author name must be between 3 and 100 characters");
        }

        // Check if author with same name already exists
        var existingAuthor = await _unitOfWork.Authors.GetOneAsync(
            a => a.Name.Equals(request.Name, StringComparison.OrdinalIgnoreCase));
        
        if (existingAuthor is not null)
        {
            _logger.LogWarning("Author with name '{AuthorName}' already exists", request.Name);
            throw new ConflictException($"An author with name '{request.Name}' already exists");
        }

        // Generate unique slug using ISlugService
        var slug = await _slugService.GenerateUniqueSlugAsync(
            _unitOfWork,
            request.Slug ?? request.Name,
            typeof(Author),
            cancellationToken: cancellationToken);

        // Create new author entity
        var author = new Author 
        { 
            Name = request.Name,
            Slug = slug,
            Bio = request.Bio
        };

        // Add to repository and save through UnitOfWork
        var createdAuthor = await _unitOfWork.Authors.AddAsync(author);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Author created successfully with ID: {AuthorId}, Slug: {Slug}", author.Id, author.Slug);

        // Map and return response
        return _mapper.Map<AuthorResponse>(createdAuthor);
    }
}
