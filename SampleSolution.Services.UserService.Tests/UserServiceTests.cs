using MediatR;
using Microsoft.Extensions.Logging;
using NSubstitute;
using SampleSolution.Core.DTOs;
using SampleSolution.Data.DataAccess.User.Queries;

namespace SampleSolution.Services.UserService.Tests
{
    public class UserServiceTests
    {
        private readonly IMediator _mediatorMock;
        private readonly ILogger<SampleSolution.UserService.UserService> _loggerMock;
        private readonly SampleSolution.UserService.UserService _sut;

        private List<UserDto> _testUserCollection =
        [
            new UserDto { Email = "user1@test.com", PasswordHash = "123", PasswordSalt = "321", RoleName = "User" },
            new UserDto { Email = "user2@test.com", PasswordHash = "123", PasswordSalt = "321", RoleName = "User" },
            new UserDto { Email = "user3@test.com", PasswordHash = "123", PasswordSalt = "321", RoleName = "User" },
            new UserDto { Email = "user4@test.com", PasswordHash = "123", PasswordSalt = "321", RoleName = "User" }
        ];

        public UserServiceTests()
        {
            _mediatorMock = Substitute.For<IMediator>();
            _loggerMock = Substitute.For<ILogger<SampleSolution.UserService.UserService>>();
            _sut = new SampleSolution.UserService.UserService(_loggerMock, _mediatorMock);
        }

        [Theory]
        [InlineData("user1@test.com")]
        [InlineData("user2@test.com")]
        [InlineData("user3@test.com")]
        public async Task CheckEmailAsync_CheckExistedEmail_ReturnTrue(string email)
        {
            //arrange
            SetupMediatorForCheckEmailAsync(email);

            var result = await _sut.CheckEmailAsync(email, CancellationToken.None);

            Assert.True(result);
        }

        [Theory]
        [InlineData("user10@test.com")]
        [InlineData("user20@test.com")]
        [InlineData("user30@test.com")]
        public async Task CheckEmailAsync_CheckNonExistedEmail_ReturnFalse(string email)
        {
            //arrange
            SetupMediatorForCheckEmailAsync(email);

            var result = await _sut.CheckEmailAsync(email, CancellationToken.None);

            Assert.False(result);
        }

        private void SetupMediatorForCheckEmailAsync(string email)
        {
            _mediatorMock.Send(Arg.Any<IsUserWithEmailExistsQuery>(),
                    Arg.Any<CancellationToken>())
                .Returns(_testUserCollection.Any(dto => dto.Email.Equals(email)));
        }
    }
}
