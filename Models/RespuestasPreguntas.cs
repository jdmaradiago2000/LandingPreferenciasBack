using System;

namespace WebApi_LandingPreferencias.Models
{
    public class RespuestasPreguntas
    {
        public int id { get; set; }
        public int codigo_pregunta { get; set; }
        public int etapa { get; set; }
        public bool estado_pregunta { get; set; }
        public int orden { get; set; }
        public string descripcion { get; set; }
        public string tipo_pregunta { get; set; }
        public Nullable<int> dependencia { get; set; }
        public int codigo_respuesta { get; set; }
        public string tipo_respuesta { get; set; }
        public string respuesta_1 { get; set; }
        public string respuesta_2 { get; set; }
        public string respuesta_3 { get; set; }
        public string respuesta_4 { get; set; }
        public string respuesta_5 { get; set; }
        public string respuesta_pregunta_1 { get; set; }
        public string respuesta_pregunta_2 { get; set; }
        public string respuesta_pregunta_3 { get; set; }
        public string respuesta_pregunta_4 { get; set; }
        public string respuesta_pregunta_5 { get; set; }
        public string contadora_anterior { get; set; }
        public string contadora_siguiente { get; set; }
    }
}
