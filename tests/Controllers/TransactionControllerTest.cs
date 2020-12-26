using AutoFixture;
using expense_tracker_backend.Controllers;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
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
        private readonly Mock<IAccountService> mockAccountService;
        private readonly TransactionController transactionController;

        public TransactionControllerTest()
        {
            mockTransactionService = new Mock<ITransactionService>();
            transactionController = new TransactionController(mockTransactionService.Object, mockAccountService.Object);
        }

        [Fact]
        [Trait("HttpVerb", "GET")]
        public async Task GivenGetAllAsyncWhenDataExistsThanReturnData()
        {
            // Arrange
            mockTransactionService
                .Setup(_ => _.GetAllAsync(1, 10))
                .ReturnsAsync(new List<Transaction>())
                .Verifiable();

            // Act
            var result = await transactionController.GetAllAsync(1, 10);

            // Assert
            mockTransactionService.VerifyAll();
            var apiResponse = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenDataExistsThanReturnData()
        {
            // Arrange
            var fixture = new Fixture();
            var transaction = fixture.Build<Transaction>()
                .Without(transaction => transaction.Account)
                .Without(transaction => transaction.Category)
                .Create();
            var id = fixture.Create<Guid>();
            mockTransactionService
                .Setup(_ => _.GetByIdAsync(id))
                .ReturnsAsync(transaction)
                .Verifiable();
            mockTransactionService
                .Setup(_ => _.DeleteAsync(transaction))
                .Verifiable();

            // Act
            var result = await transactionController.DeleteAsync(id);

            // Assert
            mockTransactionService.VerifyAll();
            var apiResponse = Assert.IsType<OkResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.OK);
        }

        [Fact]
        [Trait("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenNoDataFoundThanHandleGracefully()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<Guid>();
            mockTransactionService
                .Setup(_ => _.GetByIdAsync(id))
                .ReturnsAsync(It.IsAny<Transaction>())
                .Verifiable();

            // Act
            var result = await transactionController.DeleteAsync(id);

            // Assert
            mockTransactionService.VerifyAll();
            var apiResponse = Assert.IsType<NotFoundResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.NotFound);
        }

        [Fact]
        [Trait("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenExceptionOccursThanHandleGracefully()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<Guid>();
            mockTransactionService
                .Setup(_ => _.GetByIdAsync(id))
                .Throws<Exception>()
                .Verifiable();

            // Act
            var result = await transactionController.DeleteAsync(id);

            // Assert
            mockTransactionService.VerifyAll();
            var apiResponse = Assert.IsType<ObjectResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.InternalServerError);
        }
    }
}
