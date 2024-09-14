<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Neighborhood.aspx.cs" Inherits="Security_System.Neighborhood" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Your Neighborhood</h4>
    <p>A summary detailing crime in your neighborhood</p>
    <div class="row">
        <div class="col-md-6">
            <table class="our-table occupants-table">
                <thead>
                    <tr>
                        <th>Case ID</th> 
                        <th>Date</th>
                        <th>Resolution Date</th>
                    </tr>
                </thead>
                <tbody id="dbCases" runat="server">
                       
                </tbody>
            </table>
        </div>
        <div class="col-md-6">
            <h4>Peak times</h4>
            <h6 id="peaks" runat="server"></h6>
        </div>
    </div>
    <script>
        watch.classList.add("active");
    </script>
</asp:Content>
