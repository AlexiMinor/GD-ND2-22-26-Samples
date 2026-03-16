namespace SampleSolution.Core.DTOs;

public class RssArticleInfoDto
{
    public required string Title { get; set; }
    public required string ShortDescription { get; set; }
    public DateTime? PublishedDate { get; set; }
    public required string OriginalUrl { get; set; }

    public long SourceId { get; set; }
}