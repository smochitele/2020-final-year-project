<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="Security_System.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register </title>
    <script src="https://kit.fontawesome.com/a076d05399.js"></script>
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <link rel="stylesheet" href="css/bootstrap.css"/>
    <link rel="stylesheet" href="css/main.css"/>
    <link rel="stylesheet" href="css/register.css"/>
    
      <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
      <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
      <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
</head>
<body>
    <form id="form1" runat="server" class="input-form">
        <div class="jumbotron">
            <div class="prog">
                <p class="prog-active">1</p>
                <p>2</p>
                <p>3</p>
                <hr/>
            </div>
            <h3>Register</h3>
            <small>Add your personal details</small>
            <div class="group-input">
                <label for="Name">Name</label>
                <input type="text" id="Name" runat="server"  required/>
            </div>
            <div class="group-input">
                <label for="Lastname">Lastname</label>
                <input type="text" id="Lastname" runat="server" required/>
            
            </div>
            <div class="group-input">
                <label for="email">Email</label>
                <input type="email" id="Email" runat="server" onkeyup="ValidateEmail(Email)" required/>
                <small id="textEmail">Your email is invalid.</small>
            </div>
            <div class="group-input">
                <label for="IDNumber">SA ID Number</label>
                <input type="text" id="IDNumber" runat="server"  onkeyup="validateID(IDNumber)" required/>
                <small id="textID">Your ID number is invalid.</small>
            
            </div>
            <div class="group-input">
                <label for="password">Password</label>
                <input type="password" id="Password" runat="server" onkeypress="ValidateStrongPassword(Password)" data-toggle="tooltip" data-html="true" data-placement="right" title="Password requirements <br>1. 8 character long<br>2. Have lowercase<br>3. Have uppercase<br>4. Contain a digit" required/> 
                <small id="validate"> </small>
            </div>

            <div class="group-input">
                <label for="confirmPassword">Confirm Password</label>
                <input type="password" id="confirmPassword" runat="server"  onkeyup="ValidatePassword()" required />
                <small id="passwordMessage">Your passwords do not match</small>
            </div>
            <div class="group-links">
                <input type="checkbox" id="terms" required/>
                <a href="TermsAndConditions.aspx">Agree to our terms and conditions</a>
            </div>
            <div class="group-input">
                <asp:Button OnClick="AddUser_Click" Text="Submit" ID="AddUser" runat="server" class="aspButton"/>
            </div>
            <div class="group-links">
                <a href="Login.aspx">Login here</a>
                <a href="TermsAndConditions.aspx">Our</a>
            </div>
        </div>  
    </form>

    <script>
        $(document).ready(function () {
            $('[data-toggle="tooltip"]').tooltip();
        });
        let pass1 = document.querySelector("#Password");
        let pass2 = document.querySelector("#confirmPassword");

        const ValidatePassword = () => {
            console.log(pass1.value + " " + pass2.value)
            if (pass1.value.match(pass2.value)) {
                document.querySelector("#passwordMessage").style.display = "none";
                return;
            }
            else {
                document.querySelector("#passwordMessage").style.display = "block";
                return;
            }
        }
        
        const ValidateStrongPassword = (Pass) => {
            var lowerCaseLettersReg = /[a-z]/g;
            var upperCaseLettersReg = /[A-Z]/g;
            var numbersReg = /[0-9]/g;
            var passwordValue = Pass.value;
            //Verifying if the passwords match
            if (passwordValue.length < 8 || !passwordValue.match(lowerCaseLettersReg) || !passwordValue.match(upperCaseLettersReg) || !passwordValue.match(numbersReg)) {
                document.querySelector("#validate").innerText = "Your password is weak";
                document.querySelector("#validate").style.color = "red";
                document.querySelector("#validate").style.display = "block";
                return;
            }
            else {
                document.querySelector("#validate").innerText = "Your password is strong";
                document.querySelector("#validate").style.color = "green";
                document.querySelector("#validate").style.display = "block";
                return;             
            }
                
        }
        //This function take the email as argument and matches it with a reg ex
        function ValidateEmail(inputText) {
            var mailformat = /^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$/;
            if (inputText.value.match(mailformat)) {
                console.log("here1");
                document.querySelector("#textEmail").style.display = 'none';
                return true;
            }
            else {
                console.log("here");
                document.querySelector("#textEmail").style.display = 'block';
            }
        }
        //Checking the validate of a South African ID
        function validateID(IDN) {
            var ex = /^(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))/;       
            var theIDnumber = IDN.value;
            if (ex.test(theIDnumber) == false) {
                document.querySelector("#textID").style.display = 'block';
                return false;
            }
            document.querySelector("#textID").style.display = 'none';
            return false;
        }

    </script>
    
</body>
</html>
