using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using WebApi_LandingPreferencias.DataAccess;

namespace WebApi_LandingPreferencias.Logic
{
    public class BloqueoUser
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public BloqueoUser(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion



        public bool validUserBlock(string cedula)
        {
            try {
                DataBloqueoUsuarios bloqueo = new DataBloqueoUsuarios(context,config);
                return bloqueo.ValidBloqueoUser(cedula);
            }
            catch
            {
                return false;

            }
        }

        public bool AddUserBlock(string cedula)
        {
            try
            {
                DataBloqueoUsuarios bloqueo = new DataBloqueoUsuarios(context, config);
                return bloqueo.addBlockUser(cedula);
            }
            catch
            {
                return false;

            }
        }


    }
}
