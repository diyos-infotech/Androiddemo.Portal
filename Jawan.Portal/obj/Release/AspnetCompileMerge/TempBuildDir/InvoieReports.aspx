<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvoieReports.aspx.cs" Inherits="Jawan.Portal.InvoieReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVOICE REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="InvoieReports1" runat="server">
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
                                        <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>
                                            Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server">
                                            <span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>
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
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Invoice</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Invoice
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div class="dashboard_firsthalf" style="width: 100%">
                                    <div align="right">
                                        <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click">Export to Excel</asp:LinkButton>
                                    </div>
                                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                        <tr>
                                            <td width="100%" class="FormSectionHead">
                                                <asp:LinkButton ID="linkview" runat="server" Text="View Bills" Style="margin-left: 15px"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td>
                                                Client ID
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" class="sdrop" ID="ddlClientId" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlClientId_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Client Name
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="true" class="sdrop"
                                                    OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Bill Type
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlbilltype" runat="server" class="sdrop">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>Softwear</asp:ListItem>
                                                    <asp:ListItem>Manual</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Invoice Type:
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlinvoicetype" runat="server" class="sdrop">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>With</asp:ListItem>
                                                    <asp:ListItem>With Out</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LblToDate" runat="server" Text="Month"> </asp:Label><span style="color: Red">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" runat="server" class="sinput"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtEndDate"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="FTBEEnddate" runat="server" Enabled="True" TargetControlID="txtEndDate"
                                                    ValidChars="/0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                &nbsp;&nbsp;
                                                <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btnsearch_Click" />
                                            </td>
                                            <td>
                                                    Segment
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlsegment" runat="server" TabIndex="4"  class="sdrop" >
                                                   
                                                    </asp:DropDownList>
                                                </td>
                                        </tr>
                                        
                                    </table>
                               
                            </div>
                            <div style="width: 950px; height: 550px; margin-top:33px;">
                                <asp:GridView ID="GVInvoiceBills" runat="server" AutoGenerateColumns="False" 
                                    CssClass="datagrid" CellPadding="4" ForeColor="#333333" GridLines="Vertical"
                                    BorderColor="Wheat" OnRowDataBound="GVInvoiceBills_RowDataBound" ShowFooter="true" style="overflow: scroll; width: 950px" >
                                    <RowStyle BackColor="#EFF3FB"  />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Client Id">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientID" Text='<%# Bind("unitid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" ItemStyle-Width="125px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientName" Text='<%# Bind("clientname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Invoice No" ItemStyle-Width="125px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblInvoiceNo" Text='<%# Bind("Billno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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
                                        <asp:BoundField HeaderText="Total" DataField="total" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Others" DataField="Others" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Service Charges" DataField="ServiceChrg" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Service Tax" DataField="ServiceTax" DataFormatString="{0:0.00}" />
                                         <asp:BoundField HeaderText="SB Cess" DataField="SBCessAmt" DataFormatString="{0:0.00}" />
                                         <asp:BoundField HeaderText="KK Cess" DataField="KKCessAmt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Grand Total" DataField="grandtotal" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Remarks" DataField="remarks" />
                                      <%--  <asp:BoundField HeaderText="Service Tax 75%" DataField="servicetaxsf" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Service Tax 25%" DataField="servicetaxtf" DataFormatString="{0:0.00}" />
                                        
                                        <asp:BoundField HeaderText="Service Tax " DataField="servicetax" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="CESS" DataField="CESS" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="S&H Ed. CESS" DataField="shecess" DataFormatString="{0:0.00}" />
                                        <asp:TemplateField HeaderText="OtherComponents" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblElectricalChrgtext" runat="server" Text="ElectricalChrg:" />
                                                <asp:Label ID="lblElectricalChrg" runat="server" Text="<%# Bind('ElectricalChrg') %>" />
                                                <asp:Label ID="lblMachinaryCosttext" runat="server" Text="MachinaryCost:" />
                                                <asp:Label ID="lblMachinaryCost" runat="server" Text="<%# Bind('MachinaryCost') %>" />
                                                <asp:Label ID="lblMaterialCosttext" runat="server" Text="MaterialCost:" />
                                                <asp:Label ID="lblMaterialCost" runat="server" Text="<%# Bind('MaterialCost') %>" />
                                                <asp:Label ID="lblExtraAmtTwotext" runat="server" Text="ExtraAmtTwo:" />
                                                <asp:Label ID="lblExtraAmtTwo" runat="server" Text="<%# Bind('ExtraAmtTwo') %>" />
                                                <asp:Label ID="lblExtraAmtonetext" runat="server" Text="ExtraAmtone:" />
                                                <asp:Label ID="lblExtraAmtone" runat="server" Text="<%# Bind('ExtraAmtone') %>" />
                                                <asp:Label ID="lblServiceChrgtext" runat="server" Text="ServiceChrg:" />
                                                <asp:Label ID="lblServiceChrg" runat="server" Text="<%# Bind('ServiceChrg') %>" />
                                                <asp:Label ID="lblTotaltext" runat="server" Text="Total:" />
                                                <asp:Label ID="lblTotal" runat="server" Text="" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Discount" ItemStyle-Width="100px">
                                            <ItemTemplate>
                                                <div style="margin-bottom: 70px;">
                                                    <asp:Label ID="lblDiscounttext" runat="server" Text="Discount:" />
                                                    <asp:Label ID="lblDiscount" runat="server" Text="<%# Bind('Discount') %>" />
                                                    <asp:Label ID="lblDiscounttwotext" runat="server" Text="Discounttwo:" />
                                                    <asp:Label ID="lblDiscounttwo" runat="server" Text="<%# Bind('Discounttwo') %>" />
                                                    <asp:Label ID="lblTotalDiscounttext" runat="server" Text="TotalDiscount:" />
                                                    <asp:Label ID="lblTotalDiscount" runat="server" Text="" /></div>
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red">  </asp:Label>
                            </div>
                            <div>
                                <table width="100%">
                                    <tr style="width: 100%; font-weight: bold">
                                        <td style="width: 38%">
                                            <asp:Label ID="lbltamttext" runat="server" Visible="false" Text="Total Amount"></asp:Label>
                                        </td>
                                        <td style="width: 62%">
                                            <asp:Label ID="lbltmtinvoice" runat="server" Text="" Style="margin-left: 68%"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div>
                                <cc1:ModalPopupExtender ID="MPEViewbills" runat="server" TargetControlID="linkview"
                                    PopupControlID="pnlbillviewdetails" CancelControlID="btncancelviewbills">
                                </cc1:ModalPopupExtender>
                                <asp:Panel ID="pnlbillviewdetails" runat="server" Height="200px" Width="350px" Style="display: none;
                                    background-color: Silver">
                                    <asp:UpdatePanel ID="UPViewbills" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <table cellpadding="5" cellspacing="5" style="margin: 15px">
                                                <tr>
                                                    <td>
                                                        Enter Bill No
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtviewbillno" runat="server" class="sinput" AutoPostBack="true"
                                                            OnTextChanged="txtviewbillno_OnTextChanged"> 
                                   
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <br />
                                                <tr>
                                                    <td>
                                                        Client Id
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtclientidview" runat="server" class="sinput"> 
                                   
                                                        </asp:TextBox>
                                                    </td>
                                                    <tr>
                                                        <td>
                                                            Client Name
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtclientnameview" runat="server" class="sinput"> 
                                   
                                                            </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:Button ID="btncancelviewbills" runat="server" Text="Cancel/Close" CssClass="btn save"
                                                                Style="width: 110px" />
                                                        </td>
                                                    </tr>
                                            </table>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
       
       
    </div>
    <!-- DASHBOARD CONTENT END -->
    <!-- FOOTER BEGIN -->
    <div id="footerouter">
        <div class="footer">
            <div class="footerlogo">
                <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS | <a href="#">Privacy
                    Policy</a> | ©
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
