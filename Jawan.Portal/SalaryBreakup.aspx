<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SalaryBreakup.aspx.cs" Inherits="Jawan.Portal.SalaryBreakup" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SALARY BREAKUP</title>
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
        <div class="content-holder">
            <div id="breadcrumb">
                <ul class="crumbs">
                    <li class="first"><a href="Settings.aspx" style="z-index: 9;"><span></span>Settings</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Salary Breakup Details</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Salary Breakup Details
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                            
                              
                                                <table width="100%" cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            <table cellpadding="5" cellspacing="5">
                                                                <tr>
                                                                    <td>
                                                                        Select Designation
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList runat="server" ID="ddlDesignations" AutoPostBack="True" class="sdrop"
                                                                            OnSelectedIndexChanged="ddlDesignations_SelectedIndexChanged" TabIndex="0">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Basic :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtBasic" TabIndex="1" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtBasic"
                                                                            ErrorMessage="Please Enter Basic in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        HRA :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtHRA" TabIndex="3" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtHRA"
                                                                            ErrorMessage="Please Enter HRA in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Washing Allowance :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtWashAllo" TabIndex="5" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtWashAllo"
                                                                            ErrorMessage="Please Enter Wasing Allowance in Numbers" Style="z-index: 101;
                                                                            left: 450px; position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Conveyance :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtConceyance" TabIndex="7" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtConceyance"
                                                                            ErrorMessage="Please Enter Conceyance in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <table cellpadding="5" cellspacing="5">
                                                                <tr>
                                                                    <td>
                                                                        CTC :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtCTC" TabIndex="9" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtCTC"
                                                                            ErrorMessage="Please Enter CTC in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        DA :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtDA" TabIndex="2" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDA"
                                                                            ErrorMessage="Please Enter DA in Numbers" Style="z-index: 101; left: 450px; position: absolute;
                                                                            top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        CCA :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtCCA" TabIndex="4" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtCCA"
                                                                            ErrorMessage="Please Enter CCA in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Other Allowances :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtOtherAllo" TabIndex="6" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtOtherAllo"
                                                                            ErrorMessage="Please Enter Other Allowance in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Bonus :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox runat="server" ID="txtBonus" TabIndex="8" class="sinput"></asp:TextBox>
                                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtBonus"
                                                                            ErrorMessage="Please Enter Bonus in Numbers" Style="z-index: 101; left: 450px;
                                                                            position: absolute; top: 550px" ValidationExpression="^\d+$" ValidationGroup="check"></asp:RegularExpressionValidator>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                                
                                                
                                                    <div style="float: right; margin-right: 160px">
                                <asp:Label ID="lblresult" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>
                                <asp:Button ID="btnsave" runat="server" ValidationGroup="a1" Text="SAVE" ToolTip="SAVE"
                                    OnClientClick='return confirm(" Are you sure you want to update  the record?");'
                                    class=" btn save" OnClick="btnsave_Click" />
                                <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="CANCEL" ToolTip="CANCEL"
                                    OnClientClick='return confirm(" Are you sure you want to cancel this entry?");'
                                    class=" btn save" OnClick="btncancel_Click" />
                            </div><br/><br />
                                            <div class="rounded_corners">
                                                <asp:GridView ID="gvSalaryBreakup" runat="server" Width="100%" AutoGenerateColumns="False"
                                                    Height="50%" Style="text-align: center" CellPadding="5" CellSpacing="3" ForeColor="#333333"
                                                    GridLines="None" AllowPaging="True" OnRowDeleting="gvcreatelogin_RowDeleting"
                                                    OnPageIndexChanging="gvSalaryBreakup_PageIndexChanging">
                                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Designation">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblempid" runat="server" Text=" <%#Bind('Design')%>"></asp:Label>
                                                            </ItemTemplate>
                                                            <ItemStyle Width="40px"></ItemStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Basic" HeaderText="Basic" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="DA" HeaderText="DA" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="HRA" HeaderText="HRA" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CCA" HeaderText="CCA" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="WashAllowance" HeaderText="Washing Allowance" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="OtherAllowance" HeaderText="Other Allowance" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Conveyance" HeaderText="Conveyance" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="Bonus" HeaderText="Bonus" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="CTC" HeaderText="CTC" HeaderStyle-Width="220px">
                                                            <HeaderStyle Width="220px" />
                                                        </asp:BoundField>
                                                        <%--  <asp:TemplateField ItemStyle-Width="40px">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkdelete" runat="server" CommandName="delete" Text="Delete"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle Width="40px"></ItemStyle>
                                                </asp:TemplateField>--%>
                                                    </Columns>
                                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                    <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" BorderWidth="1px" CssClass="GridPager" />
                                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
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
