namespace SampleSolution.Services.SourceService.Models;

public class SourceModel // in case we are receiving data from an external source, we can use this model to map the data to our Source entity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string DomainUrl { get; set; }
}