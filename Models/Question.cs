using System;

namespace LandingPreferenciasBack.Models
{
    public class Question
    {
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
    }
}
