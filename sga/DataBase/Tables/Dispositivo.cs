using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("dispositivos")]
    public class Dispositivo : TableBase
    {
        [PrimaryKey]
        [HidenField]
        [JsonIgnore]
        public int id { set; get; }
        [OnlyRead]
        [HidenField]
        public DateTime created_at { set; get; }
        [OnlyRead]
        public DateTime updated_at { set; get; }
        public int user_id { set; get; }
        public string? nombre { set; get; }
        public string? platform { set; get; }
        public string? model { set; get; }
        public string? token { set; get; }

        public static List<Dispositivo> Get()
        {
            return MYSQL.Query<Dispositivo>($"SELECT * FROM dispositivos  ORDER BY id ASC");
        }

        public static List<Dispositivo> Get(int user_id)
        {
            return MYSQL.Query<Dispositivo>($"SELECT * FROM dispositivos WHERE user_id='{user_id}' ORDER BY id ASC");
        }

        public static Dispositivo Find(int user_id, string platform, string model)
        {
            return MYSQL.Query<Dispositivo>($"SELECT * FROM dispositivos WHERE user_id='{user_id}' AND platform='{platform}' AND model='{model}'").FirstOrDefault();
        }
    }
}
