<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyStock.aspx.cs" Inherits="Jawan.Portal.ModifyStock" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MODIFY STOCK</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="ModifyStock1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>
                        Employees</span></a></li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company 
                        Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server" class="current"><span>
                        Inventory</span></a></li>
                    <li class="after"><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server">
                        <span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
                        logout</span></span></a></li>
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
                                    <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li><a href="ViewItems.aspx" id="ViewItemslink" runat="server"><span>Items</span></a>
                                        </li>
                                        <li class="current"><a href="ViewStock.aspx" id="viewstocklink1" runat="server"><span>
                                            Stock</span></a> </li>
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
                Stocks Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Dispatch
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">
                            
                        <ul>
                            <li class="left leftmenu">
                                <ul>
                                    <li><a href="ViewStock.aspx" runat="server" id="ViewStockLink">Stock In Hand</a></li>
                                    <li><a href="AddStock.aspx" runat="server" id="AddStockLink">Inflow</a></li>
                                    <li><a href="ModifyStock.aspx" runat="server" id="ModifyStockLink" class="sel">
                                        Dispatch</a></li>
                                    <li><a href="DeleteStock.aspx" runat="server" id="DeleteStockLink">Delete Stock</a></li>
                                </ul>
                            </li>
                            <li class="right" style="height: auto">
                               
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                        <tr>
                                            <td width="100%" class="FormSectionHead">
                                                List of Undispatch MRFs
                                            </td>
                                        </tr>
                                    </table>
                                   
                                    <div class="rounded_corners">     
                                        <asp:GridView ID="gvMRF" runat="server" AutoGenerateColumns="False" Width="100%"
                                            EmptyDataRowStyle-BackColor="BlueViolet" EmptyDataRowStyle-BorderColor="Aquamarine"
                                            EmptyDataRowStyle-Font-Italic="true" EmptyDataText="No Records Found" 
                                            ForeColor="#333333" Style="text-align: left" GridLines="None" CellPadding="4" CellSpacing="3">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <EmptyDataRowStyle BackColor="BlueViolet" BorderColor="Aquamarine" Font-Italic="True">
                                            </EmptyDataRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="MRF ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRFId" runat="server" Text='<%#Bind("MRFId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ClientId" HeaderText="Client ID" HeaderStyle-Width="220px">
                                                    <HeaderStyle Width="220px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Date" HeaderText="Date of MRF" HeaderStyle-Width="220px"
                                                    DataFormatString="{0:dd/MM/yyyy}">
                                                    <HeaderStyle Width="220px" />
                                                </asp:BoundField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        </div>
                                   
                                    
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>
                                                    MRF Id<span style="color: Red">*</span></td>
                                                  <td>  <asp:DropDownList ID="ddlMRF" runat="server" class="sdrop" Width="130px" AutoPostBack="True" OnSelectedIndexChanged="ddlMRF_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    Client Id</td>
                                                  <td>  <asp:TextBox ID="txtclientid" class="sinput" runat="server" Width="125px" Enabled="false"></asp:TextBox>
                                                </td>
                                               <td>
                                                    Client Name</td>
                                                   <td> <asp:TextBox ID="txtcname" class="sinput" runat="server" Width="125px" Enabled="false"></asp:TextBox>
                                                </td>  
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Label ID="lblclientname" runat="server" Width="340px" Enabled="false" Visible="false"></asp:Label>
                                                </td>
                                    </tr>
                                    
                                    </table>
                                    
                                   
                                    <div class="rounded_corners">     
                                        <asp:GridView ID="gvMRFDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                            EmptyDataRowStyle-BackColor="BlueViolet" EmptyDataRowStyle-BorderColor="Aquamarine"
                                            EmptyDataRowStyle-Font-Italic="true" EmptyDataText="No Records Found" 
                                            ForeColor="#333333" Style="text-align: left" GridLines="None"  CellPadding="5" CellSpacing="3">
                                            <RowStyle BackColor="#EFF3FB" />
                                            <EmptyDataRowStyle BackColor="BlueViolet" BorderColor="Aquamarine" Font-Italic="True">
                                            </EmptyDataRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="MRF ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMRFId" runat="server" Text='<%#Bind("MRFId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemid" runat="server" Text='<%#Bind("itemid")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Item Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblitemname" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Quantity Required">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Bind("ApprovedQty")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Stock in Hand">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblActQty" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        </div><br /><br /><br />
                                        
                                        
                                         <div style="float: right;margin-right:20px">
                            <asp:DropDownList ID="ddlpdftye" runat="server" class="sdrop" Width="120px">
                            <asp:ListItem>With Rates</asp:ListItem>
                            <asp:ListItem>With out Rates</asp:ListItem>
                            </asp:DropDownList>&nbsp;&nbsp;&nbsp;
                                <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"> </asp:Label>
                                <asp:Button ID="Button1" runat="server" ValidationGroup="a1" Text="Dispatch" ToolTip="Dispatch"
                                    class=" btn save" OnClick="Button1_Click" OnClientClick='return confirm("Are you sure you want to dispatch  the stock?");' />
                                <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="CANCEL" ToolTip="CANCEL"
                                    class=" btn save" OnClick="btncancel_Click" OnClientClick='return confirm("Are you sure you want to cancel  the dispatch stock?");' />
                            </div>
                            
                            </li>
                        </ul>
                         
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
