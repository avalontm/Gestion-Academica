using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("talleres")]
    public class Taller :TableBase
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

        public string? nombre { set; get; }

        [OnlyRead]
        public string? codigo { set; get; }

        [HidenField]
        public int curso_id { set; get; }

        [HidenField]
        public int user_id { set; get; }

        public bool activo { set; get; }

        [OnlyRead]
        [HidenField]
        public bool eliminado { set; get; }


        public static List<Taller> Get(int curso_id, int limit = 100)
        {
            return MYSQL.Query<Taller>($"SELECT * FROM talleres WHERE curso_id='{curso_id}' AND eliminado='0' ORDER BY created_at DESC LIMIT {limit}");
        }

        public static Taller Find(int taller_id)
        {
            return MYSQL.Query<Taller>($"SELECT * FROM talleres WHERE id='{taller_id}' AND eliminado='0' LIMIT 1").FirstOrDefault();
        }

        public static Taller Find(string taller_codigo)
        {
            return MYSQL.Query<Taller>($"SELECT * FROM talleres WHERE codigo='{taller_codigo}' AND eliminado='0' LIMIT 1").FirstOrDefault();
        }

        public static List<Taller> Filter(int curso_id, string filter, int LIMIT = 100)
        {
            return MYSQL.Query<Taller>($"SELECT * FROM talleres WHERE curso_id='{curso_id}' AND nombre LIKE '%{filter}%' AND eliminado='0' ORDER BY created_at DESC LIMIT {LIMIT}");
        }
    }
}
