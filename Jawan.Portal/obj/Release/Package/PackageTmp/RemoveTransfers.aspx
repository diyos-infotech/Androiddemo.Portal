<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RemoveTransfers.aspx.cs" Inherits="Jawan.Portal.RemoveTransfers" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REMOVE TRANSFERS</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    
</head>
<body>

    <form id="TemproryEmployeeTransferList1" runat="server">
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" class="current">
                        <span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <%--  <li ><a href="clientattendance.aspx" id="clientattendancelink" runat="server"  class="current"> <span>Entries</span></a></li>--%>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
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
                            <div style="display: inline">
                                <div id="submenu" class="submenu">
                                    <%--<div class="submenubeforegap">
                                        &nbsp;</div>--%>
                                    <ul>
                                        <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="Entrieslonaslink" runat="server"><span>Loans</span></a>
                                        </li>
                                        <li><a href="EmployeePayments.aspx" id="Entriespaymentslink" runat="server"><span>Payments</span></a>
                                        </li>
                                        <li class="current"><a href="PostingOrderList.aspx" id="entriesEmployeeslink" runat="server">
                                            <span>Transfers</span></a> </li>
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
        <div class="content-holder" style="height: auto">
            <h1 class="dashboard_heading">
                Transfers Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Remove Transfer
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">
                    <ul>
                        <li class="left leftmenu">
                            <ul>
                                <li><a href="PostingOrderList.aspx" id="PostingOrderLink" runat="server">Posting Order</a></li>
                                <li><a href="TemproryEmployeeTransferList.aspx" id="TempTransferLink" runat="server" visible="false">
                                    Temporary Transfer</a></li>
                                <li><a href="DummyTransfer.aspx" id="DummyTransferLink" runat="server" visible="false">Dummy Transfer</a></li>
                                <li><a href="RemoveTransfers.aspx" id="transferlink" runat="server" class="sel">Remove
                                    Transfers</a></li>
                            </ul>
                        </li>
                        <li class="right">
                          
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                      
                                        <tr>
                                            <td>
                                                <asp:ScriptManager runat="server" ID="Scriptmanager1">
                                                </asp:ScriptManager>
                                                <!--  Content to be add here> -->
                                               
                                                    <table width="100%" border="0" class="FormContainer" cellpadding="5" cellspacing="5">
                                                        <tr>
                                                            <td>
                                                                Unit ID<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlUnit" class="sdrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="lblClientname" runat="server" Text="Name" Width="40%"> </asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlcname" class="sdrop" runat="server" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Employee ID
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtsearch" runat="server" class="sinput"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                                                                    ID="btnSearch" runat="server" Text="Search" class=" btn save" Style="margin-left: 20px"
                                                                    OnClick="btnSearch_Click" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                              
                                            </td>
                                        </tr>
                                    </table>
                            
                        </li>
                    </ul>
                   
                        <div class="rounded_corners">
                            <asp:GridView ID="gvemployees" runat="server" AutoGenerateColumns="False" Width="98%"
                                CellPadding="4" CellSpacing="3" ForeColor="#333333" GridLines="None" OnRowDeleting="gvemployees_RowDeleting"
                                AllowPaging="True" OnPageIndexChanging="gvemployees_PageIndexChanging" style="text-align:center">
                                <PagerSettings Mode="NumericFirstLast" />
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
                                
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkempid" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="empid" HeaderText="Emp Id" />
                                    <asp:BoundField DataField="Name" HeaderText=" Name" />
                                    <asp:TemplateField HeaderText="Designation">
                                        <ItemTemplate>
                                            <asp:Label ID="ddldesignid" runat="server" Text='<%#Bind("Designid")%>' Visible="false"></asp:Label>
                                            <asp:Label ID="ddldesignname" runat="server" Text='<%#Bind("Desgn")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Working due to ">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltransfertype" runat="server" Text=""></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Delete">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkdelete" runat="server" Text="Delete" CommandName="Delete"
                                                OnClientClick='return confirm("Are you sure you want  to delete this employee?");'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                        <asp:Label ID="MessageLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                        
                        
                   </div>   
                                           </div>
                                </div>
                        </div>
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
            </div>
            <!-- CONTENT AREA END -->
    </form>
</body>
</html>
