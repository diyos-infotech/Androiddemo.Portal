<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingReports.aspx.cs" Inherits="Jawan.Portal.BillingReports" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: BILLING REPORT</title>
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
    <form id="BillingReports1" runat="server">
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
                                    <div align="right">
                                        <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" Visible="False" OnClientClick="AssignExportHTML()">Export to Excel</asp:LinkButton>
                                    </div>
                                    
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
                                            <td style="padding-left:80px">
                                                Client Name
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcname"  runat="server" AutoPostBack="true" class="sdrop" Width="300px"
                                                    OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                      <tr>
                                          <td>GSTIN/UIN</td>
                                          <td>
                                              <asp:DropDownList ID="ddlOurGSTIN"  runat="server"  class="sdrop">
                                                </asp:DropDownList>
                                          </td>
                                          <td colspan="2"></td>
                                      </tr>
                                        <tr>
                                            <td>
                                                Bill Type
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlbilltype" runat="server" class="sdrop">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>Software</asp:ListItem>
                                                    <asp:ListItem>Manual</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="padding-left:80px" >
                                                Period  
                                                <%--Invoice Type:--%>
                                            </td>
                                            <td>
                                                 <asp:DropDownList ID="ddlPeriod" runat="server"  class="sdrop" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged" AutoPostBack="true">
                                                    <asp:ListItem>From-To</asp:ListItem>
                                                    <asp:ListItem>Month</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td style="visibility:hidden">
                                                <asp:DropDownList ID="ddlinvoicetype" runat="server" class="sdrop">
                                                    <asp:ListItem>All</asp:ListItem>
                                                    <asp:ListItem>With</asp:ListItem>
                                                    <asp:ListItem>With Out</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblfromdate" runat="server" Text="From Date :" ></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="Txt_From_Date" runat="server" class="sinput" ></asp:TextBox>
                                                <cc1:CalendarExtender ID="CE_From_Date" runat="server" Enabled="True" TargetControlID="Txt_From_Date"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_From_Date" runat="server" Enabled="True" TargetControlID="Txt_From_Date"
                                                    ValidChars="/0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                 
                                            </td>
                                            <td style="padding-left:80px" >
                                                <asp:Label ID="lbltodate" runat="server" Text="To Date :" ></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:TextBox ID="Txt_To_Date"  runat="server" class="sinput" ></asp:TextBox>
                                                <cc1:CalendarExtender ID="CE_To_Date" runat="server" Enabled="True" TargetControlID="Txt_To_Date"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="FTBE_To_Date" runat="server" Enabled="True" TargetControlID="Txt_To_Date"
                                                    ValidChars="/0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                
                                                &nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblmonth" runat="server" Text="Month" Visible="false"></asp:Label>
                                                
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txtEndDate" runat="server" class="sinput" Visible="false"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server"  BehaviorID="calendar1"
                                                            Enabled="true" Format="MMM-yyyy" TargetControlID="txtEndDate" DefaultView="Months" OnClientHidden="onCalendarHidden"  OnClientShown="onCalendarShown">
                                                        </cc1:CalendarExtender>
                                                
                                            </td>
                                            <td colspan="2">
                                                <asp:Button runat="server"  ID="Btn_Search_Invoice_Btn_Dates" Text="Submit" class="btn save" style="margin-left:80px"
                                                OnClick="Btn_Search_Invoice_Btn_Dates_Click" />
                                            </td>
                                        </tr>
                                    </table>                         
                               
                            </div>
                            <asp:HiddenField ID="hidGridView" runat="server" />
                            <div id="forExport" style="overflow: scroll; width: 960px; ">
                                <asp:GridView ID="GVInvoiceBills" runat="server" AutoGenerateColumns="False" 
                                     CellPadding="4" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed table-hover"
                                    OnRowDataBound="GVInvoiceBills_RowDataBound" ShowFooter="true" style="overflow: scroll; width: 950px" >
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblsno" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                         <asp:TemplateField HeaderText="Our GSTIN">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblOURGSTIN" Text='<%# Bind("GSTNo") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Client Id">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientID" Text='<%# Bind("unitid") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Client Name" ItemStyle-Width="125px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientName" Text='<%# Bind("clientname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                       

                                         <asp:TemplateField HeaderText="Client GSTIN" ItemStyle-Width="125px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientGSTIN" Text='<%# Bind("GSTIN") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Date" ItemStyle-Width="125px">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblbilldt" Text='<%# Bind("BillDt") %>'></asp:Label>
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

                                         <asp:TemplateField HeaderText="Service Tax" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblServiceTax" runat="server" Text='<%#Bind("ServiceTax","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalServiceTax"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                         <asp:TemplateField HeaderText="SB Cess" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSBCess" runat="server" Text='<%#Bind("SBCessAmt","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalSBCessAmt"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                        <asp:TemplateField HeaderText="KK Cess" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblKKCess" runat="server" Text='<%#Bind("KKCessAmt","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalKKCessAmt"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                        <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCGST" runat="server" Text='<%#Bind("CGSTAmt","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalCGSTAmt"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                         <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSGST" runat="server" Text='<%#Bind("SGSTAmt","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalSGSTAmt"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                         <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIGST" runat="server" Text='<%#Bind("IGSTAmt","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalIGSTAmt"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Grand Total" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGrandTotal" runat="server" Text='<%#Bind("GrandTotal","{0:0.00}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: right">
                                                            <asp:Label runat="server" ID="lblTotalGrandTotal"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                       

                                     
                                        
                                      <%-- 
                                          <asp:BoundField HeaderText="Remarks" DataField="remarks" />
                                           <asp:BoundField HeaderText="Service Tax 75%" DataField="servicetaxsf" DataFormatString="{0:0.00}" />
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
                           
                        </div>
                    </div>
                    </div>
                    <div class="clear">
                    </div>
                    </div>
                </div>
            </div>
            <!-- DASHBOARD CONTENT END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS                         <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
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
