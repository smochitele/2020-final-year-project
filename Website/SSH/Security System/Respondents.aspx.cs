using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;

namespace Security_System
{
    public partial class Respondents : System.Web.UI.Page
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

            DisplayRespondents();

        }
        private void DisplayRespondents()
        {
            string displayUsers = "";
            int counter = 1;
            dynamic users = service.GetAllRespondents();
            if (users != null)
            {
                foreach (var user in users)
                {
                    displayUsers += "<tr>";
                    displayUsers += $"<td>{ counter++ }</td>";
                    displayUsers += $"<td>{ user.Name}</td>";
                    displayUsers += $"<td>{ user.Surname }</td>";
                    displayUsers += $"<td>{ user.Email} </td>";
                    displayUsers += $"<td><a href='viewcases.aspx?id={ user.ID }'>View cases</a></td>";
                    displayUsers += "</tr>";
                }
                dbRespondents.InnerHtml = displayUsers;
            }
        }

        protected void AddRespondent_Click(object sender, EventArgs e)
        { 
            User resp = new User()
            {
                Name = Name.Value,
                Surname = Surname.Value,
                Email = Email.Value,
                ID = IDNumber.Value,
                IsActive = 1,
                Password = Email.Value,
                UserType = 2             
            };
            if(service.RegisterRespondent(resp) > 1)
            {
                DisplayRespondents();
            }
        }
    }
}