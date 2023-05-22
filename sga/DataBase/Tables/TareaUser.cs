using PluginSQL;

namespace sga.DataBase.Tables
{
    [TableName("tareas_usuarios")]
    public class TareaUser : TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public DateTime created_at { set; get; }
        public DateTime updated_at { set; get; }
        public int curso_id { set; get; }
        public int taller_id { set; get; }
        public int tarea_id { set; get; }
        public int user_id { set; get; }
        public bool eliminado { set; get; }


        public static TareaUser Find(int tarea_id)
        {
            return MYSQL.Query<TareaUser>($"SELECT * FROM tareas_usuarios WHERE tarea_id='{tarea_id}' LIMIT 1").FirstOrDefault();
        }
    }
}
