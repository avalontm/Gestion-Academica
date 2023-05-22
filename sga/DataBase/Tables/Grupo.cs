using Newtonsoft.Json;
using PluginSQL;
using sga.Sources.Attributes;

namespace sga.DataBase.Tables
{
    [TableName("grupos")]
    public class Grupo : TableBase
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

        public string? codigo { set; get; }
    }
}
