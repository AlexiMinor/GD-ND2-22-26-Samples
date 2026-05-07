using MediatR;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.CommandHandlers;

public class InsertParsedArticlesCommandHandler(SampleDbContext dbContext) : IRequestHandler<InsertParsedArticlesCommand, int>
{
    public async Task<int> Handle(InsertParsedArticlesCommand request, CancellationToken cancellationToken)
    {
        // Map ArticleDto to Article entities using mapper and it's discussible where to put it
        var articleEntities = request.Articles.Select(ArticleMapper.ArticleDtoToArticle);
        await dbContext.Articles.AddRangeAsync(articleEntities, cancellationToken);
        return await dbContext.SaveChangesAsync(cancellationToken);
        // Here you can implement the logic to insert the parsed article into the database.

    }
}