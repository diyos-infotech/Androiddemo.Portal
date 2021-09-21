<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSalaryReports.aspx.cs" Inherits="Jawan.Portal.EmployeeSalaryReports" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: EMPLOYEE SALARY REPORT</title>
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
    <form id="EmployeeSalaryReports1" runat="server">
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
                                        <li class="current"><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"
                                            runat="server"><span>Employees</span></a></li>
                                        <li><a href="ClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
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
                    <li><a href="Reports.aspx" style="z-index: 8;">Employee Reports</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">SALARY DETAILS</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                SALARY DETAILS
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div class="dashboard_firsthalf" style="width: 100%">
                                    <table width="85%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td>
                                                Employee ID<span style="color: Red">*</span> </td>
                                              <td>  <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlEmployee" class="sdrop"
                                                    OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                Employee Name<span style="color: Red">*</span></td>
                                               <td> <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlempname" class="sdrop"
                                                    OnSelectedIndexChanged="ddlempname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Button runat="server" ID="btnSearchSalaries" Text="Submit" class="btn save"
                                                    OnClick="btnSearchSalaries_Click" />
                                            </td>
                                             <td>
                                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" >Export to Excel</asp:LinkButton>
                                            </td>
                                        </tr>
                                        <tr>
                                           
                                            
                                             <td>
                                                Search Type : </td>
                                              <td>  <asp:DropDownList ID="ddlsearchtype" runat="server" class="sdrop">
                                                    <asp:ListItem>ALL</asp:ListItem>
                                                    <asp:ListItem>Employee Wise</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMonth" runat="server" Text="Select Month :"> </asp:Label><span style="color: Red">*</span>
                                              </td>
                                             <td>   <asp:TextBox ID="txtMonth" runat="server" class="sinput"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                    TargetControlID="txtMonth" Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="FTBEMonth" runat="server" Enabled="True" TargetControlID="txtMonth"
                                                    ValidChars="/0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style="overflow: scroll" class="rounded_corners">
                                    <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="200%"
                                        CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None" OnRowDataBound="GVListEmployees_RowDataBound1">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="empid" HeaderText="Emp Id" />
                                            <asp:BoundField DataField="Name" HeaderText="Name" />
                                            <asp:BoundField DataField="Design" HeaderText="Designation" />
                                            <asp:BoundField DataField="ClientId" HeaderText="Client ID" />
                                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" />
                                            <asp:TemplateField HeaderText="Duties">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblduties" Text="<%# Bind('NoOfDuties') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalDuties" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Gross"  HeaderText="Duties Amount" DataFormatString="{0:0.00}">
                                       <FooterStyle HorizontalAlign="Center"    </asp:BoundField> />--%>
                                            <asp:TemplateField HeaderText="Duties Amount">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('Gross') %>" ID="lblDutyamt" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblDutyTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="OT's">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblots" Text="<%# Bind('OT') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalOTs" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="OTamt" HeaderText="OT's Amount" DataFormatString="{0:0.00}" />
                                          --%>
                                            <asp:TemplateField HeaderText="OT's Amount">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('OTamt') %>" ID="lblotamt" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblOTsTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NH's">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblnhs" Text="<%# Bind('NHS') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalNHs" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="Nhsamt" HeaderText="NH's Amount" DataFormatString="{0:0.00}" />--%>
                                            <asp:TemplateField HeaderText="NH's Amount">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('Nhsamt') %>" ID="lblNhsamt" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNhsamtTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WO's">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblwos" Text="<%# Bind('WO') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalWOs" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="WOAmt" HeaderText="WO's Amount" DataFormatString="{0:0.00}" />
                                         --%>
                                            <asp:TemplateField HeaderText="WO's Amount">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('WOAmt') %>" ID="lblWOAmt" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblWOAmtTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="NPOT's">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblnpots" Text="<%# Bind('Npots') %>"></asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalNPOTs" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="NPOTSAmt" HeaderText="NPOT's Amount" DataFormatString="{0:0.00}" />
                                      --%>
                                            <asp:TemplateField HeaderText="NPOT's Amount">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('NPOTSAmt') %>" ID="lblNPOTSAmt" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblNPOTSAmtTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--     <asp:BoundField DataField="TotalSalary" HeaderText="Total before Dedns" DataFormatString="{0:0.00}" />
                                      --%>
                                            <asp:TemplateField HeaderText="Total before Dedns">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('TotalSalary') %>" ID="lblTotalSalary" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotalSalaryTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:BoundField DataField="pf" HeaderText="PF" DataFormatString="{0:0.00}" />
                                       --%>
                                            <asp:TemplateField HeaderText="PF" ControlStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('pf') %>" ID="lblpf" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblpfTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="esi" HeaderText="ESI" DataFormatString="{0:0.00}" />--%>
                                            <asp:TemplateField HeaderText="ESI" ControlStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('esi') %>" ID="lblesi" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblesiTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%-- <asp:BoundField DataField="ProfTax" HeaderText="PT" DataFormatString="{0:0.00}" />
                                        --%>
                                            <asp:TemplateField HeaderText="PT">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('ProfTax') %>" ID="lblPT" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPTTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField DataField="penalty" HeaderText="Penalty" DataFormatString="{0:0.00}" />
                                        --%>
                                            <asp:TemplateField HeaderText="Penalty">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('penalty') %>" ID="lblPenalty" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblPenaltyTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--       <asp:BoundField DataField="Canteenadv" HeaderText="Canteen Adv" DataFormatString="{0:0.00}" />
                                        --%>
                                            <asp:TemplateField HeaderText="Canteen Adv">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('Canteenadv') %>" ID="lblCanteenadv" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblCanteenadvTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--    <asp:BoundField DataField="saladvded" HeaderText="SALADVDED" DataFormatString="{0:0.00}" />
                                        --%>
                                            <asp:TemplateField HeaderText="Sal.Adv Ded">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('saladvded') %>" ID="lblsaladvded" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblsaladvdedTotalAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField DataField="UniformDed" HeaderText="Uniformded" DataFormatString="{0:0.00}" />
                                        --%>
                                            <asp:TemplateField HeaderText="Uniform Ded">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('UniformDed') %>" ID="lblUniformDed" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblUniformDedAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField DataField="OtherDed" HeaderText="Otherded" DataFormatString="{0:0.00}" />
                                        --%>
                                            <asp:TemplateField HeaderText="Other Ded">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('OtherDed') %>" ID="lblOtherDed" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblOtherDedAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--   <asp:BoundField DataField="Totaldeductions" HeaderText="Total Deductions" DataFormatString="{0:0.00}" />
                                       --%>
                                            <asp:TemplateField HeaderText="Total Deductions">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('Totaldeductions') %>" ID="lblTotaldeductions" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblTotaldeductionsAmt" runat="server" Text=""></asp:Label>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <%--  <asp:BoundField DataField="actualamount" HeaderText="NET PAY" DataFormatString="{0:0.00}" />
                                    --%>
                                            <asp:TemplateField HeaderText="NET PAY">
                                                <ItemTemplate>
                                                    <asp:Label Text="<%# Bind('actualamount') %>" ID="lblactualamount" runat="server"> </asp:Label>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:Label ID="lblactualamountTotalAmt" runat="server" Text=""></asp:Label>
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
