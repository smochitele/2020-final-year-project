using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class ViewCases : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return (Session["User"] != null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] == null)
                Response.Redirect("Dashboard.aspx");

            

            User user = (User)Session["User"];
            User respondent = service.GetUserByID(Request.QueryString["id"].ToString());
            userEmail.InnerText += respondent.Email;
            userName.InnerText += respondent.Name + " " + respondent.Surname;
            idNumber.InnerText += respondent.ID;
            //display the cases
            dynamic cases = service.GetCasesByRespondent(respondent.ID) ?? null;
            int counter = 1;
            string displayCases = "";
            if (cases != null)
            {
                foreach (var cs in cases)
                {
                    displayCases += "<tr>";
                    displayCases += $"<td>{ counter++ }</td>";
                    displayCases += $"<td>{ Convert.ToDateTime(cs.Date).ToShortDateString()}</td>";
                    if (cs.ResolutionDate != null)
                    {
                        displayCases += $"<td>{ Convert.ToDateTime(cs.ResolutionDate).ToShortDateString() }</td>";
                        displayCases += "<td>Resolved</td></tr>";
                    }
                    else
                    {
                        displayCases += $"<td> ... </td>";
                        displayCases += "<td>Unresolved</td></tr>";
                    }
                }
                dbCase.InnerHtml = displayCases;
            }
            var location = service.GetRespondentLocation(respondent.ID);
            string markers = "<script>";
            markers += $"mapboxgl.accessToken = 'pk.eyJ1IjoibW9uYW1hdHYiLCJhIjoiY2tkcTJyNXF6MTBwaTJxbXFqMzJqdDJ1dCJ9.1TQ2Y4AZEDpJ1DYVqzCmGw';" +
                        "var map = new mapboxgl.Map({" +
                        " container: 'map'," +
                        "style: 'mapbox://styles/mapbox/satellite-v9'," +
                        $"center: [{location.Longitude}, {location.Lattitude}]," +
                        " zoom: 15" +
                        "});";

            markers += "var marker = new mapboxgl.Marker()" +
                                $".setLngLat([{location.Longitude}, {location.Lattitude}])" +
                                ".addTo(map)\n";
            markers += "</script>";

            mapRes.InnerHtml += markers;


        }
    }
}