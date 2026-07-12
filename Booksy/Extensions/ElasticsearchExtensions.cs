using Booksy.Infrastructure.Search;
using Elastic.Clients.Elasticsearch;

namespace Booksy.Extensions;

public static class ElasticsearchExtensions
{
    public static IServiceCollection AddElasticsearch(this IServiceCollection services, IConfiguration configuration)
    {
        var url = Environment.GetEnvironmentVariable("ELASTICSEARCH_URL")
               ?? configuration["Elasticsearch:Url"]
               ?? "http://localhost:9200";

        var settings = new ElasticsearchClientSettings(new Uri(url))
            .DefaultIndex("booksy-books")
            .EnableDebugMode()              // captured in DebugInformation on failures
            .PrettyJson(false);

        // Basic auth — optional, only if credentials are configured
        var user = Environment.GetEnvironmentVariable("ELASTICSEARCH_USERNAME")
                ?? configuration["Elasticsearch:Username"];
        var pass = Environment.GetEnvironmentVariable("ELASTICSEARCH_PASSWORD")
                ?? configuration["Elasticsearch:Password"];
        if (!string.IsNullOrWhiteSpace(user) && !string.IsNullOrWhiteSpace(pass))
            settings.Authentication(new Elastic.Transport.BasicAuthentication(user, pass));

        services.AddSingleton(new ElasticsearchClient(settings));
        services.AddScoped<IBookSearchService, BookSearchService>();

        return services;
    }
}
