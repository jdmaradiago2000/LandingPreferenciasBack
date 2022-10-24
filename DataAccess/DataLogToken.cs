using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.Models;
namespace WebApi_LandingPreferencias.DataAccess
{
    public class DataLogToken
    {

        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public DataLogToken(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion


        public bool addLogRegister(LogInteraccionToken tokenLog)
        {

            Conect conect = new Conect(context, config);
            conect.CommandQuery = "SP_LP_Log_Interacción_cliente";
            conect.AddParameters("TRANSACCION", 1001);
            conect.AddParameters("ACCION", tokenLog.Accion);
            conect.AddParameters("CEDULA_CLIENTE", tokenLog.CedulaCliente);


            DataTable data = conect.GetDataTable();

            if (data != null && data.Rows.Count > 0)
            {
                return data.Rows[0].Field<bool>(0);
            }

            return false;
        }


    }
}
