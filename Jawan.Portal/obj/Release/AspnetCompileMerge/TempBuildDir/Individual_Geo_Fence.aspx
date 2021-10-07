<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Individual_Geo_Fence.aspx.cs" Inherits="Jawan.Portal.Individual_Geo_Fence" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SETTINGS:Individual Geo Fence</title>
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <link href="../css/Calendar.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
        <style type="text/css">
         .style1 {
            width: 135px;
        }

        .completionList {
            background: white;
            border: 1px solid #DDD;
            border-radius: 3px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            min-width: 165px;
            height: 200px;
            overflow: auto;
        }

        .listItem {
            display: block;
            padding: 5px 5px;
            border-bottom: 1px solid #DDD;
        }

        .itemHighlighted {
            color: black;
            background-color: rgba(0, 0, 0, 0.1);
            text-decoration: none;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            border-bottom: 1px solid #DDD;
            display: block;
            padding: 5px 5px;
        }

        .visibility {
            visibility: hidden;
        }

        .auto-style1 {
            height: 23px;
        }

        .style2 {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }

        .chosen-container {
    width: 230px !important; /* or any value that fits your needs */
}
    </style>

    <script type="text/javascript">

        function dtval(d, e) {
            var pK = e ? e.which : window.event.keyCode;
            if (pK == 8) { d.value = substr(0, d.value.length - 1); return; }
            var dt = d.value;
            var da = dt.split('/');
            for (var a = 0; a < da.length; a++) { if (da[a] != +da[a]) da[a] = da[a].substr(0, da[a].length - 1); }
            if (da[0] > 31) { da[1] = da[0].substr(da[0].length - 1, 1); da[0] = '0' + da[0].substr(0, da[0].length - 1); }
            if (da[1] > 12) { da[2] = da[1].substr(da[1].length - 1, 1); da[1] = '0' + da[1].substr(0, da[1].length - 1); }
            if (da[2] > 9999) da[1] = da[2].substr(0, da[2].length - 1);
            dt = da.join('/');
            if (dt.length == 2 || dt.length == 5) dt += '/';
            d.value = dt;
        }


        function GetEmpid() {
            $('#txtemplyid').autocomplete({
                source: function (request, response) {


                    $.ajax({
                        url: 'Autocompletion.asmx/GetFormEmpIDs',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term,
                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                },
                minLength: 4,
                select: function (event, ui) {

                    $("#txtemplyid").attr("data-Empid", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                }
            });
        }

        function GetEmpName() {

            $('#txtFname').autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: 'Autocompletion.asmx/GetFormEmpNames',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term,
                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#txtFname").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpNamechange(event, ui);
                }
            });

        }

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $('#txtemplyid').trigger('change');

        }
        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $('#txtFname').trigger('change');

        }

        $(document).ready(function () {

            GetEmpid();
            GetEmpName();

        });


    </script>
</head>
<body>
    <form id="CreateLogin1" runat="server">
        <!-- HEADER SECTION BEGIN -->
        <div id="headerouter">
            <!-- LOGO AND MAIN MENU SECTION BEGIN -->
            <div id="header">
                <!-- LOGO BEGIN -->
                <div id="logo">
                    <a href="default.aspx">
                        <img border="0" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE" /></a>
                </div>
                <!-- LOGO END -->
                <!-- TOP INFO BEGIN -->
                <div id="toplinks">
                    <ul>
                        <li><a href="Reminders.aspx">Reminders</a></li>
                        <li>Welcome <b>
                            <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                        <li class="lang"><a href="Login.aspx">Logout</a></li>
                    </ul>
                </div>
                <!-- TOP INFO END -->
                <!-- MAIN MENU BEGIN -->
                <div id="mainmenu">
                    <ul>
                        <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a></li>
                        <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server" class="current"><span>Settings</span></a></li>
                        <li class="after last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                    </ul>
                </div>
                <!-- MAIN MENU SECTION END -->
            </div>
            <!-- LOGO AND MAIN MENU SECTION END -->
            <!-- SUB NAVIGATION SECTION BEGIN -->
            <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
            <div id="submenu">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div style="display: inline;">
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li class="current"><a href="Settings.aspx" id="creak" runat="server"><span>Main</span></a>
                                            </li>
                                        </ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <!-- SUBNAVIGATION SECTION END -->
        </div>
        <!-- HEADER SECTION END -->
        <!-- CONTENT AREA BEGIN -->
        <div id="content-holder">
            <div class="content-holder">
                <div id="breadcrumb">
                    <ul class="crumbs">
                        <li class="first"><a href="Settings.aspx" style="z-index: 9;"><span></span>Settings</a></li>
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Individual Geo Fence</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">

                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Individual Geo Fence
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table runat="server" id="tblhubs"  style="width: 100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>HUB NAME
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txthUBname" runat="server" AutoComplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>Lat
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLat" runat="server" AutoComplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Lng
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLng" runat="server" AutoComplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>Distance
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDistance" runat="server" AutoComplete="off" CssClass="form-control" Width="200px"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td>Client ID
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlsiteposted" runat="server" CssClass="form-control" Width="230px" AutoPostBack="true"></asp:DropDownList>
                                                </td>
                                                <td>Address</td>
                                                <td>
                                                    <asp:TextBox ID="txtAddress" runat="server" CssClass="form-control" AutoComplete="off" TextMode="MultiLine" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table id="tblempdetails" runat="server" visible="false" style="width: 100%">
                                            <tr style="height: 35px">
                                                <td style="width: 100px">Emp ID<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtemplyid" class="form-control" AutoPostBack="true" OnTextChanged="txtemplyid_TextChanged" Width="200px"></asp:TextBox>
                                                </td>
                                                <td style="width: 150px">Employee Name
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFname" class="form-control" AutoPostBack="true" OnTextChanged="txtFname_TextChanged" Width="200px"></asp:TextBox>

                                                </td>
                                                <td>Hub id
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txthubid" AutoComplete="off" runat="server" class="form-control" Width="200px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnempsave" runat="server" Text="Save" OnClick="btnempsave_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <!-- DASHBOARD CONTENT END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS </a>
                    </div>
                    <!--    <div class="footerlogo">&nbsp;</div> -->
                    <div class="footercontent">
                        <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <!-- FOOTER END -->
            <!-- CONTENT AREA END -->
        </div>
    </form>
</body>

</html>
