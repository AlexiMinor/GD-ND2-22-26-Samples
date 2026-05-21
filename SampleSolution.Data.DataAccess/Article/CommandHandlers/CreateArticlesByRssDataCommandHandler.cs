using MediatR;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.CommandHandlers;

public class CreateArticlesByRssDataCommandHandler(SampleDbContext dbContext) : IRequestHandler<CreateArticlesByRssDataCommand>
{
    public async Task Handle(CreateArticlesByRssDataCommand request, CancellationToken cancellationToken)
    {
      
        var articleEntities = request.NewArticles.Select(ArticleMapper.RssArticleInfoDtoToArticle);
        await dbContext.Articles.AddRangeAsync(articleEntities, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}