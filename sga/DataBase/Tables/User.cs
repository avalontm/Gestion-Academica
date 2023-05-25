using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("usuarios")]
    public class User : TableBase
    {
        [PrimaryKey]
        [HidenField]
        [JsonIgnore]
        public int id { set; get; }

        [OnlyRead]
        [JsonIgnore]
        [HidenField]
        public DateTime created_at { set; get; }

        [OnlyRead]
        [HidenField]
        [JsonIgnore]
        public DateTime updated_at { set; get; }

        [ParentField("roles_usuarios")]
        [DisplayField("Acceso")]
        public int role_id { set; get; }

        public string? nombre { set; get; }

        public string? email { set; get; }

        [JsonIgnore]
        [PasswordField]
        public string? password { set; get; }

        [FileField]
        public string? avatar { set; get; }

        [DisplayField("Numero de control")]
        public string? codigo { set; get; }

        [HidenField]
        [JsonIgnore]
        public bool eliminado { set; get; }

        //Propiedades de api
        [HidenField]
        [FieldOmite]
        public string? rol_nombre { set; get; }


        public static List<User> Get(int limit = 100)
        {
            return MYSQL.Query<User>($"SELECT u.*, r.nombre as rol_nombre FROM usuarios AS u INNER JOIN roles_usuarios AS r ON r.rol_id=u.role_id WHERE u.eliminado='0' ORDER BY u.id DESC LIMIT {limit}");
        }

        public static User? Get(string email, string password)
        {
            return MYSQL.Query<User>($"SELECT * FROM usuarios WHERE email='{email}' AND password='{password}' LIMIT 1").FirstOrDefault();
        }

        public static User? GetByEmail(string email)
        {
            return MYSQL.Query<User>($"SELECT * FROM usuarios WHERE email='{email}' LIMIT 1").FirstOrDefault();
        }


        public static User? Find(int user_id)
        {
            return MYSQL.Query<User>($"SELECT u.*, r.nombre as rol_nombre FROM usuarios AS u INNER JOIN roles_usuarios AS r ON r.rol_id=u.role_id WHERE u.id='{user_id}' LIMIT 1").FirstOrDefault();
        }

        public static List<User> Filter(string filter, int LIMIT = 100)
        {
            return MYSQL.Query<User>($"SELECT u.*, r.nombre AS rol_nombre FROM usuarios AS u INNER JOIN roles_usuarios AS r ON u.role_id=r.rol_id WHERE u.nombre LIKE '%{filter}%' AND u.eliminado='0' ORDER BY u.id DESC LIMIT {LIMIT}");
        }
    }
}
