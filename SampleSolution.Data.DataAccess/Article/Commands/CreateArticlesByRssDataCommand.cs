using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Commands;

public record CreateArticlesByRssDataCommand(RssArticleInfoDto[] NewArticles) : IRequest;
