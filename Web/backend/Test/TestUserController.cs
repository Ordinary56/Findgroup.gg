namespace Test;
using Findgroup_Backend.Controllers;
using Findgroup_Backend.Data;
using Findgroup_Backend.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Moq;
public class TestUserController
{
    private readonly UserController _userController;
    private readonly Mock<ApplicationDbContext> _appMock;
    private readonly Mock<UserManager<IdentityUser>> _mockSignInManager;

    public TestUserController()
    {
        _userController = new();        

    }
    [Fact]
    public void Test_GET_returns_Users()
    {

    }
}
