<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientReports.aspx.cs" Inherits="Jawan.Portal.ClientReports" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>REPORTS: CLIENT REPORTS</title>
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
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
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
                                            <li><a href="Reports.aspx" id="EmployeeReportLink" runat="server"><span>Employees</span></a></li>
                                            <li class="current"><a href="ClientReports.aspx" id="ClientsReportLink" runat="server">
                                                <span>Clients</span></a></li>
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
                <h1>Client Reports</h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <ul class="shortcuts-c" style="margin-left: 13px">
                        <li><a href="ActiveClientReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">List Of Clients</span> </a></li>

                        <li><a href="SubClientsWithMainClientReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                            class="shortcuts-label">Sub Clients</span> </a></li>

                        <li><a href="ClientpfReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">PF</span> </a></li>

                        <li><a href="ClientEsiReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                            class="shortcuts-label">ESI</span> </a></li>

                        <li><a href="UnitWiseGrossReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Gross And PF</span> </a></li>

                        <li><a href="GrossandPFtwo.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Comparison PF</span> </a></li>

                        <li><a href="UnitwiseEsiReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Gross And ESI</span> </a></li>

                        <li><a href="InvoieReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Invoice</span> </a></li>

                        <li><a href="BillingReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bills</span> </a></li>

                        <li><a href="BillVsReceipts.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bills Vs Receipts</span> </a></li>

                        <li><a href="ServiceReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Service Tax</span> </a></li>

                        <li><a href="ClientAttenDanceReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Attendance </span></a></li>

                        <li><a href="ClentwiseEmployeesSalaryreports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Salary Details</span> </a></li>

                        <li><a href="ClentwiseEmployeesSalaryreports2.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Salary Details(2)</span> </a></li>

                        <li><a href="WageSheetReportsonlydts.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Wage Sheet Only Duties</span> </a></li>

                        <li><a href="WageSheetReportsonlyots.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Wage Sheet Only OTs</span> </a></li>

                        <li><a href="WageSheetReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Wage Sheet</span> </a></li>

                        <li><a href="ClientWiseEmployeeNetpayReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Net Pay</span> </a></li>

                        <li><a href="DeactivateEmployeeWhenattendancezero.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Deactivate Employees</span> </a></li>

                        <li><a href="ContractDetailsReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Contract Details</span> </a></li>

                        <li><a href="ContractExpireDetailsReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Contract Expiry Details</span> </a></li>

                        <li><a href="LicenceExpireDetailsReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">License Expiry Details </span></a></li>

                        <li><a href="LicenceDetailsReports.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">Licenses</span> </a></li>

                        <li><a href="MaterialSentReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Material Sent Details </span></a></li>

                        <li><a href="ReceiveReports.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Receipts</span> </a></li>

                        <li><a href="PaySheetPrintOuts.aspx"><span class="shortcuts-icon iconsi-event"></span>
                            <span class="shortcuts-label">PaySheets </span></a></li>

                        <li><a href="BillingReportingSheet.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Old Bills</span> </a></li>


                        <li><a href="ReportforProfitLoss.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Profit Margin</span> </a></li>

                        <li><a href="ReportforPBAttendance.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Art Comparison</span> </a></li>

                        <li><a href="ReportforBillingAndPaysheet.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Billing and Paysheet</span> </a></li>

                        <li><a href="PFdetailsReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">PF Details</span> </a></li>

                        <li><a href="ReportforEsiDetails.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">ESI Upload</span> </a></li>

                        <li><a href="ConractExpiryDetailsReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Contract Expiry Details</span> </a></li>

                        <li><a href="SalaryPeriodReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Salary Period Report</span> </a></li>

                        <li><a href="PaySheetReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Paysheet Report</span> </a></li>

                        <li><a href="SalaryReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Salary Report</span> </a></li>

                        <li><a href="BillingWagesReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Billing Wages Report</span> </a></li>

                        <li><a href="PaySheetWagesReport.aspx"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Paysheet Wages Report</span> </a></li>

                        <li><a href="BankUploadFormat.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bank Uploading Format</span> </a></li>

                        <li><a href="ReportForExpensesTransaction.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Expensess</span> </a></li>

                        <li><a href="ReportForBankTransactions.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bank Statments</span> </a></li>


                        <li><a href="ExpensesExpenditureReport.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">ExpenDiture Report</span> </a></li>

                        <li><a href="ReportForMinimumWages.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Minimum Wages Report</span> </a></li>

                        <li><a href="ReportForBulkpaysheetforclients.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bulk Paysheet Report</span> </a></li>

                        <li><a href="ReportforBulkClientbillings.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Bulk Billing Report</span> </a></li>


                        <li><a href="ClientForms.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Client Forms</span> </a></li>

                        <li><a href="GetDaywise_Android_Attendance.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Android Attendance</span> </a></li>

                        <li><a href="PinMyVisits.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Pin My Visits</span> </a></li>

                        <li><a href="AddHolidaysList.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Add Holidays List</span> </a></li>
                        <li><a href="LeaveBalance_HistoryReport.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Leave Balance & History Report</span> </a></li>
                        <li><a href="LeaveHolidayListReport.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Holidays List Report</span> </a></li>
                        <li><a href="LeaveSetUpGrid.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Leave Set Up</span> </a></li>
                        <li><a href="Delete_Android_Attendance.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Delete Android Attendance</span> </a></li>

                        <li><a href="PocketFaMECredentials.aspx" style="height: 130px"><span class="shortcuts-icon iconsi-event"></span><span class="shortcuts-label">Pocket FaME Credentials</span> </a></li>

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
