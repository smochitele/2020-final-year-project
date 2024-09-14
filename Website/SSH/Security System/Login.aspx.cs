using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;

namespace Security_System
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string email = Username.Value;
            string password = Password.Value;

            User user = service.GetUser(email, password);
            if (user == null)
            {
                err.Attributes.Add("class", "group-input text-center alert alert-danger");
                return;
            }
               
            Session["User"] = user;
            Response.Redirect("Dashboard.aspx");
        }
    }
}