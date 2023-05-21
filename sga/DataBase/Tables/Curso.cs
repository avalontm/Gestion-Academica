using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("cursos")]
    public class Curso : TableBase
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
        public string? nombre { set; get; }

        [HidenField]
        public int user_id { set; get; }


        public static List<Curso> Get(int limit = 100)
        {
            return MYSQL.Query<Curso>($"SELECT * FROM cursos ORDER BY id LIMIT {limit}");
        }

        public static Curso FindByDocente(int user_id)
        {
            return MYSQL.Query<Curso>($"SELECT * FROM cursos WHERE user_id='{user_id}' LIMIT 1").FirstOrDefault();
        }
    }
}
