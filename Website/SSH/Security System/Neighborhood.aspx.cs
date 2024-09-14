using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class Neighborhood : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return (Session["User"] != null && ((User)(Session["User"])).UserType > 2);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Auth())
                Response.Redirect("Login.aspx");

            string display = "";
            dynamic casesByAddress = service.GetAreaCases("polokwane");
            if (casesByAddress != null)
            {
                foreach (var cs in casesByAddress)
                {
                    display += "<tr>";
                    display += $"<td>{ cs.CaseID}</td>";
                    display += $"<td> {cs.Date} </td>";
                    display += $"<td> { Convert.ToDateTime(cs.ResolutionDate).ToShortDateString() } </td>";
                    display += "</tr>";
                }
            }
            dbCases.InnerHtml = display;
            string intervals = service.GetTimeInterval();
            string displayInts = "";
            String[] ints = intervals.Split('\n');
            foreach(var i in ints)
            {
                displayInts += $"<h6>{ i }</h6>";
            }
            peaks.InnerHtml = displayInts;
        }
    }
}