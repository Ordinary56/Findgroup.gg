using Findgroup_Backend.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Backend_Unit_Test.Mocks
{
    public static class MockFactory
    {
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser: class
        {
            var store = new Mock<IUserStore<TUser>>();
            var userManagerMock = new Mock<UserManager<TUser>>(
                store.Object,
                null,
                null,
                null,
                null,
                null,
                null,
                null,
                null);
            return userManagerMock;
        }
        public static Mock<DbSet<T>> CreateMockSet<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var mockSet = new Mock<DbSet<T>>();
            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
             // Implement IAsyncEnumerable for async LINQ support
            mockSet.As<IAsyncEnumerable<T>>().Setup(m => m.GetAsyncEnumerator(It.IsAny<CancellationToken>()))
            .Returns(new MyAsyncEnumerator<T>(queryable.GetEnumerator()));

        mockSet.As<IQueryable<T>>().Setup(m => m.Provider)
            .Returns(new IAsyncQueryProvider<T>(queryable.Provider));
            return mockSet;
        }
    }

    public class MyAsyncEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;
        public MyAsyncEnumerator(IEnumerator<T> inner) => _inner = inner;
        public T Current => _inner.Current;


        public ValueTask DisposeAsync()
        {
            _inner.Dispose();   
            return ValueTask.CompletedTask;
        }

        public ValueTask<bool> MoveNextAsync()
        {
            return new(_inner.MoveNext());
        }
    }
}
