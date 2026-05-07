using MediatR;
using Microsoft.EntityFrameworkCore;
using SampleSolution.Core.Exceptions;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.Db;

namespace SampleSolution.Data.DataAccess.Article.CommandHandlers;

public class UpdateArticlePartiallyCommandHandler(SampleDbContext dbContext) : IRequestHandler<UpdateArticlePartiallyCommand>
{
    public async Task Handle(UpdateArticlePartiallyCommand request, CancellationToken cancellationToken)
    {
        var article = await dbContext.Articles.FirstOrDefaultAsync(a => a.Id == request.Id, cancellationToken);
        if (article == null)
        {
            throw new NotFoundException(nameof(Article), request.Id);
        }

        if (request.UpdatedModelTitle != null)
        {
            article.Title = request.UpdatedModelTitle;
        }

        if (request.UpdatedModelRate != null)
        {
            article.Rate = request.UpdatedModelRate.Value;
        }

        await dbContext.SaveChangesAsync(cancellationToken);
    }

}