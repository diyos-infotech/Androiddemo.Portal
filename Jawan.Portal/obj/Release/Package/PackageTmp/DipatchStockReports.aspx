<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DipatchStockReports.aspx.cs" Inherits="Jawan.Portal.DipatchStockReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DISPATCH STOCK REPORT</title>
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
    <form id="DipatchStockReports1" runat="server">
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
                                        <li><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                        <li class="current"><a href="ListOfItemsReports.aspx" id="InventoryReportLink" class="sel"
                                            runat="server"><span>Inventory</span></a></li>
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
            <h1 class="dashboard_heading">
                Inventory Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Dispatched Stock
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">
                                <ul>
                                    <li class="left leftmenu">
                                        <ul>
                                            <li><a href="ListOfItemsReports.aspx" id="ListOfItemsLink">List Of Items</a></li>
                                            <li><a href="StockInHandReports.aspx" id="StockInHandReports">Stock In Hand</a></li>
                                            <li><a href="DipatchStockReports.aspx" id="DispatchStockReportsLink" class="sel">Dispatched
                                                Stock</a></li>
                                            <li><a href="ReportonInventoryDaily.aspx" id="ReportonInventoryDailylink">Stock Options</a></li>
                                            <li><a href="MrfReport.aspx" id="MrfReport">MRF Status</a></li>
                                        </ul>
                                    </li>
                                    <li class="right">
                                        <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                        </asp:ScriptManager>
                                        <div align="right">
                                            <asp:LinkButton ID="btnPDF" Text="PDF" runat="server" OnClick="btnPDF_Click" />
                                        </div>
                                        <table>
                                            <tr>
                                                <td class="style2">
                                                    Client Id<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DdlClientId" runat="server" AutoPostBack="true" Width="90px"
                                                        OnSelectedIndexChanged="DdlClientId_SelectedIndexChanged" class="sdrop">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style1">
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblClientname" runat="server" Text="Name" Width="40%" > </asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="true" class="sdrop" OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged"
                                                        Width="145px">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    Month
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtdate" runat="server" class="sinput"></asp:TextBox>
                                                    <cc1:calendarextender id="CalendarExtender1" runat="server" enabled="true" targetcontrolid="txtdate"
                                                        format="dd/MM/yyyy">
                                        </cc1:calendarextender>
                                                    <cc1:filteredtextboxextender id="FilteredTextBoxExtender5" runat="server" enabled="True"
                                                        targetcontrolid="txtdate" validchars="/0123456789">
                                        </cc1:filteredtextboxextender>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="style2">
                                                    MRF IDs:
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlmrfid" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlmrfid_OnSelectedIndexChanged" class="sdrop">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td class="style1">
                                                    <asp:DropDownList ID="DdlMonth" runat="server" AutoPostBack="True" Visible="false">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>Jan</asp:ListItem>
                                                        <asp:ListItem>Feb</asp:ListItem>
                                                        <asp:ListItem>March</asp:ListItem>
                                                        <asp:ListItem>April</asp:ListItem>
                                                        <asp:ListItem>May</asp:ListItem>
                                                        <asp:ListItem>June</asp:ListItem>
                                                        <asp:ListItem>July</asp:ListItem>
                                                        <asp:ListItem>August</asp:ListItem>
                                                        <asp:ListItem>September</asp:ListItem>
                                                        <asp:ListItem>October</asp:ListItem>
                                                        <asp:ListItem>November</asp:ListItem>
                                                        <asp:ListItem>December</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btn_Submit" Text="Submit" OnClick="btn_Submit_Click"
                                                        class="btn save" />
                                                </td>
                                                <td>
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                        <br />
                                        <div class="rounded_corners">
                                    <asp:GridView ID="GVListOfItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CssClass="datagrid" CellPadding="4" CellSpacing="3" ForeColor="#333333" GridLines="None">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <Columns>
                                            <asp:BoundField DataField="ItemId" HeaderText="Item Id" />
                                            <asp:BoundField DataField="itemname" HeaderText="Item Name" />
                                            <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                            <asp:BoundField DataField="ApprovedQty" HeaderText="Quantity" />
                                            <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />
                                            <asp:BoundField DataField="SellingPricetotal" HeaderText="Total" />
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <asp:Label ID="LblResult" runat="server" Text="" Style="color: red"></asp:Label>
                                </div>
                          
                                <asp:GridView ID="GVListOfItemsAll" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datagrid" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno1" runat="server" Text="<%#Container.DataItemIndex+1%>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="mrfid" HeaderText="MRF ID" />
                                        <asp:BoundField DataField="ItemId" HeaderText="Item Id" />
                                        <asp:BoundField DataField="itemname" HeaderText="Item Name" />
                                        <asp:BoundField DataField="ClientId" HeaderText="Client id" />
                                        <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                                        <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:MM/dd/yyyy}" />
                                        <asp:BoundField DataField="ApprovedQty" HeaderText="Qty" />
                                        <asp:BoundField DataField="SellingPrice" HeaderText="Price" />
                                        <asp:BoundField DataField="SellingPricetotal" HeaderText="Total" />
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView> 
                                        
                                    </li>
                                </ul>
                               
                            </div>
                        </div>
                    </div>
                    <%--   </div>--%>
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
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
                </div>
                <!--    <div class="footerlogo">&nbsp;</div> -->
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | &copy;
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </form>
</body>
</html>
