using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MailSpace;
using Security_System.SecurityService;

namespace Security_System
{
    public partial class Occupants : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return (Session["User"] != null);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["deleteID"] != null)
            {
                string deleteUserID = Request.QueryString["deleteID"].ToString();
                if (service.DeleteOccupant(deleteUserID) >= 1)
                {
                    Response.Redirect("Occupants.aspx");
                }
            }
            if (Auth())
            {
                DisplayOccupants();
            }
            else Response.Redirect("Login.aspx");
        }

        protected void AddOccupant_Click(object sender, EventArgs e)
        {
            string name = Name.Value;
            string email = Email.Value;
            string surname = Surname.Value;
            string ID = IDNumber.Value;
            string ownerID = ((User)Session["User"]).ID;
            string homeOwner = ((User)Session["User"]).Name;
            User newUser = new User()
            {
                 ID = ID,
                 Name = name,
                 Surname = surname,
                 Email = email,
                 Password = email,
                 UserType = 1
            };
            if (service.RegisterOccupant(newUser, ownerID) >= 1)
            {
                SendEmail mail = new SendEmail(email, "SSH - Registration", name, homeOwner);
                mail.sendToClient();
                Page_Load(sender, e);
            }
        }
        private void DisplayOccupants()
        {
            string displayUsers = "";
            int counter = 1;
            User homeOwner = (User)Session["User"];
            dynamic users = service.GetOwnerOccupants(homeOwner.ID);
            if (users != null)
            {
                foreach (var user in users)
                {
                    displayUsers += "<tr>";
                    displayUsers += $"<td>{ counter++ }</td>";
                    displayUsers += $"<td>{ user.Name}</td>";
                    displayUsers += $"<td>{ user.Surname }</td>";
                    displayUsers += $"<td>{ user.Email} </td>";
                    displayUsers += $"<td><button class='delete' onClick='DeleteOccupant({user.ID})'> <i class='fas fa-trash-alt text-danger'>delete</i></button></td>";
                    displayUsers += "</tr>";
                }
                Ocuppants.InnerHtml = displayUsers;
            }
        }
    }
}