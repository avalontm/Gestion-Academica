using PluginSQL;

namespace sga.DataBase.Tables
{
    [TableName("roles_usuarios")]
    public class RolUser : TableBase
    {
        [PrimaryKey]
        public int id { get; set; }
        public int rol_id { get; set; }
        public string? nombre { get; set; }

        public static RolUser Find(int rol_id)
        {
            return MYSQL.Query<RolUser>($"SELECT * FROM roles_usuarios WHERE rol_id='{rol_id}' LIMIT 1").FirstOrDefault();
        }
    }
}
