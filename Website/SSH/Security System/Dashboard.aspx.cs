using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class Dashboard : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        private bool Auth()
        {
            return (Session["User"] != null && ((User)(Session["User"])).UserType > 2);
        }
        private string displayCases()
        {
            int[] num = service.GetCasesForLastSevenDays();
            string n = "";
            for(int i = 0; i< num.Length; i++)
            {
                if((i+1) == num.Length)
                {
                    n += num[i];
                }
                else
                {
                    n += $"{num[i].ToString()+','}";
                }
            }
            return n;
        }
        private void DisplayCharts()
        {
            int[] num = { 2, 4, 66, 33, 45, 78 };
            string charts = "";
            charts = $"<script>var ctx = document.getElementById('myChart').getContext('2d');" +
                     "var myChart = new Chart(ctx, {" +
                     "type: 'line'," +
                     "data:" +
                     "{" +
                     $"labels: ['Day1', 'Day2', 'Day3', 'Day4', 'Day5', 'Day6', 'Day7']," +
                        "datasets: [{" +
                            "label: '# of Cases in the 7 days'," +
                            $"data: [{displayCases()}]," +
                            "backgroundColor: [" +
                               " 'rgba(0, 29, 36, 0.952)'," +
                               " 'rgba(0, 29, 36, 0.952)'," +
                               " 'rgba(0, 29, 36, 0.952)'," +
                                "'rgba(0, 29, 36, 0.952)'," +
                                "'rgba(0, 29, 36, 0.952)'," +
                               " 'rgba(0, 29, 36, 0.952)'," +
                                "'rgba(0, 29, 36, 0.952)'" +
                           "]," +
                           " borderColor: [" +
                                "'rgba(255, 99, 132, 1)'," +
                                "'rgba(54, 162, 235, 1)'," +
                                "'rgba(255, 206, 86, 1)'," +
                                "'rgba(75, 192, 192, 1)'," +
                                "'rgba(75, 192, 192, 1)'," +
                                "'rgba(153, 102, 255, 1)'," +
                                "'rgba(255, 159, 64, 1)'" +
                           " ]," +
                            "borderWidth: 1" +
                       " }]" +
                    "}," +
                     "options: {" +
                       " scales:" +
                        "{"+
                        "xAxes: [" +
                        "{ gridLines: " +
                            "{ display: false " +
                         "}" +
                        "}],"+
                           " yAxes: [{"+
                                "gridLines: { display: false }," +
                                "ticks:" +
                                "{"+
                                   " beginAtZero: true"+
                                "}"+
                            "}]"+
                        "}"+
                    "}" +
                "});" +
                "</script>";
            disCharts.InnerHtml = charts;


        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Auth())
                Response.Redirect("Login.aspx");

            User user = (User)Session["User"];
            if(user.UserType ==  3)
            {
                myChart1.Visible = false;
                lineGraph.Visible = false;
                barGraph.Visible = false;
                Address address = service.GetUserAddress(user.ID);
                City.Value = address.City;
                StreetName.Value = address.StreetName;
                ZIPCode.Value = address.ZIPCode;
                province.Value = address.Province;
                Surburb.Value = address.Surburb;
                HouseNo.Value = address.HouseNo;
                home.Visible = true;
                dispOwners.Visible = dispResp.Visible = false;
                int occupants = service.GetOwnerOccupants(user.ID).Count();
                int activeCase = service.ActiveCases(user.ID);
                houseActiveCases.InnerHtml = Convert.ToString(activeCase);
                numOccupnats.InnerHtml = Convert.ToString(occupants);
            }
            else if(user.UserType == 4)
            {
                int occupants = service.TotalOccupants();
                int activeCase = service.AllActiveCases();
                int respondents = service.TotalRespondents();
                int owners = service.TotalHomeOwners();
                //Display the numbers
                numHomeOwners.InnerHtml = Convert.ToString(owners);
                numRespondents.InnerHtml = Convert.ToString(respondents);
                houseActiveCases.InnerHtml = Convert.ToString(activeCase);
                numOccupnats.InnerHtml = Convert.ToString(occupants);
                DisplayCharts();
            }       
        }
    }
}