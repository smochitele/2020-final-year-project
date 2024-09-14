<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="PanicAlert.aspx.cs" Inherits="Security_System.PanicAlert" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="jumbotron">
        <h3>Respondent responsible for the alert</h3>
        <div class=" group-input ">
            <label for="Name">Name</label>
            <input type="text"  name="username" id="Name" runat="server" readonly/>           
        </div>
            <div class="group-input">
            <label for="IDNumber">Surname</label>
            <input type="text" name="Surname" id="Surname" runat="server" readonly/>  
        </div>
        <div class="group-input">
            <label for="Password">Email</label>
            <input type="email" name="email" id="Email" runat="server" readonly/>  
        </div>
            
            <div class="group-input group-links">
                
            <a href="Dashboard.aspx">Go Back</a>
        </div>
        </div>
</asp:Content>
