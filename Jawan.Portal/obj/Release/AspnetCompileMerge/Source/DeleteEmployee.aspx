<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteEmployee.aspx.cs" Inherits="Jawan.Portal.DeleteEmployee" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DEACTIVATE EMPLOYEE</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>

    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            text-align: left;
        }
        .imagesize
        {
            height: 70px;
            width: 125px;
        }
        .textboxsize
        {
            width: 120px;
        }
        .divsize
        {
            top: 500px;
            float: right;
        }
        .style4
        {
            height: 25px;
        }
    </style>
</head>
<body>
    <form id="DeleteEmployee1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE" /></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" class="current">
                        <span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                                    <ul>
                                        <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li class="current"><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server">
                                            <span>Delete</span></a></li>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                        <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                        <%--     <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>  --%>
                                        <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                        <%--        <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>   --%>
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
                Employees Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Delete Employee
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <!--  Content to beDelete Employee
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height:300px; height:auto">
                            <!--  Content to be add here> -->
                            <div class="boxin">
                                <table>
                                    <tr>
                                        <td style="font-weight: bold">
                                            Employee ID/Name :
                                        </td>
                                        <td>
                                            &nbsp;<asp:TextBox ID="txtsearch" runat="server" class="sinput"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnSearch" runat="server" Text="Search" class=" btn save" OnClick="btnSearch_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="rounded_corners">
                                    <asp:GridView ID="gvdeleteemployee" runat="server" Width="100%" AutoGenerateColumns="False"
                                        Style="text-align: center" ForeColor="#333333" GridLines="None" AllowPaging="True"
                                        OnPageIndexChanging="gvdeleteemployee_PageIndexChanging" PageSize="20" CellPadding="5"
                                        CellSpacing="3">
                                        <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        
                                         <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="5%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        
                                            <asp:TemplateField HeaderText=" Emp Id" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclientid" runat="server" Text=" <%#Bind('EmpId')%>"></asp:Label>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Middle Name" ItemStyle-Width="60px" DataField="FullName">
                                                <ItemStyle Width="60px" HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Designation" ItemStyle-Width="60px" DataField="EmpDesgn">
                                                <ItemStyle Width="60px" HorizontalAlign="Left"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Date of Birth" ItemStyle-Width="60px" DataField="EmpDtofBirth"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField HeaderText="Present Address" ItemStyle-Width="60px" DataField="EmpPresentAddress">
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:TemplateField ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="linkdelete" runat="server" Text="View" OnClientClick='return confirm(" Are you sure you want to delete the record?");'>   </asp:LinkButton>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblresult" runat="server" Text="Record Deleted Successfully"></asp:Label>
                                                </FooterTemplate>
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" BorderWidth="1px" CssClass="GridPager" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                                <asp:Label ID="LblResult" runat="server" Text="" Visible="false" Style="color: Red"> </asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
            </div>
            <!-- DASHBOARD CONTENT END -->
        </div>
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS S </a>
                </div>
                <!--    <div class="footerlogo">&nbsp;</div> -->
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    <!-- CONTENT AREA END -->
    </form>
</body>
</html>
