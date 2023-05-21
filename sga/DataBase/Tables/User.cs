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
        public DateTime created_at { set; get; }
        [OnlyRead]
        [HidenField]
        [JsonIgnore]
        public DateTime updated_at { set; get; }
        [ParentField("roles_usuarios")]
        public int role_id { set; get; }
        public string? nombre { set; get; }
        public string? email { set; get; }
        [JsonIgnore]
        [PasswordField]
        public string? password { set; get; }
        [FileField]
        public string? avatar { set; get; }

        public static User Get(string email, string password)
        {
            return MYSQL.Query<User>($"SELECT * FROM usuarios WHERE email='{email}' AND password='{password}' LIMIT 1").FirstOrDefault();
        }

        public static User Find(int user_id)
        {
            return MYSQL.Query<User>($"SELECT * FROM usuarios WHERE id='{user_id}' LIMIT 1").FirstOrDefault();
        }
    }
}
