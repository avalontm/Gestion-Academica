using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("tareas")]
    public class Tarea : TableBase
    {
        [PrimaryKey]
        [HidenField]
        [JsonIgnore]
        public int id { get; set; }

        [OnlyRead]
        [HidenField]
        [JsonIgnore]
        public DateTime created_at { set; get; }

        [OnlyRead]
        [HidenField]
        [JsonIgnore]
        public DateTime updated_at { set; get; }

        [OnlyRead]
        [HidenField]
        public int curso_id { set; get; }

        [OnlyRead]
        [HidenField]
        public int taller_id { set; get; }

        [DisplayField("Fecha de entrega")]
        public DateTime fecha_entrega { set; get; }

        [OnlyRead]
        [HidenField]
        [DisplayField("Tipo de tarea")]
        public int tipo { set; get; }

        [OnlyRead]
        [HidenField]
        public string? codigo { set; get; }

        public string? titulo { set; get; }

        [EditorField]
        public string? contenido { set; get; }

        [DisplayField("Limites de archivos")]
        public int limite { set; get; }

        public bool activo { set; get; }

        [HidenField]
        public int user_id { set; get; }


        [HidenField]
        [JsonIgnore]
        public bool eliminado { set; get; }

        public static List<Tarea> Get(int taller_id, int LIMIT = 100)
        {
            return MYSQL.Query<Tarea>($"SELECT * FROM tareas WHERE taller_id='{taller_id}' AND eliminado='0' ORDER BY created_at DESC LIMIT {LIMIT}");
        }

        public static Tarea Find(int tarea_id)
        {
            return MYSQL.Query<Tarea>($"SELECT * FROM tareas WHERE id='{tarea_id}' AND eliminado='0' LIMIT 1").FirstOrDefault();
        }

        public static Tarea Find(string codigo)
        {
            return MYSQL.Query<Tarea>($"SELECT * FROM tareas WHERE codigo='{codigo}' AND eliminado='0' LIMIT 1").FirstOrDefault();
        }

        public static List<Tarea> Filter(int taller_id, string filter, int LIMIT = 100)
        {
            return MYSQL.Query<Tarea>($"SELECT * FROM tareas WHERE taller_id='{taller_id}' AND titulo LIKE '%{filter}%' AND eliminado='0' ORDER BY created_at DESC LIMIT {LIMIT}");
        }

        public static List<Tarea> GetByDate(DateTime date, int limit = 100)
        {
            return MYSQL.Query<Tarea>($"SELECT * FROM tareas WHERE Month(fecha_entrega)='{date.Month}' AND YEAR(fecha_entrega)='{date.Year}' AND DAY(fecha_entrega)='{date.Day}' AND eliminado='0' ORDER BY fecha_entrega LIMIT {limit}");
        }

        public static List<Tarea> GetByCalendar(DateTime date, int limit = 100)
        {
            return MYSQL.Query<Tarea>($"SELECT * FROM tareas WHERE Month(fecha_entrega)='{date.Month}' AND YEAR(fecha_entrega)='{date.Year}' AND eliminado='0' ORDER BY fecha_entrega LIMIT {limit}");
        }
    }
}
