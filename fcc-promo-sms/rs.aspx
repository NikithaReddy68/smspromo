<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rs.aspx.cs" Inherits="fcc_promo_sms.rs" %>

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
    <link type="text/css" href="assets/css/argon_d.css?v=1.0.0" rel="stylesheet">
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
            </div>
            <!-- Page content -->
            <div class="container mt--8 pb-5">
                <div class="row justify-content-center">
                    <div class="col-lg-5 col-md-7">
                        <div class="card bg-secondary shadow border-0">
                            <div class="card-header bg-transparent">
                                <h2><b>Summary Reports</b></h2>
                                <div class="ErrDiv b-login-form-error" id="divError" runat="server" visible="false">
                                    <asp:Label ID="errorMessage" runat="server" Visible="False" ForeColor="Red"></asp:Label>
                                </div>
                                <table>
                                    <tr>
                                        <td style="width: 75px">
                                            <label class="form-control-label" for="input-username">ReportType</label></td>
                                        <td style="width: 200px; padding-right: 20px">
                                            <asp:DropDownList ID="ddlRptType" runat="server" class="form-control " ForeColor="Black" Font-Bold="true" Height="40px" >
                                                <asp:ListItem Text="Internal Summary" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="SP Revenue Summary" Value="2"></asp:ListItem>
                                                <asp:ListItem Text="RA Logs" Value="3"></asp:ListItem>
                                                <asp:ListItem Text="Activation Details" Value="4"></asp:ListItem>
                                                <asp:ListItem Text="Activation Summary" Value="5"></asp:ListItem>
                                                <asp:ListItem Text="SFTP Status" Value="6"></asp:ListItem>
                                                <asp:ListItem Text="SID wise Prepaid and Postpaid revenue" Value="7"></asp:ListItem>
                                            </asp:DropDownList></td>
                                        <td style="width: 100px">
                                            <label class="form-control-label" for="input-username">FromDate</label></td>
                                        <td style="width: 180px; padding-right: 20px">
                                            <div class="row">
                                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control " ReadOnly="true" Width="150px" Height="30px" ForeColor="Black" Font-Bold="true" />
                                                <%--<img src="assets/img/calender.png" width="30px" />--%>
                                            </div>
                                        </td>

                                        <td style="width: 80px">
                                            <label class="form-control-label" for="input-username">ToDate</label></td>
                                        <td style="width: 180px">
                                            <div class="row">
                                                <asp:TextBox ID="txtToDate" runat="server" class="form-control " ReadOnly="true" Width="150px" Height="30px" ForeColor="Black" Font-Bold="true" />
                                             
                                            </div>
                                        </td>
                                    
                                        <td>
                                            <asp:LinkButton ID="lnkBtnSubmit" runat="server" Height="40px" class="btn btn-default my-4" OnClick="lnkBtnSubmit_Click">Get Data</asp:LinkButton></td>
                                        <td>
                                            <asp:LinkButton ID="lnkExport" runat="server" Height="40px" class="btn btn-dribbble    my-4" OnClick="lnkExport_Click" Visible="false">Export</asp:LinkButton></td>
                                    </tr>

                                </table>
                            </div>
                            <div class="card-body px-lg-5 py-lg-5">
                                <%--<form role="form">
                                    <div class="form-group mb-3">--%>
                                <div class="row">
                                    <div style="overflow-x: scroll; overflow-y: scroll; height: 400px;">
                                        <asp:GridView ID="gvData" runat="server" AutoGenerateColumns="true" ShowHeaderWhenEmpty="True"
                                            EmptyDataText="No Records Found" GridLines="both" AlternatingRowStyle-BackColor="Window" RowStyle-Font-Size="X-Small" HeaderStyle-Font-Size="Smaller" Font-Italic="true" EmptyDataRowStyle-ForeColor="Red" HeaderStyle-BackColor="#000066" HeaderStyle-Font-Bold="true" HeaderStyle-ForeColor="White" HeaderStyle-Height="20px" />
                                    </div>
                                </div>
                                <%-- </div>
                                </form>--%>
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
        <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6/jquery.min.js" type="text/javascript"></script>
        <script src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/jquery-ui.min.js" type="text/javascript"></script>
        <link href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8/themes/base/jquery-ui.css" rel="Stylesheet" type="text/css" />
        <script type="text/javascript">
            $(function () {
                $("[id*=txtFromDate]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: 'assets/img/calender.png'
                });
            });
        </script>
        <script type="text/javascript">
            $(function () {
                $("[id*=txtToDate]").datepicker({
                    showOn: 'button',
                    buttonImageOnly: true,
                    buttonImage: 'assets/img/calender.png'
                });
            });
        </script>
        <%--    <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=txtFromDate.ClientID %>").dynDateTime({
                    showsTime: false,
                    ifFormat: "%Y/%m/%d",
                    daFormat: "%l;%M %p, %e %m,  %Y",
                    align: "BR",
                    electric: false,
                    singleClick: false,
                    displayArea: ".siblings('.dtcDisplayArea')",
                    button: ".next()"
                });
            });
        </script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#<%=txtToDate.ClientID %>").dynDateTime({
                    showsTime: false,
                    ifFormat: "%Y/%m/%d",
                    daFormat: "%l;%M %p, %e %m,  %Y",
                    align: "BR",
                    electric: false,
                    singleClick: false,
                    displayArea: ".siblings('.dtcDisplayArea')",
                    button: ".next()"
                });
            });
        </script>--%>
    </form>
</body>

</html>
