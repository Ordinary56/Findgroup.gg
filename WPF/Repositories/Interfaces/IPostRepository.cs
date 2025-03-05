using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model.DTOs.Input;
using WPF.MVVM.Model.DTOs.Output;

namespace WPF.Repositories.Interfaces
{
    public interface IPostRepository
    {
        public IAsyncEnumerable<PostDTO> GetPost();

        public Task DeletePost(PostDTO post);
        public Task ModifyPost(RegisterNewPostDTO  modifiedPost);

    }
}
