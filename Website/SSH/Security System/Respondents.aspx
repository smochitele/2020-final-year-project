<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Respondents.aspx.cs" Inherits="Security_System.Respondents" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/occupants.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Respondents Management</h4>
    <p>All the respondents in your organization are under this table. You can <i>edit and delete</i>  when necessary</p>
    <div class="">
        <div class="row">
            <div class="col-md-12">
                <div class="button">
                    <button type="button" class="our-btn btnAdd" data-toggle="modal" data-target="#exampleModal">Add Respondent</button>
                     <input type="text" id="searchCases" placeholder="Search respondent" class="searchingClass"/>
                </div>
                <table class="our-table occupants-table">
                    <thead>
                        <tr>
                            <th>No.</th> 
                            <th>Name</th>
                            <th>Lastname</th>
                            <th>Email</th>
                            <th>View</th>
                            
                        </tr>
                    </thead>
                    <tbody id="dbRespondents" runat="server">
                       
                    </tbody>
                </table>
            </div>
        </div>
    </div>
    <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content"> 
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add Respondent</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <span aria-hidden="true">&times;</span>
                </button>
              </div>
              <div class="modal-body">
                 <div class="">
                    <div class=" group-input ">
                        <label for="Name">Name</label>
                        <input type="text"  name="username" id="Name" runat="server" required/>           
                    </div>
                    <div class="group-input">
                        <label for="Password">Surname</label>
                        <input type="text" name="surname" id="Surname" runat="server" required/>  
                    </div>
                     <div class="group-input">
                        <label for="IDNumber">ID Number</label>
                        <input type="text" name="ID" id="IDNumber" runat="server" required/>  
                    </div>
                    <div class="group-input">
                        <label for="Password">Email</label>
                        <input type="email" name="email" id="Email" runat="server" required/>  
                    </div>
                     <div class="group-input">
                         <label>Assign Car</label>
                         <asp:DropDownList ID="Cars" runat="server">
                            <asp:ListItem>Car 1</asp:ListItem>
                            <asp:ListItem>Car 2</asp:ListItem>
                            <asp:ListItem>Car 3</asp:ListItem>
                            <asp:ListItem>Car 4</asp:ListItem>
                         </asp:DropDownList>
                     </div>
                     <div class="group-input">
                        <small class="text-danger">*All new added respondents will use their emails passwords</small>
                    </div>
                   </div>
              </div>
              <div class="modal-footer">
                    <button type="button" class="btn  btnCancel" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="AddRespondent" runat="server" Text="Add" class="btn btnAdd" OnClick="AddRespondent_Click"/>                
              </div>
            </div>
          </div>
        </div>

    <script>
        respodents.classList.add("active");
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
