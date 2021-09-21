<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProfitInvoiceReports.aspx.cs" Inherits="Jawan.Portal.ProfitInvoiceReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: NET PROFIT REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="ClientpfReports1" runat="server">
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
                    <li><a href="Reminders.aspx">Reminders</a></li><li>Welcome <b>
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
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server" class="current">
                        <span>Reports</span></a></li>
                    <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>
                        Settings</span></a></li>
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
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>
                                            Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server">
                                            <span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>
                                            Inventory</span></a></li>
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
            <h1>
                Settings Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <ul>
                    <li class="left leftmenu">
                        <ul>
                            <li><a href="ActiveClientReports.aspx" id="ActiveEmployeeReportLink">List Of Clients</a></li>
                            <li><a href="SubClientsWithMainClientReports.aspx" id="SubClientsWithMainClientReports">Sub Clients</a></li>
                            <li><a href="ClientpfReports.aspx" id="ClientpfReportsLink" >PF</a></li>
                            <li><a href="ClientEsiReports.aspx" id="ClientEsiReportsLink">ESI</a></li>
                            <li><a href="UnitWiseGrossReports.aspx" id="UnitWiseGrossReports">Gross And PF</a></li>
                            <li><a href="UnitwiseEsiReports.aspx" id="UnitwiseEsiReports">Gross And ESI</a></li>
                            <li><a href="InvoieReports.aspx" id="LoanReportsLink">Invoice</a></li>
                            <li><a href="BillingReports.aspx" id="BillingReportsLink">Bills</a></li>
                            <li><a href="ServiceReports.aspx" id="ServiceReportsLink">Service Tax</a></li>
                            <li><a href="ClientAttenDanceReports.aspx" id="ClientAttenDanceReportsLink">Attendance</a></li>
                            <li><a href="ClentwiseEmployeesSalaryreports.aspx" id="ClentwiseEmployeesPfreports"> Salary Details</a> </li>
                            <li><a href="ClentwiseEmployeesSalaryreports2.aspx" id="ClentwiseEmployeesSalaryreports2Link">Salary Details(2)</a> </li>
                            <li><a href="ClientWiseEmployeeNetpayReports.aspx" id="ClientWiseEmployeeNetpayReportsLink"> Net Pay</a> </li>
                            <li><a href="ProfitInvoiceReports.aspx" id="ProfitInvoiceReportsLink" class="sel">Profit Margin</a></li>
                            <li><a href="DeactivateEmployeeWhenattendancezero.aspx" id="DeactivateEmployeeWhenattendancezeroLink">Deactivate Employees</a> </li>
                            <li><a href="ContractDetailsReports.aspx" id="ContractDetailsReportsLink">Contract Details</a></li>
                            <li><a href="ContractExpireDetailsReport.aspx" id="ContractExpireReportLink">Contract Expiry Details</a></li>
                            <li><a href="LicenceExpireDetailsReports.aspx" id="LicenceExpireDetailsReportsLink">License Expiry Details</a></li>
                            <li><a href="LicenceDetailsReports.aspx" id="LicenceDetailsReports">Licenses</a></li>
                            <li><a href="MaterialSentReports.aspx" id="MaterialSentReportLink">Material Sent Details</a></li>
                            <li><a href="ReceiveReports.aspx" id="ReceiveReportsLink">Receipts</a></li>
                        </ul>
                    </li>
                    <li class="right" style="min-height: 200px; height: auto">
                        <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                        </asp:ScriptManager>
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;
                            font-weight: normal">
                            <div align="right">
                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" Visible="False">Export to Excel</asp:LinkButton>
                            </div>
                            <div>
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                    <tr>
                                        <td width="100%" class="FormSectionHead">
                                            Select Options
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <table width="100%">
                                    <tr style="width: 30%">
                                        <td width="25%" style="height: 30%">
                                            Client ID
                                            <asp:DropDownList runat="server" Width="120px" ID="ddlClientId" AutoPostBack="true"
                                                OnSelectedIndexChanged="ddlClientId_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 25%">
                                            Client Name
                                            <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="true" Width="125px"
                                                OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 40%">
                                            Month
                                            <asp:TextBox ID="txtmonth" runat="server" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                TargetControlID="txtmonth" Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtmonth"
                                                ValidChars="/0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                            <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btnsearch_Click" />
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td colspan="3" style="width: 30%">
                                            <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                    Height="50px" CssClass="datagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Client ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblclientid" Text="<%# Bind('clientid') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblclientname" Text="<%# Bind('clientname') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbltotal" Text="<%# Bind('Total') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Service Tax">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblservicetax" Text="<%# Bind('servicetax') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText="Grand Total">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblgrandtotal" Text="<%# Bind('Grandtotal') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Gross">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblgross" Text="<%# Bind('Gross') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="PF">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblpf" Text="<%# Bind('PF') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="ESI">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblesi" Text="<%# Bind('PF') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                       <asp:TemplateField HeaderText="PT">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblpt" Text="<%# Bind('PT') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="PRofit()">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblprofit"  Text="<%# Bind('Profit') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                            <div>
                                <table width="100%">
                                    <tr style="width: 100%; font-weight: bold">
                                        <td style="width: 60%">
                                            <asp:Label ID="lbltamttext" runat="server" Visible="false" Text="Total Amount"></asp:Label>
                                        </td>
                                        <td style="width: 40%">
                                            <asp:Label ID="lbltmtemppf" runat="server" Text=""></asp:Label>
                                            <asp:Label ID="lbltemprpf" runat="server" Text="" Style="margin-left: 30%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </li>
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
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
                </div>
                <!--    <div class="footerlogo">&nbsp;</div> -->
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
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
