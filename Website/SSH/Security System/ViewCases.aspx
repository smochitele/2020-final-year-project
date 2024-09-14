<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="ViewCases.aspx.cs" Inherits="Security_System.ViewCases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.js"></script>
    <link href="https://api.mapbox.com/mapbox-gl-js/v1.12.0/mapbox-gl.css" rel="stylesheet" />
    <script src='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.min.js'></script>
    <link rel='stylesheet' href='https://api.mapbox.com/mapbox-gl-js/plugins/mapbox-gl-geocoder/v4.2.0/mapbox-gl-geocoder.css' type='text/css' />
    <style>
        #map { 
            position: absolute; 
            top: 3.5%; 
            bottom: 0; 
            width: 100% !important; 
            height: 100%;
            
        }
        .newMap {
            width: 300px;
            height: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Cases per Respondent</h4>
    <p>The details of the respondent responsible for these cases</p>
    <div class="personal-details">
        <h5 id="userName" runat="server"></h5>
        <button type="button" class="btnAdd our-btn" data-toggle="modal" data-target="#exampleModal">View Respondent on Map</button>
        <img src="img/envelope.svg"><small id="userEmail" runat="server">: </small>
        <img src="img/register.svg" ><small id="idNumber" runat="server">: </small>
        <div class="cntainer">
            <div class="row">
                <div class="col-md-12">
                    <table class="our-table occupants-table">
                       <thead>
                            <tr>
                                <th>Case</th> 
                                <th>Date</th>
                                <th>Resolution Date</th>
                                <th>Status</th>
                            </tr>
                        </thead>
                        <tbody id="dbCase" runat="server">
                       
                        </tbody>
                        </table>
                </div>
            </div>
        </div>
        <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Respondent Location</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                 <div class="newMap">
                    <div id="map"></div>
                   </div>
              </div>
              
            </div>
          </div>
        </div>
        <div id="mapRes" runat="server"></div>
    </div>
</asp:Content>
