using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.Models
{
    public class UserInfo
    {
        public string DocumentType { get; set; }
        public string Document { get; set; }
        public string MobilePhone { get; set; }
        public string Captcha { get; set; }
    }
}
