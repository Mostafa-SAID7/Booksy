using AutoMapper;
using Booksy.Features.Books.Commands;
using Booksy.Features.Books.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Books.Mappings;

/// <summary>
/// AutoMapper profiles for Book feature
/// </summary>
public class BookMappingProfile : Profile
{
    public BookMappingProfile()
    {
        // Commands to Entities
        CreateMap<CreateBookCommand, Book>();
        CreateMap<UpdateBookCommand, Book>();

        // Entities to DTOs
        CreateMap<Book, BookResponse>()
            .ForMember(dest => dest.Author, opt => opt.MapFrom(src => src.Author))
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));
    }
}
