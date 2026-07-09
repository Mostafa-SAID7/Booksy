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
    /// Handler for updating a tag
    /// </summary>
    public class UpdateTagCommandHandler : ICommandHandler<UpdateTagCommand, TagResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISlugService _slugService;
        private readonly IValidationService _validationService;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateTagCommandHandler> _logger;

        public UpdateTagCommandHandler(
            IUnitOfWork unitOfWork,
            ISlugService slugService,
            IValidationService validationService,
            IMapper mapper,
            ILogger<UpdateTagCommandHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _slugService = slugService;
            _validationService = validationService;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<TagResponse> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Updating tag with ID: {TagId}, new name: {TagName}", request.Id, request.Request.Name);

            // Validate input
            if (string.IsNullOrWhiteSpace(request.Request.Name) || request.Request.Name.Length < 2 || request.Request.Name.Length > 50)
            {
                throw new BusinessException("Tag name must be between 2 and 50 characters");
            }

            // Get the tag to update
            var tag = await _unitOfWork.Tags.GetByIdAsync(request.Id);

            if (tag == null)
            {
                _logger.LogWarning("Tag not found with ID: {TagId}", request.Id);
                throw new NotFoundException($"Tag with ID {request.Id} not found");
            }

            // Check if another tag with same name exists (excluding current tag)
            var existingTag = await _unitOfWork.Tags.GetOneAsync(
                t => t.Name.ToLower() == request.Request.Name.ToLower() && t.Id != request.Id);

            if (existingTag != null)
            {
                _logger.LogWarning("Tag with name '{TagName}' already exists", request.Request.Name);
                throw new ConflictException("A tag with this name already exists");
            }

            // Update tag name
            tag.Name = request.Request.Name;
            tag.Description = request.Request.Description;

            // Update or regenerate slug using ISlugService
            var newSlug = await _slugService.GenerateUniqueSlugAsync(
                _unitOfWork,
                request.Request.Slug ?? request.Request.Name,
                typeof(Tag),
                request.Id,
                cancellationToken);

            tag.Slug = newSlug;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            _logger.LogInformation("Tag updated successfully with ID: {TagId}, new slug: {Slug}", request.Id, tag.Slug);

            return new TagResponse
            {
                Id = tag.Id,
                Name = tag.Name,
                Slug = tag.Slug,
                Description = tag.Description,
                BookCount = tag.Books?.Count ?? 0,
                CreatedAt = tag.CreatedAt,
                UpdatedAt = tag.UpdatedAt
            };
        }
    }
}
