using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;
using TorneioLuta.Services;
using ActionResult = Microsoft.AspNetCore.Mvc.ActionResult;
using Controller = Microsoft.AspNetCore.Mvc.Controller;
using HttpGetAttribute = Microsoft.AspNetCore.Mvc.HttpGetAttribute;
using RouteAttribute = System.Web.Mvc.RouteAttribute;

namespace TorneioLuta.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class TorneioLutaController : Controller
    {
        private readonly ITorneioService _torneioService;
        public TorneioLutaController(ITorneioService torneioService)
        {
            _torneioService = torneioService;
        }

        [HttpGet("GetLutadores")]
        public IActionResult GetLutadores()
        {
            try
            {
                List<LutadorModel> result = _torneioService.ListarLutadores();
                return Ok(result);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpGet("IniciarTorneio")]
        public IActionResult IniciarTorneio()
        {
            try
            {
                return Ok(_torneioService.IniciarTorneio());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}