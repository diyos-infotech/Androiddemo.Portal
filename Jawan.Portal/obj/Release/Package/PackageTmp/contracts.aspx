<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="contracts.aspx.cs" Inherits="Jawan.Portal.contracts" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>CONTRACTS</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="script/jscript.js">
    </script>

    <script type="text/javascript">

        function dtval(d, e) {
            var pK = e ? e.which : window.event.keyCode;
            if (pK == 8) { d.value = substr(0, d.value.length - 1); return; }
            var dt = d.value;
            var da = dt.split('/');
            for (var a = 0; a < da.length; a++) { if (da[a] != +da[a]) da[a] = da[a].substr(0, da[a].length - 1); }
            if (da[0] > 31) { da[1] = da[0].substr(da[0].length - 1, 1); da[0] = '0' + da[0].substr(0, da[0].length - 1); }
            if (da[1] > 12) { da[2] = da[1].substr(da[1].length - 1, 1); da[1] = '0' + da[1].substr(0, da[1].length - 1); }
            if (da[2] > 9999) da[1] = da[2].substr(0, da[2].length - 1);
            dt = da.join('/');
            if (dt.length == 2 || dt.length == 5) dt += '/';
            d.value = dt;
        }
		
    </script>

</head>
<body>
    <form id="contracts1" runat="server">
    <asp:ScriptManager runat="server" ID="Scriptmanager1">
    </asp:ScriptManager>
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
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                    <%--     <div class="submenubeforegap">
                                        &nbsp;</div>
                                <div class="submenuactions">
                                        &nbsp;</div>  --%>
                                    <ul>
                                        
                                        <li class="current"><a href="contracts.aspx" id="ContractLink" runat="server"><span>
                                            Contracts</span></a></li>
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
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                <div align="center"> <asp:Label ID="lblMsg" runat="server" style="border-color: #f0c36d;background-color: #f9edbe;width:auto;font-weight:bold;color:#CC3300;"></asp:Label></div> 
              <div align="center"> <asp:Label ID="lblSuc" runat="server" style="border-color: #f0c36d;background-color: #f9edbe;width:auto;font-weight:bold;color:#000;"></asp:Label></div> 
                     <table style="margin-top:8px;margin-bottom:8px" width="100%">
                        <tr>
                            <td style="font-weight: bold;width:100px">
                                Client ID/Name :
                            </td>
                           <td style="width:190px">
                                &nbsp;<asp:TextBox ID="txtsearch" runat="server" class="sinput"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class=" btn save" OnClick="btnSearch_Click" />
                            </td>
                             <td align="right"><a href="AddContract.aspx" class=" btn save">Add New Contract</a></td>
                        </tr>
                    </table>
                    <div class="col-md-12">
                        <div class="panel panel-inverse">
                            <div class="panel-heading">
                                <h3 class="panel-title">
                                    Contract Details</h3>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="gvcontract" runat="server" CellPadding="2" ForeColor="Black"
                                    AutoGenerateColumns="False" Width="100%" BackColor="#f9f9f9" BorderColor="LightGray"
                                    BorderWidth="1px" AllowPaging="True" onrowdeleting="gvDetails_RowDeleting" OnPageIndexChanging="gvclient_PageIndexChanging">
                                    <RowStyle Height="30px" />
                                   <Columns>
                                    
                                      <asp:TemplateField HeaderText="Clientid">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientid" Text="<%# Bind('Clientid') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                      <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblclientname" Text="<%# Bind('clientname') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                       <asp:TemplateField HeaderText="Contract ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblcontractid" Text="<%# Bind('ContractId') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>      
                                            
                                             
                                            
                                        <asp:TemplateField HeaderText="Contract Start Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCstart" Text="<%# Bind('ContractStartDate') %>" ></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        <asp:TemplateField HeaderText="Contract End Date">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblCend" Text="<%# Bind('ContractEndDate') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                       
                                       
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="lbtn_Select" ImageUrl="~/css/assets/view.png" runat="server"
                                                    ToolTip="View" OnClick="lbtn_Select_Click"  />
                                                <asp:ImageButton ID="lbtn_Edit" ImageUrl="~/css/assets/edit.png" runat="server" OnClick="lbtn_Edit_Click" ToolTip="Edit" />
                                                <asp:ImageButton ID="linkdelete" CommandName="Delete"  ImageUrl="~/css/assets/delete.png" runat="server"
                                                    OnClientClick='return confirm("Do you want to delete this record?");' ToolTip="Delete" Visible="false" />
                                            </ItemTemplate>
                                            <ItemStyle Width="80px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                          </Columns>
                                    <FooterStyle BackColor="Tan" />
                                    <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                    <HeaderStyle BackColor="White" Font-Bold="True" Height="30px" />
                                    <AlternatingRowStyle BackColor="White" Height="30px" />
                                </asp:GridView>
                                 <asp:Label ID="lblresult" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- DASHBOARD CONTENT END -->
            <%-- </div> </div>--%>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
        </div>
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by WebWonders</div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
            <!-- CONTENT AREA END -->
        </div>
    </form>
</body>
</html>
