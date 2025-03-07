using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model.DTOs.Output
{
    public sealed record UserDTO
    {
        public string id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
    }
}
