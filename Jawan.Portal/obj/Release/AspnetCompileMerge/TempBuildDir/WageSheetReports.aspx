<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WageSheetReports.aspx.cs" Inherits="Jawan.Portal.WageSheetReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: WAGE SHEET REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2 {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }

    </style>
    <script type="text/javascript">
        function AssignExportHTML() {

            document.getElementById('hidGridView').value = htmlEscape(forExport.innerHTML);
        }
        function htmlEscape(str) {
            return String(str)
            .replace(/&/g, '&amp;')
            .replace(/"/g, '&quot;')
            .replace(/</g, '&lt;')
            .replace(/>/g, '&gt;');
        }

        function onCalendarShown() {

            var cal = $find("calendar1");
            //Setting the default mode to month
            cal._switchMode("months", true);

            //Iterate every month Item and attach click event to it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }
        }

        function onCalendarHidden() {
            var cal = $find("calendar1");
            //Iterate every month Item and remove click event from it
            if (cal._monthsBody) {
                for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                    var row = cal._monthsBody.rows[i];
                    for (var j = 0; j < row.cells.length; j++) {
                        Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                    }
                }
            }

        }

        function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
    </script>
</head>
<body>
    <form id="ClentwiseEmployeesSalaryreports1" runat="server">
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
                        <li><a href="Reminders.aspx">Reminders</a></li>
                        <li>Welcome <b>
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
                        <li class="after"><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                    </ul>
                </div>
                <!-- MAIN MENU SECTION END -->
            </div>
            <!-- LOGO AND MAIN MENU SECTION END -->
            <!-- SUB NAVIGATION SECTION BEGIN -->
            <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
            <div id="submenu" style="width: 100%">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div style="display: inline">
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>Employees</span></a></li>
                                            <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>Inventory</span></a></li>
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
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Wage Sheet</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Wage Sheet
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>

                                    <div class="dashboard_firsthalf" style="width: 100%">

                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>Client Id
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged" class="sdrop">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Name
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged" class="sdrop">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Month
                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txtmonth" AutoComplete="off" runat="server" Text="" class="sinput"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Txt_Month_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                        Enabled="true" Format="MMM-yyyy" TargetControlID="txtmonth" DefaultView="Months" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btnsearch_Click" />
                                                </td>
                                                <td>
                                                    <div align="right">
                                                        <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" OnClientClick="AssignExportHTML()" Visible="false">Export to Excel</asp:LinkButton>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="3" style="width: 30%">
                                                    <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"> </asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:HiddenField ID="hidGridView" runat="server" />
                                    <div id="forExport" class="rounded_corners" style="overflow: scroll">
                                        <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="Both" ShowFooter="true"
                                            OnPageIndexChanging="GVListEmployees_PageIndexChanging"
                                            OnRowDataBound="GVListEmployees_RowDataBound">

                                            <Columns>

                                                <%-- 0--%>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 1--%>
                                                <asp:TemplateField HeaderText="Client ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="80px">
                                                    <HeaderStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text='<%#Bind("clientid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 2--%>

                                                <asp:TemplateField HeaderText="Client Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                                                    <HeaderStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientname" runat="server" Text='<%#Bind("clientname") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 3--%>
                                                <asp:TemplateField HeaderText="Area" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAreaName" Text='<%# Bind("Location") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalAreaName"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 4--%>
                                                <asp:TemplateField HeaderText="Zone" ItemStyle-Width="120px">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblZoneName" Text='<%# Bind("DivisionName") %>'></asp:Label>
                                                    </ItemTemplate>
                                                     <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalZoneName"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 5--%>
                                                <asp:TemplateField HeaderText="Emp Id" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempid" runat="server" Text='<%#Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 6--%>
                                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempname" runat="server" Text='<%#Bind("EmpMname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>


                                                <%-- 7--%>
                                                <asp:TemplateField HeaderText="Bank A/C No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbankno" runat="server" Text='<%# Eval("EmpBankAcNo") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 8--%>
                                                <asp:TemplateField HeaderText="Desgn" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesgn" runat="server" Text='<%#Bind("Desgn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 9--%>
                                                <asp:TemplateField HeaderText="Month-Year" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmonth" runat="server"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 10--%>
                                                <asp:TemplateField HeaderText="Duties" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldutyhrs" runat="server" Text='<%#Bind("NoOfDuties") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDuties"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 11--%>
                                                <asp:TemplateField HeaderText="OTs" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOts" runat="server" Text='<%#Bind("OTs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOTs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 12--%>
                                                <asp:TemplateField HeaderText="WO" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwos" runat="server" Text='<%#Bind("WO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalwos"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 13--%>
                                                <asp:TemplateField HeaderText="Nhs" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNhs" runat="server" Text='<%#Bind("NHS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNhs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 14--%>
                                                <asp:TemplateField HeaderText="Npots" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNpots" runat="server" Text='<%#Bind("npots") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNpots"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 15--%>
                                                <asp:TemplateField HeaderText="Sal Rate" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltempgross" runat="server" Text='<%#Bind("TempGross") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotaltempgross"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 16--%>
                                                <asp:TemplateField HeaderText="Basic" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="lblbasic" runat="server" Text='<%#Bind("basic") %>'>--%>
                                                        <asp:Label ID="lblbasic" runat="server" Text='<%#Eval("basic", "{0:0}") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalBasic"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 17--%>
                                                <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblda" runat="server" Text='<%#Eval("da","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 18--%>
                                                <asp:TemplateField HeaderText="HRA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhra" runat="server" Text='<%#Bind("hra","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalHRA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 19--%>
                                                <asp:TemplateField HeaderText="CCA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcca" runat="server" Text='<%#Bind("CCa","{0:0}") %>'>  
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalCCA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 20--%>
                                                <asp:TemplateField HeaderText="Conv" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConveyance" runat="server" Text='<%#Bind("conveyance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalConveyance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 21--%>
                                                <asp:TemplateField HeaderText="W.A." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwashallowance" runat="server" Text='<%#Bind("WashAllowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalWA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 22--%>
                                                <asp:TemplateField HeaderText="O.A." ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOtherallowance" runat="server" Text='<%#Bind("OtherAllowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 23--%>
                                                <asp:TemplateField HeaderText="Special AllW" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSpecialAllowance" runat="server" Text='<%#Bind("SpecialAllowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalSpecialAllowance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 24--%>
                                                <asp:TemplateField HeaderText="Uniform AllW" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUniformAllw" runat="server" Text='<%#Bind("UniformAllw","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalUniformAllw"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 25--%>
                                                <asp:TemplateField HeaderText="Mobile Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileAllowance" runat="server" Text='<%#Bind("MobileAllw","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalMobileAllowance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 26--%>
                                                <asp:TemplateField HeaderText="Medical Re-imbursement" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmedicalallowance" runat="server" Text='<%#Bind("medicalallowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalmedicalallowance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 27--%>
                                                <asp:TemplateField HeaderText="Food Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFoodAllowance" runat="server" Text='<%#Bind("FoodAllowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalFoodAllowance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 28--%>
                                                <asp:TemplateField HeaderText="Performance Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNightShiftAllw" runat="server" Text='<%#Bind("PerformanceAllw","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNightShiftAllw"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 29--%>
                                                <asp:TemplateField HeaderText="Travel Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTravelAllw" runat="server" Text='<%#Bind("TravelAllw","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalTravelAllw"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 30--%>
                                                <asp:TemplateField HeaderText="L.W" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveEncashAmt" runat="server" Text='<%#Bind("LeaveEncashAmt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalLeaveEncashAmt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 31--%>
                                                <asp:TemplateField HeaderText="Gratuity" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGratuity" runat="server" Text='<%#Bind("Gratuity","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalGratuity"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 32--%>
                                                <asp:TemplateField HeaderText="Bonus" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBonus" runat="server" Text='<%#Bind("Bonus","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalBonus"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 33--%>
                                                <asp:TemplateField HeaderText="Nfhs" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNfhs" runat="server" Text='<%#Bind("Nfhs","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNfhs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 34--%>
                                                <asp:TemplateField HeaderText="RC" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrc" runat="server" Text='<%#Bind("rc","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalrc"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 35--%>
                                                <asp:TemplateField HeaderText="CS" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcs" runat="server" Text='<%#Bind("cs","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalcs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 36--%>
                                                <asp:TemplateField HeaderText="Incentivs" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIncentivs" runat="server" Text='<%#Bind("Incentivs","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalIncentivs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 37--%>
                                                <asp:TemplateField HeaderText="WO Amt" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWoAmt" runat="server" Text='<%#Bind("WOAmt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalWOAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 38--%>
                                                <asp:TemplateField HeaderText="NHs Amt" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNhsAmt" runat="server" Text='<%#Bind("Nhsamt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNhsAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 39--%>
                                                <asp:TemplateField HeaderText="NPOTs Amt" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNpotsAmt" runat="server" Text='<%#Bind("Npotsamt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNpotsAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 40--%>
                                                <asp:TemplateField HeaderText="Att Bonus" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAttBonus" runat="server" Text='<%#Bind("Npotsamt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalAttBonus"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 41--%>
                                                <asp:TemplateField HeaderText="Pay 1" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNPCl25Per" runat="server" Text='<%#Bind("pay1","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNPCl25Per"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 42--%>
                                                <asp:TemplateField HeaderText="Pay 2" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransport6Per" runat="server" Text='<%#Bind("pay2","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalTransport6Per"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 43--%>
                                                <asp:TemplateField HeaderText="Pay 3" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransport" runat="server" Text='<%#Bind("pay3","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalTransport"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 44--%>
                                                <asp:TemplateField HeaderText="OT Amt" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOTAmt" runat="server" Text='<%#Bind("OTAmt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOTAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 45--%>
                                                <asp:TemplateField HeaderText="Addl Amount" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAddlAmount" runat="server" Text='<%#Bind("AddlAmount","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbltTotalAddlAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>





                                                <%-- 46--%>
                                                <asp:TemplateField HeaderText="Gross" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGross" runat="server" Text='<%#Bind("Gross","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalGross"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                                <%-- 47--%>
                                                <asp:TemplateField HeaderText="PF" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPF" runat="server" Text='<%#Bind("PF","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPF"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 48--%>
                                                <asp:TemplateField HeaderText="ESI" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblESI" runat="server" Text='<%#Bind("ESI","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalESI"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 49--%>
                                                <asp:TemplateField HeaderText="ProfTax" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProfTax" runat="server" Text='<%#Bind("ProfTax","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalProfTax"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 50--%>
                                                <asp:TemplateField HeaderText="Sal.Adv" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsaladv" runat="server" Text='<%#Bind("SalAdvDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalsaladv"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 51--%>
                                                <asp:TemplateField HeaderText="ADV Ded" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladvded" runat="server" Text='<%#Bind("ADVDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotaladvded"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 52--%>
                                                <asp:TemplateField HeaderText="WC Ded" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwcded" runat="server" Text='<%#Bind("WCDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalwcded"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 53--%>
                                                <asp:TemplateField HeaderText="U.D." ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbluniform" runat="server" Text='<%#Bind("UniformDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalUniformDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 54--%>
                                                <asp:TemplateField HeaderText="Others" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOtherDed" runat="server" Text='<%#Bind("OtherDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalOtherDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 55--%>
                                                <asp:TemplateField HeaderText="Total Loan ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotalloanded" runat="server" Text='<%#Bind("LoanDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotaltotalloanded"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 56--%>
                                                <asp:TemplateField HeaderText="C.A" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcantadv" runat="server" Text='<%#Bind("CanteenAdv","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalcantadv"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 57--%>
                                                <asp:TemplateField HeaderText="Sec Dep" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSecDepDed" runat="server" Text='<%#Bind("SecurityDepDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalSecDepDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 58--%>
                                                <asp:TemplateField HeaderText="Gen Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGeneralDed" runat="server" Text='<%#Bind("GeneralDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalGeneralDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                                <%-- 59--%>
                                                <asp:TemplateField HeaderText="LWF" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblowf" runat="server" Text='<%#Bind("OWF","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalowf"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 60--%>
                                                <asp:TemplateField HeaderText="Penalty" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenalty" runat="server" Text='<%#Bind("Penalty","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalPenalty"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 61--%>
                                                <asp:TemplateField HeaderText="ATM Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRentDed" runat="server" Text='<%#Bind("ATMDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalRentDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 62--%>
                                                <asp:TemplateField HeaderText="Medical Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMedicalDed" runat="server" Text='<%#Bind("MedicalDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalMedicalDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 63--%>
                                                <asp:TemplateField HeaderText="MLWF Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMLWFDed" runat="server" Text='<%#Bind("MLWFDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalMLWFDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 64--%>
                                                <asp:TemplateField HeaderText="Food Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFoodDed" runat="server" Text='<%#Bind("FoodDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalFoodDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                                <%-- 65--%>
                                                <asp:TemplateField HeaderText="IDCard Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblElectricityDed" runat="server" Text='<%#Bind("IDCardDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalElectricityDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 66--%>
                                                <asp:TemplateField HeaderText="Rent Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTransportDed" runat="server" Text='<%#Bind("RentDed1","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalTransportDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 67--%>
                                                <asp:TemplateField HeaderText="Other Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDccDed" runat="server" Text='<%#Bind("Finesded1","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalDccDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 68--%>
                                                <asp:TemplateField HeaderText="Leave Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveDed" runat="server" Text='<%#Bind("LeaveDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalLeaveDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <%-- 69--%>
                                                <asp:TemplateField HeaderText="License Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLicenseDed" runat="server" Text='<%#Bind("LicenseDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalLicenseDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 70--%>
                                                <asp:TemplateField HeaderText="Adv4 Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAdv4Ded" runat="server" Text='<%#Bind("Adv4Ded","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalAdv4Ded"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 71--%>
                                                <asp:TemplateField HeaderText="Extra" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNightRoundDed" runat="server" Text='<%#Bind("Extra","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalNightRoundDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 72--%>
                                                <asp:TemplateField HeaderText="ManpowerMob Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblManpowerMobDed" runat="server" Text='<%#Bind("ManpowerMobDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalManpowerMobDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 73--%>
                                                <asp:TemplateField HeaderText="Mobileusage Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMobileusageDed" runat="server" Text='<%#Bind("MobileusageDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalMobileusageDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                 <%-- 74--%>
                                                <asp:TemplateField HeaderText="MediClaim Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMediClaimDed" runat="server" Text='<%#Bind("MediClaimDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalMediClaimDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 75--%>
                                                <asp:TemplateField HeaderText="Crisis Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCrisisDed" runat="server" Text='<%#Bind("CrisisDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalCrisisDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 76--%>
                                                <asp:TemplateField HeaderText="PF Empr" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPFEmpr" runat="server" Text='<%#Bind("pfempr","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalPFEmpr"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                 <%-- 77--%>
                                                <asp:TemplateField HeaderText="ESI Empr" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblESIEmpr" runat="server" Text='<%#Bind("esiempr","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalESIEmpr"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                
                                                 <%-- 78--%>
                                                <asp:TemplateField HeaderText="Telephone Bill Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTelephoneBillDed" runat="server" Text='<%#Bind("TelephoneBillDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalTelephoneBillDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                 
                                                 <%-- 79--%>
                                                <asp:TemplateField HeaderText="PF & ESI Contribution" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPFESIContribution" runat="server" Text='<%#Bind("PFESIContribution","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalPFESIContribution"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 
                                                 <%-- 80--%>
                                                   <asp:TemplateField HeaderText="TDS Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTDSDed" runat="server" Text='<%#Bind("TDSDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalTDSDed"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                                <%-- 81--%>
                                                <asp:TemplateField HeaderText="Total Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeductions" runat="server" Text='<%#Bind("TotalDeductions","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDeductions"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 82--%>
                                                <asp:TemplateField HeaderText="Net Amt" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnetamount" runat="server" Text='<%#Bind("ActualAmount","{0:0}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNetAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 83--%>
                                                <asp:BoundField DataField="EmpBankCardRef" HeaderText="Reference No." DataFormatString="{0}&nbsp;" />
                                                <%-- 84--%>
                                                <asp:BoundField DataField="EmpIFSCcode" HeaderText="IFSC Code" DataFormatString="{0}&nbsp;" />
                                                <%-- 85--%>
                                                <asp:BoundField DataField="Empbankname" HeaderText="Bank Name" DataFormatString="{0}&nbsp;" />
                                            </Columns>
                                            <FooterStyle BackColor="white" Font-Bold="True" ForeColor="black" />
                                            <PagerStyle BackColor="white" ForeColor="black" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="white" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="white" Font-Bold="True" ForeColor="black" />
                                            <EditRowStyle BackColor="white" />
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
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
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
