using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.DataAccess;

namespace WebApi_LandingPreferencias.Logic
{
    public class ValidateCaptcha
    {

        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public ValidateCaptcha(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public void eliminarCaptcha()
        {
            try
            {
                new DataCaptcha(context, config).LimpiarTablaCaptcha();

            }

            catch (InvalidOperationException ex)
            {
                new SaveLog().Log("Error  " + ex);
            }

        }

        public void eliminarCaptcha(string captcha)
        {
            try
            {
                new DataCaptcha(context, config).EliminarCaptcha(captcha);
            }
            catch (InvalidOperationException ex)
            {
                new SaveLog().Log("Error " + ex);
            }

        }

        public bool validateCaptcha(string captcha)
        {
            bool respuesta = false;
            respuesta = new DataCaptcha(context, config).ConsultarCaptcha(captcha);

            return respuesta;
        }
    }
}