<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterHome.aspx.cs" Inherits="Security_System.RegisterHome" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register your house</title>
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link rel="stylesheet" href="css/registerhome.css"/>
    <script src="https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.js"></script>
    <link href="https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.css" rel="stylesheet" />
    <script src='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.min.js'></script>
    <link rel='stylesheet' href='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.css' type='text/css' />

    <style>
        #map { 
            position: absolute; 
            top: 3.5%; 
            bottom: 0; 
            width: 100%; 
            height: 80%;
            border: 1px solid #000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" class="input-form" >
        <div class="container-fluid">
            <small>Search for your area and navigate to your house and click on it</small>
            <div class="row">
                <div class="col-md-8">
                    <div id="map"></div>
                </div>
                <div class="col-md-4">
                    <div class="jumbotron">
                    <div class="prog">
                        <p>1</p>
                        <p class="prog-active">2</p>
                        <p>3</p>
                        <hr />
                    </div>
                    <h3>Add House</h3>
                    <small class="text-info">To use this service, your house details are required</small>
                    <div class="group-input">
                        <label for="Province">Province</label>
                        <input type="text" id="Province" runat="server"/>
                        <%--<asp:DropDownList runat="server" ID="Provinces">
                            <asp:ListItem Value="Limpopo">Limpopo</asp:ListItem>
                            <asp:ListItem Value="Gauteng">Gauteng</asp:ListItem>
                            <asp:ListItem Value="Mpumalanga">Mpumlanga</asp:ListItem>
                            <asp:ListItem Value="KwaZulu Natal">KwaZulu Natal</asp:ListItem>
                            <asp:ListItem Value="Free State">Free State</asp:ListItem>
                            <asp:ListItem Value="Western Cape">Western Cape</asp:ListItem>
                            <asp:ListItem Value="Eastern Cape">Eastern Cape</asp:ListItem>
                            <asp:ListItem Value="Northern Cape">Northern Cape</asp:ListItem>
                        </asp:DropDownList>--%>
                    </div>
                    <%--<div class="group-input">
                        <label for="City">City</label>
                        <input type="text" id="City"  runat="server"/>
                    </div>--%>
                    <div class="group-input">
                        <label for="Surburb">Surburb</label>
                        <input type="text" id="Surburb" runat="server" readonly/>
                    </div>
                    <div class="group-input">
                        <label for="Street Name">Street Name</label>
                        <input type="text" id="StreetName" runat="server" readonly />
                    </div>
                    <div class="group-input">
                        <label for="House">House No.</label>
                        <input type="text" id="HouseNo" runat="server" placeholder="Enter your house number"/>
                    </div>
                    <div class="group-input">
                        <label for="ZIPCode">ZIP Code</label>
                        <input type="text" id="ZIPCode" runat="server" readonly/>
                    </div>
                    <div class="group-input group-location-input">
                        <label for="GPS">Location</label>
                        <input type="text" id="longitude" runat="server" readonly value="0"/>
                        <input type="text" id="latitude" runat="server" readonly value="0"/>
                    </div>
                    <div class="group-input">
                       <asp:Button OnClick="AddHouse_Click" Text="Add House" ID="AddHouse" runat="server" class="aspButton"/>
                    </div>
                    <div class="group-links">
                        <a href="Register.aspx">Go Back</a>
                    </div>
                </div>
                </div>
            </div>
        </div>
    </form>
    <script>
        console.log("Loading the map")
        let longi = document.querySelector("#longitude");
        let lati = document.querySelector("#latitude");
        let zipCode = document.querySelector("#ZIPCode");
        let streetName = document.querySelector("#StreetName");
        let province = document.querySelector("#Province");
        let city = document.querySelector("#Surburb");

        getLocation();
        function getLocation() {

            if (navigator.geolocation) {

                navigator.geolocation.watchPosition(showPosition);
            } else {

                lati.value = "Geolocation is not supported by this browser.";
            }
        }
        function showPosition(position) {
            longi.value = position.coords.longitude;
            lati.value = position.coords.latitude;
        }
        mapboxgl.accessToken = 'pk.eyJ1IjoibW9uYW1hdHYiLCJhIjoiY2tkcTJyNXF6MTBwaTJxbXFqMzJqdDJ1dCJ9.1TQ2Y4AZEDpJ1DYVqzCmGw';
        var map = new mapboxgl.Map({
            container: 'map', //
            style: 'mapbox://styles/mapbox/satellite-v9',
            center: [28.028201300000003, -26.2005016], // starting position
            zoom: 13
        });
        map.on('mousemove', 'clusters', () => {
            map.getCanvas().style.cursor = 'pointer';
        })
        map.on('click', function (e) {
            longi.value = JSON.stringify(e.lngLat.wrap().lng)
            lati.value = JSON.stringify(e.lngLat.wrap().lat)

            fetch(`https://api.mapbox.com/geocoding/v5/mapbox.places/${longi.value},${lati.value}.json?access_token=pk.eyJ1IjoibW9uYW1hdHYiLCJhIjoiY2tkcTJyNXF6MTBwaTJxbXFqMzJqdDJ1dCJ9.1TQ2Y4AZEDpJ1DYVqzCmGw`)
                .then(response => response.json())
                .then(data => {
                    console.log(data);
                    let info = (data.features[0].place_name).split(",");

                    province.value = data.features[5].text;
                    city.value = info[2]
                    streetName.value = info[0]
                    zipCode.value = data.features[2].text;
                });





        });
        var geocoder = new MapboxGeocoder({ // Initialize the geocoder
            accessToken: mapboxgl.accessToken, // Set the access token
            mapboxgl: mapboxgl, // Set the mapbox-gl instance
            marker: false, // Do not use the default marker style
        });

        // Add the geocoder to the map
        map.addControl(geocoder);
</script>
    <script>


</script>
</body>
</html>
