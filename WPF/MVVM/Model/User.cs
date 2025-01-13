using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF.MVVM.Model
{
    public class User
    {
        public string AuthenticationToken { get; set; }
        public string RefreshToken { get; set; }
        public List<OAuthAccount> OAuthAccounts { get; set; }
    }
}
