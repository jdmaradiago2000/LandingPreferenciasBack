using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.DataAccess;
using WebApi_LandingPreferencias.Models;


namespace WebApi_LandingPreferencias.Logic
{
    public class ValidationToken
    {



        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;
        public ValidationToken(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        private string getParametric(string identificador) {

            ParametricValuesToken parametric = new ParametricValuesToken(context, config);


            string result = parametric.GetParametrico(identificador);

            if (result != null) {
                return result;
            }
            return null;

        }

        public ResponseGenerateToken generarToken(GenerarToken generarToken) {

            RequesGenetateToken token = new RequesGenetateToken();
            token.Canal = "SMS";
            token.CantidadDigitos = 4;
            token.Destinatario = generarToken.destinatario;
            token.IdAplicacion = "1AC649E4-9C83-4EBF-95C1-8A15FA12D17C";
            token.Enviar = true;
            token.Duracion = getParametricTimeValidateTokenMinutes();
            token.Mensaje = getMessageSmsToken();

            Task<ResponseGenerateToken> responseToken = new DataToken().generarToken(token);

            if (responseToken != null)
            {
                return responseToken.Result;
            }
            else {
                return null;
            }

        }



        public bool addLogToken(LogInteraccionToken logToken){

            DataLogToken tokenLog = new DataLogToken(context, config);

            return tokenLog.addLogRegister(logToken);
        }

        public ResponseGenerateToken generarTokenUntyped(GenerarToken generarToken)
        {

            RequesGenetateToken token = new RequesGenetateToken();
            token.Canal = "SMS";
            token.CantidadDigitos = 4;
            token.Destinatario = generarToken.destinatario;
            token.IdAplicacion = "1AC649E4-9C83-4EBF-95C1-8A15FA12D17C";
            token.Enviar = true;
            token.Duracion = getParametricTimeValidateTokenMinutesUntyped();
            token.Mensaje = getMessageSmsToken();

            Task<ResponseGenerateToken> responseToken = new DataToken().generarToken(token);

            if (responseToken != null)
            {
                return responseToken.Result;
            }
            else
            {
                return null;
            }

        }



        public ResponseGenerateToken generarPrimerToken(GenerarToken generarToken){

            RequesGenetateToken token = new RequesGenetateToken();
            token.Canal = "SMS";
            token.CantidadDigitos = 4;
            token.Destinatario = generarToken.destinatario;
            token.IdAplicacion = "1AC649E4-9C83-4EBF-95C1-8A15FA12D17C";
            token.Enviar = true;
            token.Duracion = getParametricTimeValidateTokenFirstTime();
            token.Mensaje = getMessageSmsToken();

            Task<ResponseGenerateToken> responseToken = new DataToken().generarToken(token);

            if (responseToken != null)
            {
                return responseToken.Result;
            }
            else
            {
                return null;
            }

        }


        public bool validarToken(ValidateToken validarToken)
        {

            RequesValidateToken validateToken = new RequesValidateToken();

            validateToken.Destinatario = validarToken.Destinatario;
            validateToken.Token = validarToken.token;
            validateToken.IdAplicacion = "1AC649E4-9C83-4EBF-95C1-8A15FA12D17C";
            Task<Boolean> responseToken = new DataToken().validarToken(validateToken);
            if (responseToken != null)
            {
                return responseToken.Result;
            }
            else
            {
                return false;
            }

        }

        public bool desactivateTokens(GenerarToken generarToken)
        {

            DesactiveToken tokenDesactive = new DesactiveToken();

            tokenDesactive.Destinatario = generarToken.destinatario;
            tokenDesactive.IdAplicacion = "1AC649E4-9C83-4EBF-95C1-8A15FA12D17C";


            Task<Boolean> responseToken = new DataToken().desactivarTokens(tokenDesactive);



            if (responseToken != null)
            {
                return responseToken.Result;
            }
            else
            {
                return false;
            }

        }




        public string test() {
            return  getParametric("MENSAJE_SMS_TOKEN");
            
        }



        private string getParametricDaysInability() {

            return getParametric("DIAS_INHABILIDAD");
        }


        public string getParametricNumberTokenAttempts()
        {
            return getParametric("CANTIDAD_INTENTOS_TOKEN");
        }


        public string getParametricNumberAttemptsUntyped() {

            return getParametric("INTENTOS_TOKEN_SIN_DIGITAR");
        }

        public int getParametricTimeValidateTokenMinutes() {
            return int.Parse(getParametric("TIEMPO_INGRESO_TOKEN"));
        
        }
        public int getParametricTimeValidateTokenMinutesUntyped()
        {
            return int.Parse(getParametric("TIEMPO_TOKEN_SIN_DIGITAR"));

        }

        public int getParametricTimeValidateTokenFirstTime()
        {
            return int.Parse(getParametric("TIEMPO_TOKEN_INICIAL"));

        }

        private string getMessageSmsToken() {
            return getParametric("MENSAJE_SMS_TOKEN");
        }






    }
}
