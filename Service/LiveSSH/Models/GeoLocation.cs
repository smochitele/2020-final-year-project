using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace LiveSSH.Models
{
    public class GeoLocation
    {
        public string Lattitude { get; set; }
        public string Longitude { get; set; }

        public GeoLocation()
        {

        }
    }
}