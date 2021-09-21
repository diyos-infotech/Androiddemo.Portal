<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DeleteClient.aspx.cs" Inherits="Jawan.Portal.DeleteClient" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DEACTIVATE CLIENT</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3
        {
            height: 24px;
        }
        .style4
        {
            height: 49px;
        }
    </style>
    <script type="text/javascript" src="script/jscript.js">
     </script>
    
    
</head>
<body>
    <form id="DeleteClient1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Product Tracking System" title="Product Tracking System" /></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a>          </li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a>              </li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a>   </li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a>                        </li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a>                </li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a>                         </li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span> Logout</span></span></a>        </li>
                       
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
                               <%--  <div class="submenubeforegap">
                                        &nbsp;</div>
                                      <div class="submenuactions">
                                        &nbsp;</div>--%> 
                                   <ul>
                                        <li><a href="clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                        <li class="current"><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
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
            <h1 class="dashboard_heading">
                Clients Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Delete Client</h2>
                        </div>
                        <div class="boxbody" style=" padding: 5px 5px 5px 5px;">
                            <!--  Content to be add here> -->
                             <div class="boxin">
                           <table>
                            <tr >
                            <td style="font-weight:bold">Client ID/Name :</td>
                            <td> <asp:TextBox ID="txtsearch" runat="server" class="sinput"></asp:TextBox>  </td>
                            <td><asp:Button  ID="btnSearch" runat="server" Text="Search" class=" btn save" 
                                    onclick="btnSearch_Click"/> 
                             </td>
                            </tr>
                            </table>
                            <br />
                             <div class="rounded_corners">
                            <div style="overflow:auto;">
                           
                            <asp:GridView ID="gvdeleteclient" runat="server" Width="150%" 
                                AutoGenerateColumns="False" Height="50%" Style="text-align: center"
                                 CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None" 
                                onrowdeleting="gvdeleteclient_RowDeleting1" AllowPaging="True"  HeaderStyle-HorizontalAlign="Center" RowStyle-HorizontalAlign="Center"
                                    onpageindexchanging="gvdeleteclient_PageIndexChanging" PageSize="15" >
                                <RowStyle BackColor="#EFF3FB" Height="30" />
                                <HeaderStyle Height="5px" />
                                
                                <Columns>
                                  <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="3%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                
                                
                                    <asp:TemplateField HeaderText=" Client Id" ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:Label ID="lblclientid" runat="server" Text=" <%#Bind('clientid')%>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Name"       ItemStyle-Width="60px"  DataField="clientname" > </asp:BoundField>
                                    <asp:BoundField HeaderText="House No."  ItemStyle-Width="60px"  DataField="ClientAddrHno" ></asp:BoundField>
                                    <asp:BoundField HeaderText="Street"     ItemStyle-Width="60px"  DataField="ClientAddrStreet" ></asp:BoundField>
                                    <asp:BoundField HeaderText="City"       ItemStyle-Width="60px"  DataField="ClientAddrCity" ></asp:BoundField>
                                    <asp:BoundField HeaderText="State"      ItemStyle-Width="60px"  DataField="ClientAddrstate" > </asp:BoundField>
                                    <asp:BoundField HeaderText="Phone No."  ItemStyle-Width="60px"  DataField="ClientPhoneNumbers" > </asp:BoundField>
                                    <asp:BoundField HeaderText="Fax No."    ItemStyle-Width="60px"  DataField="ClientFax" > </asp:BoundField>
                                    <asp:BoundField HeaderText="Email Id"   ItemStyle-Width="60px"  DataField="ClientEmail" > </asp:BoundField>
                                    <asp:TemplateField ItemStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="linkdelete" runat="server"   Text="View" 
                                            onclientclick='return confirm(" Are you sure  you  want to delete  the record?");' ></asp:LinkButton>
                                        </ItemTemplate>
                                      <ItemStyle HorizontalAlign="Center"/>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" 
                                    BorderWidth="1px" CssClass = "GridPager"/>
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
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
        </div>
       
         <!-- FOOTER BEGIN -->
        <div id="footerouter" >
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS<!--<img 
            alt="Powered by Businessface" src:"Pages/assets/footerlogo.png"/>--></a></div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>|
                    <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
                
                <div class="clear"></div>
            </div>
        </div>
          <!-- FOOTER END -->
        </div>
        <!-- CONTENT AREA END -->
      
    </form>
</body>
</html>
