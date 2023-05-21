using sga.Models;

namespace sga
{
    public static class AcessManager
    {

        public static bool isAlumno(this AuthModel auth)
        {
            if ((UserRolEnum)auth.user.role_id >= UserRolEnum.Alumno)
            {
                return true;
            }
            return false;
        }

        public static bool isDocente(this AuthModel auth)
        {
            if ((UserRolEnum)auth.user.role_id >= UserRolEnum.Docente)
            {
                return true;
            }
            return false;
        }

        public static bool isAdmin(this AuthModel auth)
        {
            if ((UserRolEnum)auth.user.role_id >= UserRolEnum.Admin)
            {
                return true;
            }
            return false;
        }
    }
}
