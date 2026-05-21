using MediatR;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.CommandHandlers;

public class UpdateArticleTextsCommandHandler(SampleDbContext dbContext) : IRequestHandler<UpdateArticleTextsCommand>
{
    public async Task Handle(UpdateArticleTextsCommand request, CancellationToken cancellationToken)
    {
        var articleIds = request.ArticleTexts.Select(x => x.Key);

        var articles = dbContext.Articles.Where(article => articleIds.Contains(article.Id)).ToList();

        foreach (var article in articles)
        {
            article.Text = request.ArticleTexts[article.Id];
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

}