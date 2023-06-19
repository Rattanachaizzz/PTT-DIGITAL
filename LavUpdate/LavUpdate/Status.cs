using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LavUpdate
{
    public class Status
    {
        public string id_station { get; set; }
        public string pbl_station { get; set; }
        public string bu_station { get; set; }
        public string name_station { get; set; }
        public string ip_sim { get; set; }
        public string dispenser { get; set; } = "-";
        public string tankgauge { get; set; } = "-";
        public string monitor { get; set; } = "-";
        public string gaia { get; set; } = "-";
        public string webconfig { get; set; } = "-";
        public string websupport { get; set; } = "-";
        public string lavupdate { get; set; } = "-";
        public string api { get; set; } = "-";
        public string remark { get; set; }

    }
}
