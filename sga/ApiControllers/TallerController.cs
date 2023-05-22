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
    public class TallerController : ControllerBase
    {
        [HttpGet("{curso_code}")]
        public string Get(string curso_code)
        {
            List<Taller> talleres = null;

            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                return JsonConvert.SerializeObject(talleres);
            }

            Curso curso = Curso.Find(curso_code);

            if(curso == null)
            {
                return JsonConvert.SerializeObject(talleres);
            }

            talleres = Taller.Get(curso.id);

            return JsonConvert.SerializeObject(talleres);
        }
    }
}
