using System;

namespace WebApi_LandingPreferencias.Models
{
    public class LogInteraccionesClientes
    {
        public int ID { get; set; }
        public string INTERACCION { get; set; }
        public string CODIGO_CLIENTE { get; set; }
        public string CODIGO_CUENTA { get; set; }
        public string NUMERO_SERVICIO { get; set; }
        public DateTime FECHA_INICIO { get; set; }
        public DateTime FECHA_FIN { get; set; }
    }
}