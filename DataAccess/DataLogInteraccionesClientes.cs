using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Data;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.DataAccess
{
    public class DataLogInteraccionesClientes
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public DataLogInteraccionesClientes(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public bool addLogInteraccionesClientesRegister(LogInteraccionesClientes interaccionesClientesLog)
        {
            Conect conect = new Conect(context, config);
            conect.CommandQuery = "LP_SP_LOG_INTERACCIONES_CLIENTES";
            conect.AddParameters("TRANSACCION", 1000);
            conect.AddParameters("ID", interaccionesClientesLog.ID);
            conect.AddParameters("INTERACCION", interaccionesClientesLog.INTERACCION);
            conect.AddParameters("CODIGO_CLIENTE", interaccionesClientesLog.CODIGO_CLIENTE);
            conect.AddParameters("CODIGO_CUENTA", interaccionesClientesLog.CODIGO_CUENTA);
            conect.AddParameters("NUMERO_SERVICIO", interaccionesClientesLog.NUMERO_SERVICIO);
            conect.AddParameters("FECHA_INICIO", interaccionesClientesLog.FECHA_INICIO);
            conect.AddParameters("FECHA_FIN", interaccionesClientesLog.FECHA_FIN);

            DataTable data = conect.GetDataTable();

            if (data != null && data.Rows.Count > 0)
            {
                if (data.Rows[0].ToString().Contains("ERROR"))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            return false;
        }
    }
}