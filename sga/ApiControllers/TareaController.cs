using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sga.DataBase.Tables;

namespace sga.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return string.Empty;
        }

        [HttpGet("taller_code")]
        public string Get(string taller_code)
        {
            List<Tarea> tareas = null;

            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                return JsonConvert.SerializeObject(tareas);
            }

            Taller taller = Taller.Find(taller_code);

            if (taller == null)
            {
                return JsonConvert.SerializeObject(tareas);
            }

            tareas = Tarea.Get(taller.id);

            return JsonConvert.SerializeObject(tareas);
        }
    }
}
