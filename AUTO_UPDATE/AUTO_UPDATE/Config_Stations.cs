using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTO_UPDATE
{
    public class Config_Stations
    {
        public string Station_id { get; set; }
        public string Station_code { get; set; }
        public string Station_name { get; set; }
        public string Station_IP { get; set; }
        public string Dispenser { get; set; }
        public string Tankgauge { get; set; }
        public string Monitor { get; set; }
        public string Web { get; set; }
        public string API { get; set; }
        public string Gaia { get; set; }
        public string Other { get; set; }
        public string Time_Transfer_File { get; set; }
        public string Time_Update_File { get; set; }
        public string Status_Transfer_File { get; set; }
        public string Status_Update_File { get; set; }
    }
}
