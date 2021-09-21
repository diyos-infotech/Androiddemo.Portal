<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanOperations.aspx.cs" Inherits="Jawan.Portal.LoanOperations" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: LOANS REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="LoanReportsOperations" runat="server">
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a>      </li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server">                  <span>Clients</span></a>        </li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server">          <span>Company Info</span></a>   </li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server">               <span>Inventory</span></a>     </li>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                    <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span> Settings</span></a>      </li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span>   <span> Logout</span></span></a> </li>
                       
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
                                        <li class="current"><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeesLink" runat="server"><span>Employees</span></a></li>
                                        <li><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server"><span>Clients</span></a>                     </li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportsLink" runat="server"><span> Inventory</span></a>                 </li>
                                        <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"> <span>Companyinfo</span></a>                    </li>      
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
                              <li><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeeReportLink">List Of Employees</a>     </li>
                              <li><a href="LoanReports.aspx" id="LoanReportsLink">Loans</a>                                    </li>
                              <li><a href="LoanOperations.aspx" id="LoanOperationsLink" class="sel">LoanOperations</a>         </li>
                              <li><a href="LoanDeductionReports.aspx" id="LoanDeductionReports">Loan Recovery</a>              </li>
                              <li><a href="EmpDueAmount.aspx" id="EmpDueAmountLink"  >Due Amount</a>                           </li>
                              <li><a href="AttendanceReport.aspx" id="AttReportLink">Attendance</a>                            </li>
                              <li><a href="EmployeeSalaryReports.aspx" id="EmployeeSalaryReportsLink" runat="server" >Salary Details</a></li>
                             
                              <li><a href="NoAdAtPhReports.aspx" id="NoAdAtPhReportsPhReportsLink">No[Address/Photos/PF/ESI]</a></li>
                              <li><a href="NoAttendanceReports.aspx" id="NoAttendanceReportsLink">No Attendance</a></li>
                              <li><a href="EmpTransferDetailReports.aspx" id="EmpTransferDetailReports">Transfers</a></li>
                              <li><a href="ESIDeductionReports.aspx" id="ESIDeductionReportsLink">Esi Deductions</a></li>
                              <li><a href="PfDeductionreports.aspx" id="PfDeductionreportsLink" >Pf Deductions</a></li>
                              <li><a href="ProfTaxDeductionReports.aspx" id="ProfTaxDeductionReportsLink">Prof.Tax Deductions</a></li>
                              <li><a href="PFAvailableandnonavailableReports.aspx" id="PFAvailableandnonavailableReportsLink">PF/ESI Details</a></li>
                         
                             <li><a href="ListOfUsersReport.aspx" id="ListOfUsersReportLink">User Details</a></li>                         
                        
                           </ul>
                    </li>
                    <li class="right" style="min-height: 200px; height: auto">
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;
                            font-weight:normal">
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
                                <div class="dashboard_firsthalf"><br />
                                <asp:Table runat="server">
                                <asp:TableRow><asp:TableCell>&nbsp&nbsp</asp:TableCell>
                                <asp:TableCell>LoanOperations:</asp:TableCell>
                                <asp:TableCell>
                                    <asp:DropDownList runat="server" ID="ddlLoanOperation" OnSelectedIndexChanged="ddlLoanOperation_OnSelectedIndexChanged">
                                    <asp:ListItem Text="--Select Operation--"/>
                                    <asp:ListItem Text="ModofiedLoans"/>
                                    <asp:ListItem Text="DeletedLoans"/>
                                    </asp:DropDownList>
                                </asp:TableCell><asp:TableCell>&nbsp&nbsp&nbsp&nbsp</asp:TableCell>
                                <asp:TableCell>FromDate:</asp:TableCell>
                                <asp:TableCell>
                                <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="CalExtendFromdate" TargetControlID="txtFromDate" Enabled="true" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender runat="server" ID="FillExtendFromDate" TargetControlID="txtFromDate" ValidChars="/1234567890"></cc1:FilteredTextBoxExtender>
                                </asp:TableCell>
                                <asp:TableCell>&nbsp&nbsp&nbsp&nbsp</asp:TableCell>
                                <asp:TableCell>ToDate:</asp:TableCell>
                                <asp:TableCell>
                                <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                <cc1:CalendarExtender runat="server" ID="CalExtendToDate" TargetControlID="txtToDate"  Enabled="true" Format="dd/MM/yyyy"></cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender runat="server" ID="FllExtendToDate" TargetControlID="txtToDate" ValidChars="/1234567890"></cc1:FilteredTextBoxExtender>
                                </asp:TableCell>
                                </asp:TableRow>
                                </asp:Table>
                                
                                <asp:Button Id="btnsave" runat="server" Text="Submit"  Height="25px" OnClick="btnsave_OnClick" AutoPostBack="true" CssClass="btn save" style="margin-left:620px"/>
                               
                                
                                </div>
                                <div>
                                <asp:Table runat="server">
                                <asp:TableRow>
                                <asp:TableCell>
                                <asp:GridView ID="gvLoanOperations" runat="server" AutoGenerateColumns="false" CssClass="datagrid" ForeColor="#333333"
                                GridLines="None" CellPadding="4" Width="100%">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                <asp:TemplateField HeaderText="LoanNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanNo" runat="server" Text="<%#Bind('Loanno')%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanType" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanType" runat="server" Text="<%#Bind('LoanType')%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpployeeId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblEmpId" runat="server" Text="<%#Bind('Empid')%>"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmployeeName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text="<%#Bind('EmpName') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanAmt" runat="server" Text="<%#Bind('LoanAmt')%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanIssueDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanIssueDate" runat="server" Text="<%#Bind('LoanIssuedDate') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanCuttingDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanCutDate" runat="server" Text="<%#Bind('ModifidedLoanCutMon') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No.OfInst" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblNoofinst" runat="server" Text="<%#Bind('NoOfInst') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ModifiedDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblNoofinst" runat="server" Text="<%#Bind('dt') %>"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                 
                                    </Columns>
                                    </asp:GridView>
                                </asp:TableCell>
                                <asp:TableCell>
                                <asp:GridView ID="gvLoanOperations2" runat="server" AutoGenerateColumns="false" CssClass="datagrid" ForeColor="#333333"
                                GridLines="None" CellPadding="4" Width="100%">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                <asp:TemplateField HeaderText="LoanNo" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanNo" runat="server" Text="<%#Bind('Loanno')%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanType" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanType" runat="server" Text="<%#Bind('LoanType')%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmpployeeId" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblEmpId" runat="server" Text="<%#Bind('Empid')%>"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EmployeeName" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblEmpName" runat="server" Text="<%#Bind('EmpName') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanAmount" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanAmt" runat="server" Text="<%#Bind('LoanAmt')%>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanIssueDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanIssueDate" runat="server" Text="<%#Bind('LoanIssuedDate') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LoanCuttingDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblLoanCutDate" runat="server" Text="<%#Bind('ModifidedLoanCutMon') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No.OfInst" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblNoofinst" runat="server" Text="<%#Bind('NoOfInst') %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="DeletedDate" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                <asp:Label ID="lblNoofinst" runat="server" Text="<%#Bind('dtd') %>"></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                    </Columns>
                                    </asp:GridView>
                                </asp:TableCell>
                                </asp:TableRow>
                                </asp:Table>
                                </div>
                         </div>
                      </li>
                     </ul>
            </div>
        </div>
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOSss="footercontent">
                    <a href="#">Terms &amp; Conditions</a>|
                    <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server" meta:resourcekey="lblcnameResource1"></asp:Label>
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
