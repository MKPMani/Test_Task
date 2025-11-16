using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using User.API.Controllers;
using User.Application.Commands;

namespace WebApi.Test;

public class UserControllerTest
{
    private readonly Mock<IMediator> _mediatorMock;

    public UserControllerTest() => _mediatorMock = new();

    [Fact]
    public async Task Controller_CreateUser_Should_ReturnSuccessResult_WhenUserObjectIsValid()
    {
        //Arrange
        var controller = new UserController(_mediatorMock.Object);
        var command = new CreateUserCommand()
        {
            Name = "test",
            Email = "test@email.com",
        };

        //Act
        var result = await controller.CreateUser(command);

        //Assert
        Assert.NotEqual(Guid.Empty, result);
    }

    [Fact]
    public async Task Controller_GetUser_Should_ReturnSuccessResult_WhenUserObjectIsValid()
    {
        // Arrange
        var controller = new UserController(_mediatorMock.Object);

        // Act
        var result = await controller.GetUserById(Guid.NewGuid());

        // Assert
        Assert.Equal(200, Assert.IsType<OkObjectResult>(result.Result).StatusCode);
    }

}