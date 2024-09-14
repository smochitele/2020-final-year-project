using Security_System.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Security_System
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["RegisterHomeOwner"];
            if (user != null)
            {
                Name.Value = user.Name;
                Lastname.Value = user.Surname;
                IDNumber.Value = user.ID;
                Email.Value = user.Email;   
            }
        }

        protected void AddUser_Click(object sender, EventArgs e)
        {
            User user = new User()
            {
                Surname = Lastname.Value,
                ID = IDNumber.Value,
                Email = Email.Value,
                Name = Name.Value,
                Password = Password.Value,
                IsActive = 1,
                UserType = 3//The issue was here
            };
            Session["RegisterHomeOwner"] = user;
            Response.Redirect("RegisterHome.aspx");
        }
    }
}