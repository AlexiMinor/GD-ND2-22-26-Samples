using Microsoft.EntityFrameworkCore;
using SampleSolution.Data.Db;
using SampleSolution.Data.Db.Entities;

namespace SampleSolution.Services.SourceService
{
    public class SourceService(SampleDbContext context) : ISourceService
    {
        public async Task<Source[]> GetSourcesAsync(CancellationToken cancellationToken)
        {
            return await context.Sources.ToArrayAsync(cancellationToken);
        }
        public async Task CreateSourceAsync(Source source, CancellationToken cancellationToken)
        {
            await context.Sources.AddAsync(source, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
        }

        public async Task<bool> CheckDomainUrlExistsAsync(string domainUrl)
        {
            return await context.Sources.AnyAsync(source => source.DomainUrl.Equals(domainUrl));
        }
    }
}