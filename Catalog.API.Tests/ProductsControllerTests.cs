using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using Catalog.API.Controllers;
using Catalog.API.Models;
using Catalog.API.Repositories;

namespace Catalog.API.Tests;

public class ProductsControllerTests
{
    private readonly Mock<IProductRepository> _repositoryStub;
    private readonly ProductsController _controller;

    public ProductsControllerTests()
    {
        _repositoryStub = new Mock<IProductRepository>();
        _controller = new ProductsController(_repositoryStub.Object);
    }

    [Fact]
    public async Task GetById_MissingId_ReturnsNotFound()
    {
        // Arrange
        _repositoryStub.Setup(repo => repo.GetByIdAsync(It.IsAny<int>()))
            .ReturnsAsync((Product)null);

        // Act
        var result = await _controller.Get(999);

        // Assert
        Assert.IsType<NotFoundResult>(result.Result);
    }
}
