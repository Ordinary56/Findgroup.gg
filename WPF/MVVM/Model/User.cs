using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace WPF.MVVM.Model
{
    [Serializable]
    public class User
    {
        public string Username { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        
        public string AuthenticationToken { get; set; }
        public string RefreshToken { get; set; }
        [JsonIgnore]
        public List<OAuthAccount> OAuthAccounts { get; set; }
    }
}
