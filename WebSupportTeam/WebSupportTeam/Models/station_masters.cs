using System.ComponentModel.DataAnnotations;

namespace WebSupportTeam.Models
{
    public class station_masters
    {
        [Key]
        public int id_station { get; set; }
        public string? pbl_station { get; set; }
        public string? bu_station { get; set; }
        public string? name_station { get; set; }
       /* public string ip_bo { get; set; }*/
        public string? ip_sim { get; set; }
        public string? brand_station { get; set; }
        public string? pump_master { get; set; }
        public string? service_version { get; set; }
        public DateTime? create_date { get; set; }
    }
}
