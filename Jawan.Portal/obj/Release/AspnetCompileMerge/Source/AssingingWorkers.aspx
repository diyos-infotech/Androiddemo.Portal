<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssingingWorkers.aspx.cs" Inherits="Jawan.Portal.AssingingWorkers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ASSIGNING EMPLOYEES</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="AssingingWorkers1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a></div>
            <!-- LOGO END -->
            <!-- TOP INFO BEGIN -->
            <div id="toplinks">
                <ul>
                    <li><a href="Reminders.aspx">Reminders</a></li><li>Welcome <b>
                        <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b>
                    </li>
                    <li class="lang"><a href="Login.aspx">Logout</a> </li>
                </ul>
            </div>
            <!-- TOP INFO END -->
            <!-- MAIN MENU BEGIN -->
            <div id="mainmenu">
                <ul>
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" class="current">
                        <span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a>
                    </li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a>
                    </li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a>
                    </li>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a>
                    </li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a>
                    </li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a>
                    </li>
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
                                        <li class="current"><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server">
                                            <span>Assign Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                        <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                        <%-- <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
                                        <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                        <%--<li><a href="EmployeeSalaries.aspx" id="SalaryLink" runat="server"><span>Salaries</span></a></li>--%>
                                        <%--    <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>  --%>
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
                                Assigning Employees</h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height: 300px; height: auto">
                            <!--  Content to be add here> -->
                            <div class="boxin">
                                <table width="80%" cellspacing="2" cellpadding="1" border="0" style="height: 50px">
                                    <tr>
                                        <td>
                                            Operational Manager<span style="color: Red">*</span>
                                        </td>
                                        &nbsp;<td>
                                            <asp:DropDownList ID="ddloperationalmanager" runat="server" class="sdrop" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddloperationalmanager_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddloperationalmanagername" runat="server" class="sdrop" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddloperationalmanagername_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                                <div class="dashboard_full">
                                    <div class="dashboard_secondhalf">
                                        <table cellspacing="2" cellpadding="1" border="0" style="height: 50px">
                                            <tr>
                                                <td colspan="2" style="height: 10px">
                                                    <asp:Label ID="lblemplist" runat="server" Style="font-weight: bold; font-size: 15px"
                                                        Text="UnAssigned Employees"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                                <div>
                                    <div class="rounded_corners">
                                        <asp:GridView ID="gvemployeement" runat="server" AutoGenerateColumns="False" Width="98%"
                                            CellPadding="4" CellSpacing="3" ForeColor="#333333" EmptyDataText="No Records Found" GridLines="None"
                                            BorderStyle="Outset">
                                            <RowStyle BackColor="#EFF3FB" Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="20px"
                                                HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="checkdisselect" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Emp Id" ItemStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text=" <%#Bind('EmpId')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Full Name" ItemStyle-Width="60px" DataField="FullName">
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Designation" ItemStyle-Width="60px" DataField="Empdesgn">
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                    <div class="dashboard_SecondOfThree">
                                        <div>
                                            <asp:Button ID="btnleft" runat="server" Text="<<" class="btn save" Width="20px" OnClick="btnleft_click" />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnright" runat="server" Text=">>" class="btn save" Width="20px"
                                                OnClick="btnright_click" />
                                        </div>
                                    </div>
                                   <div class="rounded_corners">
                                        <asp:GridView ID="gvunemployeement" runat="server" Width="98%" CellPadding="4" CellSpacing="3" ForeColor="#333333"
                                            GridLines="None" AutoGenerateColumns="False" EmptyDataText="No Records Found">
                                            <RowStyle BackColor="#EFF3FB" Height="30px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="20px"
                                                HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="10px">
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="checkempid" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Center" />
                                                    <ItemStyle Width="10px" HorizontalAlign="Center"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText=" Emp Id" ItemStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text=" <%#Bind('EmpId')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText="Full Name" ItemStyle-Width="60px" DataField="FullName">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Designation" ItemStyle-Width="60px" DataField="EmpDesgn">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div class="buttons_holder1" style="margin-top: 350px">
                                    <asp:Label ID="lblresult" runat="server" Text="" class="fontstyle" Style="color: Red"> </asp:Label>
                                    <%--<asp:Button ID="btnadd" runat="server" Text="Save" class="btn save" />
                        <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn save" /> --%>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                </div>
                <!-- DASHBOARD CONTENT END -->
            </div>
        </div>
        <!-- CONTENT AREA END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS<!--<img 
            alt="Powered by Businessface" src:"Pages/assets/footerlogo.png"/>--></a></div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>| <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </div>
    </form>
</body>
</html>
