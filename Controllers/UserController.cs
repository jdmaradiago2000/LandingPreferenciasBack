using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : Controller
    {
        #region MetodoConstructor
        private readonly IConfiguration config;

        public UserController(IConfiguration config)
        {
            this.config = config;
        }
        #endregion

        public IActionResult Index()
        {
            return Json(new { controlador = "user" });
        }

        [HttpPost]
        [Route("ValidateUserInfo")]
        public IActionResult validateUserInfo(UserInfo user)
        {
            AppResponse appResponse = new AppResponse();
            appResponse.Msg = new ValidateUserInfo(Request.HttpContext, config).validateBasicInfo(user);
            return Ok(appResponse);
        }
    }
}
