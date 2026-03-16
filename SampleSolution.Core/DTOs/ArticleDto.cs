namespace SampleSolution.Core.DTOs;

public class ArticleDto //DTO - Data Transfer Object, used for transferring data between layers of the application
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string OriginalUrl { get; set; }
    public string Text { get; set; }
    public DateTime PublishedDate { get; set; }
    public int SourceId { get; set; }
}