using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSH_Service.Models
{
    public class Package 
    {
        public int PackageID { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
    }
}