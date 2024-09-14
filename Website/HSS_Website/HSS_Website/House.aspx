<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="House.aspx.cs" Inherits="HSS_Website.House" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
        <div class="register-form">
            <div class="form-title">
                <h3>Register</h3>
            </div>
            <div class="login-input">
                <label for="name">Province</label>
                <input type="text" name="name" id="province" runat="server" />
            </div>
            <div class="login-input">
                <label for="username">City</label>
                <input type="text" name="lastname" id="city" runat="server" />
            </div>
            <div class="login-input">
                <label for="id">Surburb</label>
                <input type="text" name="idnumber" id="surburb" runat="server" />
            </div>
            <div class="login-input">
                <label for="username">House No.</label>
                <input  name="email" id="houseno" runat="server" />
            </div>
            <div class="login-input">
                <label for="password">ZIP Code</label>
                <input  name="password" id="zip" runat="server" />
            </div>
            <div class="login-input">
                <asp:Button ID="RegisterButton" runat="server" Text="Finish Registration" class="button" OnClick="RegisterButton_Click" />
            </div>
            <a href="Login.aspx" class="register-link">Have an account? Login</a>
        </div>
    </div>
    </form>
</body>
</html>
