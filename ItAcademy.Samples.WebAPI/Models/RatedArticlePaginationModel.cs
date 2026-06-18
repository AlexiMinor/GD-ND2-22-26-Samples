namespace ItAcademy.Samples.WebAPI.Models
{
    public class RatedArticlePaginationModel
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        public int? MinRate { get; set; }
    }
}
