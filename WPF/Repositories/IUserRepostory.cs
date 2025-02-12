using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;

namespace WPF.Repositories
{
    public interface IUserRepostory
    {
        public IAsyncEnumerable<User> GetUsers();

        public Task DeleteUser(User user);
        public Task ModifyUser(User modifiedUser);
        public Task CreateNew(User newUser);

    }
}
