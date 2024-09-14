<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="CaseInformation.aspx.cs" Inherits="Security_System.CaseInformation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .jumbotron {
            margin-top: 2em !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h3>Case Information</h3>
        <div class=" group-input ">
            <label for="Name">Case ID</label>
            <input type="text"  name="username" id="ID" runat="server" readonly/>           
        </div>
            <div class="group-input">
            <label for="IDNumber">Date</label>
            <input type="text" name="Surname" id="Date" runat="server" readonly/>  
        </div>
        <div class="group-input">
            <label for="Password">House No</label>
            <input type="email" name="email" id="HouseNo" runat="server" readonly/>  
        </div>
        <div class="group-input">
            <label for="Password">Description</label>
            <input type="email" name="email" id="Description" runat="server" readonly/>  
        </div>
            
            <div class="group-input group-links">
                
            <a href="Dashboard.aspx">Go Back</a>
        </div>
        </div>
    <script>
        duty.classList.add("dutyies");
    </script>
</asp:Content>
