using MediatR;
using SampleSolution.Core.DTOs;

namespace SampleSolution.Data.DataAccess.Article.Queries;

public record GetArticleByIdQuery(long Id) :  IRequest<ArticleDto>;