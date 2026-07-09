using AutoMapper;
using Booksy.Features.Tags.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Tags.Mappings
{
    /// <summary>
    /// AutoMapper profile for Tag entity mappings
    /// </summary>
    public class TagMappingProfile : Profile
    {
        public TagMappingProfile()
        {
            // Map Tag entity to TagResponse
            CreateMap<Tag, TagResponse>()
                .ForMember(dest => dest.BookCount, opt => opt.MapFrom(src => src.Books != null ? src.Books.Count : 0));

            // Map TagCreateRequest to Tag entity
            CreateMap<TagCreateRequest, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());

            // Map TagUpdateRequest to Tag entity
            CreateMap<TagUpdateRequest, Tag>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
                .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                .ForMember(dest => dest.Books, opt => opt.Ignore());
        }
    }
}
