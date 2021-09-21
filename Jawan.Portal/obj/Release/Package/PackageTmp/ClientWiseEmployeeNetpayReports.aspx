<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientWiseEmployeeNetpayReports.aspx.cs" Inherits="Jawan.Portal.ClientWiseEmployeeNetpayReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: NET PAY REPORT</title>
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
             .replace(/'/g, '&#39;')
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
    <form id="ClientWiseEmployeeNetpayReports1" runat="server">
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
                                        <li ><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>Inventory</span></a></li>
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
            <div id="breadcrumb">
                <ul class="crumbs">
                    <li class="first"><a href="#" style="z-index: 9;"><span></span>Reports</a></li>
                    <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Net Pay</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                             Net Pay
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                            
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                    
                        <div class="dashboard_firsthalf" style="width: 100%">
                            
                            <div align="right">
                                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click"  OnClientClick="AssignExportHTML()">Export to Excel</asp:LinkButton>
                                            </div>
                            
                            
                          
                                <table width="100%" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td>
                                            Client Id
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="True"
                                             OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged" class="sdrop">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Name
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="True"
                                             OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged"  class="sdrop">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Month
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmonth" AutoComplete="off" runat="server" Text="" class="sinput" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="Txt_Month_CalendarExtender" runat="server"  BehaviorID="calendar1"
                                                            Enabled="true" Format="MMM-yyyy" TargetControlID="txtmonth" DefaultView="Months" OnClientHidden="onCalendarHidden"  OnClientShown="onCalendarShown">
                                                        </cc1:CalendarExtender>
                          
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" 
                                                onclick="btn_Submit_Click" />
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
                            <div id="forExport" class="rounded_corners" style="overflow:scroll">
                  
                         <div style="overflow: scroll" class="rounded_corners" >
                                <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellSpacing="3" CellPadding="5" ForeColor="#333333" ShowFooter="true"  onrowdatabound="GVListEmployees_RowDataBound" GridLines="Both">
                                    <RowStyle BackColor="white" Height="30" />
                                    <Columns>
                                           <%--<asp:BoundField DataField="clientid" HeaderText="Client Id" />
                                         <asp:BoundField DataField="clientname" HeaderText="Clien  Name" />
                                        <asp:BoundField DataField="basic" HeaderText="Basic" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="da" HeaderText="DA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="hra" HeaderText="HRA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="cca" HeaderText="CCA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="Conveyance" HeaderText="Conveyance" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="WashAllowance" HeaderText="WashAllowance" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="OtherAllowance" HeaderText="OtherAllowance" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="OtherAllowance" HeaderText="OtherAllowance" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="leaveencashamt" HeaderText="LeaveEncashamt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="OTAmt" HeaderText="OTAmt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="WOAmt" HeaderText="WOAmt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="totalGross" HeaderText="Total Gross" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="pf" HeaderText="PF" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="esi" HeaderText="ESI" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="ProfTax" HeaderText="PT" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="penalty" HeaderText="Penalty" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="saladvded" HeaderText="SALADVDED" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="UniformDed" HeaderText="Uniformded" DataFormatString="{0:0.00}" />
                                       <asp:TemplateField HeaderText="InsDed">
                                      <ItemTemplate>
                                      <asp:Label ID="lblinsded" runat="server" Text="0.00"></asp:Label>
                                      </ItemTemplate>
                                      </asp:TemplateField>
                                        <asp:BoundField DataField="OtherDed" HeaderText="Otherded" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="CanteenAdv" HeaderText="CanteenAdv" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="TotalDeductions" HeaderText="TotalDeductions" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="actualamount" HeaderText="Net AMount" DataFormatString="{0:0.00}" />--%>

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
                                                <asp:TemplateField HeaderText="Client ID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15px">
                                                    <HeaderStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text='<%#Bind("clientid") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <%-- 2--%>

                                                <asp:TemplateField HeaderText="Client Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="15px">
                                                    <HeaderStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientname" runat="server" Text='<%#Bind("clientname") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <%-- 3--%>
                                                <asp:TemplateField HeaderText="Basic" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBasic" runat="server" Text='<%#Bind("basic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalBasic"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 4--%>
                                                <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDA" runat="server" Text='<%#Bind("da") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 5--%>
                                                <asp:TemplateField HeaderText="HRA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblHRA" runat="server" Text='<%#Bind("hra") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalHRA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 6--%>
                                                <asp:TemplateField HeaderText="CCA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblmonth" runat="server" Text='<%#Bind("cca") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalCCA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 7--%>
                                                <asp:TemplateField HeaderText="Conveyance" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConveyance" runat="server" Text='<%#Bind("Conveyance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalConveyance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 8--%>
                                                <asp:TemplateField HeaderText="WashAllowance" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWashAllowance" runat="server" Text='<%#Bind("WashAllowance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbltotalWashAllowance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 9--%>
                                                <asp:TemplateField HeaderText="OtherAllowance" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOtherAllowances" runat="server" Text='<%#Bind("OtherAllowance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOtherAllowance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 10--%>
                                                <asp:TemplateField HeaderText="LeaveEncashamt" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveEncashamt" runat="server" Text='<%#Bind("leaveencashamt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalLeaveEncashamt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 11--%>
                                                <asp:TemplateField HeaderText="OTAmt" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" >
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOTAmt" runat="server" Text='<%#Bind("OTAmt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOTAmt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 12--%>
                                                <asp:TemplateField HeaderText="WOAmt" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWOAmt" runat="server" Text='<%#Bind("WOAmt") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalWOAmt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 13--%>
                                                <asp:TemplateField HeaderText="Total Gross" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotalGross" runat="server" Text='<%#Bind("totalGross") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotaltotalGross"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 14--%>
                                                <asp:TemplateField HeaderText="PF" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPF" runat="server" Text='<%#Bind("pf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPF"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                 <%-- 15--%>
                                                <asp:TemplateField HeaderText="ESI" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblESI" runat="server" Text='<%#Bind("esi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalESI"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                  
                                                 <%-- 16--%>
                                                <asp:TemplateField HeaderText="PT" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPT" runat="server" Text='<%#Bind("ProfTax") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPT"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                  
                                                   <%-- 17--%>
                                                <asp:TemplateField HeaderText="SALADVDED" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSALADVDED" runat="server" Text='<%#Bind("saladvded") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalSALADVDED"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                   <%-- 18--%>
                                                <asp:TemplateField HeaderText="Uniformded" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblUniformded" runat="server" Text='<%#Bind("UniformDed") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalUniformded"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                   
                                                  <%-- 19--%>
                                                <asp:TemplateField HeaderText="Otherded" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOtherded" runat="server" Text='<%#Bind("OtherDed") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOtherded"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                <%-- 20--%>
                                                <asp:TemplateField HeaderText="CanteenAdv" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCanteenAdv" runat="server" Text='<%#Bind("CanteenAdv") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalCanteenAdv"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>

                                         <%-- 21--%>
                                                <asp:TemplateField HeaderText="PFESIContribution" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPFESIContribution" runat="server" Text='<%#Bind("PFESIContribution") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPFESIContribution"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                         
                                         <%-- 22--%>
                                         <asp:TemplateField HeaderText="TDS Ded" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTDSDed" runat="server" Text='<%#Bind("TDSDed") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalTDSDed"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>

                                                <%-- 23--%>
                                                <asp:TemplateField HeaderText="TotalDeductions" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalDeductions" runat="server" Text='<%#Bind("TotalDeductions") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalTotalDeductions"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                 <%-- 24--%>
                                                <asp:TemplateField HeaderText="Net AMount" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblactualamount" runat="server" Text='<%#Bind("actualamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalactualamount"></asp:Label>
                                                    </FooterTemplate>
                                                    </asp:TemplateField>
                                                
                                                                               
                                    </Columns>
                                    <FooterStyle BackColor="white" Font-Bold="True" ForeColor="black" />
                                    <PagerStyle BackColor="white" ForeColor="black" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="white" Font-Bold="True" ForeColor="black" />
                                    <HeaderStyle BackColor="white" Font-Bold="True" ForeColor="black" Height="30" />
                                    <EditRowStyle BackColor="white" />
                                    <AlternatingRowStyle BackColor="white" />
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
