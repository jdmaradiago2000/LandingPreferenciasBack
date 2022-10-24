using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class ParametricMessages
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public ParametricMessages(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion


        public string GetMessage(string alias)
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_Consultar_Mensajes_Parametricos";
            conect.AddParameters("TRANSACCION", alias);

            DataTable data = conect.GetDataTable();

            if (data != null && data.Rows.Count > 0)
            {
                return data.Rows[0].Field<string>(0);
            }

            return "En este momento no es posible realizar la solicitud intentelo más tarde, Gracias!";
        }

    }
}
