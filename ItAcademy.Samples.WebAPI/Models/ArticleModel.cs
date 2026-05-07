namespace ItAcademy.Samples.WebAPI.Models;

public class ArticleModel
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ShortDescription { get; set; }
    public string OriginalUrl { get; set; }
    public string Text { get; set; }
    public DateTime PublishedDate { get; set; }
    public int SourceId { get; set; }
}