using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


/**
 * Autor : 
 */

namespace WebApi_LandingPreferencias.Models
{
    public class RequesGenetateToken
    {
        public int CantidadDigitos { get; set; }
        public string Canal { get; set; }  
        public string Destinatario { get; set; }
        public string IdAplicacion { get; set; }
        public bool Enviar { get; set; }
        public int Duracion { get; set; }
        public string Mensaje { get; set; }


        
    }
}
