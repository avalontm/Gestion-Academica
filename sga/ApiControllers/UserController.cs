using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PluginSQL;
using sga.ApiModels;
using sga.DataBase.Tables;
using sga.Models;

namespace sga.ApiControllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    { // GET: api/<UserController>
        [HttpGet]
        public string Get()
        {
            return null;
        }

        // GET api/<UserController>/5
        [HttpGet("device/{id}")]
        public string DeviceGet(int id)
        {
            ResponseModel response = new ResponseModel();

            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                return JsonConvert.SerializeObject(null);
            }

            List<Dispositivo> devices = Dispositivo.Get(user.id);

            return JsonConvert.SerializeObject(devices);
        }

        // POST api/<UserController>
        [HttpPost("device")]
        public string DevicePost([FromBody] PostModel model)
        {
            ResponseModel response = new ResponseModel();
            User user = this.HttpContext.GetHubUser();

            if (user == null)
            {
                response.title = "Sin sesion";
                response.message = $"Debes iniciar sesion primero";
                return JsonConvert.SerializeObject(response);
            }

            if (model == null || model.data.Count == 0 || model.data.Count < 4)
            {
                response.title = "Error";
                response.message = $"faltan datos";
                return JsonConvert.SerializeObject(response);
            }

            string name = model.data[0].ToString();
            string phone_model = model.data[1].ToString();
            string platform = model.data[2].ToString();
            string token = model.data[3].ToString();

            if (string.IsNullOrEmpty(name))
            {
                response.title = "Requerido";
                response.message = $"Se requiere un nombre";
                return JsonConvert.SerializeObject(response);
            }
            if (string.IsNullOrEmpty(phone_model))
            {
                response.title = "Requerido";
                response.message = $"Se requiere un modelo de telefono";
                return JsonConvert.SerializeObject(response);
            }
            if (string.IsNullOrEmpty(platform))
            {
                response.title = "Requerido";
                response.message = $"Se requiere la plataforma del telefono";
                return JsonConvert.SerializeObject(response);
            }
            if (string.IsNullOrEmpty(token))
            {
                response.title = "Requerido";
                response.message = $"Se requiere un token";
                return JsonConvert.SerializeObject(response);
            }

            Dispositivo device = Dispositivo.Find(user.id, platform, phone_model);

            if (device == null)
            {
                device = new Dispositivo();
                device.created_at = DateTime.Now;
                device.updated_at = DateTime.Now;
                device.user_id = user.id;
                device.nombre = name;
                device.platform = platform;
                device.model = phone_model;
                device.token = token;

                if (device.Insert())
                {
                    response.status = true;
                    response.title = "Dispositivo Nuevo";
                    response.message = $"Se ha registrado un nuevo dispositivo";
                    return JsonConvert.SerializeObject(response);
                }
            }
            else
            {
                device.updated_at = DateTime.Now;
                device.user_id = user.id;
                device.nombre = name;
                device.platform = platform;
                device.model = phone_model;
                device.token = token;

                if (device.Update())
                {
                    response.status = true;
                    response.title = "Dispositivo Actualiado";
                    response.message = $"Se ha actualizado el dispositivo";
                    return JsonConvert.SerializeObject(response);
                }
            }

            response.title = "Error";
            response.message = $"No se pudo registrar el dispositivo";
            return JsonConvert.SerializeObject(response);
        }

        // PUT api/<UserController>/5
        [HttpPut("device/{id}")]
        public void DevicePut(int id, [FromBody] PostModel model)
        {

        }

        // DELETE api/<UserController>/5
        [HttpDelete("device/{id}")]
        public void DeviceDelete(int id)
        {
        }
    }
}
