using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using WebApi_LandingPreferencias.DataAccess;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CaptchaController : Controller
    {
        #region MetodoConstructor
        private readonly IConfiguration config;

        public CaptchaController(IConfiguration config)
        {
            this.config = config;
        }
        #endregion

        public IActionResult Index()
        {
            return Json(new { controlador = "captcha" });
        }




        [HttpGet]
        [Route("generarCaptcha")]
        public IActionResult generarCaptcha()
        {

            new ValidateCaptcha(Request.HttpContext, config).eliminarCaptcha();
            string error = string.Empty;
            AppResponse appResponse = new AppResponse();

            CaptchaVpap.SupercatchClient GetInfo = new CaptchaVpap.SupercatchClient();
            CaptchaVpap.GetCaptchaResponse objCaptcha = new CaptchaVpap.GetCaptchaResponse();
            CaptchaVpap.GetCaptchaRequest request = new CaptchaVpap.GetCaptchaRequest();
            request.sender = "superelkin";
            request.sError = error;

            objCaptcha = GetInfo.GetCaptchaAsync(request).Result;
            CaptchaInfo captcha = new CaptchaInfo();
            captcha.objCaptcha = objCaptcha.GetCaptchaResult;
            captcha.fecha = DateTime.Now;

            new DataCaptcha(Request.HttpContext, config).GuardarCaptcha(captcha.objCaptcha.ValorCaptcha);
            
            appResponse.Data = Convert.ToBase64String(objCaptcha.GetCaptchaResult.imagen);
            appResponse.State = (string.IsNullOrEmpty(error)) ? true : false;
            appResponse.Exception = error;

            return Ok(appResponse);
        }

        [HttpGet]
        [Route("validateCaptcha/{captcha}")]
        public IActionResult validateCaptcha(string captcha)
        {
            bool valid = new ValidateCaptcha(Request.HttpContext, config).validateCaptcha(captcha);
            return Ok(valid);
        }


        



    }
}
