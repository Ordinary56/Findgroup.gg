using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Input
{
    public sealed record RegisterNewUserDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
