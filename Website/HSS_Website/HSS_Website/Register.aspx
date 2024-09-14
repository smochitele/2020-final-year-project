<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="HSS_Website.Register" %>

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
                <label for="password">Password</label>
                <input type="password" name="password" id="password" runat="server" />
            </div>
            <div class="login-input">
                <label for="password">Confirm Password</label>
                <input type="password" name="password" id="password1" runat="server" />
            </div>
            <div class="login-input">
                <asp:Button ID="RegisterButton" runat="server" Text="Next" class="button" OnClick="RegisterButton_Click" />
            </div>
            <a href="Login.aspx" class="register-link">Have an account? Login</a>
        </div>
    </div>
    </form>
</body>
</html>
