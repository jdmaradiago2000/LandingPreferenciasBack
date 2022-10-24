using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Data;


namespace WebApi_LandingPreferencias.DataAccess
{
    public class DataBloqueoUsuarios
    {
        #region
        private HttpContext context;
        private IConfiguration config;

        public DataBloqueoUsuarios(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public Boolean ValidBloqueoUser(string cedula)
        {
            try
            {

                Conect conect = new Conect(context, config);
                conect.CommandQuery = "LP_SP_Administrar_Usuarios_Bloqueados";
                conect.AddParameters("TRANSACCION", 101);
                conect.AddParameters("CEDULA", cedula);
                DataTable data = conect.GetDataTable();

                if (data != null && data.Rows.Count > 0)
                {
                    return data.Rows[0].Field<Boolean>(0);
                }
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return false;

        }


        public Boolean addBlockUser(string cedula)
        {
            try
            {

                Conect conect = new Conect(context, config);
                conect.CommandQuery = "LP_SP_Administrar_Usuarios_Bloqueados";
                conect.AddParameters("TRANSACCION", 1001);
                conect.AddParameters("CEDULA", cedula);
                DataTable data = conect.GetDataTable();

                if (data != null && data.Rows.Count > 0)
                {
                    return data.Rows[0].Field<Boolean>(0);
                }
            }
            catch (ArgumentNullException)
            {
                return false;
            }
            catch (InvalidOperationException)
            {
                return false;
            }
            return false;

        }




    }
}
