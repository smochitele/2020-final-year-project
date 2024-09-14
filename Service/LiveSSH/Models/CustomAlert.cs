using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiveSSH.Models
{
    public class CustomAlert 
    {
        public int id { get; set; }
        public int HouseID { get; set; }
        public string OccupantID { get; set; }
        public string Lattitude { get; set; }
        public string Longitude { get; set; }

    }
}