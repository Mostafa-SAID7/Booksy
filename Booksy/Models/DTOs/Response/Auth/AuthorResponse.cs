namespace Booksy.Models.DTOs.Response.Auth
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
// optional, number of books
    }
}
