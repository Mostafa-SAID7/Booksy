using AutoMapper;
using Booksy.Features.Books.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Inventory.Mappings;

/// <summary>
/// AutoMapper profiles for Inventory feature
/// </summary>
public class InventoryMappingProfile : Profile
{
    public InventoryMappingProfile()
    {
        // Entities to DTOs
        CreateMap<Book, BookResponse>();
    }
}
