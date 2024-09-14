using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System;
using Security_System.SecurityService;

namespace Security_System
{
    public partial class MainLayout : System.Web.UI.MasterPage
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return true;// (Session["User"] != null && ((User)(Session["User"])).UserType > 2);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Auth())
                Response.Redirect("Login.aspx");
            User user = (User)Session["User"];
            Username.InnerHtml = user.Name;
            switch(user.UserType)
            {
                case 3:
                    {
                        PanicButton.Visible = true;
                        respodents.Visible = false;
                        summary.Visible = false;
                        dutyies.Visible = false;
                        break;
                    }
                case 4:
                    {
                       watch.Visible = false;
                       occupants.Visible = false;
                       break;
                    }
            }
        }

        protected void PanicButton_Click(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            Response.Redirect($"panicalert.aspx?ID={user.ID}");
        }
    }
}