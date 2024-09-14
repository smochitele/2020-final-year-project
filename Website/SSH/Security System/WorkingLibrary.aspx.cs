using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class WorkingLibrary : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return (Session["User"] != null && ((User)(Session["User"])).UserType > 2);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Auth())
                Response.Redirect("Login.aspx");

            int count = 0;
            string display = "";
            dynamic respondents = service.GetAllRespondents();
            if(respondents != null)
            {
                foreach(var res in respondents)
                {
                    display += $"<tr><td>{count++}</td>";
                    display += $"<td>{res.Name} {res.Surname}</td>";
                    int status = service.GetStatus(res.ID);
                    switch(status)
                    {
                        case 0:
                            display += $"<td>Offline</td>";
                            break;
                        case 1:
                            display += $"<td>Online</td>";
                            break;
                        case 2:
                            display += $"<td>Attending scene</td>";
                            break;
                        default:
                            display += $"<td>Unknown scene</td></tr>";
                            break;

                    }
                }
                
            }
            dbRespondets.InnerHtml = display;

        }
    }
}