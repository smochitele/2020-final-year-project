using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSH_Service
{
    public class Case
    {
        public int CaseID { get; set; }
        public DateTime Date { get; set; }
        public String ReposponseTime { get; set;}
        public DateTime ResolutionDate { get; set; }
        public string Description { get; set; }
        public string UserID { get; set; }
        public int HouseID { get; set; }
        public string RespondentID { get; set; }
    }
}