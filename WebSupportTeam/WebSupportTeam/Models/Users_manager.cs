using System.ComponentModel.DataAnnotations;

namespace WebSupportTeam.Models
{
    public class Users_manager
    {
        [Key]
        public int user_id { get; set; }
        public string user_name { get; set; }
        public string password { get; set; }
        public string role { get; set; }
        public string department { get; set; }
        public bool enable { get; set; }
        public DateTime login_date { get; set; }
    }
}
