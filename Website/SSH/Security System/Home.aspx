<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="Security_System.Home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <title>Home of Secured Home Security</title>
</head>
<body>

    <nav class="navbar navbar-expand-lg navbar-light bg-light">
        <a class="navbar-brand" href="#">HomeSS</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarNav" aria-controls="navbarNav" aria-expanded="false" aria-label="Toggle navigation">
        <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarNav">
            <ul class="navbar-nav">
                <li class="nav-item">
                    <a class="nav-link" href="/">Home</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">About</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="#">Pricing</a>
                </li>               
            </ul>
            <ul class="navbar-nav ml-auto">
                <li class="nav-item login-btn">
                    <a class="nav-link" href="Login.aspx">Login</a>
                </li>
                <li class="nav-item">
                    <a class="nav-link" href="Register.aspx">Register</a>
                </li>
                             
            </ul>
        </div>
    </nav>
    <section class="home-section">
        <div class="container">
            <div class="row">
                <div class="col-md-12 text-center text-white p-5">
                    <h1 class="">Welcome To Smart Secured Home</h1>
                    <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English</p>
                </div>
                
            </div>
        </div>
    </section>

    <section class="about mt-5">
        <div class="container">
            <h3>ABOUT US</h3>
            <p>A brief background about us</p>
            <div class="row mt-5">
                <div class="col-md-4">
                    <img src="../img/auth.svg" alt=""/>
                </div>             
                <div class="col-md-8">
                   <div class="container">
                       <div class="row">
                           <div class="col-md-6">
                               <h4>Advanced</h4>
                               <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout</p>
                           </div>
                           <div class="col-md-6">
                               <h4>Less Costly</h4>
                               <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout</p>
                           </div>
                           <div class="col-md-6">
                               <h4>Modern</h4>
                               <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout</p>
                           </div>
                           <div class="col-md-6">
                               <h4>Secure</h4>
                               <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout</p>
                           </div>
                       </div>
                   </div>
                </div>
            </div>
        </div>
    </section>
    
    <section class="more-about">
         <div class="container">
            <div class="row">
                <div class="col-md-7 p-5">
                    <h1 class="">Break-ins in South Africa</h1>
                    <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English</p>
                    <p>It is a long established fact that a reader will be distracted by the readable content of a page when looking at its layout. The point of using Lorem Ipsum is that it has a more-or-less normal distribution of letters, as opposed to using 'Content here, content here', making it look like readable English</p>
                </div>
                <div class="col-md-5 py-5">
                    <img src="../img/alert.svg" alt="Alternate Text" />
                </div>
                
            </div>
        </div>
    </section>
    
    <form id="form1" runat="server">
        <div>
        </div>
    </form>
</body>
</html>
