<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewContracts.aspx.cs" Inherits="Jawan.Portal.ViewContracts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>VIEW CONTRACT</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="script/jscript.js">
    </script>

    <script type="text/javascript">

        function dtval(d, e) {
            var pK = e ? e.which : window.event.keyCode;
            if (pK == 8) { d.value = substr(0, d.value.length - 1); return; }
            var dt = d.value;
            var da = dt.split('/');
            for (var a = 0; a < da.length; a++) { if (da[a] != +da[a]) da[a] = da[a].substr(0, da[a].length - 1); }
            if (da[0] > 31) { da[1] = da[0].substr(da[0].length - 1, 1); da[0] = '0' + da[0].substr(0, da[0].length - 1); }
            if (da[1] > 12) { da[2] = da[1].substr(da[1].length - 1, 1); da[1] = '0' + da[1].substr(0, da[1].length - 1); }
            if (da[2] > 9999) da[1] = da[2].substr(0, da[2].length - 1);
            dt = da.join('/');
            if (dt.length == 2 || dt.length == 5) dt += '/';
            d.value = dt;
        }
		
    </script>
 <style type="text/css">
        .style2
        {
            text-align: left;
            font-weight: bold;
        }
        h3{color:#0088cc;text-decoration:underline}
        h4{color:Red;text-decoration:underline}
    </style>
</head>
<body>
    <form id="contracts1" runat="server">
    <asp:ScriptManager runat="server" ID="Scriptmanager1">
    </asp:ScriptManager>
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a></div>
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
                    <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>
                        Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                </ul>
            </div>
            <!-- MAIN MENU SECTION END -->
        </div>
        <!-- LOGO AND MAIN MENU SECTION END -->
        <!-- SUB NAVIGATION SECTION BEGIN -->
        <div id="submenu">
            <table width="100%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td>
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                                    <%--     <div class="submenubeforegap">
                                        &nbsp;</div>
                                <div class="submenuactions">
                                        &nbsp;</div>  --%>
                                    <ul>
                                       <%-- <li><a href="clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>--%>
                                        <li class="current"><a href="contracts.aspx" id="ContractLink" runat="server"><span>
                                            Contracts</span></a></li>
                                        <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                        <li><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="AssigningClients.aspx" id="Operationlink" runat="server"><span>Operations</span></a></li>
                                        <li><a href="ClientBilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a></li>
                                        <li><a href="MaterialRequisitForm.aspx" id="MRFLink" runat="server"><span>MRF</span></a></li>
                                        <li><a href="ApproveMRF.aspx" id="ApproveMRFLink" runat="server"><span>ApproveMRF</span></a></li>
                                        <li><a href="Receipts.aspx" id="ReceiptsLink" runat="server"><span>Receipts</span></a></li>
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
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="col-md-12" style="margin-top: 8px; margin-bottom: 8px">
                        <div class="panel panel-inverse">
                            <div class="panel-heading">
                            <table width="100%">
                               <tr>
                               <td>  <h3 class="panel-title">
                                    View Contract</h3></td>
                               <td align="right"><< <a href="contracts.aspx" style="color:#003366">Back</a>  </td>
                               </tr>
                               </table>
                              
                            </div>
                            <div class="panel-body">
                                <table width="100%" cellpadding="10" cellspacing="10">
                                    <tr>
                                        <td width="50%" valign="top">
                                            <table style="width: 100%">
                                            
                                             <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Client Name</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblClientname" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Starting Date</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblStartdate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">BG Amount</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblBgAmount" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Payments</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblPayments" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Wages</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblWages" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                              
                                            </table>
                                        </td>
                                        <td width="50%" valign="top">
                                            <table style="width: 100%">
                                               
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Client Id</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblClientid" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Ending Date</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblEnddate" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Billing Dates</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblBillingdates" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Contractid</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblContractid" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Validity Date</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblValidity" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" cellpadding="10" cellspacing="10">
                                    <tr>
                                        <td width="50%" valign="top">
                                        <h3>Billing Details</h3><br />
                                            <table style="width: 100%">
                                              <tr>
                                                    <td style="text-align: left" width="160px">
                                                        <span style="font-weight: normal">Material Cost For Month</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblMaterialcost" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Machinery Cost For Month</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblMachinerycost" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Service Charge</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblServicecharge" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Contract Description</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblContractdesc" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Service Tax</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblServicetax" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Invoice Description</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblInvoicedesc" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                
                                            </table>
                                        </td>
                                        <td width="50%" valign="top">
                                        <h3>Paysheet Details</h3><br />
                                            <table style="width: 100%">
                                            <tr>
                                                    <td style="text-align: left" width="160px">
                                                        <span style="font-weight: normal">PF</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblPf" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">ESI</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblEsi" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">PF Limit </span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblPflimit" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">Esi Limit</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblEsilimit" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">OT</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblOt" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: left">
                                                        <span style="font-weight: normal">OT Amount</span>
                                                    </td>
                                                    <td style="text-align: left">
                                                        :
                                                        <asp:Label ID="lblOtamount" runat="server"></asp:Label>
                                                        
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                             
                             
                            <h3>Human Resource Needs</h3>
                             <br />
                               
                               
                                 <asp:GridView ID="gvHrs" runat="server" CellPadding="2" ForeColor="Black"
                                    AutoGenerateColumns="False" Width="100%" BackColor="#f9f9f9" BorderColor="#A1DCF2"
                                    BorderWidth="1px" AllowPaging="True" >
                                    <RowStyle Height="30px" />
                                   <Columns>
                                    
                                      <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDesign" Text="<%# Bind('Designations') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="Qty">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('Quantity') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="P.R">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('Amount') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="D.T">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('PayType') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="No of Days">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('NoOfDays') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Nots">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('nots') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText=" BASIC">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('basic') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             <asp:TemplateField HeaderText="DA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('da') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HRA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('hra') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                              <asp:TemplateField HeaderText=" Conv">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('conveyance') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText=" CCA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('cca') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText=" L A">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('leaveamount') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="Gratuity">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('gratuity') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText=" Bonus">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('bonus') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="W A">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('washallownce') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="O A">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('OtherAllowance') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="NFHs">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('nfhs') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="R.C">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('rc') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="C S">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('cs') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                             <asp:TemplateField HeaderText="Summary">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblQty" Text="<%# Bind('Summary') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                          </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="White" ForeColor="GhostWhite" />
                                    <HeaderStyle BackColor="LightBlue" Font-Bold="True" ForeColor="Black" Height="30px" />
                                    <AlternatingRowStyle BackColor="White" Height="30px" />
                                </asp:GridView>
                               
                               
                                
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- DASHBOARD CONTENT END -->
            <%-- </div> </div>--%>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
        </div>
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by WebWonders</div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
            <!-- CONTENT AREA END -->
        </div>
    </form>
</body>
</html>
