using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.TestSamples;

public class SourceType
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<ArticleDto>? Articles { get; set; }
    public string EnumValue { get; set; }

}

public record Source2Type
{
    public int Id { get; init; }
    public SourceType[] Values { get; init; }
}