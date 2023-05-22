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
        public DateTime created_at { set; get; }

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
        public bool eliminado { set; get; }


        public static TareaUser Find(int tarea_id, int user_id)
        {
            return MYSQL.Query<TareaUser>($"SELECT * FROM tareas_usuarios WHERE tarea_id='{tarea_id}' AND user_id='{user_id}' AND eliminado='0' LIMIT 1").FirstOrDefault();
        }
    }
}
