using HSS_Website.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSS_Website
{
    public partial class Register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            HSS_Website.ServiceReference1.User user = new User
            {
                ID_Number = 00,
                Email = email.Value,
                Password = password.Value,
                Name = name.Value,
                Surname = lastname.Value,
                IsActive = 1
            };
            Session["NewUser"] = user;
            Response.Redirect("House.aspx");
        }
    }
}