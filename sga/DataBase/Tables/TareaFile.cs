using Newtonsoft.Json;
using PluginSQL;

namespace sga.DataBase.Tables
{
    [TableName("tarea_archivos")]
    public class TareaFile : TableBase
    {
        [PrimaryKey]
        public int id { set; get; }
        public DateTime created_at { set; get; }
        public int user_id { set; get; }
        public int curso_id { set; get; }
        public int taller_id { set; get; }
        public int tarea_id { set; get; }
        public string? archivo { set; get; }
        public string? nombre { set; get; }

        [JsonIgnore]
        public bool eliminado { set; get; }
    }
}
