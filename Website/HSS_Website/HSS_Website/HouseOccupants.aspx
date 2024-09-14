<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="HouseOccupants.aspx.cs" Inherits="HSS_Website.HouseOccupants" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>House Occupants</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
    
    <div class="side-menu" id="side">
        <div class="container">

            <h1>Manage <br />House</h1>
            <a href="Home.aspx" >Personal Details</a>
            <a href="Package.aspx" class="">Package</a>
            <a href="Payments.aspx" class="">Payments</a>
            <a href="Houseoccupants.aspx" class="activeitem">House Occupants</a>
            <a href="Logout.aspx">Log Out</a>
            <div>

            </div>
        </div>
    </div>
    <form runat="server" id="form1">
    <div class="container mt-4" id="mainSection">
        <h3 class="m-4">House Management</h3>
        <p class="m-4">All the occupants of your house are under this table. They can use the Panic Alert, Switch On & Off your alarm system</p>
        <div class="row mt-5">
            <div class="col-md-8">
                <table class="occupants-table">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th>Name</th>
                        <th>Email</th>
                        <th>Action</th>
                    </tr>
                       
                </thead>
                <tbody runat="server" id="occupants">
                   
                </tbody>
            </table>
            </div>
            <div class="col-md-4 add-occupant">
                <div class="add-form my-3">
                    <h5 class="mx-5">Add New Occupant </h5>
                   <div class="login-input">
                      <label for="name">Name</label>
                       <input type="text" name="name" id="name" runat="server" />
                    </div>
                <div class="login-input">
                    <label for="username">Last name</label>
                    <input type="text" name="lastname" id="lastname" runat="server" />
                </div>
<%--            <div class="login-input">
                <label for="id">Identity No.</label>
                <input type="text" name="idnumber" id="idnumber" runat="server" />
                </div>--%>
                <div class="login-input">
                    <label for="username">Email</label>
                    <input type="email" name="email" id="email" runat="server" />
                </div>
                <div class="login-input">
                     <asp:Button ID="RegisterOccupant" runat="server" Text="Add Occupant" class="button"  OnClick ="RegisterOccupant_Click1"/>
                </div>
                <div class="login-input px-5">
                    <small class=" text-danger">New added occupants will use their username as password</small>
                </div>
                </div>
            </div>
        </div>
        </div>
     </form>
    <script>
        const delButton = document.querySelector('#delete');
       // delButton.addEventListener('click', deleteOccupant);

        function deleteOccupant(id, nam){
            if (confirm("Are you sure you want to delete this occupant?")) {
                window.location.href = `HouseOccupants.aspx?delete=${id}`;
            }
            else {
                window.location.href = 'HouseOccupants.aspx';
            }
        }
    </script>
</body>
</html>
