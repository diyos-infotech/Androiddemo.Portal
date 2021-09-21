<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TaxComponents.aspx.cs" Inherits="Jawan.Portal.TaxComponents" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Master Page</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2 {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="EmployeeSalaryReports1" runat="server">
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
                        <%--<li><a href="Reminders.aspx">Reminders</a></li>--%>
                        <li>Welcome <b>
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
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                        <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>

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
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">
                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>
                                    


                                    <%--  <asp:HiddenField ID="hidGridView" runat="server" />--%>
                                    <asp:GridView ID="GVTaxComponents" runat="server" AutoGenerateColumns="False"
                                        EmptyDataText="No Records Found" Width="80%" CssClass="table table-striped table-bordered table-condensed table-hover"
                                        CellPadding="4" CellSpacing="3" ForeColor="#333333" GridLines="None" OnRowEditing="GVTaxComponents_RowEditing" OnRowCancelingEdit="GVTaxComponents_RowCancelingEdit" OnRowUpdating="GVTaxComponents_RowUpdating" style="margin:0px auto">

                                        <Columns>

                                            <%-- 0--%>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <HeaderStyle Width="5px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <%-- 1--%>
                                            <asp:TemplateField HeaderText="Component" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                                                <HeaderStyle Width="15px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblComponent" runat="server" Text='<%#Bind("TaxCmpName") %>'></asp:Label>
                                                    <asp:Label ID="lblCmpID" runat="server" Text='<%#Bind("TaxCmpID") %>' Visible="false"></asp:Label>

                                                </ItemTemplate>
                                                 <EditItemTemplate>
                                                    <asp:TextBox ID="txtComponent" runat="server" Text='<%#Bind("TaxCmpName") %>' class="sinput" Style="text-align:center" ></asp:TextBox>
                                                    <asp:Label ID="lblEditCmpID" runat="server" Text='<%#Bind("TaxCmpID") %>' Visible="false"></asp:Label>
                                                </EditItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField HeaderText="Percent" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="180px">
                                                <HeaderStyle Width="15px" />
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCmpper" runat="server" Text='<%#Bind("TaxCmpPer") %>'></asp:Label>
                                                </ItemTemplate>
                                                 <EditItemTemplate>
                                                    <asp:TextBox ID="txtCmpper" runat="server" Text='<%#Bind("TaxCmpPer") %>' class="sinput" Style="text-align:center" ></asp:TextBox>
                                                     <cc1:FilteredTextBoxExtender ID="FTBCmpPer" runat="server" Enabled="True"
                                                        TargetControlID="txtCmpper" ValidChars=".0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </EditItemTemplate>
                                            </asp:TemplateField>

                                            <%-- 2 --%>

                                            <asp:TemplateField HeaderText="Visibility" ItemStyle-HorizontalAlign="center" ItemStyle-Width="20px">
                                                <HeaderStyle Width="15px" />
                                                <ItemTemplate>
                                                            <asp:CheckBox ID="ChkVisibility" runat="server"  Enabled="false" Checked='<%#Convert.ToBoolean(Eval("visibility")) %>'/>  
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                         <asp:CheckBox ID="ChkEditVisibility" runat="server"  Enabled="true" Checked='<%#Convert.ToBoolean(Eval("visibility")) %>'/>  
                                                </EditItemTemplate>
                                            </asp:TemplateField>


                                            <asp:TemplateField  HeaderText=""  HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"  HeaderStyle-Width="150px">
                                                <ItemTemplate>
                                                <asp:LinkButton ID="linkedit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                </ItemTemplate>

                                                <EditItemTemplate>
                                                <asp:LinkButton ID="linkupdate" runat="server" CommandName="update" Text="Update" 
                                                OnClientClick='return confirm(" Are you sure you want to update?");' style="color:Black"></asp:LinkButton>
                                                
                                                    <asp:LinkButton ID="linkcancel" runat="server" CommandName="cancel" Text="Cancel" 
                                                OnClientClick='return confirm(" Are you sure you want to cancel this entry ?");' style="color:Black">
                                                </asp:LinkButton>

                                            </EditItemTemplate>
                                            </asp:TemplateField>
                                            

                                        </Columns>
                                    </asp:GridView>

                                  


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
                        <a href="http://www.diyostech.com" target="_blank">Powered by DIYOS </a>
                    </div>
                    <!--    <div class="footerlogo">&nbsp;</div> -->
                    <div class="footercontent">
                        <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
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


