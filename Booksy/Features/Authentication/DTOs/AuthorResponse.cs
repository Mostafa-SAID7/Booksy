namespace Booksy.Features.Authentication.DTOs
{
    public class AuthorResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Slug { get; set; } = string.Empty;
        // optional, number of books
    }
}
