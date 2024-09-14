<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddPackage.aspx.cs" Inherits="Security_System.AddPackage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Choose Your Package</title>
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link rel="stylesheet" href="css/addpackage.css"/>
</head>
<body>
    <form id="form1" runat="server">
        <div class="jumbotron">
            <div class="prog">
                <p>1</p>
                <p>2</p>
                <p class="prog-active">3</p>
                <hr />
            </div>
            <div class="container mt-4">
                <h3 class="text-center">Choose your package</h3>
                <p class="text-center">They come at different prices for different needs</p>
                <div class="row">
                    <div class="col-md-4">
                        <div class="package1">
                            <ul class="package-list">
                                <li class="list-heading">Standard</li>
                                <li class="pricing">R400</li>
                                <li>10 occupants</li>
                                <li>Normal RR</li>
                                <li>GPS coordinates used</li>
                                <li>Images</li>
                                <li>Item #5</li>
                                <li><asp:Button class="addPackageBtn" Text="Choose" id="Package1" OnClick="Package1_Click" runat="server"/></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="package2">
                            <ul class="package-list">
                                <li class="list-heading">Normal</li>
                                <li class="pricing">R800</li>
                                <li>20 occupants</li>
                                <li>Good RR</li>
                                <li>GPS coordinates used</li>
                                <li>Images Used</li>
                                <li>Item #5</li>
                                <li><asp:Button class="addPackageBtn" Text="Choose" id="Package2" OnClick="Package2_Click" runat="server"/></li>
                            </ul>
                        </div>
                    </div>
                    <div class="col-md-4">
                        <div class="package3">
                            <ul class="package-list">
                                <li class="list-heading">Premium</li>
                                <li class="pricing">R1200</li>
                                <li>Unlimited occupants</li>
                                <li>High RR</li>
                                <li>Directions Given</li>
                                <li>Images and Videos used</li>
                                <li>Item #5</li>
                                <li><asp:Button class="addPackageBtn" Text="Choose" id="Package3" OnClick="Package3_Click" runat="server"/></li>
                            </ul>
                        </div>
                    </div>
                </div>
                <a href="RegisterHome.aspx" class="text-white">Go back</a>
            </div>
        </div>
    </form>
</body>
</html>
