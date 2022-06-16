using AutoFixture;
using ExpenseTracker.Controllers;
using ExpenseTracker.Domain;
using ExpenseTracker.Services.Interfaces;
using ExpenseTracker.Transfer;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    [Trait("xUnit", "Controller | Account")]
    public class AccountControllerTest
    {
        private readonly Mock<IAccountService> mockAccountService;
        private readonly AccountController accountController;

        public AccountControllerTest()
        {
            mockAccountService = new Mock<IAccountService>();
            accountController = new AccountController(mockAccountService.Object);
        }

        [Fact]
        [Trait("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenDataExistsThenReturnsData()
        {
            // Arrange
            mockAccountService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<Account>())
                .Verifiable();

            // Act
            var result = await accountController.GetAllAsync();

            // Assert
            mockAccountService.VerifyAll();
            var apiResponse = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("HttpVerb", "GET")]
        public async Task GivenUpdateAsyncWhenDataExistsThenReturnsData()
        {
            // Arrange
            var fixture = new Fixture();
            var accountRequest = fixture.Create<AccountRequest>();
            accountRequest.Amount = "223";
            var account = fixture.Build<Account>()
                .Without(account => account.Transactions)
                .Create();
            mockAccountService
                .Setup(_ => _.UpdateAsync(It.IsAny<Account>()))
                .Verifiable();

            // Act
            var result = await accountController.UpdateAsync(accountRequest);

            // Assert
            mockAccountService.VerifyAll();
            var apiResponse = Assert.IsType<OkResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}
