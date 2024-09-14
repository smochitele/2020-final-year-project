<%@ Page Title="" Language="C#" MasterPageFile="~/MainLayout.Master" AutoEventWireup="true" CodeBehind="Occupants.aspx.cs" Inherits="Security_System.Occupants" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="css/occupants.css"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <h4>Occupants Management</h4>
    <p>All the occupants in your house are under this table. You can edit and delete when necessary</p>


<!-- Modal -->
    
    <div class="">
        <div class="row">
            <div class="col-md-12">
                <div class="button">
                    <button type="button" class="our-btn btnAdd" data-toggle="modal" data-target="#exampleModal">Add Occupant</button>
                    <input type="text" id="searchCases" placeholder="Search occupant" class="searchingClass"/>
                </div>
                <table class="our-table occupants-table">
                    <thead>
                        <tr>
                            <th>No.</th> 
                            <th> Name</th>
                            <th>Lastname</th>
                            <th>Email</th>
                            <th>Action</th>
                        </tr>
                    </thead>
                    <tbody id="Ocuppants" runat="server">
                       
                    </tbody>
                </table>
            </div>
        </div>
    </div>
       <div class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
          <div class="modal-dialog">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLabel">Add Occupant</h5>
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
                        <small class="text-danger">*All new added occupants will use their emails passwords</small>
                    </div>
                   </div>
              </div>
              <div class="modal-footer">
                    <button type="button" class="btn  btnCancel" data-dismiss="modal">Cancel</button>
                    <asp:Button ID="AddOccupant" runat="server" Text="Add" class="btn btnAdd" OnClick="AddOccupant_Click" />                
              </div>
            </div>
          </div>
        </div>

    <script>
        occupants.classList.add("active");
        const DeleteOccupant = ID => {
            if (confirm("Are you sure you want to delete this occupant?")) {
                window.location.href = `occupants.aspx?deleteID=${ID}`;
            }
            else {
                
            }
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
</asp:Content>
