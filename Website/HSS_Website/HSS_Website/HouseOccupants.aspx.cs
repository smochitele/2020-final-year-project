using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using HSS_Website.ServiceReference1;
namespace HSS_Website
{
    public partial class HouseOccupants : System.Web.UI.Page
    {
        Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Request.QueryString["delete"] != null)
            {
                int ID = Convert.ToInt32(Request.QueryString["delete"].ToString());
                bool state = service.RemoveHouseOccupant(ID);
            }
            dynamic users = service.GetHouseOccupants(((User)Session["User"]).UserID);
            string content = "";
            int count = 0;
            foreach(var user in users)
            {
                content += "<tr>";
                content += $"<td>{count}</td>";
                content += $"<td>{user.Name}</td>";
                content += $"<td>{user.Surname}</td>";
                content += $"<td> <span class='delete' onclick='deleteOccupant({user.UserID})' >x </span> </td>";
                count++;
                content += "</tr>";
            }
            occupants.InnerHtml += content;
        }

        protected void RegisterOccupant_Click(object sender, EventArgs e)
        {
            

        }

        protected void RegisterOccupant_Click1(object sender, EventArgs e)
        {
            Users user = new Users
            {
                ID_Number = 00,
                Email = email.Value,
                Password = email.Value,
                Name = name.Value,
                Surname = lastname.Value,
                IsActive = 1
            };
            bool state = service.AddHouseOccupant(user, ((User)Session["User"]).UserID);
            if (state)
            {
                Response.Redirect("HouseOccupants.aspx");
            }
        }
    }
}