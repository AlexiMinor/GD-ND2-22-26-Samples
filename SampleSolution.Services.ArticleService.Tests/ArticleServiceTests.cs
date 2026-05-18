using MediatR;
using NSubstitute;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.Article.Commands;
using SampleSolution.Data.DataAccess.Article.Queries;
using SampleSolution.Data.DataAccess.Sources.Queries;
using System.Collections.ObjectModel;
using Microsoft.Extensions.Configuration;

namespace SampleSolution.Services.ArticleService.Tests
{
    public class ArticleServiceTests
    {
        private readonly IMediator _mediatorMock;
        private readonly IRssService _rssMock;
        private readonly IConfiguration _configurationMock;
        private readonly IWebScrapperService _webScrapperMock;
        private readonly ArticleService _sut;

        public ArticleServiceTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _rssMock = Substitute.For<IRssService>();
            _webScrapperMock = Substitute.For<IWebScrapperService>();
            _configurationMock = Substitute.For<IConfiguration>();
            _sut = new ArticleService(_mediatorMock, _rssMock, _webScrapperMock, _configurationMock);
        }

        [Fact]
        //method name should be GetArticlesByPageAsync_WhenCalled_ReturnsArticlesByPage
        public async Task GetArticlesByPageAsync_WhenCalledWithCorrectParameters_ReturnData()
        {
            //AAA
            //Arrange
            //setup dbContext and mediator with mock data
            _mediatorMock.Send(Arg.Any<GetArticlesByPageQuery>(), Arg.Any<CancellationToken>())
                .Returns(Enumerable.Range(1, 10)
                    .Select(i => new ArticlePreviewDto { Id = i, Title = $"Article {i}", ShortDescription = $"Summary {i}" })
                    .ToList()
                    .AsReadOnly());

            //Act
            //execute GetArticlesByPageAsync method
            var articles = await _sut.GetArticlesByPageAsync(1, 10, CancellationToken.None);

            //Assert
            //verify that the result is correct, for example by checking the count of returned articles and their content
            Assert.NotNull(articles);
            Assert.Equal(10, articles.Count);
            //Assert.DoesNotContain(articles, art => art.Id == 11);
        }

        [Fact]
        //method name should be GetArticlesByPageAsync_WhenCalled_ReturnsArticlesByPage
        public async Task GetArticlesByPageAsync_WhenCalledWithTooBigParameters_ReturnsEmptyCollection()
        {
            _mediatorMock.Send(Arg.Any<GetArticlesByPageQuery>(), Arg.Any<CancellationToken>())
                .Returns(new ReadOnlyCollection<ArticlePreviewDto>([]));

            //Act
            //execute GetArticlesByPageAsync method
            var articles = await _sut.GetArticlesByPageAsync(1000000, 10000, CancellationToken.None);

            //Assert
            //verify that the result is correct, for example by checking the count of returned articles and their content
            Assert.NotNull(articles);
            Assert.Empty(articles);
            //Assert.DoesNotContain(articles, art => art.Id == 11);
        }

        [Fact]
        public async Task GetArticlesByPageAsync_WhenCalledWithNegativeParameters_ThrowException()
        {
            await Assert.ThrowsAsync<ArgumentException>(()=> _sut.GetArticlesByPageAsync(-1, -10, CancellationToken.None));
        }


        [Fact]
        public async Task AggregateArticlesAsync_WhenCalled_AggregateData()
        {
            SetupMediator();
            SetupRss();
            SetupWebScrapper();

           var insertedCount = await  _sut.AggregateArticlesAsync(CancellationToken.None);
           
            Assert.True(insertedCount > 0);
            Assert.Equal(2, insertedCount);
        }

        private void SetupRss()
        {
            _rssMock.GetDataFromRss(Arg.Any<string>(), Arg.Any<long>())
                .Returns(new List<RssArticleInfoDto>()
                {
                    new()
                    {
                        Title = "Test Article 1",
                        ShortDescription = "Summary of Test Article 1",
                        OriginalUrl = "http://test1.com/article1",
                        PublishedDate = DateTime.UtcNow,
                        SourceId = 1
                    },
                    new()
                    {
                        Title = "Test Article 2",
                        ShortDescription = "Summary of Test Article 2",
                        OriginalUrl = "http://test2.com/article2",
                        PublishedDate = DateTime.UtcNow,
                        SourceId = 2
                    }
                }.AsReadOnly());
        }

        private void SetupWebScrapper()
        {
            _webScrapperMock.WebScrapArticleText(Arg.Any<IEnumerable<RssArticleInfoDto>>(), Arg.Any<CancellationToken>())
                .Returns((new List<ArticleDto>
                {
                    new()
                    {
                        Title = "Test Article 1",
                        ShortDescription = "Summary of Test Article 1",
                        OriginalUrl = "http://test1.com/article1",
                        PublishedDate = DateTime.UtcNow,
                        SourceId = 1,
                        Id = 1,
                        Text = "Full text of Test Article 1"
                    },
                    new()
                    {
                        Title = "Test Article 1",
                        ShortDescription = "Summary of Test Article 1",
                        OriginalUrl = "http://test1.com/article1",
                        PublishedDate = DateTime.UtcNow,
                        SourceId = 1,
                        Id = 2, 
                        Text = "Full text of Test Article 2"
                    }
                }).AsReadOnly());
        }


        private void SetupMediator()
        {
            _mediatorMock.Send(Arg.Any<GetRssUrlsQuery>(), Arg.Any<CancellationToken>())
                .Returns(new List<Tuple<int, string>>()
                {
                    new(1, "test1.com/rss"),
                    new(2, "test2.com/feed")
                });

            _mediatorMock.Send(Arg.Any<GetExistedArticleUrlsQuery>(), Arg.Any<CancellationToken>())
                .Returns([]);

            _mediatorMock.Send(Arg.Any<InsertParsedArticlesCommand>(), Arg.Any<CancellationToken>())
                .Returns(2);
        }
    }
}
