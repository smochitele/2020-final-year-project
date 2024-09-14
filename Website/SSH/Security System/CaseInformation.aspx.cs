using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{

    public partial class CaseInformation : System.Web.UI.Page
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

            if (Request.QueryString["case"] == null)
                Response.Redirect("Dashboard.aspx");

            var cs = service.GetAllCases().Where(a => a.CaseID == Convert.ToInt32(Request.QueryString["case"])).FirstOrDefault();
            if(cs != null)
            {
                ID.Value = Convert.ToString(cs.CaseID);
                Description.Value = Convert.ToString(cs.Description);
                Date.Value = Convert.ToString(cs.Date);
                HouseNo.Value = Convert.ToString(cs.HouseID);
            }

        }
    }
}