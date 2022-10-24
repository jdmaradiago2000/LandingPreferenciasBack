using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RespuestasController : Controller
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;

        public RespuestasController(IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public IActionResult Index()
        {
            return Json(new { controlador = "respuestas" });
        }



        [HttpPost]
        [Route("addRespuestas/{etapa}")]
        public IActionResult addRespuestas(Respuestas respuestas)
        {
            bool log = new LogicaRespuestas(Request.HttpContext, config).addRespuestas(respuestas);

            if (log)
            {
                return Ok(log);
            }
            else
            {
                return StatusCode(500);
            }
        }

    }
}
