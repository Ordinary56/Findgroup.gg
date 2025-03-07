using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Input
{
    public sealed record RegisterNewUserDTO
    {
        public string id { get; set; } = Guid.NewGuid().ToString();
        public string userName { get; set; }
        public string password { get; set; }
        public string email { get; set; }
    }
}
