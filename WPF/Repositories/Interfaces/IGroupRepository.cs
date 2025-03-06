using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model.DTOs.Input;
using WPF.MVVM.Model.DTOs.Output;

namespace WPF.Repositories.Interfaces
{
    internal interface IGroupRepository
    {
        public IAsyncEnumerable<GroupDTO> GetGroups();

        public Task DeleteGroup(GroupDTO group);
        public Task ModifyGroup(CreateNewGroupDTO modifiedGroup);
    }
}
