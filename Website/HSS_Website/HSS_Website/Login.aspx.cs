using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HSS_Website.ServiceReference1;

namespace HSS_Website
{
    public partial class Login : System.Web.UI.Page
    {
        Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            if (username.Value == "" || password.Value == "")
                Response.Redirect("Login.aspx");
           
            User user = service.Login(username.Value, password.Value);
            if (user != null)
            {
                Session["User"] = user;
                Response.Redirect("Home.aspx");
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
    }
}