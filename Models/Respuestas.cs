using System;

namespace WebApi_LandingPreferencias.Models
{
    public class Respuestas
    {
        public int ID { get; set; }
        public string CODIGO_CLIENTE { get; set; }
        public string CODIGO_CUENTA { get; set; }
        public int CODIGO_PREGUNTA { get; set; }
        public int CODIGO_RESPUESTA { get; set; }
        public string RESPUESTA_1 { get; set; }
        public string RESPUESTA_2 { get; set; }
        public string RESPUESTA_3 { get; set; }
        public string RESPUESTA_4 { get; set; }
        public string RESPUESTA_5 { get; set; }
        public DateTime FECHA_HORA { get; set; }
    }
}