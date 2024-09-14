using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;

namespace Security_System
{
    public partial class PanicAlert : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client(); 
        protected void Page_Load(object sender, EventArgs e)
        {
           if(Request.QueryString["ID"] != null)
           {
                User user = (User)Session["User"];
                
           }
        }
    }
}