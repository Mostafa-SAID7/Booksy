using Booksy.Models.DTOs.Request.Auth;
using Booksy.Models.DTOs.Request.Books;
using Booksy.Models.DTOs.Request.Category;
using Booksy.Models.DTOs.Response.Auth;
using Booksy.Models.DTOs.Response.Books;
using Booksy.Models.DTOs.Response.Category;
using Booksy.Models.Entities.Books;
using Mapster;

namespace Booksy.Utility.Mapping
{
    public static class MapsterConfig
    {
     
        public static void RegisterMappings()
        {
            // Category -> CategoryResponse
            TypeAdapterConfig<Category, CategoryResponse>
        .NewConfig()
        .Map(dest => dest.Id, src => src.Id)
        .Map(dest => dest.Name, src => src.Name);

            // CategoryRequest -> Category
            TypeAdapterConfig<CategoryCreateRequest, Category>
                .NewConfig()
                .Map(dest => dest.Name, src => src.Name);

            TypeAdapterConfig<Book, BookResponse>
                .NewConfig()
                .Map(dest => dest.Id, src => src.Id)
                .Map(dest => dest.Title, src => src.Title)
                .Map(dest => dest.Author, src => src.Author)
                .Map(dest => dest.CoverImageUrl, src => src.CoverImageUrl)
                .Ignore(dest => dest.Category); // avoid circular reference

            TypeAdapterConfig<BookCreateRequest, Book>
                .NewConfig()
                .Ignore(dest => dest.Id); // Id generated in DB

            TypeAdapterConfig<BookUpdateRequest, Book>
                .NewConfig()
                .Ignore(dest => dest.Id);
        }
    }
}
