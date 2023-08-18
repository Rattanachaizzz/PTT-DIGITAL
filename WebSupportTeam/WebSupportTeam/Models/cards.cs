using System.ComponentModel.DataAnnotations;

namespace WebSupportTeam.Models
{
    public class cards
    {
        [Key]
        public int card_id { get; set; }
        public string? title { get; set; }
        public string? detail { get; set; }
        public string? user { get; set; }
        public DateTime? time_update { get; set; }
    }
}
