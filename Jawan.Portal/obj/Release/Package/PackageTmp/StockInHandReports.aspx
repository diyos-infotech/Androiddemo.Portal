<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StockInHandReports.aspx.cs" Inherits="Jawan.Portal.StockInHandReports" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: STOCK IN HAND</title>
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
    <form id="StockInHandReports1" runat="server">
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
                   <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current">
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
                                      <li ><a href="Reports.aspx" id="EmployeeReportLink"  runat="server"><span>Employees</span></a></li>
                                        <li><a href="ClientReports.aspx" id="ClientsReportLink" runat="server"><span> Clients</span></a></li>
                                        <li class="current"><a href="ListOfItemsReports.aspx" id="InventoryReportLink" class="sel" runat="server"><span>
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
            <h1 class="dashboard_heading">
                Inventory Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Stock in Hand
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">
                            
                <ul>
                    <li class="left leftmenu">
                        <ul>
                            <li><a href="ListOfItemsReports.aspx" id="ListOfItemsLink" >List Of Items</a></li>
                            <li><a href="StockInHandReports.aspx" id="StockInHandReports" class=" sel">Stock In  Hand</a></li>
                           
                            <li><a href="DipatchStockReports.aspx" id="DipatchStockReports">Dispatch Items</a></li>
                            <li><a href="ReportonInventoryDaily.aspx" id="ReportonInventoryDailylink" >Stock Options</a></li>
              <li><a href="MrfReport.aspx" id="MrfReport">MRF Status</a></li>
                        </ul>
                    </li>
                   <li class="right">
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                    
                        
                              <div align="right">
                            <asp:LinkButton ID="btnPDF" Text="PDF" runat="server" onclick="btnPDF_Click" />
                        </div>
                        
                         <asp:GridView ID="GVListOfItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="None"
                                     ShowFooter="true" onrowdatabound="GVListOfItems_RowDataBound">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblItemid" Text="<%# Bind('ItemId') %>"></asp:Label>
                                            </ItemTemplate>
                                       
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblItemName" Text="<%# Bind('ItemName') %>"></asp:Label>
                                            </ItemTemplate>
                                         </asp:TemplateField>   
                                        <asp:TemplateField HeaderText="Units">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblUnits" Text="<%# Bind('UnitMeasure') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Manufactured by">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblManifacturedBy" Text="<%# Bind('ManifactureBy') %>"></asp:Label>
                                            </ItemTemplate>                                      
                                             </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('MinimumQty') %>"></asp:Label>
                                            </ItemTemplate>
                                           <FooterTemplate>
                                                <asp:Label runat="server" ID="lblTotalQty" Text=" "></asp:Label>
                                                </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblPrice" Text="<%# Bind('buyingPrice') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:Label runat="server" ID="lblTotalPrice" Text=" "></asp:Label>
                                                </FooterTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTotal" Text="<%# Bind('Total') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                <asp:Label runat="server" ID="lblGTotal" Text=" "></asp:Label>
                                                </FooterTemplate>
                                        </asp:TemplateField>
                                        </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:Label ID="LblResult" runat="server" Text="" style=" color:red"></asp:Label>
                         
                            </li>
                        </ul>
                        
                            <div class="rounded_corners">
                               
                                
                            </div>
                            
                            
                         </div>
                            <div class="clear">
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
