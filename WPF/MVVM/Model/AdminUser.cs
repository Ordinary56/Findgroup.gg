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
    public class AdminUser
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
