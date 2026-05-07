using ItAcademy.Samples.WebAPI.Models;
using Riok.Mapperly.Abstractions;
using SampleSolution.Core.DTOs;

namespace ItAcademy.Samples.WebAPI.Mappers;

[Mapper]
public partial class ArticleMapper
{
    public static partial ArticleModel? ArticleDtoToArticleModel(ArticleDto? articleDto);

}