using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.DataAccess;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Logic
{
    public class SaveLogInteraccionesClientes
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public SaveLogInteraccionesClientes(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public bool addLogInteraccionesClientes(LogInteraccionesClientes logInteraccionesClientes)
        {
            DataLogInteraccionesClientes interaccionesClientesLog = new DataLogInteraccionesClientes(context, config);

            return interaccionesClientesLog.addLogInteraccionesClientesRegister(logInteraccionesClientes);
        }
    }
}