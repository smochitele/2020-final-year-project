<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Cases.aspx.cs" Inherits="Security_System.Cases" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/occupants.css"/>
    <link  rel="stylesheet" href="css/cases.css"/>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager runat="server" ID="Manager" EnablePageMethods="true"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="ContentManager">
        <ContentTemplate>
            <h4>Case management</h4>
            <p>All the cases related to your entity</p>
            <small>Filter</small>
            <asp:DropDownList class="dropMenu" runat="server" ID="SortCases" AutoPostBack="true" OnSelectedIndexChanged="SortCases_SelectedIndexChanged">
                <asp:ListItem Text="All" Value="1"/>
                <asp:ListItem Text="Resolved" Value="2"/>
                <asp:ListItem Text="Unresolved" Value="3"/>      
            </asp:DropDownList>
            <input type="text" id="searchCases" placeholder="Search cases" class="searchingClass"/>
                <div class="contDainer">
                    <div class="row" runat="server" id="dbCases">
                        <div class="col-md-12">
                    
                    <table class="our-table occupants-table">
                        <thead>
                            <tr>
                                <th>Case</th> 
                                <th>Date</th>
                                <th>Resolution Date</th>
                                <th>Status</th>
                                <th>Action</th>
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
                <h5 class="modal-title" id="exampleModalLabel">More Information</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                 <div class="">
                    <div class=" group-input ">
                        <label for="Name">Case ID</label>
                        <input type="text" id="Description" runat="server" />           
                    </div>
                     
                    
                    
                   </div>
              </div>
              
            </div>
          </div>
        </div>
            <script>
                let caseID = document.querySelector("#CaseID");
                let caseDate = document.querySelector("#Date");
                let caseDesc = document.querySelector("#Desciption");
                let caseType = document.querySelector("#Res");
                let caseHouse = document.querySelector("#HouseNumber");
                let caseUser = document.querySelector("#CaseID");
                let caseRes = document.querySelector("#ResName");


                console.log("I am in here")
                casess.classList.add("active");
                function PopulateData(info4) {
                    
                    caseDesc.value = info4;
                }
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
            
    </ContentTemplate>
        
    </asp:UpdatePanel>
    
</asp:Content>
