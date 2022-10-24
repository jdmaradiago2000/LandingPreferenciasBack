using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi_LandingPreferencias.Logic;
using WebApi_LandingPreferencias.Models;
using Microsoft.Extensions.Configuration;

namespace WebApi_LandingPreferencias.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BloqueoController : ControllerBase
    {

        #region MetodoConstructor
        private readonly IConfiguration config;

        public BloqueoController(IConfiguration config)
        {
            this.config = config;
        }
        #endregion



        [HttpPost]
        [Route("blockUser")]
        public IActionResult addBloqueoUser(UserBlock block){


            bool result = new BloqueoUser(Request.HttpContext, config).AddUserBlock(block.cedula);

            if (result)
            {
                return Ok(result);
            }
            else
            {
                return StatusCode(500);
            }
        }


    }
}
