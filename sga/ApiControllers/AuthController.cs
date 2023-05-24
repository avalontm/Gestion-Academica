using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using sga.ApiModels;
using sga.DataBase.Tables;
using Ubiety.Dns.Core.Common;

namespace sga.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet]
        public string Get()
        {
            User user = this.HttpContext.GetHubUser();

            var absUrl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, user.avatar);

            user.password = string.Empty;
            user.avatar = absUrl;

            return JsonConvert.SerializeObject(user);
        }

        [HttpPost]
        public string Post(CuentaModel model)
        {
            ResponseModel response = new ResponseModel();

            User user = sga.DataBase.Tables.User.GetByEmail(model.email);

            if (user == null)
            {
                response.title = "No encontrado";
                response.message = $"El correo que ingreso no se encontro";
                return JsonConvert.SerializeObject(response);
            }


            if (user.password != model.password)
            {
                response.title = "Incorreto";
                response.message = $"La contraseña es incorrecta";
                return JsonConvert.SerializeObject(response);
            }

            var absUrl = string.Format("{0}://{1}{2}", Request.Scheme, Request.Host, user.avatar);

            user.password = string.Empty;
            user.rol_nombre = RolUser.Find(user.role_id)?.nombre ?? "Desconocido";
            user.avatar = absUrl;

            response.status = true;
            response.title = "Correcto";
            response.message = $"Iniciaste sesion correctamente";
            response.data.Add(user);
            response.data.Add(TokenManager.GenerateToken(user.id.ToString()));

            return JsonConvert.SerializeObject(response);
        }
    }
}
