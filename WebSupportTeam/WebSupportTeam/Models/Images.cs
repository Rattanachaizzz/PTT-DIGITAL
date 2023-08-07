using System.ComponentModel.DataAnnotations;

namespace WebSupportTeam.Models
{
    public class Images
    {
        [Key]
        public int? file_path_id { get; set; }
        public string file_name { get; set; }
        public string path_file { get; set; }
    }
}
