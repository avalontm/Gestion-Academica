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
    public class CursoController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            List<Curso> cursos = null;

            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                return JsonConvert.SerializeObject(cursos);
            }

            cursos = Curso.Get();

            foreach(var curso in cursos)
            {
                var _user = sga.DataBase.Tables.User.Find(curso.user_id);
                if (_user != null)
                {
                    curso.docente_nombre = _user.nombre;
                }
            }

            return JsonConvert.SerializeObject(cursos);
        }
    }
}
