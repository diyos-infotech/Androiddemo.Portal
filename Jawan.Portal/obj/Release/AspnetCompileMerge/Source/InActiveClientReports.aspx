<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InActiveClientReports.aspx.cs" Inherits="Jawan.Portal.InActiveClientReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: INACTIVE CLIENT REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="InActiveClientReports1" runat="server">
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
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
                        Logout</span></span></a></li>
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
                                        <li><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeeRepotsLink" runat="server"><span>Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server">
                                            <span>Clients</span></a></li>
                                        <li><a href="ActiveInventoryReports.aspx" id="InventoryReportsLink" runat="server"><span>
                                            Inventory</span></a></li>
                                            <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"> <span>Companyinfo</span></a>  </li> 
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
                            <%-- <li><a href="ActiveClientReports.aspx">List Of Employees</a></li>--%>
                            <li><a href="InActiveClientReports.aspx" class="sel" id="InactiveEmployeeReportsLink">List Of Clients</a></li>
                            <%--<li><a href="ResponseBasedReports.aspx">Response Base</a></li>
                          <li><a href="InvoiceSubmitedReports.aspx">Invoice Submitted</a></li>
                          <li><a href="SegmentReports.aspx">Segment</a></li>
                          <li><a href="SubmitsReports.aspx">Submitted</a> </li>
                          <li><a href= "OperationalManagerBasedReports.aspx">Operational Manager</a></li>
                          <li><a href="PendingAmountReports.aspx">Pending Amount From Client </a>   </li>
                          <li><a href= "CollectedAmountReports.aspx">Collected Amount </a>   </li>
                          <li><a href= "UnitWiseReports.aspx" >Unit Wise</a> </li>--%>
                        </ul>
                    </li>
                    <asp:ScriptManager runat="server" ID="ScriptInActiveClients">
                    </asp:ScriptManager>
                    <li class="right" style="min-height: 200px; height: auto">
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;
                            font-weight: normal">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                <tr>
                                    <td width="100%" class="FormSectionHead">
                                        Select Options
                                    </td>
                                </tr>
                            </table>
                            <div>
                                <div class="dashboard_firsthalf">
                                    <table>
                                        <tr>
                                            <td>
                                                Starting Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtStrtDate" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                    TargetControlID="txtStrtDate" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="dashboard_secondhalf">
                                    <table>
                                        <tr>
                                            <td>
                                                Ending Date
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtEndDate"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btn_Submit_Click" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <asp:GridView ID="GVListClients" runat="server" AutoGenerateColumns="False" Width="100%"
                                CssClass="datagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Client ID">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientId" Text="<%# Bind('ClientId') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="lblClientId" Text="<%# Bind('ClientId') %>">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client Name">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientName" Text="<%# Bind('ClientName') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtClientName" Text="<%# Bind('ClientName') %>"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Client H.No">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientHNo" Text="<%# Bind('ClientAddrHno') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtClientHNo" Text="<%# Bind('ClientAddrHno') %>"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="City">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientCity" Text="<%# Bind('ClientAddrCity') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtClientCity" Text="<%# Bind('ClientAddrCity') %>"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="State">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientAddrState" Text="<%# Bind('ClientAddrState') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList runat="server" ID="ddlClientAddrState" Text="<%# Bind('ClientAddrState') %>">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Segment">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientSegment" Text="<%# Bind('ClientSegment') %>"></asp:Label></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtClientSegment" Text="<%# Bind('ClientSegment') %>"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Person Designation">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblDOfBirth" Text="<%# Bind('ClientPersonDesgn') %>"></asp:Label></ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtDOfBirth" Text="<%# Bind('ClientPersonDesgn') %>"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Phone No(s)">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientPhonenumbers" Text="<%# Bind('ClientPhonenumbers') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtClientPhonenumbers" Text="<%# Bind('ClientPhonenumbers') %>"> </asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Description">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblClientDesc" Text="<%#Bind('ClientDesc') %>"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" ID="txtClientDesc" Text="<%#Bind('ClientDesc') %>"></asp:TextBox>
                                        </EditItemTemplate>
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
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS</a></div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>|
                    <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </form>
</body>
</html>

