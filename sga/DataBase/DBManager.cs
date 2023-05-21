using PluginSQL;
using sga.DataBase.Tables;

namespace sga
{
    public static class DBManager
    {
        public static void Connect(IConfiguration config)
        {
            string _host = config.GetSection("MYSQL").GetValue<string>("host");
            int _port = config.GetSection("MYSQL").GetValue<int>("port");
            string _user = config.GetSection("MYSQL").GetValue<string>("user");
            string _pwd = config.GetSection("MYSQL").GetValue<string>("pwd");
            string _name = config.GetSection("MYSQL").GetValue<string>("name");

            MYSQL.Init(_host, _port, _user, _pwd, _name);

            bool status = MYSQL.CreateDataBase();

            status = MYSQL.CheckStatus();

            if (!status)
            {
                Console.WriteLine($"no se pudo conectar con la base de datos");
                return;
            }

            Console.WriteLine($"============================================");
            Console.WriteLine($"Generando Tablas");
            Console.WriteLine($"============================================");


            //Creamos tablas
            MYSQL.CreateTable<User>();
            MYSQL.CreateTable<RolUser>();

            //Llenamos informacion
            if (MYSQL.Table<User>().Count == 0)
            {
                User user = new User();
                user.created_at = DateTime.Now;
                user.updated_at = DateTime.Now;
                user.nombre = "Raul Mendez";
                user.email = "avalontm21@gmail.com";
                user.password = "cinder";
                user.role_id = (int)UserRolEnum.Admin;

                if (user.Insert())
                {
                    Console.WriteLine($"[Nuevo usuario] {user.nombre}");
                }
            }

            if (MYSQL.Table<RolUser>().Count == 0)
            {
                RolUser rol = new RolUser();
                rol.rol_id = (int)UserRolEnum.Alumno;
                rol.nombre = "Alumno";

                if (rol.Insert())
                {
                    Console.WriteLine($"[Nuevo rol de usuario] {rol.nombre}");
                }

                rol = new RolUser();
                rol.rol_id = (int)UserRolEnum.Docente;
                rol.nombre = "Docente";

                if (rol.Insert())
                {
                    Console.WriteLine($"[Nuevo rol de usuario] {rol.nombre}");
                }

                rol = new RolUser();
                rol.rol_id = (int)UserRolEnum.Admin;
                rol.nombre = "Administrador";

                if (rol.Insert())
                {
                    Console.WriteLine($"[Nuevo rol de usuario] {rol.nombre}");
                }
            }

            Console.WriteLine($"============================================");
            Console.WriteLine($"HOST: {_host}");
            Console.WriteLine($"PORT: {_port}");
            Console.WriteLine($"USER: {_user}");
            Console.WriteLine($"PSW:  {_pwd}");
            Console.WriteLine($"NAME: {_name}");
            Console.WriteLine($"============================================");
        }
    }
}
