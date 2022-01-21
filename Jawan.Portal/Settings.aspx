<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Settings.aspx.cs" Inherits="Jawan.Portal.Settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>SETTINGS</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Load.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2 {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }
    </style>

    <script language="javascript">
        function OnFocus(txt, text) {
            if (txt.value == text) {
                txt.value = "";
            }
        }
        function OnBlur(txt, text) {
            if (txt.value == "") {
                txt.value = text;
            }
        }
    </script>

</head>
<body>
    <form id="form1" runat="server">
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
                                <div style="display: inline">
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li class="current"><a href="#" id="EmployeeReportLink" runat="server"><span>Main</span></a></li>
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
                <h1>Settings Dashboard</h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <ul class="shortcuts-s" style="margin-left: 13px">
                        <li><a href="CreateLogin.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Create Login</span> </a></li>
                        <li><a href="CreatePocketFameLogin.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Create Pocket FaME Login</span> </a></li>

                        <li><a href="ChangePassword.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Change Password</span> </a></li>
                        <li><a href="Department.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Department</span> </a></li>
                        <li><a href="Designation.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Designation</span> </a></li>
                        <li><a href="Segment.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Segment</span> </a></li>
                        <li><a href="BankNames.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bank Names</span> </a></li>
                        <li><a href="Categories.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Categories</span> </a></li>
                        <li><a href="Resources.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Resources</span> </a></li>
                        <li><a href="SalaryBreakup.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">SalaryBreakup Details</span> </a></li>
                        <li><a href="BillingAndSalary.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Billing/Salary Details</span> </a></li>
                        <li><a href="ActivateEmployee.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Active/Inactive</span> </a></li>

                        <li><a href="MeasuredUnits.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Measured Units</span> </a></li>

                        <li><a href="BloodGroups.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Blood Groups</span> </a></li>

                        <li><a href="Previligers.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Previligers</span> </a></li>

                        <li><a href="ExpensesPurpose.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Expenses Purpose</span> </a></li>

                        <li><a href="ExpensesApprovedBy.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Expenses Approved By</span> </a></li>

                        <li><a href="Payment_Mode.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">PayMent Mode</span> </a></li>

                        <li><a href="Minimum_Wages.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Minimum Wages</span> </a></li>

                        <li><a href="Minimum_Wages_Categories.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Minimum Wages Categories</span> </a></li>


                        <li><a href="Charts.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">DASHBOARD</span> </a></li>

                        <li><a href="ESIBranches.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">ESI Branches</span> </a></li>

                        <li><a href="Branches.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Branches</span> </a></li>

                        <li><a href="Division.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Division</span> </a></li>

                        <li><a href="M_AddingSourceofLeads.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Source of Lead</span> </a></li>

                        <li><a href="M_AddLeadstatus.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Lead Status</span> </a></li>
                        <li><a href="Enable_or_Disable_Pin_My_Visit.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">App Feature</span> </a></li>
                        <li><a href="Create_UserFeature.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">User Feature</span> </a></li>
                        <li><a href="Create_Tab_App_Passwords.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Create Tab App Passwords</span> </a></li>
                        <li><a href="Individual_Geo_Fence.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Individual Geo Fence</span> </a></li>

                    </ul>
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
