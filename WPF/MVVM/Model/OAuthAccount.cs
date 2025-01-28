using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace WPF.MVVM.Model
{
    public class OAuthAccount
    {
        public string ServiceName { get; set; } = "";
        public string AccountName { get; set; } = "";
        public string AccessToken { get; set; } = "";
        public string RefreshToken { get; set; } = "";
    }
}
