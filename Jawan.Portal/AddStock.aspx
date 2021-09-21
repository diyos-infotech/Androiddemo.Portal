<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddStock.aspx.cs" Inherits="Jawan.Portal.AddStock" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVENTORY: ADD STOCK</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>

<body>
<form id="AddStock1" runat="server">
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" > <span>Employees</span></a></li>
                    <li ><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server" class="current"><span>Inventory</span></a></li>
                    <li class="after"><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span> logout</span></span></a></li>
               
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
                                        <li><a href="ViewItems.aspx" id="ItemsLink" runat="server"><span>Items</span></a>
                                        
                                        </li>
                                        <li class="current"><a href="ViewStock.aspx" id="StockLink" runat="server"><span>Stock</span></a>
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
        <div class="content-holder" style="height: auto">
            <h1 class="dashboard_heading">
                Stocks Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Inflow
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">
                                <ul>
                                    <li class="left leftmenu">
                                        <ul>
                                            <li><a href="ViewStock.aspx">Stock In Hand</a></li>
                                            <li><a href="AddStock.aspx" class="sel">Inflow</a></li>
                                            <li><a href="ModifyStock.aspx">Dispatch</a></li>
                                            <li><a href="DeleteStock.aspx">Delete Stock</a></li>
                                        </ul>
                                    </li>
                                    <li class="right">
                                        <%-- <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                <tr>
                                    <td width="100%" class="FormSectionHead">
                                        Select Options
                                    </td>
                                </tr>
                                <tr>
                                    <td>Select Options
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                                
                                </table>--%>
                                        <asp:ScriptManager runat="server" ID="Scriptmanager1">
                                        </asp:ScriptManager>
                                        <div class="rounded_corners">
                                            <asp:GridView ID="gvaddstock" runat="server" AutoGenerateColumns="False" Style="font-family: Arial;
                                                font-size: 13px; font-weight: normal" Width="100%" OnRowEditing="gvaddstock_RowEditing"
                                                OnRowCommand="gvaddstock_RowCommand" CellPadding="5" CellSpacing="3" ForeColor="Black"
                                                GridLines="None" OnRowDataBound="gvaddstock_RowDataBound" BorderColor="Black">
                                                <RowStyle BackColor="#EFF3FB" Height="30" />
                                                <Columns>
                                                
                                                <asp:TemplateField HeaderText="S.No"  HeaderStyle-HorizontalAlign="Center">
                                                  <ItemTemplate>
                                                       <asp:Label ID="lblsno" runat="server"  text="<%#Container.DataItemIndex+1 %>" ></asp:Label>
                                                 </ItemTemplate>
                                                 <ItemStyle Width="8%" />
                                              </asp:TemplateField>
                                                
                                                    <asp:TemplateField HeaderText="Item Id" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlitemid" runat="server" Width="100px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DDL_GetItemName">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:DropDownList ID="ddlitemname" runat="server" Width="185px" AutoPostBack="true"
                                                                OnSelectedIndexChanged="DDL_GetItemId">
                                                            </asp:DropDownList>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtquantity" runat="server" Width="80"> </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                TargetControlID="txtquantity" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Actual Quantity" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblitemQuantity" runat="server" Width="50" Text="0"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Buying Price" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtbuyingprice" Width="120px" runat="server"> </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                TargetControlID="txtbuyingprice" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Selling Price" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtsellingprice" runat="server" Width="120px"> </asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBESellingprice" runat="server" Enabled="True"
                                                                TargetControlID="txtsellingprice" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
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
                                        <asp:Button ID="BtnAddItem" runat="server" Text="ADD ITEM" class=" btn save" OnClick="BtnAddItem_Click"
                                            Style="float: right; margin-right: 5px" />
                                        <div style="float: right">
                                            <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"></asp:Label>
                                            <asp:Button ID="Button1" runat="server" ValidationGroup="a1" Text="SAVE" ToolTip="SAVE"
                                                class=" btn save" meta:resourcekey="Button1Resource1" OnClick="Button1_Click1"
                                                OnClientClick='return confirm("Are you sure you want to add  the stock?");' />
                                            <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="CANCEL" ToolTip="CANCEL"
                                                class=" btn save" meta:resourcekey="btncancelResource1" OnClientClick='return confirm("Are you sure you want to cancel  this entry?");' />
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
