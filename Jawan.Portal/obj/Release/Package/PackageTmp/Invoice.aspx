<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Invoice.aspx.cs" Inherits="Jawan.Portal.Invoice" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVOICE</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>

    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        #social div
        {
            display: block;
        }
        .HeaderStyle
        {
            text-align: Left;
        }
        </style>
</head>
<body>
    <form id="Invoice1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a>
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
                    <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>
                        Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                                  <%--   <div class="submenuactions">
                                        &nbsp;</div> --%>
                                    <ul>
                                        <li><a href="clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                        <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                        <li><a href="clientattendance.aspx" id="ClientAttenDanceLink" runat="server">
                                            <span>Attendance</span></a></li>
                                        <li><a href="AssigningClients.aspx" id="OperationLink" runat="server"><span>Operations</span></a></li>
                                       <li class="current"><a href="ClientBilling.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                      <li><a href="MaterialRequisitForm.aspx" id="MRFLink" runat="server"><span>MRF</span></a></li>
                                    <li><a href="ApproveMRF.aspx" id="ApproveMRFLink" runat="server"><span>ApproveMRF</span></a></li>   
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
                Clients Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Invoice</h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height: 200px; height: auto">
                            <!--  Content to be add here> -->
                       
                       <div> Client Id  &nbsp; 
                       <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="True" 
                               onselectedindexchanged="ddlclientid_SelectedIndexChanged">
                       <asp:ListItem Enabled="true" Value="1">--Select Client Id--</asp:ListItem>
                       </asp:DropDownList>
                       Month &nbsp;
                        <asp:DropDownList ID="ddlmonth" runat="server" AutoPostBack="True" 
                               onselectedindexchanged="ddlmonth_SelectedIndexChanged">
                        <asp:ListItem Enabled="true" Value="1">--Select Month--</asp:ListItem>
                         </asp:DropDownList>
                         Designatiom &nbsp;&nbsp;
                         
                         <asp:DropDownList ID="ddldesgn" runat="server" AutoPostBack="true" >
                         <asp:ListItem Selected="true" Value="1">--Select Designation-- </asp:ListItem>
                         
                         </asp:DropDownList>
                         
                         
                         
                       </div>
                       <div>From &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                       <asp:TextBox ID="txtfromdate" runat="server" Width="120px"></asp:TextBox>
                           To &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="txttodate" runat="server" Width="115px"></asp:TextBox>
                       
                       <asp:Button ID="btninvoice" runat="server" Text="Invoice" class="btn save" 
                               onclick="btninvoice_Click" />
                         </div>
                       <div>
                      
                       <asp:GridView ID="gvclientpayment" runat="server" AutoGenerateColumns="false" 
                               Width="100%" onrowdatabound="gvclientpayment_RowDataBound">
                        <Columns>
           
                            <asp:BoundField DataField="unitid" HeaderText="unitId"/> 
                               <asp:BoundField DataField="designation" HeaderText="designation"/> 
                              <asp:BoundField DataField="noofemps" HeaderText="No Of Employees"/> 
                              <asp:BoundField DataField="payrate" HeaderText="Payate"/> 
                             <asp:BoundField DataField="DutyHours" HeaderText="No.Of Dts/Hrs"/> 
      
      <asp:TemplateField HeaderText="Amount (Rs)">
      <ItemTemplate>
      <asp:Label ID="lblamount" runat="server" Text="">
      
      </asp:Label>
      </ItemTemplate>
      </asp:TemplateField>
                             
                      </Columns>
                        </asp:GridView>
                       
                      </div>
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
                <asp:Label ID="lblcname" runat="server" meta:resourcekey="lblcnameResource1"></asp:Label>.
            </div>
            <div class="clear">
            </div>
        </div>
        <!-- FOOTER END -->
    </div>
    
        
        
        
        
        
    </div>
    </form>
</body>
</html>
