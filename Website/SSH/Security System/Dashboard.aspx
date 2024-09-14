<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Security_System.Dashboard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <link rel="stylesheet" href="css/main.css"/>
    <link rel="stylesheet" href="css/register.css"/>
    <style>
        .jumbotron {
            width: 450px;
        }
    </style>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/2.9.3/Chart.min.js" integrity="sha512-s+xg36jbIujB2S2VKfpGmlC3T5V2TF3lY48DX7u2r9XzGzgPsa6wTpOQA7J9iffvdeBN0q9tKzRxVxw1JviZPg==" crossorigin="anonymous"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <h4>Dashboard</h4>
    <p>Summary to your account</p>
    <div class="container-12">
        <div class="row">
            <div class="col-md-3">
                <div class="summary-card">
                    <img src="img/legislation.svg" alt="">
                    <h6>Active Cases</h6>
                    <small id="houseActiveCases" runat="server"> </small>
                </div>
            </div>
            <div class="col-md-3">
                <div class="summary-card">
                    <img src="img/group.svg" alt="">
                    <h6>Occupants</h6>
                    <small id="numOccupnats" runat="server"></small>
                </div>
            </div>
            <div class="col-md-3">
                <div class="summary-card" runat="server" id="dispOwners">
                    <img src="img/group.svg" alt="">
                    <h6>HomeOwners</h6>
                    <small runat="server" id="numHomeOwners">4</small>
                </div>
            </div>
            <div class="col-md-3">
                <div class="summary-card" runat="server" id="dispResp">
                    <img src="img/group.svg" alt="">
                    <h6>Respondents</h6>
                    <small id="numRespondents" runat="server"></small>
                </div>
            </div>
        </div>
        <div class="col-md-6">
            <div class="alarm">
        
            </div>
        </div>
        <div class="col-md-3">
            <div class="input-form input-home" runat="server" id="home" visible="false">
                <br />
                    <h6>Your House</h6>
                    <div class="group-home">
                        <label for="City">Province</label>
                        <input type="text" id="province"  runat="server"/>
                    </div>
                    <div class="group-home">
                        <label for="City">City</label>
                        <input type="text" id="City"  runat="server"/>
                    </div>
                    <div class="group-home">
                        <label for="Surburb">Surburb</label>
                        <input type="text" id="Surburb" runat="server"/>
                    </div>
                    <div class="group-home">
                        <label for="Street Name">Street Name</label>
                        <input type="text" id="StreetName" runat="server" />
                    </div>
                    <div class="group-home">
                        <label for="House">House No.</label>
                        <input type="text" id="HouseNo" runat="server" />
                    </div>
                    <div class="group-home">
                        <label for="ZIPCode">ZIP Code</label>
                        <input type="text" id="ZIPCode" runat="server"/>
                    </div>
            </div>
        </div>
        </div>
        <div class="container-fluid">
            <div class="row">
                <div class="col-md-8">
                    <div class="chartsJS" runat="server" id="charts" >
                         <canvas id="myChart" width="400" height="220"></canvas>
                    </div>
                </div>
                <div class="col-md-4">
                    <canvas id="myChart1" width="400" height="400" runat="server"></canvas>
                </div>
            </div>
        </div>
        <h4>More information on cases</h4>
        <p>Intevals and cases under investigation</p>
        <div class="container-fluid">
            
            <div class="row">
                <div class="col-md-6">
                    <div class="chartsJS" runat="server" id="no" >
                         <canvas id="barGraph" width="400" height="200" runat="server"></canvas>
                    </div>
                </div>
                <div class="col-md-6">
                    <canvas id="lineGraph" width="400" height="200" runat="server"></canvas>
                </div>
            </div>

        </div>
        <div id="disCharts" runat="server">

        </div>

        

        <script>
            dashboard.classList.add("active");
            var ctx = document.getElementById('lineGraph').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['Cold cases', 'Investigation underway', 'closed'],
                    datasets: [{
                        label: 'Cases',
                        data: [5, 19, 6],
                        borderColor: [
                            'rgba(255, 206, 86)',
                            'rgba(255, 206, 86)',
                            'rgba(255, 206, 86)',

                        ],
                        backgroundColor: [
                            'rgba(255, 206, 86)',
                            'rgba(255, 206, 86)',
                            'rgba(255, 206, 86)'
                        ],
                        fill: true,
                        borderWidth: 2
                    }]
                },
                options: {
                    scales: {
                        xAxes: [
                            {
                                gridLines: {
                                    display: false
                                }
                            }
                        ],
                        yAxes: [
                            {
                                gridLines: {
                                    display: false
                                }
                            }
                        ]
                    }
                }
            });

            var ctx = document.getElementById('barGraph').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'bar',
                data: {
                    labels: ['00h00-04h00', '04h01-08h00', '08h01-12h00', '12h01-16h00', '16h01-20h00', '20h01-24h00'],
                    datasets: [{
                        label: 'Intervals',
                        data: [12, 19, 3, 5, 5, 15],
                        backgroundColor: [
                            'rgba(0, 29, 36)',
                            'rgba(0, 29, 36)',
                            'rgba(0, 29, 36)',
                            'rgba(0, 29, 36)',
                            'rgba(0, 29, 36)',
                            'rgba(0, 29, 36)'
                        ],
                        borderColor: [
                            'rgba(180, 118, 19, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(0, 29, 36, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(54, 162, 235, 1)'
                        ],
                        borderWidth: 1

                    }]
                },
                options: {
                    scales: {
                        xAxes: [
                            {
                                gridLines: {
                                    display: false
                                }
                            }
                        ],
                        yAxes: [
                            {
                                gridLines: {
                                    display: false
                                }
                            }
                        ]
                    }
                }
            });
            var ctx = document.getElementById('myChart1').getContext('2d');
            var myChart = new Chart(ctx, {
                type: 'doughnut',
                data: {
                    labels: ['Panic', 'Door Breakin', 'Window Breakin', 'Outdoor sensors tricked'],
                    datasets: [{
                        label: '# of Votes',
                        data: [12, 19, 3, 5],
                        backgroundColor: [
                            'rgb(180, 118, 19)',
                            'rgba(54, 162, 235)',
                            'rgba(255, 206, 86)',
                            'rgba(0, 29, 36)'
                        ],
                        borderColor: [
                            'rgba(180, 118, 19, 1)',
                            'rgba(54, 162, 235, 1)',
                            'rgba(255, 206, 86, 1)',
                            'rgba(0, 29, 36, 1)'
                        ],
                        borderWidth: 1
                    }]
                }
            });
        </script>
    <%--<script>
        dashboard.classList.add("active");
        var ctx = document.getElementById('myChart').getContext('2d');
        var myChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: ['1', '2', '3', '4', '5', '6', '7'],
                datasets: [{
                    label: 'Cases of last 7 days',
                    data: [12, 19, 3, 5, 2, 3, 4],
                    backgroundColor: [
                        'rgba(255, 99, 132, 0.2)',
                        'rgba(54, 162, 235, 0.2)',
                        'rgba(255, 206, 86, 0.2)',
                        'rgba(75, 192, 192, 0.2)',
                        'rgba(153, 102, 255, 0.2)',
                        'rgba(255, 159, 64, 0.2)',
                        'rgba(255, 159, 64, 0.2)'
                    ],
                    borderColor: [
                        'rgba(255, 99, 132, 1)',
                        'rgba(54, 162, 235, 1)',
                        'rgba(255, 206, 86, 1)',
                        'rgba(75, 192, 192, 1)',
                        'rgba(153, 102, 255, 1)',
                        'rgba(255, 159, 64, 1)',
                        'rgba(255, 159, 64, 1)'
                    ],
                    borderWidth: 1
                }]
            },
            options: {
                scales: {
                    yAxes: [{
                        ticks: {
                            beginAtZero: true
                        }
                    }]
                }
            }
        });
        
    </script>--%>
</asp:Content>
