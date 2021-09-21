<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Reports.aspx.cs" Inherits="Jawan.Portal.Reports" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>REPORTS</title>
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
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current">
                            <span>Reports</span></a></li>
                        <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                            <li class="current"><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"
                                                runat="server"><span>Employees</span></a></li>
                                            <li><a href="ClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>Inventory</span></a></li>
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
                <h1>Employee Reports</h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <ul class="shortcuts" style="margin-left: 13px">
                        <li><a href="ActiveEmployeeReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">List Of Employees</span> </a></li>
                        <li><a href="LoanReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                            class="shortcuts-label">Loans</span> </a></li>
                        <li><a href="LoanDeductionReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Loan Recovery</span> </a></li>
                        <li><a href="EmpDueAmount.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                            class="shortcuts-label">Due Amount</span> </a></li>
                        <li><a href="AttendanceReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Attendance</span> </a></li>
                        <li><a href="EmployeeSalaryReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Salary Details</span> </a></li>
                        <li><a href="FullandFinal.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Full and Final</span> </a></li>

                        <li><a href="NoAdAtPhReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">No[Addr/Photos
                            <br />
                                /PF/ESI]</span> </a></li>
                        <li><a href="NoAttendanceReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">No Attendance</span> </a></li>
                        <li><a href="EmpTransferDetailReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Transfers</span> </a></li>
                        <li><a href="ESIDeductionReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Esi Deductions</span> </a></li>
                        <li><a href="PfDeductionreports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Pf Deductions</span> </a></li>
                        <li><a href="ProfTaxDeductionReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Prof.Tax Deductions</span> </a></li>
                        <li><a href="PFAvailableandnonavailableReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">PF/ESI Details</span> </a></li>
                        <li><a href="ListOfUsersReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">User Details</span> </a></li>

                        <li><a href="ImportBankPfEsiNos.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Import Excel [Bank/Pf/Esi]</span> </a></li>
                       
                        <li><a href="EmpBioData.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Bio Data</span> </a></li>
                        <li><a href="EmpIDCard.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">ID Card</span> </a></li>
                        <li><a href="EmployeeForms.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Employee Forms</span> </a></li>

                        <li><a href="UniformItemIssuedDetails.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Uniform Issued Details</span> </a></li>

                        <li><a href="EmpWageSlips.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Employee Wageslips</span> </a></li>

                        <li><a href="EmpWisePaysheetDetails.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Wageslips Preview</span> </a></li>



                        <li><a href="Imports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Imports</span> </a></li>

                        <li><a href="ImportEmpDetails.aspx" runat="server" id="A5"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Import Emp Details</span> </a></li>
                          <li><a href="LoansReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Loans Reports</span> </a></li>


                     <li><a href="LoanDetailsReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Emp Loan Details </span> </a></li>

                          <li><a href="SampleAttendance.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Sample Attendance </span> </a></li>

                          <li><a href="ImportAttendance.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Import Attendance </span> </a></li>

                        <li><a href="ImportRoster.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Import Roster </span> </a></li>

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
