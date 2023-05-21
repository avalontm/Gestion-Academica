using sga.DataBase.Tables;

namespace sga.Models
{
    public class AuthModel
    {
        public bool status { set; get; }
        public User user { set; get; }
        public string token { set; get; }
    }
}
