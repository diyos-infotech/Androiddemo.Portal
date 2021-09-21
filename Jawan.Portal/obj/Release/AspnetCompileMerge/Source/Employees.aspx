<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Employees.aspx.cs" Inherits="Jawan.Portal.Employees" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EMPLOYEES</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />


</head>
<body>
    <form id="Employees1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Product Tracking System" title="FAMS" /></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" class="current"><span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                                    <%--    <div class="submenubeforegap">
                                        &nbsp;</div>--%>
                                    <ul>
                                        <%--<li class="current"><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>
                                            Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>--%>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                        <%--        <li><a href="ResourceMesurments.aspx" id="ResourceMesurmentslink" runat="server"><span>Masters</span></a></li>
                                        <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
                                        <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                        <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                        
                                        <%--<li><a href="EmployeeSalaries.aspx" id="SalaryLink" runat="server"><span>Salaries</span></a></li>
                                       <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>--%>
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
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                <div align="center"> <asp:Label ID="lblMsg" runat="server" style="border-color: #f0c36d;background-color: #f9edbe;width:auto;font-weight:bold;color:#CC3300;"></asp:Label></div> 
                <div align="center"> <asp:Label ID="lblSuc" runat="server" style="border-color: #f0c36d;background-color: #f9edbe;width:auto;font-weight:bold;color:#000;"></asp:Label></div> 
                    <table style="margin-top:8px;margin-bottom:8px" width="100%">
                        <tr>
                            <td style="font-weight: bold;width:120px">
                                Employee ID/Name :
                            </td>
                            <td style="width:190px">
                                &nbsp;<asp:TextBox ID="txtsearch" runat="server" class="sinput" autocomplete="off" MaxLength="50"
                                ToolTip="Enter Searched Employee ID Or Name"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class=" btn save" OnClick="btnSearch_Click" ToolTip="Search" />
                            </td>
                            <td align="right"><a href="AddEmployee.aspx" class=" btn save">Add New Employee</a></td>
                            <td align="right"><a href="AndroidEmployee.aspx" class=" btn save">Android Employee</a></td>
                        </tr>
                    </table>
                    <div class="col-md-12">
                        <div class="panel panel-inverse">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Employee Details</h3>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="gvemployee" runat="server" CellPadding="2" ForeColor="Black"
                                    AutoGenerateColumns="False" Width="100%" BackColor="#f9f9f9" BorderColor="LightGray" PageSize="15"
                                    BorderWidth="1px" AllowPaging="True" onrowdeleting="gvDetails_RowDeleting" OnPageIndexChanging="gvemployee_PageIndexChanging" OnRowDataBound="gvemployee_RowDataBound">
                                    <RowStyle Height="30px" />
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
                                        <asp:TemplateField HeaderText=" Emp Id" ItemStyle-Width="40px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblempid" runat="server" Text=" <%#Bind('EmpId')%>"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Employee Name" ItemStyle-Width="110px" DataField="FullName">
                                            <ItemStyle Width="110px" HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Designation" ItemStyle-Width="60px" DataField="Designation">
                                            <ItemStyle Width="60px" HorizontalAlign="Left"></ItemStyle>
                                        </asp:BoundField>
                                          <asp:BoundField HeaderText="Site Posted To" ItemStyle-Width="90px" DataField="clientname" >
                                            <ItemStyle Width="90px" HorizontalAlign="Left" ></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Date of Joining" ItemStyle-Width="60px" DataField="EmpDtofJoining"
                                            >
                                            <ItemStyle Width="60px"></ItemStyle>
                                        </asp:BoundField>
                                         <asp:TemplateField HeaderText="Status" ItemStyle-Width="30px">
                                            <ItemTemplate>
                                            <asp:Label runat="server" ID="lblempGen" Text="<%#Bind('empstatus')%>" ></asp:Label>
                                        
                                        
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lbtn_Select" ImageUrl="~/css/assets/view.png" runat="server"
                                                    ToolTip="View" OnClick="lbtn_Select_Click"  Visible="false" />
                                                <asp:ImageButton ID="lbtn_Edit" ImageUrl="~/css/assets/edit.png" runat="server" OnClick="lbtn_Edit_Click" ToolTip="Edit" />
                                                 <asp:ImageButton ID="lbtn_clntman" ImageUrl="~/css/assets/clmanicon.png" Height="18px" runat="server" OnClick="lbtn_clntman_Click" ToolTip="" />
                                                <asp:ImageButton ID="linkdelete" CommandName="Delete"  ImageUrl="~/css/assets/delete.png" runat="server"
                                                    OnClientClick='return confirm("Do you want to delete this record?");' ToolTip="Inactive" Visible="false"/>
                                            </ItemTemplate>
                                            <ItemStyle Width="40px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="LightBlue" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <HeaderStyle BackColor="White" Font-Bold="True" Height="30px" />
                                    <AlternatingRowStyle BackColor="White" Height="30px" />
                                </asp:GridView>
                                 <asp:Label ID="lblresult" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- DASHBOARD CONTENT END -->
            <%-- </div> </div>--%>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
        </div>
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by WebWonders</div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
            <!-- CONTENT AREA END -->
        </div>
    </form>
</body>
</html>
