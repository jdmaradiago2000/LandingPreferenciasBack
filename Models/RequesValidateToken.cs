using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.Models
{
    public class RequesValidateToken
    {

        public string Destinatario { get; set; }
        public string IdAplicacion {get ; set ;}
        public int Token { get; set; }


    }
}
