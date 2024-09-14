using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;

namespace Security_System
{
    public partial class CasesPerCity : System.Web.UI.Page
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

            string name;
            if (Request.QueryString["city"] == null)
                Response.Redirect("Summary.aspx");

            name = Request.QueryString["city"];
            DisplayCases(name);
        }
        private void DisplayCases(string name = "")
        {
            string displayUsers = "";
            dynamic cases = service.GetAreaCases(name);
            if (cases != null)
            {
                foreach (var cs in cases)
                {
                    displayUsers += "<tr>";
                    displayUsers += $"<td>{ cs.CaseID }</td>";
                    displayUsers += $"<td>{ cs.Date }</td>";
                    displayUsers += $"<td>{ Convert.ToDateTime(cs.ReposponseTime).ToShortDateString() }</td>";
                    displayUsers += $"<td>{ Convert.ToDateTime(cs.ResolutionDate).ToShortDateString() } </td>";
                    displayUsers += $"<td>{ cs.HouseID } </td>";
                    displayUsers += $"<td>{ cs.UserID } </td>";
                    displayUsers += $"<td>{ cs.RespondentID } </td>";
                    displayUsers += "</tr>";
                }
                dbCase.InnerHtml = displayUsers;
            }
        }
    }
}