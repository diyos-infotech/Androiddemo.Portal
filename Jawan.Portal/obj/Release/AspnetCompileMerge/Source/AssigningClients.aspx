<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AssigningClients.aspx.cs" Inherits="Jawan.Portal.AssigningClients" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ASSIGNING CLIENTS</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="AssigningClients1" runat="server">
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
                        <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b>
                    </li>
                    <li class="lang"><a href="Login.aspx">Logout</a> </li>
                </ul>
            </div>
            <!-- TOP INFO END -->
            <!-- MAIN MENU BEGIN -->
            <div id="mainmenu">
                <ul>
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a> </li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span> Company Info</span></a> </li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a> </li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a> </li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a>  </li>
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
                                 <%--  <div class="submenubeforegap">
                                       &nbsp;</div> 
                                  <div class="submenuactions">
                                        &nbsp;</div>--%>
                               <ul>
                                        <li><a href="Clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                        <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                        <li><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li class="current"><a href="AssigningClients.aspx" id="Operationlink" runat="server"><span>Operations</span></a></li>
                                    <li><a href="clientBilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a></li>
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
              
                   <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               List of Clients
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin" style="height: 750px">
                           
                           <div class="dashboard_firsthalf" style="width: 100%">
                             <table width="65%" cellspacing="5" cellpadding="5" border="0">
                                        <tr>
                                            <td>
                                                Operational Manager<span style=" color:Red">*</span>
                                            </td>
                                          <td>
                                                <asp:DropDownList ID="ddloperationalmanager" runat="server" class="sdrop"
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddloperationalmanager_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                         
                                            <td> 
                                               Name</td>
                                               
                                               <td><asp:DropDownList ID="ddloperationalmanagername" 
                                                    runat="server" class="sdrop"
                                                    AutoPostBack="True" onselectedindexchanged="ddloperationalmanagername_SelectedIndexChanged" 
                                                    >
                                                </asp:DropDownList>
                                            </td>
                                            
                                        </tr>
                                    </table> 
                                    
                                    
            <div style="height:auto">
                           
                                    <table cellspacing="5" cellpadding="5" border="0" style="height: 50px" width="100%">
                                        <tr>
                                            <td width="20%">
                                               <%-- Operational Manager<span style=" color:Red">*</span>--%>
                                            </td>
                                            <td width="30%">
                                              <%--  <asp:DropDownList ID="ddloperationalmanager" runat="server" Width="160px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddloperationalmanager_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>--%>
                                            </td>
                                            <td width="50%" font-weight: bold; font-size:20px">
                                            <asp:Label ID="Label1" runat="server" Text="UnAssigned Clients"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>  
                                    </div>
                                    </div>                          
                                
                                    <div class="dashboard_FirstOfThree">
                                    <div class="rounded_corners">
                                        <asp:GridView ID="gvasclient" runat="server" AutoGenerateColumns="False" width="98%"
                                            CellPadding="4" ForeColor="#333333" EmptyDataText="No Records Found" 
                                            GridLines="None" BorderStyle="Outset">
                                            <RowStyle BackColor="#EFF3FB" Height="30px" HorizontalAlign="Left" 
                                                VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                                Height="20px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                        <asp:CheckBox  ID="checkunclient" runat="server"/>
                                        </ItemTemplate>
                                        
                                        </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText=" Client Id" ItemStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text=" <%#Bind('clientid')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText=" Name" ItemStyle-Width="60px" DataField="clientName">
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Segment" ItemStyle-Width="60px" DataField="ClientSegment">
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        </div> 
                                    </div>
                                    
                                    
                                    <div class="dashboard_SecondOfThree">
                                        <div>                  
                                             <asp:Button ID="btnleft" runat="server" Text="<<" 
                                                  style=" margin-left:0px; text-align:left; width:20px; margin-top:40px" 
                                                  class=" btn save" onclick="btnleft_Click" /> 
                                          </div>
                                          <div >
                                           <asp:Button ID="btnright" runat="server" Text=">>" 
                                                  style=" margin-left:0px; text-align:left; width:20px; margin-top:10px; position:static" 
                                                  class=" btn save" onclick="btnright_Click" />                 
                                        </div>
                                    </div>
                                    
                                    
                                    
                               
                                <div class="dashboard_ThirdOfThree" >
                                <div class="rounded_corners">
                                    <asp:GridView ID="gvunasclient" runat="server" AllowPaging="True" AutoGenerateColumns="False" width="98%"
                                            CellPadding="5" CellSpacing="3" ForeColor="#333333" EmptyDataText="No Records Found" 
                                            GridLines="None" BorderStyle="Outset" PageSize="15">
                                            <RowStyle BackColor="#EFF3FB" Height="30px" HorizontalAlign="Left" 
                                                VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                             <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" 
                                    BorderWidth="1px" CssClass = "GridPager"/>
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                                Height="20px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField ItemStyle-Width="20px">
                                        <ItemTemplate>
                                        <asp:CheckBox  ID="checkunclient" runat="server"/>
                                        </ItemTemplate>
                                        
                                        </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText=" Client Id" ItemStyle-Width="60px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text=" <%#Bind('clientid')%>"></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:TemplateField>
                                                <asp:BoundField HeaderText=" Name" ItemStyle-Width="60px" DataField="clientName">
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                                <asp:BoundField HeaderText="Segment" ItemStyle-Width="60px" DataField="ClientSegment">
                                                    <ItemStyle Width="60px"></ItemStyle>
                                                </asp:BoundField>
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                </div>
                                 <div class=" buttons_holder1" style=" margin-top:100px">
           <asp:Label ID="lblresult" runat="server" Text="" class="fontstyle" style=" color:Red"> </asp:Label>
           <%-- <asp:Button ID="btnsave" runat="server" class="btn save" Text="save" />
          
           <asp:Button  ID="btncancel" runat="server" class="btn save" Text="Cancel" />--%>
           
             </div>
              <%--<div>                  
                 <asp:Button ID="btnleft" runat="server" Text="<<" 
                      style=" margin-left:0px; text-align:left; width:20px; margin-top:40px" 
                      class=" btn save" onclick="btnleft_Click" /> 
              </div>
              <div >
               <asp:Button ID="btnright" runat="server" Text=">>" 
                      style=" margin-left:0px; text-align:left; width:20px; margin-top:10px; position:static" 
                      class=" btn save" onclick="btnright_Click" />                 
                   </div>--%>             
                           
                 
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

