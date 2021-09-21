<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Minimum_Wages.aspx.cs" Inherits="Jawan.Portal.Minimum_Wages" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MINIMUM WAGES</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="SalaryBreakup1" runat="server">
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
                    <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server" class="current"><span>
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
                                        <li class="current"><a href="CreateLogin.aspx" id="creak" runat="server"><span>Main</span></a>
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
     <asp:ScriptManager runat="server" ID="Scriptmanager1">
    </asp:ScriptManager>
        <div class="content-holder">
            <div id="breadcrumb">
                <ul class="crumbs">
                    <li class="first"><a href="Settings.aspx" style="z-index: 9;"><span></span>Settings</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Minimum Wages</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               Minimum Wages
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                             <div class="rounded_corners">
                                              <asp:GridView ID="Gv_Minimum_Wages" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    Height="50%" Style="text-align: center" CellPadding="5" CellSpacing="3" ForeColor="#333333"
                                                    GridLines="None"  
                                                    OnRowUpdating="Gv_Minimum_Wages_RowUpdating"
                                                    OnRowCancelingEdit="Gv_Minimum_Wages_RowCancelingEdit"
                                                     OnRowEditing="Gv_Minimum_Wages_RowEditing">
                                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Category ID">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Category" runat="server" Text=" <%#Bind('id')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbl_Category" runat="server" Text="<%#Bind('id')%>"></asp:Label>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                          <asp:TemplateField HeaderText="Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbl_Category_Name" runat="server" Text=" <%#Bind('name')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lbl_Category_Name" runat="server" Text="<%#Bind('name')%>"></asp:Label>
                                                            </EditItemTemplate>
                                                        </asp:TemplateField>
                                                        
                                                        <asp:TemplateField HeaderText="BASIC" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                               
                                               <ItemTemplate>
                                               <asp:Label ID="Lbl_Basic" runat="server" Text="<%#Bind('Basic')%>" ></asp:Label>
                                               </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Basic" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('Basic')%>" > </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Basic" runat="server" Enabled="True"
                                                        TargetControlID="Txt_Basic" ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="DA">
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_Da" runat="server" Text="<%#Bind('DA')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Da" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('DA')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Da" runat="server" Enabled="True"
                                                        TargetControlID="Txt_Da" ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="HRA">
                                            
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_Hra" runat="server" Text="<%#Bind('HRA')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Hra" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('HRA')%>"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Hra" runat="server" Enabled="True" TargetControlID="Txt_Hra"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Conv">
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_Conv" runat="server" Text="<%#Bind('Conveyance')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Conv" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('Conveyance')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Conv" runat="server" Enabled="True" TargetControlID="Txt_Conv"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            <asp:TemplateField HeaderText="CCA">
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_CCA" runat="server" Text="<%#Bind('CCA')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Cca" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('CCA')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Cca" runat="server" Enabled="True" TargetControlID="Txt_Cca"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="L A">
                                            
                                              <ItemTemplate>
                                               <asp:Label ID="Lbl_LeaveAmount" runat="server" Text="<%#Bind('LeaveAmount')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_LeaveAmount" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('LeaveAmount')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_LeaveAmount" runat="server" Enabled="True" TargetControlID="Txt_LeaveAmount"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="Gratuity">
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_Gratuity" runat="server" Text="<%#Bind('Gratuity')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Gratuity" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('Gratuity')%>" > </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Gratuity" runat="server" Enabled="True" TargetControlID="Txt_Gratuity"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="Bonus">
                                            
                                            <ItemTemplate>
                                               <asp:Label ID="Lbl_Bonus" runat="server" Text="<%#Bind('Bonus')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Bonus" runat="server" Width="35px" Style="text-align: center"  Text="<%#Bind('Bonus')%>" >  </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Bonus" runat="server" Enabled="True" TargetControlID="Txt_Bonus"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="W A">
                                            
                                            <ItemTemplate>
                                               <asp:Label ID="Lbl_WashAllownce" runat="server" Text="<%#Bind('WashAllownce')%>" ></asp:Label>
                                               </ItemTemplate>
                                               
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_WashAllownce" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('WashAllownce')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_WashAllownce" runat="server" Enabled="True" TargetControlID="Txt_WashAllownce"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="O A">
                                            
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_OtherAllowance" runat="server" Text="<%#Bind('OtherAllowance')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                            
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_OtherAllowance" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('OtherAllowance')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_OtherAllowance" runat="server" Enabled="True" TargetControlID="Txt_OtherAllowance"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            
                                            <asp:TemplateField HeaderText="NFHs">
                                            
                                            
                                            <ItemTemplate>
                                               <asp:Label ID="Lbl_NFhs" runat="server" Text="<%#Bind('NFhs')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Nfhs" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('NFhs')%>" > </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Nfhs" runat="server" Enabled="True" TargetControlID="Txt_Nfhs"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="R.C">
                                            
                                            
                                             <ItemTemplate>
                                               <asp:Label ID="Lbl_Rc" runat="server" Text="<%#Bind('RC')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Rc" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('RC')%>" > </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Rc" runat="server" Enabled="True" TargetControlID="Txt_Rc"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                            <asp:TemplateField HeaderText="C S">
                                            
                                            <ItemTemplate>
                                               <asp:Label ID="Lbl_Cs" runat="server" Text="<%#Bind('cs')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Cs" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('cs')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Cs" runat="server" Enabled="True" TargetControlID="Txt_Cs"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                             <asp:TemplateField HeaderText="Total">
                                            
                                            <ItemTemplate>
                                               <asp:Label ID="Lbl_Total" runat="server" Text="<%#Bind('cs')%>" ></asp:Label>
                                               </ItemTemplate>
                                            
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="Txt_Total" runat="server" Width="35px" Style="text-align: center" Text="<%#Bind('cs')%>"> </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FTBE_Total" runat="server" Enabled="True" TargetControlID="Txt_Total"
                                                        ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            
                                            
                                                      <asp:TemplateField HeaderText="Operations">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkedit" runat="server" CommandName="Edit" Text="Edit">
                                                        </asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="linkupdate" runat="server" CommandName="update" Text="Update"
                                                            OnClientClick='return confirm(" Are you  sure you  want to update  the Minimum Wages Category??");'  style="color:Black"></asp:LinkButton>
                                                        <asp:LinkButton ID="linkcancel" runat="server" CommandName="cancel" Text="Cancel"
                                                            OnClientClick='return confirm(" Are you  sure you  want to cancel  the Minimum Wages Category??");'  style="color:Black"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>  
                                                    
                                                     
                                                    </Columns>
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" BorderWidth="1px" CssClass="GridPager" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                                     <EditRowStyle ForeColor="#000" BackColor="#C2D69B" />
                                                    <AlternatingRowStyle BackColor="White" />
                                                </asp:GridView>   
                                            </div>
                              
                                                
                                                    <div style="float: right; margin-right: 160px">
                                <asp:Label ID="lblresult" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsave" runat="server" ValidationGroup="a1" Text="SAVE" ToolTip="SAVE"
                                    OnClientClick='return confirm(" Are you sure you want to update  the record?");'
                                    class=" btn save" OnClick="btnsave_Click" />
                                <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="CANCEL" ToolTip="CANCEL"
                                    OnClientClick='return confirm(" Are you sure you want to cancel this entry?");'
                                    class=" btn save" OnClick="btncancel_Click" />
                            </div>
                            
                            <br/><br />
                                           
                                            
                                
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
