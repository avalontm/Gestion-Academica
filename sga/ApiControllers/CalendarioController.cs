using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sga.DataBase.Tables;
using System.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace sga.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarioController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            List<Tarea> tareas = null;

            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                return JsonConvert.SerializeObject(tareas);
            }

            tareas = Tarea.GetByDate(DateTime.Now);

            return JsonConvert.SerializeObject(tareas);
        }

        [HttpGet("{date_sting}")]
        public string Get(string date_sting)
        {
            List<Tarea> tareas = null;

            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                return JsonConvert.SerializeObject(tareas);
            }

            try
            {
                DateTime date = DateTime.Parse(date_sting);

                tareas = Tarea.GetByDate(date);
            }
            catch
            {
                tareas = null;
            }


            return JsonConvert.SerializeObject(tareas);
        }
    }
}
