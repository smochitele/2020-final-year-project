using Security_System.SecurityService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class RegisterHome : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            Address address = (Address)Session["RegisterHome"];
            if (address != null && Session["RegisterHomeOwner"] != null)
            {
                //City.Value = address.City;
                Surburb.Value = address.Surburb;
                HouseNo.Value = address.HouseNo;
                StreetName.Value = address.StreetName;
                ZIPCode.Value = address.ZIPCode;
            }

        }

        protected void AddHouse_Click(object sender, EventArgs e)
        {
            Address address = new Address()
            {
                ZIPCode = "22",
                StreetName = StreetName.Value,
                HouseNo = HouseNo.Value,
                Province = Province.Value,
                City = Surburb.Value,
                Surburb = Surburb.Value,
                Lattitute = latitude.Value,
                Longitute = longitude.Value
            };
            Session["RegisterHome"] = address;
            //Response.Redirect("AddPackage.aspx");
            User newUser = (User)Session["RegisterHomeOwner"] ?? null;
            Address newAddress = (Address)Session["RegisterHome"] ?? null;
            if (newUser != null && newAddress != null)
            {
                House house = new House();
                house.HouseAddress = newAddress;
                int state = service.RegisterOwner(newUser, house, 1);
                if (state >= 1)
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
    }
}