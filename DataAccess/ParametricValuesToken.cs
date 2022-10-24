using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Data;


namespace WebApi_LandingPreferencias.DataAccess
{
    public class ParametricValuesToken
    {

        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public ParametricValuesToken(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion


        public string GetParametrico(string identificador)
        {
            try
            {

                Conect conect = new Conect(context, config);
                conect.CommandQuery = "LP_SP_Administrar_Preguntas_Landing_Preferencias";
                conect.AddParameters("TRANSACCION", 108);
                conect.AddParameters("PARAMETRICO_", identificador);
                DataTable data = conect.GetDataTable();

                if (data != null && data.Rows.Count > 0)
                {
                    return data.Rows[0].Field<string>(0);
                }

            }
            catch (ArgumentNullException)
            {
                return null;
            }
            catch (InvalidOperationException)
            {
                return null;
            }
            return null;

        }
        

    }
}
