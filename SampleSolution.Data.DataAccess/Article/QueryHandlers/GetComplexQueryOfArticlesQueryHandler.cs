using System.Collections.ObjectModel;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.QueryHandlers;

//public class GetComplexQueryOfArticlesQueryHandler(SampleDbContext dbContext) : IRequestHandler<GetExistedArticleUrlsQuery, ReadOnlyCollection<string>>
//{
//    //public async Task<ReadOnlyCollection<string>> Handle(GetExistedArticleUrlsQuery request, CancellationToken cancellationToken)
//    //{
//    //    var articles = dbContext.Articles.AsNoTrackingWithIdentityResolution();

//    //    if (request.A.HasValue())
//    //    {
//    //        articles = articles.Where(x=>x.Equals().Equals().)
//    //    }

//    //    return (articles
//    //            .ToArrayAsync(cancellationToken))
//    //        .AsReadOnly();

//    //}
//}