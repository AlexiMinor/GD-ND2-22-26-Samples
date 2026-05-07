using Riok.Mapperly.Abstractions;
using SampleSolution.Data.DataAccess.Article;

namespace SampleSolution.Data.DataAccess.TestSamples;


[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target,
    AllowNullPropertyAssignment = true,
    EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class TestMapper
{
    [MapperIgnoreTarget(nameof(TargetType.Articles))]
    public static partial TargetType SourceTypeToTargetType(SourceType source);

    [UserMapping(Default = true)]
    public static TargetType MapSourceTypeToTargetType(SourceType source)
    {
        var target = SourceTypeToTargetType(source);

        if (source.Articles != null)
        {
            target.Articles = source.Articles.Select(ArticleMapper.ArticleDtoToArticle).ToList().AsReadOnly();
        }

        return target;
    }
    
    public static partial Target2Type SourceTypeToTarget2Type(Source2Type source);
}