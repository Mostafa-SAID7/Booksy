using AutoMapper;
using Booksy.Features.Authors.Commands;
using Booksy.Features.Authentication.DTOs;
using Booksy.Models.Entities.Books;

namespace Booksy.Features.Authors.Mappings;

/// <summary>
/// AutoMapper profiles for Author feature
/// </summary>
public class AuthorMappingProfile : Profile
{
    public AuthorMappingProfile()
    {
        // Commands to Entities
        CreateMap<CreateAuthorCommand, Author>();
        CreateMap<UpdateAuthorCommand, Author>();

        // Entities to DTOs
        CreateMap<Author, AuthorResponse>();
    }
}
