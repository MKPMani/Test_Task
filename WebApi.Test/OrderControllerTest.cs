using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Ordering.API.Controllers;
using Ordering.Application.Commands;
using Ordering.Application.Handlers;
using Ordering.Application.Queries;
using Ordering.Application.Responses;
using Ordering.Core.Entities;
using Ordering.Core.Repositories;

namespace WebApi.Test;

public class OrderControllerTest
{
    private readonly Mock<IMediator> _mediatorMock;

    public OrderControllerTest() => _mediatorMock = new();

    [Fact]
    public async Task Controller_CreateOrder_Should_ReturnSuccessResult_WhenOrderObjectIsValid()
    {
        //Arrange
        var controller = new OrderController(_mediatorMock.Object, null);
        var command = new CreateOrderCommand()
        {
            UserId = "TestUser",
            Product = "Test",
            Price = 1,
            Quantity = 1
        };

        //Act
        var result = await controller.CreateOrder(command);

        //Assert
        Assert.NotEqual(Guid.Empty, result);
    }


    [Fact]
    public async Task Controller_GetOrder_Should_ReturnSuccessResult_WhenOrderObjectIsValid()
    {
        // Arrange
        var controller = new OrderController(_mediatorMock.Object, null);

        // Act
        var result = await controller.GetOrderById(Guid.NewGuid());

        // Assert
        Assert.Equal(200, Assert.IsType<OkObjectResult>(result.Result).StatusCode);
    }
}