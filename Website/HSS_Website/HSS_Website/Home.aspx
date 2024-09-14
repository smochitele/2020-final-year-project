<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="HSS_Website.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Homepage</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
    <span onclick="openNav()">Manage Here</span>
    <div class="side-menu" id="side">
        <div class="container">

            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
            <h1>Manage <br />House</h1>
            <a href="Home.aspx" class="activeitem">Personal Details</a>
            <a href="Package.aspx">Package</a>
            <a href="Payments.aspx">Payments</a>
            <a href="Houseoccupants.aspx">House Occupants</a>
            <a href="Logout.aspx">Log Out</a>
        </div>
    </div>
    <form runat="server" id="form1">
    <div class="container mt-5" id="mainSection">
        <div class="row mt-5">
            <div class="col-md-12">
                <div class="login-input">
                    
                    <div class="info-personnel">
                        <img src="images/user.png" />
                        <p>Name: <span id="name" runat="server"></span></p>
                        <p>Lastname: <span id="lastname" runat="server"></span></p>
                    </div>
                </div>
                <div class="login-input m-5 panic">
                    <asp:Button ID="PanicButton" runat="server" Text="Panic Alert" class="button" OnClick="PanicButton_Click"/>
                </div>
            </div>
        </div>
    </div>
     </form>

    <script>
       
    </script>
</body>
</html>
