<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientLicenses.aspx.cs" Inherits="Jawan.Portal.ClientLicenses" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LICENSE MANAGEMENT</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        
    </style>
    <script type="text/javascript" src="script/jscript.js">
    </script>
</head>
<body>
    <form id="ClientLicenses1" runat="server">
    <asp:ScriptManager runat="server" ID="Scriptmanager1"></asp:ScriptManager>
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
                              <%--      <div class="submenubeforegap">
                                        &nbsp;</div>
                              <div class="submenuactions">
                                        &nbsp;</div>   --%>  
                                <ul>
                                        <li><a href="clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                        <li class="current"><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
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
            <h1 class="dashboard_heading">
                Clients Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                
                
                
                    <div class="sidebox">
                  <div class="boxhead">
                    <h2>Licenses Expiring this Month</h2>
                  </div>
                  
      
                  <div class="boxbody" style="padding:5px 5px 5px 5px;">
                  <div class="rounded_corners">
                  
                       <asp:GridView ID="dgLicExpire" runat="server" AllowPaging="True" 
                           AutoGenerateColumns="False" 
                           EmptyDataRowStyle-BackColor="BlueViolet" 
                           EmptyDataRowStyle-BorderColor="Aquamarine" EmptyDataRowStyle-Font-Italic="true" 
                           EmptyDataText="No Records Found" GridLines="None" Height="97px" PageSize="5" 
                           style="margin-bottom: 0px" Width="100%" 
                           CellPadding="5" CellSpacing="3">
                           <RowStyle HorizontalAlign="Left" BackColor="#EFF3FB" />
                           <EmptyDataRowStyle BackColor="SkyBlue" BorderColor="Aquamarine" 
                               Font-Italic="True" />
                           <Columns>
                               <asp:TemplateField  
                                   HeaderText="Client ID" >
                                   <ItemTemplate>
                                       <asp:Label ID="lblCust0" runat="server" Text='<%#Bind("UnitId")%>'></asp:Label>
                                   </ItemTemplate>
                                   <HeaderStyle Wrap="False" />
                                   <ItemStyle HorizontalAlign="Center" />
                               </asp:TemplateField>
                               <asp:BoundField DataField="ClientName"  
                                   HeaderText="Client Name" >
                                    <ItemStyle HorizontalAlign="Center" />
                               </asp:BoundField>
                               <asp:BoundField DataField="LicenseStartDate" DataFormatString="{0:d}"
                                   HeaderText="License StartDate" >
                                    <ItemStyle HorizontalAlign="Center" />
                               </asp:BoundField>
                               <asp:BoundField DataField="LicenseEndDate" DataFormatString="{0:d}" 
                                  HeaderText="License EndDate" HtmlEncode="False" 
                                  >
                                   <ItemStyle HorizontalAlign="Center" />
                               </asp:BoundField>
                               <asp:BoundField DataField="LicenseOfficeLoc" 
                                   HeaderText="Location of LicenseOffice" HtmlEncode="False" 
                                   >
                                    <ItemStyle HorizontalAlign="Center" />
                               </asp:BoundField>
                           </Columns>
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" 
                                    BorderWidth="1px" CssClass = "GridPager"/>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                       </asp:GridView>
                       </div>
                  </div>
                </div>
                    <div class="sidebox" style="margin:10px 0px 0px 0px;">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Add Licenses</h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <!--  Content to be add here> -->
                            
                           
                            <div class="boxin">
                                <div class="dashboard_firsthalf" style="width:100%">
                                <br />
                                <table width="100%"  cellpadding="5" cellspacing="5">
                                <tr>
                                <td valign="top">
                                
                                
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>
                                                    Client ID<span style=" color:Red">*</span>
                                                </td>
                                                <td>
                                                <asp:DropDownList runat="server" class="sdrop" ID="ddlClientId" AutoPostBack="True" 
                                                        onselectedindexchanged="ddlClientId_SelectedIndexChanged"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    LicenseNo<span style=" color:Red">*</span>
                                                </td>
                                                <td>
                                                <asp:TextBox runat="server" ID="txtLicenseNo"  class="sinput" TabIndex="1"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    License Start Date<span style=" color:Red">*</span>
                                                </td>
                                                <td>
                                                <asp:TextBox runat="server" ID="txtLicStart"  class="sinput" TabIndex="3"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="txtLicStart"
                                                                    Format="MM/dd/yyyy">
                                                                </cc1:CalendarExtender>
                                                                
                                                                 <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                                 runat="server" Enabled="True" TargetControlID="txtLicStart"
                                                                  ValidChars="/0123456789">
                                                                  </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                   </table>
                                   
                                   
                                   </td>
                                <td>
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td>
                                                Client Name<span style=" color:Red">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlCname" runat="server"  class="sdrop" width="300px" AutoPostBack="true" OnSelectedIndexChanged="ddlCname_OnSelectedIndexChanged">  </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Location Of LicenseOffice
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtLicOffLoc"  class="sinput" TabIndex="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                License End Date<span style=" color:Red">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtLicEnd"  class="sinput" TabIndex="4"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="txtLicEnd"
                                                                    Format="MM/dd/yyyy">
                                                                </cc1:CalendarExtender>
                                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                                 runat="server" Enabled="True" TargetControlID="txtLicEnd"
                                                                  ValidChars="/0123456789">
                                                                  </cc1:FilteredTextBoxExtender>
                                            </td>
                                        </tr>
                                        <tr><td>&nbsp;</td></tr>
                                        <tr><td>&nbsp;</td><td align="right"> <asp:Label ID="lblresult" runat="server" Visible="false" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnaddclint" runat="server" 
                                OnClick="btnaddclint_Click" 
                                OnClientClick='return confirm(" Are you sure you  want to add This record?");'
                                    Text="Save" class="btn save" ValidationGroup="a" />
                                <asp:Button ID="btncancel" runat="server" Text="CANCEL" ToolTip="Cancel Client" 
                                    class=" btn save" 
                                    OnClientClick='return confirm(" Are you sure you want to cancel This  entry?");' 
                                    onclick="btncancel_Click" /></td></tr>
                                    </table>
                                    </td>
                                    </tr>
                                </table>
                                   
                                </div>
                                
                            </div>
                           
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- DASHBOARD CONTENT END -->
        </div>
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS<!--<img 
            alt="Powered by Businessface" src:"Pages/assets/footerlogo.png"/>--></a></div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>|
                    <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </div>
    <!-- CONTENT AREA END -->
    </form>
</body>
</html>
