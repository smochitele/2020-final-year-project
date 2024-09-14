using SSH_Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSH_Service
{
    public class House
    {
        public int HouseID { get; set; }
        public int AlarmStatus { get; set; }
        public string OwnerID { get; set; }
        public Address HouseAddress { get; set; }
    }
}