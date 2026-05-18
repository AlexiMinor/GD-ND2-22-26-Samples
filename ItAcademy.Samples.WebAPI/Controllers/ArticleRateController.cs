using Hangfire;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.Services.ArticleService;

namespace ItAcademy.Samples.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregateArticlesController(IArticleService articleService) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Aggregate(CancellationToken cancellationToken)
        {
            RecurringJob.AddOrUpdate(
                "ArticleAggregation",
                () => articleService.AggregateArticlesAsync(cancellationToken),
                "0 1/2 * * *");
            return NoContent();
        }
    }
}
