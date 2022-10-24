namespace WebApi_LandingPreferencias.Models
{
    /// <summary>
    /// Clase sin métodos, en la que se definen los atributos que se emplean para las respuestas genéricas de la aplicación.
    /// </summary>
    public class AppResponse
    {
        public bool State { get; set; }
        public string Exception { get; set; }
        public string Msg { get; set; }
        public object Data { get; set; }

        public AppResponse()
        {

        }
    }
}