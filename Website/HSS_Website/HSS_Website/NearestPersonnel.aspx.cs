using HSS_Website.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSS_Website
{
    public partial class NearestPersonnel : System.Web.UI.Page
    {
        Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            User loggedIn = (User)Session["User"];
            string user = service.GetNearestRespondent(loggedIn.UserID, 24.987, -30.987);
            if (user == null)
                Response.Redirect("eve.uj.ac.za");
            else
            {
                string[] details = user.Split(' ');
                name.Value = details[0];
                lastname.Value = details[1];
                distance.Value = (Math.Round(service.GetDistanceBetweenPoints(24.987, -30.987, 26.097, 25.097) / 1000000)) + " KMs";
                responsetime.Value = "SAPS";
            }
        }
    }
}