<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewClient.aspx.cs" Inherits="Jawan.Portal.ViewClient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>VIEW CLIENT</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
   <style type="text/css">
       
        h3{color:#0088cc;text-decoration:underline}
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
                    <li>
                    <a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>
                        Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
                        Logout</span></span></a></li>
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
                                <%--  <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div> --%>
                                <ul>
                                        
                                        <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                        <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                        <li><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="AssigningClients.aspx" id="Operationlink" runat="server"><span>Operations</span></a></li>
                                        <li><a href="ClientBilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a>   </li>
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
                  
                    <div class="col-md-12" style="margin-top:8px;margin-bottom:8px">
                        <div class="panel panel-inverse">
                            <div class="panel-heading">
                            
                             <table width="100%">
                               <tr>
                               <td>  <h3 class="panel-title">
                                     View Clients</h3></td>
                               <td align="right"><< <a href="Clients.aspx" style="color:#003366">Back</a>  </td>
                               </tr>
                               </table>
                           
                              
                            </div>
                            <div class="panel-body">
                              
                              <table width="100%" cellpadding="10" cellspacing="10">
                              <tr>
                              <td width="50%" valign="top">
                               <table style="width:100%">
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Client Id</span></td>
                        <td style="text-align: left">:
        <asp:Label ID="lblClientid" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Short Name</span></td>
                        <td style="text-align: left">:
        <asp:Label ID="lblShortname" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Contact Person</span></td>
                        <td style="text-align: left">:
        <asp:Label ID="lblContact" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Phone No</span></td>
                        <td style="text-align: left">:
        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Email-ID</span></td>
                        <td style="text-align: left">:
        <asp:Label ID="lblEmail" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                    <td><br /><h3>Billing Details</h3><br /></td>
                    <td></td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Line One</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtchno" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Line Three</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtarea" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Line Five</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtcity" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Description</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtdescription" runat="server"></asp:Label>
                        </td>
                    </tr>
                    </table>
                    </td>
                   <td width="50%">
                   
                   <table style="width:100%">
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Name of the Client</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="lblNameclient" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Segment</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="lblSegment" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Person Designation</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Landline No.</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="lblLand" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Fax No.</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="lblFaxno" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    </tr>
                     <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Line Two</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtstreet" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Line Four</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtcolony" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Line Six</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="txtstate" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Sub Unit Status</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="chkSubUnit" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Main Unit</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="radioyesmu" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">Invoice</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="radioinvoiceyes" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td style="text-align: left">
                            <span style="font-weight: normal">PaySheet</span></td>
                        <td style="text-align: left">:
                            <asp:Label ID="radiopaysheetyes" runat="server"></asp:Label>
                        </td>
                    </tr>
                   
                    </table>
                             </td>
                             </tr>
                             </table> 
                             
                             
                             
                             
                             
                             
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
