using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Security_System.SecurityService;
namespace Security_System
{
    public partial class Summary : System.Web.UI.Page
    {
        private readonly Service1Client service = new Service1Client();
        protected void Page_Load(object sender, EventArgs e)
        {
            string hot = "";
            string burbs = "";
            dynamic dbCity;
            dynamic hots = service.Top5Cities();
            int counter = 1;
            if (hots != null)
            {
                foreach (var name in hots)
                {
                    dbCity = service.Top5Surbub(Convert.ToString(name));
                    if (dbCity != null)
                    {
                        burbs += "<tr>";
                        burbs += $"<td> { name } </td>";
                        burbs += $"<td>";
                        foreach (var burb in dbCity)
                        {
                            burbs += $"{ burb ?? "..." } <br>";

                        }
                        burbs += "</td>";
                        burbs += "</tr>";
                    }
                    hot += "<div class='col-md-2'>";
                    hot += "<div class='hotspots'>";
                    hot += $"<h5>{ counter++ + "." + name }</h5>";
                    hot += "</div>";
                    hot += "</div>";
                }
            }
            LoadMarkers();
            Hotspots.InnerHtml = hot;
            dbCities.InnerHtml = burbs;

        }
        private void LoadMarkers()
        {
            string markers = "<script>";
            markers += "mapboxgl.accessToken = 'pk.eyJ1IjoibW9uYW1hdHYiLCJhIjoiY2tkcTJyNXF6MTBwaTJxbXFqMzJqdDJ1dCJ9.1TQ2Y4AZEDpJ1DYVqzCmGw';" +
                        "var map = new mapboxgl.Map({" +
                        " container: 'map'," +
                        "style: 'mapbox://styles/mapbox/satellite-v9'," +
                        "center: [27.9970848, -26.184611]," +
                        " zoom: 15" +
                        "});";
            dynamic locations = service.GetGeoLocations();
            if (locations != null)
            {
                foreach (var loc in locations)
                {
                    markers += "var marker = new mapboxgl.Marker()" +
                                $".setLngLat([{loc.Longitude}, {loc.Lattitude}])" +
                                ".addTo(map)\n";


                }
                markers += "</script>";
            }

            mapsMarkers.InnerHtml += markers;
        }
    }
}