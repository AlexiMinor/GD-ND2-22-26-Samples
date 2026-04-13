using MediatR;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.CommandHandlers;

public class InsertParsedArticlesCommandHandler(SampleDbContext dbContext) : IRequestHandler<InsertParsedArticlesCommand>
{
    public async Task Handle(InsertParsedArticlesCommand request, CancellationToken cancellationToken)
    {
        // Map ArticleDto to Article entities using mapper and it's discussible where to put it
        var articleEntities = request.Articles.Select(dto => new Db.Entities.Article()
        {
            Title = dto.Title,
            ShortDescription = dto.ShortDescription,
            Text = dto.Text,
            PublishedDate = dto.PublishedDate,
            SourceId = dto.SourceId,
            OriginalUrl = dto.OriginalUrl,
            Rate = null,
        });
        await dbContext.Articles.AddRangeAsync(articleEntities, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        // Here you can implement the logic to insert the parsed article into the database.

    }
}