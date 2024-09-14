using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class Cases : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return (Session["User"] != null && ((User)(Session["User"])).UserType > 2);
        }
        private void Display(int type = 1)
        {
            User user = (User)Session["User"];
            dynamic cases = (user.UserType == 3) ? service.HouseCases(user.ID) : service.GetAllCases();

            if (cases != null)
            {
                int counter = 1;
                string displayCases = "";
                foreach (Case cs in cases)
                {
                    if (type == 2 && cs.ResolutionDate.Year >= 2000)
                    {
                        displayCases += "<tr>";
                        displayCases += $"<td>{ counter++ }</td>";
                        displayCases += $"<td>{ Convert.ToDateTime(cs.Date).ToShortDateString()}</td>";
                        displayCases += $"<td>{ Convert.ToDateTime(cs.ResolutionDate).ToShortDateString() }</td>";

                        displayCases += "<td>Resolved </td>";
                        displayCases += $"<td><a href='caseinformation.aspx?case={cs.CaseID}'>More Info</a></td></tr>";

                    }
                    else if (type == 3 && cs.ResolutionDate.Year <= 1000)
                    {
                        displayCases += "<tr>";
                        displayCases += $"<td>{ counter++ }</td>";
                        displayCases += $"<td>{ Convert.ToDateTime(cs.Date).ToShortDateString()}</td>";
                        displayCases += $"<td> ... </td>";

                        displayCases += "<td> Unresolved </td>";
                        displayCases += $"<td><a href='caseinformation.aspx?case={cs.CaseID}'>More Info</a><a href='cases.aspx?resolve={cs.CaseID}'>Resolve</a></td></tr>";

                    }
                    else if (type == 1)
                    {
                        displayCases += "<tr>";
                        displayCases += $"<td>{ counter++ }</td>";
                        displayCases += $"<td>{ Convert.ToDateTime(cs.Date).ToShortDateString()}</td>";
                        if (cs.ResolutionDate.Year >= 2000)
                        {
                            displayCases += $"<td>{ Convert.ToDateTime(cs.ResolutionDate).ToShortDateString() }</td>";

                            displayCases += "<td>Resolved</td>";
                            displayCases += $"<td><a href='caseinformation.aspx?case={cs.CaseID}'>More Info</a></td></tr>";
                        }
                        else
                        {
                            displayCases += $"<td> ... </td>";

                            displayCases += "<td>Unresolved</td>";
                            displayCases += $"<td><a href='caseinformation.aspx?case={cs.CaseID}'>More Info</a><a href='cases.aspx?resolve={cs.CaseID}'>Resolve</a></td></tr>";

                        }

                    }
                }
                dbCase.InnerHtml = displayCases;
            }
            else dbCases.InnerHtml = "<h2 class='text-center'>Your house has 0 cases</h2>";
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Auth())
                Response.Redirect("Login.aspx"); ;

            if (Request.QueryString["resolve"] != null)
                service.SolveCase(Convert.ToInt32(Request.QueryString["resolve"]));
            Display(); 

        }

        protected void SortCases_SelectedIndexChanged(object sender, EventArgs e)
        {
            Display(Convert.ToInt32(SortCases.SelectedValue));
        }
    }
}