using Findgroup_Backend.Controllers;
using Findgroup_Backend.Data;
using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Moq;
using System.Net;
using MockFactory = Backend_Unit_Test.Mocks.MockFactory;

namespace Backend_Unit_Test;
public class TestUserController
{
    private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
    private readonly Mock<ApplicationDbContext> _mockDbContext;

    public TestUserController()
    {

        _userManagerMock = MockFactory.MockUserManager<IdentityUser>();
        var mockConfig = new Mock<IConfiguration>();
        mockConfig.Setup(x => x["ConnectionStrings:DevelopmentDB"]).Returns("testDataBase");
        var mockEnv = new Mock<IWebHostEnvironment>();
        mockEnv.Setup(x => x.EnvironmentName).Returns("Development");
        _mockDbContext = new(mockConfig.Object, mockEnv.Object);
    }
    [Fact]
    public async Task Test_GetUsers_Returns_Ok()
    {
        // TODO: Arrange the mocked db and userManager here
        // Arrange
        var UserSet = MockFactory.CreateMockSet<User>([
            new(){
                UserName = "Test",
            },
            new() {UserName = "Test1"}
            ]);
        _mockDbContext.Setup(m => m.Users).Returns(UserSet.Object);
        UserController controller = new(_mockDbContext.Object, manager: _userManagerMock.Object);
        // Act
        var result = await controller.GetUsers();
        // Assert
        Assert.Equal(200, (result.Result as ObjectResult).StatusCode);
        Assert.NotNull(result);
    }

}
