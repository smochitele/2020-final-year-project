using HSS_Website.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSS_Website
{
    public partial class Home : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            User user = (User)Session["User"];
            name.InnerText = user.Name;
            lastname.InnerText = user.Surname;
        }

        protected void PanicButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("NearestPersonnel.aspx");
        }
    }
}