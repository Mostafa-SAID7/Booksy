namespace Booksy.Models.DTOs.Response.Auth
{
    public class AuthorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public int BookCount { get; set; } // optional, number of books
    }
}
