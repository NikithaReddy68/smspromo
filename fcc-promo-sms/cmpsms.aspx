<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="cmpsms.aspx.cs" Inherits="fcc_promo_sms.cmpsms" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="To send SMS ">
    <meta name="author" content="Mobility International">
    <title>FCC | Promotional SMS</title>
    <!-- Favicon -->
    <link href="mi.ico" rel="icon" type="image/png">
    <!-- Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet">
    <!-- Icons -->
    <link href="assets/vendor/nucleo/css/nucleo.css" rel="stylesheet">
    <link href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" rel="stylesheet">
    <!-- Argon CSS -->
    <link type="text/css" href="assets/css/argon.css?v=1.0.0" rel="stylesheet">
    <link href="Styles/calendar-blue.css" rel="stylesheet" type="text/css" />
</head>




<body class="bg-default">
    <form id="form1" runat="server">
        <div class="main-content">
            <!-- Navbar -->
            <nav class="navbar navbar-top navbar-horizontal navbar-expand-md navbar-dark">
                <div class="container px-4">
                    <a class="navbar-brand" href="#">
                        <img src="assets/img/brand/logo.png" />
                    </a>
                    <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbar-collapse-main" aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                        <span class="navbar-toggler-icon"></span>
                    </button>
                    <div class="collapse navbar-collapse" id="navbar-collapse-main">
                        <!-- Collapse header -->
                        <div class="navbar-collapse-header d-md-none">
                            <div class="row">
                                <div class="col-6 collapse-brand">
                                    <a href="#">
                                        <img src="assets/img/brand/logo.png">
                                    </a>
                                </div>
                                <div class="col-6 collapse-close">
                                    <button type="button" class="navbar-toggler" data-toggle="collapse" data-target="#navbar-collapse-main" aria-controls="sidenav-main" aria-expanded="false" aria-label="Toggle sidenav">
                                        <span></span>
                                        <span></span>
                                    </button>
                                </div>
                            </div>
                        </div>
                        <!-- Navbar items -->
                        <ul class="navbar-nav ml-auto">
                            <li class="nav-item">
                                <a class="nav-link nav-link-icon" href="rs.aspx">
                                    <i class="fa fa-paper-plane"></i>
                                    <span class="nav-link-inner--text">Internal Reports
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link nav-link-icon" href="cmpsms.aspx">
                                    <i class="fa fa-paper-plane"></i>
                                    <span class="nav-link-inner--text">Compose SMS
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link nav-link-icon" href="psms.aspx">
                                    <i class="fa fa-paper-plane"></i>
                                    <span class="nav-link-inner--text">Upload excel</span>
                                </a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link nav-link-icon" href="index.aspx">
                                    <i class="ni ni-key-25"></i>
                                    <span class="nav-link-inner--text">Logout</span>
                                </a>
                            </li>

                        </ul>
                    </div>
                </div>
            </nav>
            <!-- Header -->
            <div class="header bg-gradient-primary py-7 py-lg-8">

                <%--<div class="separator separator-bottom separator-skew zindex-100">
                    <svg x="0" y="0" viewBox="0 0 2560 100" preserveAspectRatio="none" version="1.1" xmlns="http://www.w3.org/2000/svg">
                        <polygon class="fill-default" points="2560 0 2560 100 0 100"></polygon>
                    </svg>
                </div>--%>
            </div>
            <!-- Page content -->
            <div class="container mt--8 pb-5">
                <div class="row justify-content-center">
                    <div class="col-lg-5 col-md-7">
                        <div class="card bg-secondary shadow border-0">
                            <div class="card-header bg-transparent">
                                <h2><b>Compose SMS</b></h2>
                                <div class="ErrDiv b-login-form-error" id="divError" runat="server" visible="false">
                                    <asp:Label ID="errorMessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="card-body px-lg-5 py-lg-5">
                                <form role="form">

                                    <div class="form-group mb-3">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label class="form-control-label" for="input-username">Shortcode</label>
                                            </div>
                                            <div class="col-lg-8">
                                                <asp:DropDownList ID="ddlShortCode" runat="server" class="form-control form-control-alternative" ForeColor="Black" Font-Bold="true" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="form-group mb-3">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label class="form-control-label" for="input-username">Publish Time</label>
                                            </div>
                                            <%-- <div class="col-lg-2">
                                                <img src="assets/img/calender.png" />
                                            </div>--%>
                                            <div class="col-lg-8">
                                                <div class="row">
                                                    <asp:TextBox ID="txtPublishTime" runat="server" class="form-control form-control-alternative" ReadOnly="true" placeholder="Publish Time" Width="200px" ForeColor="Black" Font-Bold="true" />
                                                    <img src="assets/img/calender.png" width="30px" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <label class="form-control-label" for="input-username">
                                                    Recipients 
                                                   <%-- <h6 style="text-align: right">Upload Text File, Each line should have only one MSISDN with country code.</h6>--%>
                                                </label>

                                            </div>
                                            <div class="col-lg-8">
                                                <div class="row">
                                                    <asp:FileUpload ID="fileUpload" runat="server" />
                                                    <%--<asp:LinkButton ID="btnUploadDoc" runat="server" class="btn btn-outline-success" OnClick="fileUpload_Load" Height="35px">Upload File</asp:LinkButton>--%>
                                                    <%--<asp:TextBox ID="txtRecipients" runat="server" class="form-control form-control-alternative" TextMode="MultiLine" Height="100px" />
                                                <h6 style="text-align: right">
                                                    <asp:Label ID="lblRecieps" runat="server" Font-Bold="true"></asp:Label></h6>--%>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group mb-3">
                                        <div class="row">
                                            <label class="form-control-label" for="input-username">
                                                Message                                                
                                            </label>
                                            <h6>160 Characters = 1 SMS</h6>
                                        </div>
                                        <div class="row">
                                            <asp:TextBox ID="txtMessage" runat="server" class="form-control form-control-alternative" TextMode="MultiLine" Height="200px" onkeyup="countChars('txtMessage','charcount');" onkeydown="countChars('txtMessage','charcount');" onmouseout="countChars('txtMessage','charcount');" />
                                            <h6 style="text-align: right"><span id="charcount">0</span> characters entered.</h6>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <asp:LinkButton ID="lnkBtnSubmit" runat="server" class="btn btn-danger my-4" OnClick="lnkBtnClear_Click">Clear</asp:LinkButton>
                                        <asp:LinkButton ID="lnkBtnClear" runat="server" class="btn btn-primary my-4" OnClick="lnkBtnSubmit_Click">Send</asp:LinkButton>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Argon Scripts -->
        <!-- Core -->
        <script src="assets/vendor/jquery/dist/jquery.min.js"></script>
        <script src="assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
        <!-- Argon JS -->
        <script src="assets/js/argon.js?v=1.0.0"></script>

        <script type="text/javascript">
            function countChars(countfrom, displayto) {
                var len = document.getElementById(countfrom).value.length;
                document.getElementById(displayto).innerHTML = len;
            }
            function Length_TextField_Validator() {
                var len = form_name.text_name.value.length; //the length
                return (true);
            }
        </script>
        <script src="Scripts/jquery-1.4.1.min.js" type="text/javascript"></script>
        <script src="Scripts/jquery.dynDateTime.min.js" type="text/javascript"></script>
        <script src="Scripts/calendar-en.min.js" type="text/javascript"></script>

        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=txtPublishTime.ClientID %>").dynDateTime({
                    showsTime: true,
                    ifFormat: "%Y/%m/%d %H:%M",
                    daFormat: "%l;%M %p, %e %m,  %Y",
                    align: "BR",
                    electric: false,
                    singleClick: false,
                    displayArea: ".siblings('.dtcDisplayArea')",
                    button: ".next()"
                });
            });
        </script>
    </form>
</body>

</html>

