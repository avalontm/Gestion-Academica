using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("tareas_usuarios")]
    public class TareaUser : TableBase
    {
        [HidenField]
        [PrimaryKey]
        public int id { set; get; }

        [HidenField]
        [OnlyRead]
        public DateTime created_at { set; get; }

        [OnlyRead]
        public DateTime updated_at { set; get; }

        [HidenField]
        public int curso_id { set; get; }

        [HidenField]
        public int taller_id { set; get; }

        [HidenField]
        public int tarea_id { set; get; }

        [HidenField]
        public int user_id { set; get; }

        public int calificacion { set; get; }

        public bool calificado { set; get; }

        [HidenField]
        [JsonIgnore]
        public bool eliminado { set; get; }

        //Propiedades de la api
        [HidenField]
        [FieldOmite]
        public string? nombre { set; get; }

        public static List<TareaUser> Get(int tarea_id, int limit = 100)
        {
            return MYSQL.Query<TareaUser>($"SELECT * FROM tareas_usuarios WHERE tarea_id='{tarea_id}' AND eliminado='0' LIMIT {limit}");
        }

        public static TareaUser Find(int tarea_id, int user_id)
        {
            return MYSQL.Query<TareaUser>($"SELECT * FROM tareas_usuarios WHERE tarea_id='{tarea_id}' AND user_id='{user_id}' AND eliminado='0' LIMIT 1").FirstOrDefault();
        }

        public static List<TareaUser> Filter(int tarea_id, string filter, int LIMIT = 100)
        {
            return MYSQL.Query<TareaUser>($"SELECT * FROM tareas_usuarios as t INNER JOIN usuarios as u ON u.id=t.user_id WHERE t.tarea_id='{tarea_id}' AND u.nombre LIKE '%{filter}%' AND t.eliminado='0' ORDER BY t.updated_at DESC LIMIT {LIMIT}");
        }
    }
}
