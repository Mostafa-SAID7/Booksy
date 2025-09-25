namespace Booksy.DTOs.Response
{
    public class ProductWithRelatedResponse
    {
        public Product Product { get; set; } = null!;
        public List<Product> RelatedProducts { get; set; } = null!;
        public List<Product> TopTraffic { get; set; } = null!;
        public List<Product> SimilarProducts { get; set; } = null!;
    }
}
