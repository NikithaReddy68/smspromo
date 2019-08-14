<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="fcc_promo_sms.Index" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <meta name="description" content="To send SMS ">
    <meta name="author" content="Mobility International">
    <title>FCC | Login</title>
    <!-- Favicon -->
    <link href="mi.ico" rel="icon" type="image/png">
    <!-- Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet">
    <!-- Icons -->
    <link href="assets/vendor/nucleo/css/nucleo.css" rel="stylesheet">
    <link href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" rel="stylesheet">
    <!-- Argon CSS -->
    <link type="text/css" href="assets/css/argon.css?v=1.0.0" rel="stylesheet">
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

                         <%--   <li class="nav-item">
                                <a class="nav-link nav-link-icon" href="compose.aspx">
                                    <i class="fa fa-paper-plane"></i>
                                    <span class="nav-link-inner--text">Compose SMS</span>
                                </a>
                            </li>--%>
                            <li class="nav-item">
                                <a class="nav-link nav-link-icon" href="../examples/index.html">
                                    <i class="ni ni-key-25"></i>
                                    <span class="nav-link-inner--text">Login</span>
                                </a>
                            </li>

                        </ul>
                    </div>
                </div>
            </nav>
            <!-- Header -->
            <div class="header bg-gradient-primary py-7 py-lg-8">

                <div class="separator separator-bottom separator-skew zindex-100">
                    <%--<svg x="0" y="0" viewBox="0 0 2560 100" preserveAspectRatio="none" version="1.1" xmlns="http://www.w3.org/2000/svg">
                        <polygon class="fill-default" points="2560 0 2560 100 0 100"></polygon>
                    </svg>--%>
                </div>
            </div>
            <!-- Page content -->
            <div class="container mt--8 pb-5">
                <div class="row justify-content-center">
                    <div class="col-lg-5 col-md-7">
                        <div class="card bg-secondary shadow border-0">
                            <div class="card-header bg-transparent pb-5">
                                <div class="ErrDiv b-login-form-error" id="divError" runat="server" visible="false">
                                    <asp:Label ID="lblErrMsg" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                </div>
                            </div>
                            <div class="card-body px-lg-5 py-lg-5">
                                <div class="text-center text-muted mb-4">
                                    <small>Sign in with credentials</small>
                                </div>
                                <form role="form">
                                    <div class="form-group mb-3">
                                        <div class="input-group input-group-alternative">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="fa fa-user"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtUserName" runat="server" name="u" placeholder="Username" class="form-control" />
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <div class="input-group input-group-alternative">
                                            <div class="input-group-prepend">
                                                <span class="input-group-text"><i class="ni ni-lock-circle-open"></i></span>
                                            </div>
                                            <asp:TextBox ID="txtPassword" runat="server" name="p" placeholder="Password" required="required" TextMode="Password" class="form-control" />
                                        </div>
                                    </div>

                                    <div class="text-center">
                                        <asp:LinkButton ID="btnSubmit" runat="server" class="btn btn-primary my-4"
                                            Text="Sign in" OnClick="btnSubmit_Click"></asp:LinkButton>
                                    </div>
                                </form>
                            </div>
                        </div>

                    </div>
                </div>
            </div>
        </div>
        <!-- Footer -->
     <%--   <footer class="py-5">
            <div class="container">
                <div class="row align-items-center justify-content-xl-between">
                    <div class="col-xl-6">
                        <div class="copyright text-center text-xl-left text-muted">
                            &copy; 2018 <a href="http://www.mobility.international" class="font-weight-bold ml-1" target="_blank">Mobility International</a>
                        </div>
                    </div>
                    <div class="col-xl-6">
                        <ul class="nav nav-footer justify-content-center justify-content-xl-end">
                            <li class="nav-item">
                                <a href="http://www.mobility.international" class="nav-link" target="_blank">Mobility International</a>
                            </li>
                            <li class="nav-item">
                                <a href="http://www.mobility.international/about-us.html" class="nav-link" target="_blank">About Us</a>
                            </li>

                        </ul>
                    </div>
                </div>
            </div>
        </footer>--%>
        <!-- Argon Scripts -->
        <!-- Core -->
        <script src="assets/vendor/jquery/dist/jquery.min.js"></script>
        <script src="assets/vendor/bootstrap/dist/js/bootstrap.bundle.min.js"></script>
        <!-- Argon JS -->
        <script src="assets/js/argon.js?v=1.0.0"></script>
    </form>
</body>




</html>
