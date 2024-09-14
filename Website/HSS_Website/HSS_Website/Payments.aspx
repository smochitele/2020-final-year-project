<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Payments.aspx.cs" Inherits="HSS_Website.Payments" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Manage Payments</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
    <div class="side-menu" id="side">
        <div class="container">

            <a href="javascript:void(0)" class="closebtn" onclick="closeNav()">&times;</a>
            <h1>Manage <br />House</h1>
            <a href="Home.aspx" class="">Personal Details</a>
            <a href="Package.aspx">Package</a>
            <a href="Payments.aspx" class="activeitem">Payments</a>
            <a href="Houseoccupants.aspx">House Occupants</a>
            <a href="Logout.aspx">Log Out</a>
        </div>
    </div>
    <form runat="server" id="form1">
    <div class="container mt-5" id="mainSection">
        <h3 class="m-4">House Management</h3>
        <p class="m-4">All these are the records of payment made by this account</p>
        <div class="row mt-5">
            <div class="col-md-4">
                <div class="payments">
                    <h2>May, 2020</h2>
                    <small>REF: 12376fjg47bd</small>
                    <p>Amount: R2000</p>
                    <p>Remaining amount: R00</p>
                </div>
            </div>
            <div class="col-md-4">
                <div class="payments">
                    <h2>June, 2020</h2>
                    <small>REF: GFT34dshveg</small>
                    <p>Amount: R2000</p>
                    <p>Remaining amount: R00</p>
                </div>
            </div>
            <div class="col-md-4">
                <div class="payments">
                    <h2>July, 2020</h2>
                    <small>REF: 12376fjg47bd</small>
                    <p>Amount: R1500</p>
                    <p>Remaining amount: R500</p>
                </div>
            </div>
        </div>
    </div>
     </form>
</body>
</html>
