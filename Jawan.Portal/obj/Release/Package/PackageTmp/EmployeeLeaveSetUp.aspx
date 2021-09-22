<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeLeaveSetUp.aspx.cs" Inherits="Jawan.Portal.EmployeeLeaveSetUp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: Leave Set Up</title>
    <link href="css/global.css" rel="stylesheet" />
    <link href="css/chosen.css" rel="stylesheet" />
    <script type="text/javascript" src="script/jquery.min.js"></script>
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

   
    <script src="https://harvesthq.github.io/chosen/chosen.jquery.js" type="text/javascript"></script>


    <script type="text/javascript">




        function AssignExportHTML() {

            document.getElementById('hidGridView').value = htmlEscape(forExport.innerHTML);
        }
        function htmlEscape(str) {
            return String(str)
                .replace(/&/g, '&amp;')
                .replace(/"/g, '&quot;')
                .replace(/</g, '&lt;')
                .replace(/>/g, '&gt;');
        }

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


    </script>


    <style type="text/css">
        #social div {
            display: block;
        }

        .HeaderStyle {
            text-align: Left;
        }

        .style3 {
            height: 24px;
        }

        .modalBackground {
            background-color: Gray;
            z-index: 10000;
        }

        .slidingDiv {
            background-color: #99CCFF;
            padding: 10px;
            margin-top: 10px;
            border-bottom: 5px solid #3399FF;
        }

        .show_hide {
            display: none;
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
    </style>




    <script type="text/javascript">


        jQuery(document).ready(function mchoose() {
            jQuery(".chosen").data("placeholder", "Select Frameworks...").chosen();
        });


    </script>



</head>
<body>
    <form id="Segment1" runat="server">
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
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"  class="current"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class=" after last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                <div style="display: inline">
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeesLink" runat="server"><span>Employees</span></a></li>
                                            <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server"><span>Clients</span></a> </li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportsLink" runat="server"><span>Inventory</span></a> </li>
                                            <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"><span>Companyinfo</span></a>
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
                        <li class="first"><a href="#" style="z-index: 9;"><span></span>Reports</a></li>
                        <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Leave Set Up</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Leave Set Up
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">

                                <div class="boxin">

                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>


                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td valign="top">

                                                    <table width="100%" cellpadding="5" cellspacing="5">

                                                        <tr>
                                                            <td>Type of Leave
                                                            </td>
                                                            <td>
                                                                <%--<asp:TextBox ID="txtTypeOfleave" runat="server" class="form-control" TabIndex="1"></asp:TextBox>--%>
                                                                <asp:DropDownList ID="ddlTypeOfleave" runat="server" CssClass="form-control" AutoPostBack="True" Width="230px" OnSelectedIndexChanged="ddlTypeOfleave_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Frequency
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlFrequency" runat="server" class="form-control" Width="230px" TabIndex="3">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem Value="1">Monthly</asp:ListItem>
                                                                    <asp:ListItem Value="2">Quarterly</asp:ListItem>
                                                                    <asp:ListItem Value="3">Yearly</asp:ListItem>
                                                                    <asp:ListItem Value="4">Once</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Max. Continuous Days
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaxContDays" runat="server" class="form-control" TabIndex="5"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Reset On
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtmonth" runat="server" Text="" class="form-control" TabIndex="7"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                                    TargetControlID="txtmonth" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtmonth"
                                                                    ValidChars="/0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>Holiday Combine
                                                            </td>

                                                            <td>
                                                                <asp:RadioButton ID="rbnHolidayCombineYes" runat="server" Text="YES" GroupName="HolidayCombine" TabIndex="9" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbnHolidayCombineNo" runat="server" Text="NO" GroupName="HolidayCombine" TabIndex="10" />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Holiday Trailing
                                                            </td>

                                                            <td>
                                                                <asp:RadioButton ID="rbnHolidayTrailingYes" runat="server" Text="YES" GroupName="HolidayTrailing" TabIndex="12" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbnHolidayTrailingNo" runat="server" Text="NO" GroupName="HolidayTrailing" TabIndex="13" />
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>WO Combine
                                                            </td>
                                                            <td>
                                                                <asp:RadioButton ID="rbnWOCombineYes" runat="server" Text="YES" GroupName="WOCombine" TabIndex="15" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rbnWOCombineNo" runat="server" Text="NO" GroupName="WOCombine" TabIndex="16" />
                                                            </td>
                                                        </tr>


                                                    </table>

                                                </td>


                                                <td valign="top" align="right">
                                                    <table width="100%" cellpadding="5" cellspacing="5">

                                                        <tr>
                                                            <td>Short Name
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtShortName" runat="server" TabIndex="2" class="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Number of Leaves
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtNumOfLeaves" runat="server" TabIndex="4" class="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Combine with 
                                                            </td>

                                                            <td>
                                                                <asp:ListBox ID="LstCombinewith" runat="server" SelectionMode="Multiple" class="chosen" TabIndex="6"></asp:ListBox>

                                                                <asp:TextBox runat="server" ID="txtLangKnown" class="sinput" MaxLength="80" Visible="false">
                                                                </asp:TextBox>
                                                            </td>

                                                        </tr>

                                                        <tr>
                                                            <td>Max Cashable
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaxCashable" runat="server" TabIndex="8" class="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Max. Value
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaxValue" runat="server" TabIndex="11" class="form-control"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                        <tr>
                                                            <td>Max. Rollover
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtMaxRollover" runat="server" class="form-control" TabIndex="14"></asp:TextBox>
                                                            </td>
                                                        </tr>


                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td style="padding-left: 60px; padding-top: 20px">
                                                                <asp:Button ID="Btn_AddLeaveSetUp" runat="server" Text="Save" ToolTip="Add Leave Set Up" class=" btn save" TabIndex="17"
                                                                    ValidationGroup="a1" OnClick="Btn_AddLeaveSetUp_Click"
                                                                    OnClientClick='return confirm(" Are you sure you want to add Leave Set Up ?");' />

                                                                <asp:Button ID="Btn_Cancel" runat="server" Text="Cancel" ToolTip="Cancel Leave Set Up"
                                                                    OnClientClick='return confirm(" Are you sure you want to Cancel this entry ?");'
                                                                    class=" btn save" OnClick="Btn_Cancel_Click" TabIndex="18" BackColor="Gray" /></td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>

                                                    </table>
                                                </td>
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

            <!-- CONTENT AREA END -->
        </div>

        <!-- DASHBOARD CONTENT END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS              
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



    <script type="text/javascript">
        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function () {


                jQuery(document).ready(function mchoose() {
                    jQuery(".chosen").data("placeholder", "Select").chosen();
                });

            });
        };


    </script>
</body>
</html>
