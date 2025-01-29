using Findgroup_Backend.Data.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Unit_Test.Mock
{
    public static class MockUserRepository
    {
        public static Mock<IUserRepository> CreateMock()
        {
            Mock<IUserRepository> mockUserRepository = new();
            return mockUserRepository;
        }
    }
}
