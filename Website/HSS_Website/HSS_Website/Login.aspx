<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="HSS_Website.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="Styles/bootstrap.css" rel="stylesheet" />
    <link href="Styles/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <div class="container">
        <div class="login-form">
            <div class="form-title">
                <h3>Login</h3>
            </div>
            <div class="login-input">
                <label for="username">Username</label>
                <input type="text" name="username" id="username" runat="server" />
            </div>
            <div class="login-input">
                <label for="password">Password</label>
                <input type="password" name="password" id="password" runat="server" />
            </div>
            <div class="login-input">
                <asp:Button ID="LoginButton" runat="server" Text="Login" class="button" OnClick="LoginButton_Click" />
            </div>
            <a href="Register.aspx" class="register-link">Don't have account? Register</a>
        </div>
    </div>
    </form>

    <script src="Scripts/app.js"></script>
</body>
</html>
