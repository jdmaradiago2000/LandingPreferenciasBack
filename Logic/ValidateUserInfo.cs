using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.DataAccess;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Logic
{
    public class ValidateUserInfo
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public ValidateUserInfo(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public string validateBasicInfo(UserInfo user) 
        {
            IdsUser idsUser = new IdsUser();

            BloqueoUser userBlock = new BloqueoUser(context, config);
            //validate si el user tiene un bloqueo

            if (userBlock.validUserBlock(user.Document)) {
                return "Ha superado el número máximo de intentos del dia, por favor inténtalo mañana.";
            }
         
            if (new Inf_General().validateDocument(user.Document, out idsUser))
            {
                if (new Inf_General().validateMobileNumber(user.MobilePhone, idsUser.Cust_id.ToString()))
                {
                    //SendToken(user.MobilePhone);
                    return "Se realizará el envio del token/" + idsUser.Cust_id + "/" + idsUser.Acct_id;
                }
                else
                {
                    return new ParametricMessages(context, config).GetMessage("Celular_Invalido");
                }
            }
            else 
            {
                return new ParametricMessages(context, config).GetMessage("Documento_Invalido");
            }

            return "En este momento no es posible realizar la solicitud intentelo más tarde, Gracias!";
        }
    }
}
