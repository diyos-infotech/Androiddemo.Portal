<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewItems.aspx.cs" Inherits="Jawan.Portal.ViewItems" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>VIEW ITEM</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="ViewItems1" runat="server">
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
                    <%--<li><a href="Reminders.aspx">Reminders</a></li>--%><li>Welcome <b>
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
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server" class="current"><span>
                        Inventory</span></a></li>
                    <li class="after"><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
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
                                   
                                    <%-- <ul>
                <li class="current"><a href="Inventory.aspx" id="saleslink" runat="server"><span>Add</span></a></li>
                <li><a href="ModifyingStock.aspx" id="modifylink" runat="server"><span>Modify</span></a></li>
                <li><a href="Delete.aspx" id="deletelink" runat="server"><span>Delete</span></a></li>
                
             <li><a href="ListOfItems.aspx" id="ListOfItemslink" runat="server"><span> List Of Items</span></a></li>
                <li><a href="AddNewItem.aspx" id="NewItemlink" runat="server"><span>New Item</span></a></li>
           <li><a href="ModifyItem.aspx" id="ModifyItemlink" runat="server"><span>Modify Item</span></a></li>
               
                <li><a href="DeliveredItems.aspx" id="DeliveringItemlink" runat="server"><span>Delivered Items</span></a></li>
                <li><a href="ViewStock.aspx" id="ViewStocklink" runat="server"><span>View Stock</span></a></li>
        
         </ul>--%>
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
               </h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <ul class="shortcuts" style="margin-left: 13px">
                    <li><a href="InvPODetails.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Add PO Details</span> </a></li>
                     <li><a href="InvModifyPODetails.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Modify PO Details</span> </a></li>
                    <li><a href="InvInflowDetails.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                        class="shortcuts-label">Add Inflow Details</span> </a></li>
                     <li><a href="ModifyInflowDetails.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                        class="shortcuts-label">Modify Inflow Details</span> </a></li>
                    <%-- <li><a href="InvOutflowDetails.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                        class="shortcuts-label">Add Outflow Details</span> </a></li>
                    <li><a href="ModifyDC.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                        class="shortcuts-label">Modify Outflow Details</span> </a></li>--%>
                    <li><a href="InvVendorMaster.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Add Vendor Details</span> </a></li>
                     <li><a href="ModifyInvVendorMaster.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Modify Vendor Details</span> </a></li>
                  
                    <li><a href="AddnewItem.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Add New Item</span> </a></li>
                     <li><a href="ModifyItem.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Modify Item</span> </a></li>
                      <li><a href="POPrint.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">PO Print</span> </a></li>
                     <li><a href="POReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">PO Report</span> </a></li>
                    <%-- 
                          <li><a href="InvClientMaster.aspx"><span class="shortcuts-icon iconsi-event"></span><span
                        class="shortcuts-label">Client Rate Details</span> </a></li>
                        
                        <li><a href="DCReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">DC Report</span> </a></li>
                    <li><a href="DCList.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">DC List</span> </a></li>

                     <li><a href="DCFormat.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">DC Print</span> </a></li>--%>
                     <li><a href="ReportForStockList.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Stock in Hand</span> </a></li>
                    <li><a href="POInFlowDetailsReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">PO/Inflow Details Report</span> </a></li>
                      <li><a href="UniformPDF.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Uniform PDF</span> </a></li>

                     <li><a href="EmpInvReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Emp INV Report</span> </a></li>
                    <%--<li><a href="DCReportMonthly.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">DC Monthly Report</span> </a></li>
                    <li><a href="ClientwiseBPreport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Buying price Report</span> </a></li>
                     <li><a href="ClientWiseSPreport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="sho rtcuts-label">Selling price Report</span> </a></li>
                     <li><a href="ClientWiseBPSPDifferenceReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">BP/SP Comparision Report</span> </a></li>--%>
                     <%--<li><a href="InflowOutflowReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Stock Details Report</span> </a></li>
                      <li><a href="StockConsumptionReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Stock Consumption Report</span> </a></li>--%>
                  <%--  <li><a href="ItemWiseReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Item Wise Report</span> </a></li>--%>
                  <%--  <li><a href="ItemWiseInflowOutflowReport.aspx"><span class="shortcuts-icon iconsi-event"></span>
                        <span class="shortcuts-label">Item Wise (Inflow/Outflow) Report</span> </a></li>--%>
                    
                </ul>
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
