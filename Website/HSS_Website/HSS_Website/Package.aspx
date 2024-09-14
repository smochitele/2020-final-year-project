<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Package.aspx.cs" Inherits="HSS_Website.Package" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Package</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
   
    <div class="side-menu" id="side">
        <div class="container">

            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
            <h1>Manage <br />House</h1>
            <a href="Home.aspx" >Personal Details</a>
            <a href="Package.aspx" class="activeitem">Package</a>
            <a href="Payments.aspx" class="">Payments</a>
            <a href="Houseoccupants.aspx">House Occupants</a>
            <a href="Logout.aspx">Log Out</a>
        </div>
    </div>
    <form runat="server" id="form1">
    <div class="container mt-5" id="mainSection">
        <h3 class="m-4">Package Management</h3>
        <p class="mx-4">Under this section, you get to choose which package suits your budget plan. 
        The packages offer different values</p>
        <div class="row mt-5">
            <div class="col-md-3">
                <div class="package1">
                    
                    <ul>
                        <li class="heading1">
                            Normal Package
                        </li>
                        <li class="pricing">
                            R1200/pm
                        </li>
                        <li>
                             Response Rate
                        </li>
                        <li>
                            GPS Coordinates Functionality
                        </li>
                        <li>
                            Images Used
                        </li>
                        <li>
                            Up To 10 Occupants
                        </li>
                    </ul>
                </div>

            </div>
            <div class="col-md-3">
                <div class="package3">
                    
                    <ul>
                        <li class="heading3">
                            Medium Package
                        </li>
                        <li class="pricing">
                            R2000/pm
                        </li>
                        <li>
                             Response Rate
                        </li>
                        <li>
                            GPS Coordinates Functionality
                        </li>
                        <li>
                            Images Used
                        </li>
                        <li>
                            Up To 20 Occupants
                        </li>
                    </ul>
                </div>

            </div>
            <div class="col-md-3">
                <div class="package2">
                    
                    <ul>
                        <li class="heading2">
                            Premium Package
                        </li>
                        <li class="pricing">
                            R5000/pm
                        </li>
                        <li>
                            High Response Rate
                        </li>
                        <li>
                            Directions Given
                        </li>
                        <li>
                            Videos & Images Used
                        </li>
                        <li>
                           More Than 30 Occupants
                        </li>
                    </ul>
                </div>
            </div>
            <div class="col-md-3 my-5">
                <h3>Choose Package</h3>
            <div class="login-input">
                <select >
                    <option>Package 1</option>
                    <option>Package 2</option>
                    <option>Package 3</option>
                </select>
                <br />
                <small>I agree to the terms and conditions of using this package</small>
                <br />
                <asp:Button ID="submit" runat="server" Text="Take Package" class="btn button" OnClick="submit_Click"/>
            </div>
            </div>
            
        </div>
        </div>
     </form>

    <script>
        let package1 = document.querySelector('#package1');
        let package2 = document.querySelector('#package2');
        let package3 = document.querySelector('#package3');
       
        
    </script>
</body>
</html>
