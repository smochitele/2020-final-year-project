<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NearestPersonnel.aspx.cs" Inherits="HSS_Website.NearestPersonnel" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <title>Nearest Personnel</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container mt-5 info">
            
            <div class="info register-form">
                <div class="form-title mt-4">
                    <h4>The respondent responsible for your rescue</h4>
                </div>
                <div class="login-input">
                    <label for="name">Name</label>
                    <input type="text" name="name" id="name" runat="server" />
                </div>
                <div class="login-input">
                    <label for="name">Lastname</label>
                    <input type="text" name="name" id="lastname" runat="server" />
                </div>
                <div class="login-input">
                    <label for="name">Distance</label>
                    <input type="text" name="name" id="distance" runat="server" />
                </div>
                <div class="login-input">
                    <label for="name">Estimated Response Time</label>
                    <input type="time" name="name" id="responsetime" runat="server" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
