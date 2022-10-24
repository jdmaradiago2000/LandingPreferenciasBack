using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;
using WebApi_LandingPreferencias.DataAccess;

namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class QuestionController : Controller
    {
        #region MetodoConstructor
        private readonly IConfiguration config;

        public QuestionController(IConfiguration config)
        {
            this.config = config;
        }
        #endregion

        public IActionResult Index()
        {
            return Json(new { controlador = "question" });
        }

        [HttpGet]
        [Route("getQuestion")]
        public IActionResult getQuestion()
        {
            AppResponse appResponse = new AppResponse();

            return Ok(new Questions(Request.HttpContext, config).GetQuestions());
            
        }

        //[HttpGet]
        //[Route("getQuestion/{etapa}")]
        //public IActionResult getQuestionStageOriginal(int etapa)
        //{
        //    AppResponse appResponse = new AppResponse();

        //    return Ok(new Questions(Request.HttpContext, config).GetQuestionStage(etapa));

        //}

        [HttpGet]
        [Route("getQuestion/{etapa}")]
        public IActionResult getQuestionStage(int etapa)
        {
            AppResponse appResponse = new AppResponse();

            return Ok(new Questions(Request.HttpContext, config).GetQuestionStage(etapa));

        }


        [HttpGet]
        [Route("getQuestionPrevious/{codigo_pregunta}")]
        public IActionResult getQuestionPrevious(int codigo_pregunta)
        {
            AppResponse appResponse = new AppResponse();

            return Ok(new Questions(Request.HttpContext, config).GetQuestionPrevious(codigo_pregunta));

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


        [HttpPost]
        [Route("updateRespuestas/{etapa}")]
        public IActionResult updateRespuestas(Respuestas respuestas)
        {
            bool log = new LogicaRespuestas(Request.HttpContext, config).updateRespuestas(respuestas);

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
