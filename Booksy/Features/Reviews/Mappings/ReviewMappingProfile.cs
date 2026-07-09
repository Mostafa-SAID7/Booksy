using AutoMapper;
using Booksy.Features.Reviews.Commands;
using Booksy.Features.Reviews.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Reviews.Mappings;

/// <summary>
/// AutoMapper profiles for Review feature
/// </summary>
public class ReviewMappingProfile : Profile
{
    public ReviewMappingProfile()
    {
        // Commands to Entities
        CreateMap<CreateReviewCommand, Review>();
        CreateMap<UpdateReviewCommand, Review>();

        // DTOs to Entities
        CreateMap<ReviewCreateRequest, CreateReviewCommand>();
        CreateMap<ReviewUpdateRequest, UpdateReviewCommand>();

        // Entities to DTOs
        CreateMap<Review, ReviewDetailResponse>();
    }
}
