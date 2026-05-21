using MediatR;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.CommandHandlers;

public class UpdateArticleRatesCommandHandler(SampleDbContext dbContext) : IRequestHandler<UpdateArticleRatesCommand>
{
    public async Task Handle(UpdateArticleRatesCommand request, CancellationToken cancellationToken)
    {
        var articleIds = request.ArticleRates.Select(x => x.Key);

        var articles = dbContext.Articles.Where(article => articleIds.Contains(article.Id)).ToList();

        foreach (var article in articles)
        {
            article.Rate = request.ArticleRates[article.Id];
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

}