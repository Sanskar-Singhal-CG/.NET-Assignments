namespace Catalog.API.Configuration;

public class PaginationOptions
{
    public const string SectionName = "PaginationOptions";
    public int DefaultPageSize { get; set; } = 10;
}
