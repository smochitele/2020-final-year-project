using HSS_Website.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HSS_Website
{
    public partial class House : System.Web.UI.Page
    {
        Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            User user = (User)Session["NewUser"];
            ServiceReference1.House house = new ServiceReference1.House
            {
                Province = province.Value,
                City = city.Value,
                Surburb = surburb.Value,
                HouseNo = int.Parse(houseno.Value),
                ZIPCode = zip.Value
            };
            service.Register(user, house);
            Response.Redirect("Login.aspx");
        }
    }
}