using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;

namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogInteraccionesController : Controller
    {
        #region MetodoConstructor
        private HttpContext context;
        private IConfiguration config;

        public LogInteraccionesController(HttpContext context, IConfiguration config)
        {
            this.context = context;
            this.config = config;
        }
        #endregion

        public IActionResult Index()
        {
            return Json(new { controlador = "LogInteraccionesClientes" });
        }

        [HttpPost]
        [Route("addLogInteracciones/{etapa}")]
        public IActionResult addLogInteracciones(LogInteraccionesClientes logInteraccionesClientes)
        {
            bool log = new SaveLogInteraccionesClientes(Request.HttpContext, config).addLogInteraccionesClientes(logInteraccionesClientes);

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
