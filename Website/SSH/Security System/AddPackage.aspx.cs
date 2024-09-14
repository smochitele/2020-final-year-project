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
    public partial class AddPackage : System.Web.UI.Page
    {
        //private readonly SecurityServiceClient service = new SecurityServiceClient();
        private readonly Service1Client service = new Service1Client();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["RegisterHomeOwner"] == null || Session["RegisterHome"] == null)
                Response.Redirect("Register.aspx");

        }
        private void SubmitRegistration(int packageID)
        {
           
            User newUser = (User)Session["RegisterHomeOwner"] ?? null;
            Address newAddress = (Address)Session["RegisterHome"] ?? null;
            if(newUser != null && newAddress != null)
            {
                House house = new House();
                house.HouseAddress = newAddress;
                int state = service.RegisterOwner(newUser, house, packageID);
                if(state >= 1)
                {
                    Session["User"] = Session["RegisterHomeOwner"];
                    Session["RegisterHomeOwner"] = null;
                    Session["RegisterHome"] = null;              
                    Response.Redirect("Dashboard.aspx");
                }
                else
                {
                    //Tell the user what happened...
                }
                
            }     
        }
        protected void Package1_Click(object sender, EventArgs e)
        {
            SubmitRegistration(1);
        }

        protected void Package2_Click(object sender, EventArgs e)
        {
            SubmitRegistration(2);
        }

        protected void Package3_Click(object sender, EventArgs e)
        {
            SubmitRegistration(3);
        }
    }
}