<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="psms.aspx.cs" Inherits="fcc_promo_sms.psms" %>

<!DOCTYPE html>
<html>
<head id="Head1" runat="server">
    <meta charset="utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no"/>
    <meta name="description" content="To send SMS "/>
    <meta name="author" content="Mobility International"/>
    <title>FCC | Promotional SMS</title>
    <!-- Favicon -->
    <link href="mi.ico" rel="icon" type="image/png"/>
    <!-- Fonts -->
    <link href="https://fonts.googleapis.com/css?family=Open+Sans:300,400,600,700" rel="stylesheet"/>
    <!-- Icons -->
    <link href="assets/vendor/nucleo/css/nucleo.css" rel="stylesheet"/>
    <link href="assets/vendor/@fortawesome/fontawesome-free/css/all.min.css" rel="stylesheet"/>
    <!-- Argon CSS -->
    <link type="text/css" href="assets/css/argon.css?v=1.0.0" rel="stylesheet"/>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }

        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript">
        function ShowProgress() {
            setTimeout(function () {
                var modal = $('<div />');
                modal.addClass("modal");
                $('body').append(modal);
                var loading = $(".loading");
                loading.show();
                var top = Math.max($(window).height() / 2 - loading[0].offsetHeight / 2, 0);
                var left = Math.max($(window).width() / 2 - loading[0].offsetWidth / 2, 0);
                loading.css({ top: top, left: left });
            }, 200);
        }
        $('form').live("submit", function () {
            ShowProgress();
        });
    </script>
</head>




<body class="bg-default">
    <form id="form1" runat="server">
        <div class="loading" align="center">
            Uploading. Please wait.<br />
            <br />
            <img src="assets/img/brand/logo.png" alt="" />
        </div>
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
                                <a class="nav-link nav-link-icon" href="delsubscriber.aspx">
                                    <i class="fa fa-paper-plane"></i>
                                    <span class="nav-link-inner--text">Delete Subscribers
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

                <div class="separator separator-bottom separator-skew zindex-100">
                 
                </div>
            </div>
            <!-- Page content -->
            <div class="container mt--8 pb-5">
                <div class="row justify-content-center">
                    <div class="col-lg-5 col-md-7">
                    
                        <div class="card bg-secondary shadow border-0" id="DivExcelUpload" runat="server" visible="true">
                            <div class="card-body px-lg-5 py-lg-5">
                                <form role="form">
                                    <div class="form-group mb-3">
                                        <div class="ErrDiv b-login-form-error" id="divError" runat="server" visible="false">
                                            <asp:Label ID="errorMessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                        </div>
                                        <br />
                                        <div class="row">
                                            <label class="form-control-label" for="input-username">
                                                Choose your file and upload for campainging 
                                                    <h6 style="text-align: right">Upload excel File with required details.</h6>
                                            </label>
                                            <div class="col-lg-6">
                                                <%--<label class="form-control-label" for="input-username">Recipients 
                                                    <h6 style="text-align: right">Upload excel File with required details.</h6>
                                                </label>--%>
                                                <asp:FileUpload ID="fuExcel" runat="server" Width="300px" />

                                            </div>
                                            <div class="col-lg-6">

                                                <asp:LinkButton ID="btnUploadExcel" runat="server" class="btn btn-outline-primary" OnClick="btnUpload_Click">Upload excel File</asp:LinkButton><br />

                                            </div>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>
                       

                    </div>
                </div>
            </div>
        </div>
        <!-- Footer -->

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
    </form>
</body>

</html>
