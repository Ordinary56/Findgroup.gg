using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;
using WPF.MVVM.Model.DTOs.Input;
using WPF.MVVM.Model.DTOs.Output;

namespace WPF.Repositories
{
    public interface IUserRepostory
    {
        public IAsyncEnumerable<UserDTO> GetUsers();

        public Task DeleteUser(UserDTO user);
        public Task ModifyUser(RegisterNewUserDTO modifiedUser);
        public Task CreateNew(RegisterNewUserDTO newUser);

    }
}
