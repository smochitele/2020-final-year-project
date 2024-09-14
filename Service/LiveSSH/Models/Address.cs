using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSH_Service.Models
{
    public class Address 
    {
        public int ID { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Surburb { get; set; }
        public string StreetName { get; set; }
        public string HouseNo { get; set; }
        public string ZIPCode { get; set; }
        public string Longitute { get; set; }
        public string Lattitute { get; set; }
        public int HouseID { get; set; }
    }
}