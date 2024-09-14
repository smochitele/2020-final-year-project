<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="CasesPerCity.aspx.cs" Inherits="Security_System.CasesPerCity" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Case by city</h4>
    <p>All the cases related to this city</p>
    <div class="contDainer">
        <div class="row" runat="server" id="dbCases">
            <div class="col-md-12">                   
                <table class="our-table occupants-table">
                    <thead>
                        <tr>
                            <th>Case</th> 
                            <th>Date</th>
                            <th>Response Time</th>
                            <th>Resolution Date</th>
                            <th>House ID</th>
                            <th>User ID</th>
                            <th>Respondent ID</th>
                        </tr>
                    </thead>
                    <tbody id="dbCase" runat="server">
                       
                    </tbody>
                </table>
                </div>
             </div>
      </div>



</asp:Content>
