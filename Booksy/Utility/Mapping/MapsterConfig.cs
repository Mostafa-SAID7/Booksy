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
            TypeAdapterConfig<Book, BookResponse>.NewConfig()
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.AuthorName, src => src.Author.Name)
                .Map(dest => dest.CoverImageUrl, src => src.CoverImageUrl);

            TypeAdapterConfig<BookCreateRequest, Book>.NewConfig()
                .Ignore(dest => dest.Id); // Id is generated in DB

            TypeAdapterConfig<BookUpdateRequest, Book>.NewConfig()
                .Ignore(dest => dest.Id); // Set manually in controller
        }
        public static void RegisterAuthorMappings()
        {
            TypeAdapterConfig<Author, AuthorResponse>.NewConfig()
                .Map(dest => dest.BookCount, src => src.Books.Count);

            TypeAdapterConfig<AuthorCreateRequest, Author>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Books);
        }
        public static void RegisterCategoryMappings()
        {
            TypeAdapterConfig<Category, CategoryResponse>.NewConfig()
                .Map(dest => dest.BookCount, src => src.Books.Count);

            TypeAdapterConfig<CategoryCreateRequest, Category>.NewConfig()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.Books);

         
        }
    }
}
