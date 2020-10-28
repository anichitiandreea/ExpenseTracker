using expense_tracker_backend.Controllers;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
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
    }
}
