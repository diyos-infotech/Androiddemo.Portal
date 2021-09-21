<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ApproveMRF.aspx.cs" Inherits="Jawan.Portal.ApproveMRF" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVENTORY: APPROVE MRF</title>
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
    <form id="ApproveMRF1" runat="server">
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
                                    <%--   <div class="submenubeforegap">
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
                                        <li><a href="Clientbilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a></li>
                                        <li><a href="MaterialRequisitForm.aspx" id="MRFLink" runat="server"><span>MRF</span></a></li>
                                        <li class="current"><a href="ApproveMRF.aspx" id="ApproveMRFLink" runat="server"><span>
                                            ApproveMRF</span></a></li>
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
                                Approve MRF
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <div class="rounded_corners">
                                    <asp:GridView ID="gvMRF" runat="server" AutoGenerateColumns="False" Width="100%"
                                        EmptyDataRowStyle-BackColor="BlueViolet" EmptyDataRowStyle-BorderColor="Aquamarine"
                                        EmptyDataRowStyle-Font-Italic="true" EmptyDataText="No Records Found" CellPadding="4"
                                        ForeColor="#333333" Style="text-align: left" GridLines="None">
                                        <RowStyle BackColor="#EFF3FB" />
                                        <EmptyDataRowStyle BackColor="BlueViolet" BorderColor="Aquamarine" Font-Italic="True">
                                        </EmptyDataRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="MRF ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRFId" runat="server" Text='<%#Bind("MRFId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="ClientId" HeaderText="Client ID" HeaderStyle-Width="220px">
                                                <HeaderStyle Width="220px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Date" HeaderText="Date of MRF" HeaderStyle-Width="220px"
                                                DataFormatString="{0:dd/MM/yyyy}">
                                                <HeaderStyle Width="220px" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:BoundField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                                <table width="100%" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td width="100px">
                                            Select MRF ID<span style="color: Red">*</span>
                                        </td>
                                        <td width="200px">
                                            <asp:DropDownList runat="server" ID="ddlMRFs" AutoPostBack="True" OnSelectedIndexChanged="ddlMRFs_SelectedIndexChanged"
                                                class="sdrop">
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            Client ID
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtClient" Enabled="false" class="sinput"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            Client Name
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCname" runat="server" Enabled="false" class="sinput" Width="350px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <div style="margin-left: 17px">
                                    <h4>
                                        MRF Details</h4>
                                </div>
                                <%--<div>
                            <asp:GridView ID="gvMRFDetails" runat="server" AutoGeneraMRF Details</h3>
                            </div>--%>
                                <div class="rounded_corners">
                                    <asp:GridView ID="gvMRFDetails" runat="server" AutoGenerateColumns="False" Width="100%"
                                        EmptyDataRowStyle-BackColor="BlueViolet" EmptyDataRowStyle-BorderColor="Aquamarine"
                                        EmptyDataRowStyle-Font-Italic="true" EmptyDataText="No Records Found" CellPadding="4"
                                        CellSpacing="3" ForeColor="#333333" Style="text-align: left" GridLines="None"
                                        OnRowDataBound="gvMRFDetails_RowDataBound">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <EmptyDataRowStyle BackColor="BlueViolet" BorderColor="Aquamarine" Font-Italic="True">
                                        </EmptyDataRowStyle>
                                        <Columns>
                                            <asp:TemplateField HeaderText="MRF ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblMRFId" runat="server" Text='<%#Bind("MRFId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item ID">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemId" runat="server" Text='<%#Bind("ItemId")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="ClosingStock">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblclosingstock" runat="server" Text=""></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity Required">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                            <%--<asp:BoundField DataField="Quantity" HeaderText="Quantity Required" HeaderStyle-Width="220px">
                                    <HeaderStyle Width="220px" /></asp:BoundField>--%>
                                            <asp:TemplateField HeaderText="Approved Quantity">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lblMRFId" runat="server" Text='<%#Bind("MRFId")%>'></asp:Label>--%>
                                                    <asp:TextBox ID="txtApprovedQty" runat="server" Text="0"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                        TargetControlID="txtApprovedQty" FilterMode="ValidChars" FilterType="Numbers">
                                                    </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                                <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                <ItemStyle HorizontalAlign="Left" />
                                            </asp:TemplateField>
                                        </Columns>
                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                        <EditRowStyle BackColor="#2461BF" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </div>
                                <div style="margin-left: 17px">
                                    <asp:CheckBox ID="ShowStockList" Text="&nbsp;Show already sent Stock list" runat="server"
                                        Checked="false" AutoPostBack="True" OnCheckedChanged="ShowStockList_CheckedChanged" />
                                </div>
                                <asp:Panel ID="ShowStockPanel" runat="server">
                                    <div>
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    From Date :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                        TargetControlID="txtFromDate" Format="MM/dd/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    To Date :
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtToDate" runat="server"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtTo_CalendarExtender" runat="server" Enabled="true" TargetControlID="txtToDate"
                                                        Format="MM/dd/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnShowStockList" Text="Show Stock" runat="server" class="btn save"
                                                        OnClick="btnShowStockList_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <h3>
                                            List of Stock Already Sent</h3>
                                    </div>
                                    <div class="rounded_corners">
                                        <asp:GridView ID="gvShowStock" runat="server" AutoGenerateColumns="False" Width="100%"
                                            EmptyDataRowStyle-BackColor="BlueViolet" EmptyDataRowStyle-BorderColor="Aquamarine"
                                            EmptyDataRowStyle-Font-Italic="true" EmptyDataText="No Records Found" CellPadding="4"
                                            CellSpacing="3" ForeColor="#333333" Style="text-align: left" GridLines="None"
                                            OnRowDataBound="gvMRFDetails_RowDataBound">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <EmptyDataRowStyle BackColor="BlueViolet" BorderColor="Aquamarine" Font-Italic="True">
                                            </EmptyDataRowStyle>
                                            <Columns>
                                                <asp:TemplateField HeaderText="Item ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblItemId" runat="server" Text='<%#Bind("ItemId")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="ItemName" HeaderText="Item Name" HeaderStyle-Width="220px">
                                                    <HeaderStyle HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Stock Already Sent">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblQty" runat="server" Text='<%#Bind("Quantity")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Price(in Rs)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPrice" runat="server" Text='<%#Bind("Price")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Cost(in Rs)">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCost" runat="server" Text='<%#Bind("cost")%>'></asp:Label>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" HorizontalAlign="Left" />
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </asp:Panel>
                                <div align="right" style="margin-right: 15px">
                                    <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"> </asp:Label>
                                    <asp:Button runat="server" ID="Approve" Text="Approve" class="btn save" OnClick="Approve_Click"
                                        OnClientClick='return confirm(" Are you sure you  want to approve this record ?");' />
                                    <asp:Button runat="server" ID="Reject" Text="Reject" class="btn save" OnClientClick='return confirm(" Are you sure you  want to reject this record ?");'
                                        OnClick="Reject_Click" />
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
