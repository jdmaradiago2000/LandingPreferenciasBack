using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using WebApi_LandingPreferencias.DataAccess;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Logic
{
    public class LogicaRespuestas
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public LogicaRespuestas(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public bool addRespuestas(Respuestas respuestas)
        {
            DataRespuestas dataRespuestas = new DataRespuestas(context, config);

            return dataRespuestas.addRespuestasRegister(respuestas);
        }

        public bool updateRespuestas(Respuestas respuestas)
        {
            DataRespuestas dataRespuestas = new DataRespuestas(context, config);

            return dataRespuestas.updateRespuestasRegister(respuestas);
        }

        public List<Respuestas> ObtenerRespuestasPorId(int ID)
        {
            DataRespuestas dataRespuestas = new DataRespuestas(context, config);

            var respuestas = dataRespuestas.ObtenerRespuestasPorId(ID);

            return respuestas;
        }

        public List<Respuestas> ObtenerRespuestasPorCodigoCliente(string CODIGO_CLIENTE)
        {
            DataRespuestas dataRespuestas = new DataRespuestas(context, config);

            var respuestas = dataRespuestas.ObtenerRespuestasPorCodigoCliente(CODIGO_CLIENTE);

            return respuestas;
        }

        public List<Respuestas> ObtenerRespuestasPorCodigoClienteYCodigoPregunta(string CODIGO_CLIENTE, int CODIGO_PREGUNTA)
        {
            DataRespuestas dataRespuestas = new DataRespuestas(context, config);

            var respuestas = dataRespuestas.ObtenerRespuestasPorCodigoClienteYCodigoPregunta(CODIGO_CLIENTE, CODIGO_PREGUNTA);

            return respuestas;
        }
    }
}
