using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.Models
{
    public class ResponseGenerateToken
    {
         public string token { get; set;  }
         public DateTime expiracion { get; set; }

    }
}
