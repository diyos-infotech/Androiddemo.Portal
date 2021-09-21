<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClentwiseEmployeesSalaryreports3.aspx.cs" Inherits="Jawan.Portal.ClentwiseEmployeesSalaryreports3" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>FACILITY MANAGEMENT SOFTWARE</title>
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
    <form id="ClentwiseEmployeesSalaryreports1" runat="server">
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
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                    <li class="after"><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                </ul>
            </div>
            <!-- MAIN MENU SECTION END -->
        </div>
        <!-- LOGO AND MAIN MENU SECTION END -->
        <!-- SUB NAVIGATION SECTION BEGIN -->
        <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
        <div id="submenu" style=" width:100%">
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
                                        <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>  Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"> <span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span> Inventory</span></a></li>
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
                            <li><a href="SubClientsWithMainClientReports.aspx" id="SubClientsWithMainClientReports"> Sub Clients</a></li>
                               
                            <li><a href="ClientpfReports.aspx" id="ClientpfReportsLink">PF</a></li>
                            <li><a href="ClientEsiReports.aspx" id="ClientEsiReportsLink">ESI</a></li> <li>
                           
                            <a href="UnitWiseGrossReports.aspx" id="UnitWiseGrossReports">Gross And PF</a>  </li>
                          
                            <li><a href="UnitwiseEsiReports.aspx" id="UnitwiseEsiReports">Gross And ESI</a></li>
                                <li><a href="InvoieReports.aspx" id="LoanReportsLink">Invoice</a></li>
                                <li><a href="BillingReports.aspx" id="BillingReportsLink">Bills</a></li>
                                <li><a href="ServiceReports.aspx" id="ServiceReportsLink">Service Tax</a></li>
                                <li><a href="ClientAttenDanceReports.aspx" id="ClientAttenDanceReportsLink">Attendance</a></li>
                                <li><a href="ClentwiseEmployeesSalaryreports.aspx" id="ClentwiseEmployeesSalaryreportsLink">Salary Details</a> </li>
                                <li><a href="ClentwiseEmployeesSalaryreports2.aspx" id="ClentwiseEmployeesSalaryreports2Link" >Salary Details(2)</a> </li>
                                <li><a href="ClentwiseEmployeesSalaryreports3.aspx" id="ClentwiseEmployeesSalaryreports3Link" class="sel">Salary Details(3)</a> </li>
                                <li><a href="WageSheetReports.aspx" id="WageSheetReportsLink" >Wage Sheet</a> </li>
                                 
                                <li><a href="ClientWiseEmployeeNetpayReports.aspx" id="ClientWiseEmployeeNetpayReportsLink">Net Pay</a> </li>
                              <li><a href="DeactivateEmployeeWhenattendancezero.aspx" id="DeactivateEmployeeWhenattendancezeroLink">Deactivate Employees</a> </li>
                                <li><a href="ContractDetailsReports.aspx" id="ContractDetailsReportsLink">Contract Details</a></li>
                                <li><a href="ContractExpireDetailsReport.aspx" id="ContractExpireReportLink">Contract Expiry Details</a></li>
                                   
                                <li><a href="LicenceExpireDetailsReports.aspx" id="LicenceExpireDetailsReportsLink"> License Expiry Details</a></li>
                                   
                                <li><a href="LicenceDetailsReports.aspx" id="LicenceDetailsReports">Licenses</a></li>
                                <li><a href="MaterialSentReports.aspx" id="MaterialSentReportLink">Material Sent Details</a></li>
                                <li><a href="ReceiveReports.aspx" id="ReceiveReportsLink">Receipts</a></li>
                        </ul>
                    </li>
                    <li class="right" style="min-height: 200px; height: auto;">
                        <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                        </asp:ScriptManager>
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;
                            font-weight: normal">
                            
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
                            <div>
                                <table width="100%">
                                    <tr style="width: 100%">
                                        <td style="width: 8%">
                                            Client Id
                                        </td>
                                        <td style="width: 10%">
                                            <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="True"
                                             OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 8%">
                                            Name
                                        </td>
                                        <td style="width: 25%">
                                            <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged" Width="100%">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 8%">
                                            Month
                                        </td>
                                        <td style="width: 15%">
                                            <asp:TextBox ID="txtmonth" runat="server" Text=""></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                TargetControlID="txtmonth" Format="MM/dd/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtmonth"
                                                ValidChars="/0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btnsearch_Click" />
                                        </td>
                                        <td style="width: 15%">
                                            <div align="right">
                                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" Visible="False">Export to Excel</asp:LinkButton>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr style="width: 100%">
                                        <td colspan="3" style="width: 30%">
                                            <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div style="overflow: scroll; height:480px; width:790px">
                                <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datagrid" CellPadding="4" ForeColor="#333333" GridLines="None" 
                                    onpageindexchanging="GVListEmployees_PageIndexChanging" 
                                    onrowdatabound="GVListEmployees_RowDataBound">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField DataField="clientid" HeaderText="Client Id" />
                                        <asp:BoundField DataField="clientname" HeaderText="Name" />
                                        <asp:BoundField DataField="empid" HeaderText="Emp Id" />
                                        <asp:BoundField DataField="empmname" HeaderText="Name" />
                                         <asp:BoundField DataField="desgn" HeaderText="Desgn" />
                                        <asp:BoundField DataField="Noofduties" HeaderText="No OF Duties" />
                                        <asp:BoundField DataField="ot" HeaderText="No of Ots" />
                                    
                                        <asp:BoundField DataField="basic" HeaderText="Basic" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="da" HeaderText="DA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="totalgross" HeaderText="Gross" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="otamt" HeaderText="OT Amt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="pf" HeaderText="PF" DataFormatString="{0:0.00}" />
                                       <asp:BoundField DataField="emprpf" HeaderText="PFEMPR" DataFormatString="{0:0.00}" />
                                       <asp:BoundField DataField="pftotal" HeaderText="PF Total" DataFormatString="{0:0.00}" />
                                       <asp:BoundField DataField="empepfno" HeaderText="PF No." DataFormatString="{0:0.00}" />
                                     
                                        <asp:BoundField DataField="esi" HeaderText="ESI" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="empresi" HeaderText="ESIEmpr" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="esitotal" HeaderText="ESI Total" DataFormatString="{0:0.00}" />
                                       <asp:BoundField DataField="EmpESINo" HeaderText="ESINo." DataFormatString="&nbsp; {0}" />
                                        <asp:BoundField DataField="Proftax" HeaderText="PT Amt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="owf" HeaderText="OWFDED" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="saladvded" HeaderText="SALADVDED" DataFormatString="{0:0.00}" />
                                         <asp:BoundField DataField="UniformDed" HeaderText="UniformDed" DataFormatString="{0:0.00}" />
                                          <asp:BoundField DataField="OtherDed" HeaderText="OtherDed" DataFormatString="{0:0.00}" />
                                            <asp:BoundField DataField="CanteenAdv" HeaderText="CanteenAdv" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="penalty" HeaderText="Penalty" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="actualamount" HeaderText="Net AMount" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="Empbankacno" HeaderText="Bank A/C No." DataFormatString="&nbsp; {0}" />
                                       <asp:BoundField DataField="EmpBankCardRef" HeaderText="Reference No." DataFormatString="&nbsp; {0}" />
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
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
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS </a>
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
