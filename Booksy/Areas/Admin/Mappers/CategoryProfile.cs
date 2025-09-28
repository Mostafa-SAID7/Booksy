using AutoMapper;
using Booksy.Models.DTOs.Request.Category;
using Booksy.Models.DTOs.Response.Books;
using Booksy.Models.DTOs.Response.Category;
using Booksy.Models.Entities.Books;

namespace Booksy.Areas.Admin.Mappers
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<Category, CategoryResponse>();
            CreateMap<Book, BookResponse>();
            CreateMap<CategoryRequest, Category>();
        }
    }
}
