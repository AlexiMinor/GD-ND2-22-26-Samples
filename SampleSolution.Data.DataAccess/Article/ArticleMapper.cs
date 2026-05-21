using Riok.Mapperly.Abstractions;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target,
    AllowNullPropertyAssignment = true,
    EnumMappingStrategy = EnumMappingStrategy.ByName)]
public static partial class ArticleMapper
{
    //[MapProperty(nameof(ArticleDto.ContentText), nameof(Db.Entities.Article.Text))]
    [MapperIgnoreTarget(nameof(Db.Entities.Article.Rate))]
    [MapperIgnoreTarget(nameof(Db.Entities.Article.Id))]
    [MapperIgnoreTarget(nameof(Db.Entities.Article.Source))]
    //[MapValue(nameof(Db.Entities.Article.ArticleIdentifier), Use = nameof(GetNewIdentifier))]
    public static partial Db.Entities.Article ArticleDtoToArticle(ArticleDto dto);

    [MapperIgnoreTarget(nameof(Db.Entities.Article.Rate))]
    [MapperIgnoreTarget(nameof(Db.Entities.Article.Id))]
    [MapperIgnoreTarget(nameof(Db.Entities.Article.Text))]
    [MapperIgnoreTarget(nameof(Db.Entities.Article.Source))]
    public static partial Db.Entities.Article RssArticleInfoDtoToArticle(RssArticleInfoDto dto);

    public static partial ArticleDto? ArticleToArticleDto(Db.Entities.Article? entity);

    private static Guid GetNewIdentifier() => Guid.NewGuid();

    public static partial ArticlePreviewDto ArticleToArticlePreviewDto(Db.Entities.Article article);

    public static partial IQueryable<ArticlePreviewDto> ProjectArticleToArticlePreviewDto(this IQueryable<Db.Entities.Article> queryable);

}