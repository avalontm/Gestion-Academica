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
        [JsonIgnore]
        public DateTime created_at { set; get; }

        [OnlyRead]
        [HidenField]
        [JsonIgnore]
        public DateTime updated_at { set; get; }

        [OnlyRead]
        [ParentField("cursos")]
        public int curso_id { set; get; }

        [ParentField("usuarios")]
        public int user_id { set; get; }

        public string? content { set; get; }
    }
}
