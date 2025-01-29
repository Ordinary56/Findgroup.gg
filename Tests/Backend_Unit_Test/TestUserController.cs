using AutoMapper;
using Findgroup_Backend.Controllers;
using Findgroup_Backend.Data;
using Findgroup_Backend.Data.Repositories;
using Findgroup_Backend.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;

namespace Backend_Unit_Test;
public class TestUserController
{
    private readonly Mock<IUserRepository> _mockUserRepo;
    private readonly Mock<IMapper> _mockMapper;
    private UserController _controller;
    public TestUserController()
    {
        _mockUserRepo = new Mock<IUserRepository>();
        _mockMapper = new Mock<IMapper>();
        _controller = new(_mockUserRepo.Object, _mockMapper.Object);
    }
    [Fact]
    public async Task Test_GetUsers_Returns_Ok()
    {
        // TODO: Arrange the mocked db and userManager here
        // Arrange
        List<User> Users = [
            new() {UserName = "Asd" },
            new() {UserName = "Asd1"}
            ];
        var asyncUsers = Users.ToAsyncEnumerable();
        _mockUserRepo.Setup(service => service.GetUsers()).Returns(asyncUsers);
        

        // Act
        IAsyncEnumerable<User> result = _controller.GetUsers();
        var resultList = await result.ToListAsync();

        // Assert
        Assert.NotNull(resultList);
        Assert.NotEmpty(resultList);
        Assert.Equal(2, resultList.Count);
    }

    [Theory]
    [InlineData("1")]
    [InlineData("2")]
    [InlineData("3")]
    public async Task Test_Get_User_Id_Returns_User(string id)
    {

        // Act
        List<User> users = [
            new(){Id = "1", UserName = "Asd"},
            new() {Id = "2", UserName = "Asd1"},
            new() {Id = "3", UserName = "Asd2"}
            ];
        _mockUserRepo.Setup(service => service.GetUserById(It.IsAny<string>()))
            .Returns(async (string i) =>
            {
                await Task.Delay(1);
                return users.First(u => u.Id == i);
            });
        var result = await _controller.GetUserById(id);
        var resultType = Assert.IsType<OkObjectResult>(result.Result);
        Assert.IsType<User>(resultType.Value);
        Assert.NotNull(resultType.Value);

    }

}
