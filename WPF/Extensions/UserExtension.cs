using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF.MVVM.Model;

namespace WPF.Extensions
{
    public static class UserExtension
    {
        public static string SerializeData(this User user)
        {
            return $"{user.Username}-{user.Password}-{user.AuthenticationToken}-{user.RefreshToken}";
        }
    }
}
