using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class DataCaptcha
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public DataCaptcha(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion


        public bool GuardarCaptcha(string Captcha)
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "Ron_SP_Validacion_Captcha";
            conect.AddParameters("TRANSACCION", 1001);
            conect.AddParameters("CAPTCHA", Captcha);
            conect.AddParametersOutPut("RESPUESTA", 0, SqlDbType.VarChar, 200);
            conect.GetDataTable();
            string respuesta = conect.GetValueParameterOut("RESPUESTA").ToString();

            if (respuesta.ToLower().Contains("error"))
            {
                return false;
            }

            return true;
        }


        public bool ConsultarCaptcha(String Captcha)
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "Ron_SP_Validacion_Captcha";
            conect.AddParameters("TRANSACCION", 101);
            conect.AddParameters("CAPTCHA", Captcha);
            conect.AddParametersOutPut("RESPUESTA", 0, SqlDbType.VarChar, 200);
            DataTable data = conect.GetDataTable();

            if (data != null && data.Rows.Count > 0)
            {
                return true;
            }

            return false;
        }


        public bool EliminarCaptcha(String Captcha)
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "Ron_SP_Validacion_Captcha";
            conect.AddParameters("TRANSACCION", 1002);
            conect.AddParameters("CAPTCHA", Captcha);
            conect.AddParametersOutPut("RESPUESTA", 0, SqlDbType.VarChar, 200);
            conect.GetDataTable();
            string respuesta = conect.GetValueParameterOut("RESPUESTA").ToString();

            if (respuesta.ToLower().Contains("error"))
            {
                return false;
            }

            return true;
        }


        public bool LimpiarTablaCaptcha()
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "Ron_SP_Validacion_Captcha";
            conect.AddParameters("TRANSACCION", 1003);
            conect.AddParametersOutPut("RESPUESTA", 0, SqlDbType.VarChar, 200);
            conect.GetDataTable();
            string respuesta = conect.GetValueParameterOut("RESPUESTA").ToString();

            if (respuesta.ToLower().Contains("error"))
            {
                return false;
            }

            return true;
        }
    }
}