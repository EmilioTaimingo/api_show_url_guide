using api_show_url_guide.Commands;
using api_show_url_guide.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;


namespace api_show_url_guide.Controllers
{
    public class MuestraURLController : ApiController
    {
        [HttpPost]
        public Reply Muestra([FromBody] Response guia_paquete)
        {
            obtenerURLCommands url = new obtenerURLCommands();
            var respuesta = url.muestraURL(guia_paquete);
            return respuesta;
        }
    }
}