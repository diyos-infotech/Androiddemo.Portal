<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanRecovery.aspx.cs" Inherits="Jawan.Portal.LoanRecovery" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: LOAN RECOVERY</title>
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
    <form id="LoanRecovery1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE"></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="employeeslink" runat="server" class="current">
                        <span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
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
                                    <%--    <div class="submenuactions">
                                        &nbsp;</div> --%>
                                    <ul>
                                        <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttenDanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li class="current"><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                        <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                        <%--   <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>
                                    <li><a href="EmployeeSalaries.aspx" id="SalaryLink" runat="server"><span>Salaries</span></a></li>--%>
                                        <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                        <li><a href="jobleaving.aspx" id="JobLeavingReasonLink" runat="server"><span>Job Leaving
                                            Reasons</span></a></li>
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
            <h1 class="dashboard_heading">
                Loans Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               Recovery Details
                            </h2>
                        </div>
                    <div class="contentarea" id="Div1">
                        <div class="boxinc">
                            <ul>
                                <li class="left leftmenu">
                                    <ul>
                                        <li><a href="NewLoan.aspx">New Loan</a></li>
                                        <li><a id="A1" href="ModifyLoan.aspx" runat="server">Modify Loan</a> </li>
                                        <li><a href="LoanRecovery.aspx" class="sel">Recovery Details</a></li>
                                        <li><a href="LoanRepayment.aspx">Loan Repayment</a></li>
                                        <li><a href="ResourceAllocationEmp.aspx">Resource Issue</a></li>
                                         <li><a href="ResourceReturnEmp.aspx" runat="server" id="ResourceReturnLink">Resource Return</a></li>  

                                    </ul>
                                </li>
                                <li class="right">
                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table width="90%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td valign="top">
                                                    <table cellpadding="5" cellspacing="5">
                                                        <tr>
                                                            <td>
                                                                Emp ID<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlEmpId" class="sdrop"
                                                                    OnSelectedIndexChanged="ddlEmpId_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none">
                                                            <td>
                                                                Middle Name
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlempmname" class="sdrop" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlempmname_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Starting Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtStartingDate" class="sinput" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td align="right">
                                                    <table cellpadding="5" cellspacing="5">
                                                        <tr>
                                                            <td>
                                                                First Name
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlfname" class="sdrop" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlfname_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Loan ID<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" class="sdrop" ID="ddlLoanTaken" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged"
                                                                    AutoPostBack="True">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Loan Amount
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtLoanAmount" class="sinput" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <%-- <tr>
                                                <td>
                                                    Ending Date
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtEndingDate" Enabled="False"></asp:TextBox>
                                                </td>
                                            </tr>--%>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </li>
                            </ul>
                            <div class="rounded_corners" style="margin-left:200px">
                                <div style="text-align: left; font-size: small; font-weight: normal;">
                                    <asp:GridView ID="gvRecovery" runat="server" Width="72%" AutoGenerateColumns="False"
                                        EnableViewState="False" Style="text-align: center" CellPadding="4" CellSpacing="3"
                                        ForeColor="#333333" GridLines="None" EmptyDataText="">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <Columns>
                                        
                                          <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                                
                                        
                                            <asp:BoundField HeaderText="Payment Date" ItemStyle-Width="60px" DataField="LoanDt"
                                                DataFormatString="{0:dd/MM/yyyy}" />
                                            <asp:TemplateField HeaderText="Paid Amount" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblPaidAmt" Text="<%#Bind('RecAmt') %>"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                    <br />
                </div>
                <!-- DASHBOARD CONTENT END -->
            </div>
        </div>
        <!-- CONTENT AREA END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
                </div>
                <!--    <div class="footerlogo">&nbsp;</div> -->
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | &copy;
                    <asp:Label ID="lblcname" runat="server" meta:resourcekey="lblcnameResource1"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </div>
    </form>
</body>
</html>
