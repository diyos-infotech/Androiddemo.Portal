<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MaterialRequisitForm.aspx.cs" Inherits="Jawan.Portal.MaterialRequisitForm" %>s

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVENTORY: MRF</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdsize
        {
            height: 15px;
        }
        .style8
        {
            width: 335px;
            height: 29px;
        }
    </style>
</head>
<body>
    <form id="MaterialRequisitForm1" runat="server">
    <asp:ScriptManager runat="server" ID="Scriptmanager1">
    </asp:ScriptManager>
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Managment Software" title="Facility Managment Software" /></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span>
                    </a></li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a>
                    </li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>
                        Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span>
                    </a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span> </a>
                    </li>
                    <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span>
                    </a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
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
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                                    <%--    <div class="submenubeforegap">
                                        &nbsp;</div>
                                     <div class="submenuactions">
                                        &nbsp;</div>--%>
                                    <ul>
                                        <li><a href="clients.aspx" id="AddClientLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyClient.aspx" id="ModifyClientLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteClient.aspx" id="DeleteClientLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                        <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                        <li><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="AssigningClients.aspx" id="Operationlink" runat="server"><span>Operations</span></a></li>
                                          <li><a href="Clientbilling.aspx" id="BillingLink" runat="server"><span>
                                            Billing</span></a></li>
                                        <li class="current"><a href="MaterialRequisitForm.aspx" id="MRFLink" runat="server">
                                            <span>MRF</span></a></li>
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
                                Material Requistion Form
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; height: auto">
                            <div class="boxin">
                                <table width="100%" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td>
                                            Select Client<span style="color: Red">*</span>
                                        </td>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlClients" AutoPostBack="True" OnSelectedIndexChanged="ddlClients_SelectedIndexChanged"
                                                class="sdrop" Width="110">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Client Name<span style="color: Red">*</span>
                                        </td>
                                        <td colspan="2">
                                            <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="true" class="sdrop"
                                                OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged" Width="350">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            MRF Id
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtMRFId" Enabled="false" class="sinput" Width="60"></asp:TextBox>
                                        </td>
                                        <td width="140">
                                            Material Cost per month
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmcpm" runat="server" Enabled="false" class="sinput" Width="60"></asp:TextBox>
                                        </td>
                                        <td width="140">
                                            Machinary cost per month
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtmecpm" class="sinput" Enabled="false" Width="60"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div class="rounded_corners">
                                    <asp:GridView ID="gvmrf" runat="server" AutoGenerateColumns="False" Width="100%"
                                        ForeColor="#333333" Style="text-align: center" CellPadding="4" CellSpacing="3"
                                        GridLines="None" OnRowDataBound="gvmrf_RowDataBound1">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <Columns>
							<asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                <asp:Label  ID="lblsno" runat="server" ></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Id">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlitemid" runat="server" Width="145px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="DDL_GetItemName">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:DropDownList ID="ddlitemname" runat="server" Width="145px" AutoPostBack="true"
                                                        OnSelectedIndexChanged="DDL_GetItemId">
                                                    </asp:DropDownList>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Closing Stock" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:TextBox runat="server" ID="txtClosingStock" Enabled="false"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        TargetControlID="txtClosingStock" ValidChars="0123456789-">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <FooterTemplate>
                                                    <asp:TextBox runat="server" ID="txtClosingStock"></asp:TextBox>
                                                </FooterTemplate>
                                                <ItemStyle Width="40px"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Order Quantity" ItemStyle-Width="30px">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="Server" OnTextChanged="txtQuantity_ValueChanged"
                                                        AutoPostBack="true"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                        TargetControlID="txtQuantity" FilterMode="ValidChars" FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <ItemStyle Width="60px"></ItemStyle>
                                                <FooterTemplate>
                                                    <asp:TextBox ID="txtQuantity" runat="Server" Text=""></asp:TextBox>
                                                    <asp:LinkButton ID="linkinsert" runat="server" CommandName="Insert" Text="Insert"
                                                        ForeColor="White"></asp:LinkButton>
                                                </FooterTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Selling Price">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtsellingprice" runat="server" OnTextChanged="txtsellingprice_ValueChanged"
                                                        AutoPostBack="true">
                                                    </asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" Enabled="True"
                                                        TargetControlID="txtsellingprice" ValidChars="0123456789.">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Total Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txttotalamount" runat="server" Enabled="false"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                    <asp:Label ID="lblbuyingprice" runat="server" Text="Total:  " Style="margin-left: 68%"></asp:Label>
                                    <asp:Label ID="lblbtotal" runat="server" Text="0.00"></asp:Label>
                                    <asp:Label ID="lblsellingprice" runat="server" Text="Total:  " Style="margin-left: 9%"></asp:Label>
                                    <asp:Label ID="lblstotal" runat="server" Text="0.00"></asp:Label><br />
                                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblresult" runat="server" Text="" Visible="true"
                                        Style="color: Red">  </asp:Label>
                                   <div align="right"> <asp:Button ID="btnaddnewitem" runat="server" Text="ADD NEW ITEM" class=" btn save"
                                        OnClick="btnaddnewitem_Click" Width="100px" />
                                    <asp:Button runat="server" ID="Button2" Text="ADD" class="btn save" OnClick="Button2_Click"
                                        OnClientClick='return confirm(" Are you sure you  want to add this record ?");' />
                                        </div>
                                </div>
                            </div>
                            <div class="clear">
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
    </div>
    </form>
</body>
</html>
