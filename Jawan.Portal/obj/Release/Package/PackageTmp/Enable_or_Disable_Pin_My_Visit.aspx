<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Enable_or_Disable_Pin_My_Visit.aspx.cs" Inherits="Jawan.Portal.Enable_or_Disable_Pin_My_Visit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SETTINGS:App Feature</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
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
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">App Feature</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">

                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">App Feature
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table style="font-family: Arial; font-weight: normal; font-variant: normal; font-size: 13px"
                                            width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>Auto Approval<span style="color: Red">*</span>&nbsp;
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlAutoApproval" runat="server" class="sdrop">
                                                        <asp:ListItem Value="0">False</asp:ListItem>
                                                        <asp:ListItem Value="1">True</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Attendance Days Permitted
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtAttendancedayspermitted" runat="server" class="sinput"> </asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnsave" runat="server" Text="SAVE" class=" btn save" OnClick="btnsave_Click" />
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
