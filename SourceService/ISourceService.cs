using SampleSolution.Data.Db.Entities;

namespace SampleSolution.Services.SourceService;

public interface ISourceService
{
    public Task<Source[]> GetSourcesAsync(CancellationToken cancellationToken);

    public Task CreateSourceAsync(Source source, CancellationToken cancellationToken);
    Task<bool> CheckDomainUrlExistsAsync(string domainUrl);
}