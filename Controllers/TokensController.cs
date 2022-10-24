using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.DataAccess;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;

/**
 * Autor : 
 */
namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TokensController : ControllerBase
    {

        #region MetodoConstructor
        private readonly IConfiguration config;

        public TokensController(IConfiguration config)
        {
            this.config = config;
        }
        #endregion
        [HttpGet]
        [Route("test")]
        public IActionResult resuelto()
        {

            string result = new ValidationToken(Request.HttpContext, config).test();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
                
            }
        }

        [HttpGet]
        [Route("cantidadIntentosToken")]
        public IActionResult cantidadIntentosToken() {
            string result = new ValidationToken(Request.HttpContext, config).getParametricNumberTokenAttempts();
            if (result != null){
                return Ok(result);
            }else{
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("cantidadIntentosTokenSinDigitar")]
        public IActionResult cantidadIntentosTokenSinDigitar() {
            string result = new ValidationToken(Request.HttpContext, config).getParametricNumberAttemptsUntyped();
            if (result != null){
                return Ok(result);
            }
            else{
                return StatusCode(500);
            }
        }

        [HttpGet]
        [Route("tiempoDuracionToken")]
        public IActionResult tiempoDuracionToken()
        {
            int result = new ValidationToken(Request.HttpContext, config).getParametricTimeValidateTokenMinutes();
            return Ok(result);
        }


        [HttpGet]
        [Route("tiempoDuracionTokenSinDigitar")]
        public IActionResult tiempoDuracionTokenSinDigitar()
        {
            int result = new ValidationToken(Request.HttpContext, config).getParametricTimeValidateTokenMinutesUntyped();
            return Ok(result);
        }


        [HttpGet]
        [Route("tiempoDuracionPrimerToken")]
        public IActionResult tiempoDuracionFirstToken()
        {
            int result = new ValidationToken(Request.HttpContext, config).getParametricTimeValidateTokenFirstTime();
            return Ok(result);
        }


        [HttpPost]
        [Route("generarToken")]
        public IActionResult generarToken(GenerarToken token) {

            ResponseGenerateToken tokens = new ValidationToken(Request.HttpContext, config).generarToken(token);

            if (token != null) {
                return Ok(tokens.expiracion);
            }
            else{
                return StatusCode(500);
            }
        }



        [HttpPost]
        [Route("generarPrimerToken")]
        public IActionResult generarPrimerToken(GenerarToken token)
        {

            ResponseGenerateToken tokens = new ValidationToken(Request.HttpContext, config).generarPrimerToken(token);

            if (token != null)
            {
                return Ok(tokens.expiracion);
            }
            else
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Route("generarTokenUntyped")]
        public IActionResult generarTokenUntyped(GenerarToken token)
        {

            ResponseGenerateToken tokens = new ValidationToken(Request.HttpContext, config).generarTokenUntyped(token);

            if (token != null)
            {
                return Ok(tokens.expiracion);
            }
            else
            {
                return StatusCode(500);
            }
        }



        [HttpPost]
        [Route("validarToken")]
        public IActionResult validarToken(ValidateToken token)
        {

            bool tokens = new ValidationToken(Request.HttpContext, config).validarToken(token);

            if (tokens){
                return Ok(tokens);
            }else
            {
                return Ok(false);
            }
        }


        [HttpPost]
        [Route("deshabilitarToken")]
        public IActionResult deshabilitarToken(GenerarToken token)
        {
            bool tokens = new ValidationToken(Request.HttpContext, config).desactivateTokens(token);

            if (tokens)
            {
                return Ok(tokens);
            }
            else
            {
                return Ok(false);
            }
        }

        [HttpPost]
        [Route("logToken")]
        public IActionResult addLogToken(LogInteraccionToken token)
        {
            bool tokens = new ValidationToken(Request.HttpContext, config).addLogToken(token);

            if (tokens){
                return Ok(tokens);
            }
            else
            {
                return StatusCode(500);
            }
        }

    }

}
