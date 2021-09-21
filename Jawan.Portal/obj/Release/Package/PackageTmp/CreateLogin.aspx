<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateLogin.aspx.cs" Inherits="Jawan.Portal.CreateLogin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SETTINGS: CREATE LOGIN</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="CreateLogin1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE" /></a></div>
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
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server" class="current"><span>
                        Settings</span></a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
                    <li class="after last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
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
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li class="current"><a href="Settings.aspx" id="creak" runat="server"><span>Main</span></a>
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
                    <li class="first"><a href="Settings.aspx" style="z-index: 9;"><span></span>Settings</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Create Login</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
              
                   <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               Create Login
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                           
                                   <div class="dashboard_firsthalf" style="width: 100%">
                                          <table style="font-family: Arial; font-weight: normal; font-variant: normal; font-size: 13px"
                                                    width="100%" cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            Emp Id<span style="color: Red">*</span>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlempid" runat="server" class="sdrop" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlempid_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="lblempname" runat="server" Text="Emp Name"> </asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlempname" runat="server" class="sdrop" AutoPostBack="true"
                                                                OnSelectedIndexChanged="ddlempname_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Designation
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtdesign" runat="server" Text="" autocomplete="off" AutoCompleteType="None"
                                                                class="sinput" Enabled="false"> </asp:TextBox>
                                                        </td>
                                                        <td>
                                                            Privilege
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlpreviligers" runat="server" class="sdrop">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            User Name<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                            <asp:TextBox ID="txtusername" runat="server" autocomplete="off" AutoCompleteType="None"
                                                                class="sinput"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Password<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                            <asp:TextBox ID="txtpwd" runat="server" TextMode="Password" autocomplete="off" AutoCompleteType="None"
                                                                class="sinput"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Confirm Password<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
                                                            <asp:TextBox ID="txtcpwd" runat="server" TextMode="Password" autocomplete="off" AutoCompleteType="None"
                                                                class="sinput"></asp:TextBox>
                                                            <asp:CompareValidator ID="comparepasword" runat="server" Text="*" ControlToValidate="txtcpwd"
                                                                ControlToCompare="txtpwd">
                                                            </asp:CompareValidator>
                                                        </td>
                                                        <td>
                                                            &nbsp;
                                                        </td>
                                                        <td>
                                                            <div style="float: right;margin-right:20px">
                                            <asp:Label ID="lblresult" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>
                                            <asp:Button ID="btnsave" runat="server" ValidationGroup="a1" Text="SAVE" ToolTip="SAVE"
                                                OnClientClick='return confirm(" Are you sure  you  want to add the record?");'
                                                class=" btn save" OnClick="btnsave_Click" />
                                            <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="CANCEL" ToolTip="CANCEL"
                                                OnClientClick='return confirm(" Are you  sure you  want to cancel this  entry?");'
                                                class=" btn save" OnClick="btncancel_Click" />
                                        </div>
                                                        </td>
                                                    </tr>
                                                    
                                                </table>
                                                 </div>
                                       
                                            <div class="rounded_corners">
                                                <asp:GridView ID="gvcreatelogin" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    Height="50%" Style="text-align: center" CellPadding="5" CellSpacing="3" ForeColor="#333333"
                                                    GridLines="None" AllowPaging="True" OnRowDeleting="gvcreatelogin_RowDeleting">
                                                    <PagerSettings Mode="NextPreviousFirstLast" />
                                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText=" Emp Id">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempid" runat="server" Text=" <%#Bind('Emp_Id')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="User Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblname" runat="server" Text="<%#Bind('username')%>">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="60px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="40px">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkdelete" runat="server" CommandName="delete" Text="Delete"
                                                                    OnClientClick='return confirm("Are you sure you want to delete login details for this employee?"); '></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40px"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
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
