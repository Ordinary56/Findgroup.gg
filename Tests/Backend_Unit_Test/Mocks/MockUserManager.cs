using Findgroup_Backend.Models;
using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Unit_Test.Mocks
{
    internal class MockUserManager : Mock<UserManager<User>>
    {
    }
}
