<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteCompanyInfo.aspx.cs" Inherits="Jawan.Portal.DeleteCompanyInfo" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DELETE COMPANY INFO</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
</head>

<body>
<form id="DeleteCompanyInfo1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="Film Development Corporation" title="Film Development Corporation"/></a></div>
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
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server" class="current"><span>
                        Company Info</span></a></li>
                    <li class="after"><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
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
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                                <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                 <ul>
                                        <li><a href="companyinfo.aspx" id="AddCompanyInfoLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyCompanyInfo.aspx" id="ModifyCompanyInfoLink" runat="server"><span>
                                            Modify</span></a></li>
                                        <li class="current"><a href="DeleteCompanyInfo.aspx" id="DeleteCompanyInfoLink" runat="server">
                                            <span>Delete</span></a></li>
                                        <%--    <li><a href="default_packaging.html" id="packaging" runat="server"><span>Packaging</span></a></li>
                <li><a href="default_shipping.html" id="shippinglink" runat="server"><span>Shipping</span></a></li>
                <li><a href="default_administrator.html" id="administratorlink" runat="server"><span>Administrator</span></a></li>
          --%>
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
                CompanyIfo Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
<div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                DELETE COMPANY INFORMATION
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;  min-height:300px; height:auto">
                            <asp:GridView ID="gvdeletecompanyinfo" runat="server" Width="100%" AutoGenerateColumns="False"
                                Style="text-align: center" CellPadding="4" ForeColor="#333333" Height="50%"
                                GridLines="None" onrowdeleting="gvdeletecompanyinfo_RowDeleting">
                                <RowStyle BackColor="#EFF3FB" />
                                <HeaderStyle Height="5px" />
                                <Columns>
                                    <asp:TemplateField HeaderText=" Company Name" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcompanyname" runat="server" Text=" <%#Bind('CompanyName')%>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Short Name" ItemStyle-Width="60px" DataField="ShortName" />
                                    <asp:BoundField HeaderText="Address" ItemStyle-Width="60px" DataField="Address" />
                                    <asp:BoundField HeaderText="PFNo" ItemStyle-Width="60px" DataField="PFNo" />
                                    <asp:BoundField HeaderText="ESINO" ItemStyle-Width="60px" DataField="ESINO" />
                                    <%--     <asp:BoundField  HeaderText="BillDesc" ItemStyle-Width="60px"  DataField="BillDesc"/>--%>
                                    <asp:BoundField HeaderText="BillSeq" ItemStyle-Width="60px" DataField="BillSeq" />
                                    <%--<asp:BoundField  HeaderText="Labourrule" ItemStyle-Width="60px" DataField="labourrule"/>--%>
                                    <asp:TemplateField ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkdelete" runat="server" CommandName="delete" Text="Delete" onclientclick='return confirm(" Are You Want to Delete The Record?");'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            
                            <asp:Label ID="lblresult" runat="server" Visible="false" style="color:Red; margin-left:550px"> </asp:Label>
                            
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
        </div>
       </form>
</body>

</html>
