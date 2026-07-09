using AutoMapper;
using Booksy.Features.Categories.Commands;
using Booksy.Features.Categories.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Categories.Mappings;

/// <summary>
/// AutoMapper profiles for Category feature
/// </summary>
public class CategoryMappingProfile : Profile
{
    public CategoryMappingProfile()
    {
        // Commands to Entities
        CreateMap<CreateCategoryCommand, Category>();
        CreateMap<UpdateCategoryCommand, Category>();

        // Entities to DTOs
        CreateMap<Category, CategoryResponse>();
    }
}
