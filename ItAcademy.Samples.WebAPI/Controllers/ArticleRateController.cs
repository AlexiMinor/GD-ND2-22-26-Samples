using Microsoft.AspNetCore.Mvc;
using SampleSolution.Services.ArticleService;

namespace ItAcademy.Samples.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AggregateArticlesController(IArticleAggregatorService articleAggregatorService) : ControllerBase
    {
        [HttpPatch]
        public async Task<IActionResult> Aggregate(CancellationToken cancellationToken)
        {
            var result = await articleAggregatorService.SwitchAggregatorAsync(cancellationToken);
            return Ok(new { IsAggregationEnabled = result });
        }
    }
}
