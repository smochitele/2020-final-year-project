<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Security_System.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <link rel="stylesheet" href="css/main.css"/>
</head>
<body>
    <form id="form1" runat="server" class="input-form">
        <div class="jumbotron">
            <h3>Login</h3>
            <div class="group-input pt-0">
            </div>
            <div class=" group-input ">
                <label for="Username">Email</label>
                <input type="email"  name="username" id="Username" runat="server"/>           
            </div>
            <div class="group-input">
                <label for="Password">Password</label>
                <input type="password" name="password" id="Password" runat="server"/>  
            </div>
            <div class="group-input">
                <asp:Button Text="Submit" ID="Submit" runat="server" OnClick="Submit_Click" class="aspButton"/>  
            </div>
            <div class="group-input errors text-center alert alert-danger" runat="server" id="err">
                <p>Your credentials do not match...</p>
            </div>
            <div class="group-links">
                <a href="/forgotPassword">Forgot Password?</a> |<a href="Register.aspx"> Register here</a>
            </div>
        </div>
     </form>
</body>
</html>
