using AutoMapper;
using Booksy.Common.Services;
using Booksy.Core.Exceptions;
using Booksy.Core.Interfaces;
using Booksy.Features.Tags.DTOs;
using Booksy.Models.Entities.Books;
using Booksy.Repositories.IRepositories;
using Microsoft.Extensions.Logging;

namespace Booksy.Features.Tags.Commands
{
    /// <summary>
    /// Handler for creating a new tag
    /// </summary>
    public class CreateTagCommandHandler : ICommandHandler<CreateTagCommand, TagResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlugService _slugService;
        private readonly IMapper _mapper;
        private readonly ILogger<CreateTagCommandHandler> _logger;

        public CreateTagCommandHandler(
            IUnitOfWork unitOfWork,
            ISlugService slugService,
            IMapper mapper,
            ILogger<CreateTagCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _slugService = slugService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TagResponse> Handle(CreateTagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new tag with name: {TagName}", request.Request.Name);

            // Validate input
            if (string.IsNullOrWhiteSpace(request.Request.Name) || request.Request.Name.Length < 2 || request.Request.Name.Length > 50)
            {
                throw new BusinessException("Tag name must be between 2 and 50 characters");
            }

            // Check if tag with same name already exists
            var existingTag = await _unitOfWork.Tags.GetOneAsync(
                t => t.Name.ToLower() == request.Request.Name.ToLower());

            if (existingTag != null)
            {
                _logger.LogWarning("Tag with name '{TagName}' already exists", request.Request.Name);
                throw new ConflictException("A tag with this name already exists");
            }

            // Generate unique slug using ISlugService
            var slug = await _slugService.GenerateUniqueSlugAsync(
                _unitOfWork,
                request.Request.Slug ?? request.Request.Name,
                typeof(Tag),
                cancellationToken: cancellationToken);

            // Create new tag
            var tag = new Tag
            {
                Name = request.Request.Name,
                Slug = slug,
                Description = request.Request.Description
            };

            await _unitOfWork.Tags.AddAsync(tag);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Tag created successfully with ID: {TagId}, Slug: {Slug}", tag.Id, tag.Slug);

            return new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name,
                Slug = tag.Slug,
                Description = tag.Description,
                BookCount = 0,
                CreatedAt = tag.CreatedAt,
                UpdatedAt = tag.UpdatedAt
            };
        }
    }
}
