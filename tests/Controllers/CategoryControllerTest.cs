﻿using AutoFixture;
using expense_tracker_backend.Controllers;
using expense_tracker_backend.Domain;
using expense_tracker_backend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Controllers
{
    [Trait("xUnit", "Controller | Category")]
    public class CategoryControllerTest
    {
        private readonly Mock<ICategoryService> mockCategoryService;
        private readonly CategoryController categoryController;

        public CategoryControllerTest()
        {
            mockCategoryService = new Mock<ICategoryService>();
            categoryController = new CategoryController(mockCategoryService.Object);
        }

        [Fact]
        [Trait("HttpVerb", "DELETE")]
        public async Task GivenDeleteAsyncWhenDataExistsThanReturnData()
        {
            // Arrange
            var fixture = new Fixture();
            var id = fixture.Create<Guid>();
            var category = fixture.Build<Category>()
                .Without(category => category.Transactions)
                .Create();
            mockCategoryService
                .Setup(_ => _.GetByIdAsync(id))
                .ReturnsAsync(category)
                .Verifiable();
            mockCategoryService
                .Setup(_ => _.DeleteAsync(category))
                .Verifiable();

            // Act
            var result = await categoryController.DeleteAsync(id);

            // Assert
            mockCategoryService.VerifyAll();
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
            var category = fixture.Build<Category>()
                .Without(category => category.Transactions)
                .Create();
            mockCategoryService
                .Setup(_ => _.GetByIdAsync(id))
                .ReturnsAsync(It.IsAny<Category>())
                .Verifiable();

            // Act
            var result = await categoryController.DeleteAsync(id);

            // Assert
            mockCategoryService.VerifyAll();
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
            mockCategoryService
                .Setup(_ => _.GetByIdAsync(id))
                .Throws<Exception>()
                .Verifiable();

            // Act
            var result = await categoryController.DeleteAsync(id);

            // Assert
            mockCategoryService.VerifyAll();
            var apiResponse = Assert.IsType<ObjectResult>(result);
            Assert.Equal(apiResponse.StatusCode, (int)HttpStatusCode.InternalServerError);
        }
    }
}
