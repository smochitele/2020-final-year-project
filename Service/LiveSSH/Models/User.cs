using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSH_Service
{
    public class User
    {
        public string ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IsActive { get; set; }
        public int UserType { get; set; }
        public DateTime DateRegistered { get; set; }
    }
}