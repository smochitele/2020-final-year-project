<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Summary.aspx.cs" Inherits="Security_System.Summary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.js"></script>
    <link href="https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.css" rel="stylesheet" />
    <style>
	    body { margin: 0; padding: 0; }
	    #map { position: absolute; top: 40%; bottom: 0; width: 80%; }
        .marker {
            background-image: url('img/mapbox-icon.png');
              background-size: cover;
              width: 50px;
              height: 50px;
              border-radius: 50%;
              cursor: pointer;
          }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Crime Hotspots</h4>
    <p>Summary about the crime hotspots and the peak times</p>
    <div class="buttons">
            <button onclick="openTap(event, 'texts')" class="taplink activeButton" type="button">Textual</button>
            <button onclick="openTap(event, 'graphics')" class="taplink" type="button">Graphic</button>
        </div>
    <div class="container">
        
        <div class="textual information" id="texts">
            <div class="row" id="Hotspots" runat="server">
            </div>
            <br />
            <input type="search" placeholder="Get cases by area" id="city" class="searchingClass" aria-label="Search" onsearch="searchPlace(city)"/>
            <table class="our-table occupants-table">
                <thead>
                    <tr>
                        <th>City</th>
                        <th>Surburbs</th>
                    </tr>
                 </thead>
                <tbody id="dbCities" runat="server">
                </tbody>
            </table>
        </div>
        <div class="graphic information" id="graphics" style="display:none">
            <div id="map"></div>
            
        </div>     
    </div>
    <div id="mapsMarkers" runat="server"></div>
    <script>
        //28.028032099999997, -26.2004891
        //mapboxgl.accessToken = 'pk.eyJ1IjoibW9uYW1hdHYiLCJhIjoiY2tkcTJyNXF6MTBwaTJxbXFqMzJqdDJ1dCJ9.1TQ2Y4AZEDpJ1DYVqzCmGw';

        //var map = new mapboxgl.Map({
        //    container: 'map',
        //    style: 'mapbox://styles/mapbox/streets-v11',
        //    center: [27.9970848, -26.184611],
        //    zoom: 15
        //});
        //mapboxgl.accessToken = 'pk.eyJ1IjoibW9uYW1hdHYiLCJhIjoiY2tkcTJyNXF6MTBwaTJxbXFqMzJqdDJ1dCJ9.1TQ2Y4AZEDpJ1DYVqzCmGw';
        //var map = new mapboxgl.Map({
        //    container: 'map',
        //    style: 'mapbox://styles/mapbox/streets-v11',
        //    center: [28.028032099999997, -26.2004891],
        //    zoom: 8
        //});





        //var marker = new mapboxgl.Marker()
        //    .setLngLat([27.9985272, -26.1755394])
        //    .addTo(map)
        //var marker = new mapboxgl.Marker()
        //    .setLngLat([27.9970848, -26.184611])
        //    .addTo(map)
        //var marker = new mapboxgl.Marker()
        //    .setLngLat([27.9882113, -26.1841952])
        //    .addTo(map)



    </script>
    <script>
        function openTap(evt, cityName) {
            var i, x, tablinks;
            x = document.getElementsByClassName("information");
            for (i = 0; i < x.length; i++) {
                x[i].style.display = "none";
            }
            tablinks = document.getElementsByClassName("taplink");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].classList.remove("activeButton");
            }
            document.getElementById(cityName).style.display = "block";
            evt.currentTarget.classList.add("activeButton");
        }

        const searchPlace = (name) => {
            //console.log(e.target.value)
            window.location.href = `casespercity.aspx?city=${name.value}`;
        }
        summary.classList.add("active");
    </script>
</asp:Content>
