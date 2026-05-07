using System.Collections.ObjectModel;
using SampleSolution.Core;

namespace SampleSolution.Data.DataAccess.TestSamples;

public class TargetType
{
    public long Id { get; set; }
    public string Name { get; set; }
    public ReadOnlyCollection<Db.Entities.Article>? Articles { get; set; }
    public TestEnum EnumValue { get; set; }
}

public record Target2Type
{
    public int Id { get; init; }
    public TargetType[] Values { get; init; }
}