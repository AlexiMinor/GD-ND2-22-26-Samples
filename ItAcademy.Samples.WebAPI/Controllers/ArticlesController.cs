using ItAcademy.Samples.WebAPI.Mappers;
using ItAcademy.Samples.WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SampleSolution.Core.DTOs;
using SampleSolution.Services.ArticleService;

namespace ItAcademy.Samples.WebAPI.Controllers;

/// <summary>
/// ArticlesController is responsible for handling HTTP requests related to articles
/// </summary>
/// <param name="logger">The logger instance for logging information and errors.</param>
/// <param name="articleService">The service instance for managing articles.</param>
[ApiController]
[Authorize]
[Route("api/[controller]")]
public class ArticlesController(ILogger<ArticlesController> logger, IArticleService articleService)
    : ControllerBase
{
    /// <summary>
    /// Retrieves the article with the specified identifier.
    /// </summary>
    /// <param name="id">The unique identifier of the article to retrieve.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>An <see cref="IActionResult"/> containing the article data if found; otherwise, a 404 Not Found result.</returns>
    [HttpGet("{id}")]
    [ProducesResponseType<ArticleDto>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(long id, CancellationToken cancellationToken)
    {
        var article = await articleService.GetArticleByIdAsync(id, cancellationToken);

        return article == null ? 
            NotFound() : 
            Ok(ArticleMapper.ArticleDtoToArticleModel(article));
    }

    //domain.com/api/clients/{id}/accounts/{id}

    //[HttpGet]
    ////[Route("get-all")]
    //public IActionResult GetAll()
    //{
    //    return Ok(Enumerable.Range(1, 15).Select(index => new ArticleDto()
    //    {
    //        Id = index,
    //        Title = $"Article {index}",
    //        Text = $"This is the content of article {index}."
    //    })
    //    .ToArray());
    //}

    /// <summary>
    /// Retrieves a collection of articles filtered by the specified rate, if provided.
    /// </summary>
    /// <param name="rate">The minimum rate to filter articles by. If null, all articles are returned.</param>
    /// <returns>An IActionResult containing an array of ArticleDto objects that match the specified rate filter. Returns all
    /// articles if no rate is specified.</returns>
    [HttpGet]
    [ProducesResponseType<ArticleDto[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetByRateAndSource(decimal? minRate, int? sourceId, CancellationToken cancellationToken)
    {
        var articles = await articleService.GetArticlesByRateAndSourceAsync(minRate, sourceId, cancellationToken);
        return Ok(articles);
    }

    //[HttpPut("{id}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
    //public IActionResult Update(long id, ArticleDto article)
    //{
    //    // Simulate updating an article
    //    //article.Id = id;
    //    return NoContent();
    //}

    //[HttpPatch("{id}")]
    //[ProducesResponseType(StatusCodes.Status204NoContent)]
    //[ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ProducesResponseType(StatusCodes.Status404NotFound)]
    //[ProducesResponseType(StatusCodes.Status500InternalServerError)]

    //public async Task<IActionResult> UpdatePartially(long id, UpdateArticleModel updatedModel, CancellationToken cancellationToken)
    //{
    //    await articleService.UpdateArticlePartiallyAsync(id, updatedModel.Title, updatedModel.Rate, cancellationToken);
    //    return NoContent();
    //}

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(long id)
    {
        return NoContent();
    }

}