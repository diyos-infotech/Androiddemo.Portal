<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ServiceReports.aspx.cs" Inherits="Jawan.Portal.ServiceReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: SERVICE REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="ServiceReports1" runat="server">
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
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
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
                                        <li><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeesLink" runat="server"><span>
                                            Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server">
                                            <span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportsLink" runat="server"><span>
                                            Inventory</span></a></li>
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
            <div id="breadcrumb">
                <ul class="crumbs">
                    <li class="first"><a href="#" style="z-index: 9;"><span></span>Reports</a></li>
                    <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Bills</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Bills
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div class="dashboard_firsthalf" style="width: 100%">
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                        <tr style="width: 100%">
                                           <td>
                                             Client ID :</td>
                                            <td>    <asp:DropDownList runat="server" class="sdrop" ID="ddlClientId" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlClientId_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                           <td>
                                                Client Name :
                                            </td>
                                           <td>
                                                <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="true" class="sdrop"
                                                    OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>  <asp:Label ID="lblMonth" runat="server" Text="Select Month"> </asp:Label></td>
                                              <td>  <asp:TextBox ID="txtMonth" runat="server" class="sinput"></asp:TextBox><span style="color: Red">*</span>
                                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                    TargetControlID="txtMonth" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="FTBEMonth" runat="server" Enabled="True" TargetControlID="txtMonth"
                                                    ValidChars="/0123456789">
                                                </cc1:FilteredTextBoxExtender></td>
                                                 <td>
                                                <asp:Button runat="server" ID="btnSubmit" Text="Submit" class="btn save" OnClick="btnsearch_Click" /><br />
                                                <asp:Label ID="LblResult" runat="server" Style="color: Red"> </asp:Label>
                                            </td>
                                        </tr>
                                       
                                    </table>
                                </div>
                                
                                <div class="rounded_corners">
                                    <asp:GridView ID="gvlistofclients" runat="server" AutoGenerateColumns="False" Width="100%"
                                        CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="None" ShowFooter="true"
                                        OnRowDataBound="gvlistofclients_RowDataBound">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField HeaderText="Clientid" DataField="clientid" />
                                            <asp:BoundField HeaderText="Client Name" DataField="clientname" />
                                            <asp:BoundField HeaderText="Bill No" DataField="Billno" />
                                            <asp:TemplateField HeaderText="Period From-To" ItemStyle-Width="60px">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblfromtext" runat="server" Text="From :"></asp:Label>
                                                    <asp:Label ID="lblfrom" runat="server" Text='<%#Eval("FromDt", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                    <asp:Label ID="lbltotext" runat="server" Text="To:"></asp:Label>
                                                    <asp:Label ID="lblto" runat="server" Text='<%#Eval("ToDt", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle HorizontalAlign="Left" />
                                                <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField  HeaderText="Grand Total" DataField="grandtotal" DataFormatString="{0:0.00}" />
                                     <asp:BoundField  HeaderText="Service Tax" DataField="servicetax" DataFormatString="{0:0.00}"/>
                                     <asp:BoundField  HeaderText="CESS" DataField="cess" DataFormatString="{0:0.00}"/>
                                     <asp:BoundField  HeaderText="SHECess" DataField="shecess" DataFormatString="{0:0.00}"/>
                                     <asp:BoundField  HeaderText="ServiceTax Total" DataField="ServiceTaxTotal" DataFormatString="{0:0.00}"/>--%>
                                            <asp:TemplateField HeaderText="Service Tax Total">
                                                <ItemTemplate>
                                                    <asp:Label Text='<% #Eval("ServiceTaxTotal","{0:F2}") %>' ID="lblServiceTaxtotal" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalServiceTaxtotal" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Grand Total">
                                                <ItemTemplate>
                                                    <asp:Label Text='<% #Eval("grandtotal","{0:F2}") %>' ID="lblgrandtotal" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Service Tax">
                                                <ItemTemplate>
                                                    <asp:Label Text='<% #Eval("servicetax","{0:F2}") %>' ID="lblservicetax" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalservicetax" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="CESS">
                                                <ItemTemplate>
                                                    <asp:Label Text='<% #Eval("cess","{0:F2}") %>' ID="lblCESS" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalCESS" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SHECess">
                                                <ItemTemplate>
                                                    <asp:Label Text='<% #Eval("shecess","{0:F2}") %>' ID="lblshecess" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalshecess" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks" ItemStyle-Width="170px">
                                                <ItemTemplate>
                                                    <asp:Label Text='<%# (Eval("ServiceTax75")!=DBNull.Value ? ((Convert.ToInt32(Eval("ServiceTax75"))!=0)? Eval("Remarks"):""):"NULL")%>' ID="lblremarks" runat="server"> </asp:Label>
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
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS </a>
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
        <!-- CONTENT AREA END -->
    </div>
    </form>
</body>
</html>
