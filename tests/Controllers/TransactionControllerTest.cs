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
    [Trait("xUnit", "Controller | Transaction")]
    public class TransactionControllerTest
    {
        private readonly Mock<ITransactionService> mockTransactionService;
        private readonly TransactionController transactionController;

        public TransactionControllerTest()
        {
            mockTransactionService = new Mock<ITransactionService>();
            transactionController = new TransactionController(mockTransactionService.Object);
        }

        [Fact]
        [Trait("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenDataExistsThanReturnData()
        {
            // Arrange
            mockTransactionService
                .Setup(_ => _.GetAllAsync())
                .ReturnsAsync(new List<Transaction>())
                .Verifiable();

            // Act
            var result = await transactionController.GetAllAsync();

            // Assert
            mockTransactionService.VerifyAll();
            var apiResponse = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.OK);
        }
    }
}
