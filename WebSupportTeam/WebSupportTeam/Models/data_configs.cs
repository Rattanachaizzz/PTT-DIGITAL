using System.ComponentModel.DataAnnotations;

namespace WebSupportTeam.Models
{
    public class data_configs
    {
        [Key]
        public int config_id { get; set; }
        public string? key { get; set; }
        public string? value { get; set; }
    }
}
