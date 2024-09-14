<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="WorkingLibrary.aspx.cs" Inherits="Security_System.WorkingLibrary" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <link rel="stylesheet" href="css/occupants.css"/>
     <link  rel="stylesheet" href="css/cases.css"/>
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>On Duty</h4>
    <p>The details of the respondent on or off duty</p>
    <input type="text" id="searchCases" placeholder="Search respondent" class="searchingClass"/>
        <div class="contDainer">
            <div class="row" runat="server" id="dbCases">
                <div class="col-md-12">
            <table class="our-table occupants-table">
                <thead>
                    <tr>
                        <th></th>
                        <th>Full Name</th> 
                        
                        <th>Status</th>
                    </tr>
                </thead>
            <tbody id="dbRespondets" runat="server">
                       
            </tbody>
        </table>
    </div>
        </div>
    </div>
    <script>
        let inputAct = document.querySelector('#searchCases');
        let searchValue = document.querySelector('#searchCases');
        let cases = document.querySelectorAll('tbody tr');
        //search live the school table
        inputAct.addEventListener('input', () => {
            Array.from(cases).forEach((cs) => {
                let content = cs.textContent.toLowerCase();
                if (content.indexOf(searchValue.value.toLowerCase()) != -1) {
                    cs.style.display = 'table-row';
                    console.log('found');
                }
                else {
                    cs.style.display = 'none';
                }
            });
        });
    </script>
</asp:Content>
