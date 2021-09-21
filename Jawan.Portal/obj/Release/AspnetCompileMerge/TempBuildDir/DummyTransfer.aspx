<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DummyTransfer.aspx.cs" Inherits="Jawan.Portal.DummyTransfer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>DUMMY TRANSFER</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />

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
    <form id="DummyTransfer1" runat="server">
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
                    <li><a href="Reminders.aspx">Reminders</a></li><li>Welcome <b>
                        <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                    <li class="lang"><a href="Login.aspx">Logout</a></li>
                </ul>
            </div>
            <!-- TOP INFO END -->
            <!-- MAIN MENU BEGIN -->
            <div id="mainmenu">
                <ul>
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" class="current">
                        <span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <%--  <li ><a href="clientattendance.aspx" id="clientattendancelink" runat="server"  > <span>Entries</span></a></li>--%>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
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
                                    <%-- <div class="submenubeforegap">
                                        &nbsp;</div>--%>
                                    <ul>
                                        <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="Entrieslonaslink" runat="server"><span>Loans</span></a>
                                        </li>
                                        <li><a href="EmployeePayments.aspx" id="Entriespaymentslink" runat="server"><span>Payments</span></a>
                                        </li>
                                        <li class="current"><a href="PostingOrderList.aspx" id="entriesEmployeeslink" runat="server">
                                            <span>Transfers</span></a> </li>
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
        <div class="content-holder" style="height: auto">
            <h1 class="dashboard_heading">
                Transfers Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Dummy Transfer
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">
                                <ul>
                        <li class="left leftmenu">
                            <ul>
                                <li><a href="PostingOrderList.aspx" id="PostingOrderLink" runat="server">Posting Order</a></li>
                                <li><a href="TemproryEmployeeTransferList.aspx" id="TempTransferLink" runat="server">
                                    Temporary Transfer</a></li>
                                <li><a href="DummyTransfer.aspx" class="sel" id="DummyTransferLink" runat="server">Dummy
                                    Transfer</a></li>
                                <li><a href="RemoveTransfers.aspx" id="transferlink" runat="server">Remove Transfers</a></li>
                            </ul>
                        </li>
                        <li class="right">
                           
                                <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                   
                                    <tr>
                                        <td>
                                            <asp:ScriptManager runat="server" ID="Scriptmanager1">
                                            </asp:ScriptManager>
                                            <!--  Content to be add here> -->
                                           
                                                <table width="100%">
                                                    <tr>
                                                        <td valign="top">
                                                            <table width="100%" border="0" class="FormContainer" cellpadding="5" cellspacing="5">
                                                                <tr>
                                                                    <td>
                                                                        Unit ID<span style="color: Red">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlUnit" class="sdrop" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlUnit_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Emp Id<span style="color: Red">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlempid" class="sdrop" runat="server" AutoPostBack="True"
                                                                            OnSelectedIndexChanged="ddlempid_SelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Order Date<span style="color: Red">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtorderdate" TabIndex="9" runat="server" class="sinput" MaxLength="10"
                                                                            onkeyup="dtval(this,event)"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CEorderdate" runat="server" Enabled="true" TargetControlID="txtorderdate"
                                                                            Format="dd/MM/yyyy">
                                                                        </cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEorderdate" runat="server" Enabled="True" TargetControlID="txtorderdate"
                                                                            ValidChars="/0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        Joining Date<span style="color: Red">*</span>
                                                                    </td>
                                                                    <td width="60%">
                                                                        <asp:TextBox ID="txtjoindate" TabIndex="9" runat="server" class="sinput" MaxLength="10"
                                                                            onkeyup="dtval(this,event)"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CEjoindate" runat="server" Enabled="true" TargetControlID="txtjoindate"
                                                                            Format="dd/MM/yyyy">
                                                                        </cc1:CalendarExtender>
                                                                        <cc1:FilteredTextBoxExtender ID="FTBEjoindate" runat="server" Enabled="True" TargetControlID="txtjoindate"
                                                                            ValidChars="/0123456789">
                                                                        </cc1:FilteredTextBoxExtender>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        PF
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkpf" runat="server" Checked="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        ESI
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkesi" runat="server" Checked="true" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            <table width="100%" border="0" class="FormContainer" cellpadding="5" cellspacing="5">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblClientname" runat="server" Text="Name" Width="40%"> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlcname" runat="server" class="sdrop" AutoPostBack="true"
                                                                            OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <tr>
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
                                                                    <td width="30%">
                                                                        Order Id :
                                                                    </td>
                                                                    <td width="60%">
                                                                        <asp:TextBox ID="txtorderid" class="sinput" runat="server" Enabled="False"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Label ID="lblDesignation" runat="server" Text="Emp Desig"> </asp:Label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:DropDownList ID="ddlDesignation" runat="server" class="sdrop" AutoPostBack="true">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                </tr>
                                                                <%--<tr>
                                <td width="30%"> Previous Unit Id :</td>
                                <td width="60%"> 
                                   <asp:TextBox ID="txtPrevUnitId" Width="50%" runat="server" Enabled="False" ></asp:TextBox>
                                </td>
                             </tr>
                             <tr>
                                <td width="30%"> Reliving Date :</td>
                                <td width="60%">
                                    <asp:TextBox ID="txtrelivingdate" Width="50%" runat="server" ></asp:TextBox>
                                    <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                         TargetControlID="txtrelivingdate" Format=" MM/dd/yyyy">
                                    </cc1:CalendarExtender>
                                </td>
                             </tr>--%>
                                                                <tr>
                                                                    <td>
                                                                        Remarks :
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtremarks" runat="server" class="sinput" TextMode="MultiLine"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        PT
                                                                    </td>
                                                                    <td>
                                                                        <asp:CheckBox ID="chkpt" runat="server" Checked="true" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button ID="btntransfer" runat="server" Visible="true" Text="Transfer" class="btn save"
                                                                            OnClick="btntransfer_Click" OnClientClick='return confirm("Are you sure you want  to transfer this employee?");' />
                                                                        <asp:Label ID="MessageLabel" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                           
                                        </td>
                                    </tr>
                                </table>
                         
                        </li>
                    </ul>
                 
                        <table width="100%" border="0">
                            <tr>
                                <td width="98%" class="FormSectionHead">
                                    List of Employees working at selected Client
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <div class="rounded_corners">
                                        <asp:GridView ID="gvemppostingorder" runat="server" AutoGenerateColumns="False" Style="text-align: center;
                                            font-size: 13px" Width="98%" Height="80%" CellPadding="4" CellSpacing="3" ForeColor="#333333"
                                            GridLines="None">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
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
                                            
                                                <asp:BoundField DataField="empid" HeaderText="Emp Id" ItemStyle-Width="10%">
                                                   
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Name" HeaderText="Emp Name" ItemStyle-Width="25%">
                                                   
                                                </asp:BoundField>
                                                <asp:TemplateField HeaderText="Designation" ItemStyle-Width="15%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="ddldesignid" runat="server" Text='<%#Bind("Designid")%>' Visible="false"></asp:Label>
                                                        <asp:Label ID="ddldesignname" runat="server" Text='<%#Bind("Desgn")%>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Working due to " ItemStyle-Width="20%">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltransfertype" runat="server" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PF">
                                                    <ItemTemplate>
                                                        <%# (Boolean.Parse(Eval("pf").ToString())) ? "Yes" : "No" %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ESI">
                                                    <ItemTemplate>
                                                        <%# (Boolean.Parse(Eval("ESI").ToString())) ? "Yes" : "No"%></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="PT">
                                                    <ItemTemplate>
                                                        <%# (Boolean.Parse(Eval("PT").ToString())) ? "Yes" : "No"%></ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30"  />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        
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
                            <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
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
            </div>
            <!-- CONTENT AREA END -->
    </form>
</body>
</html>
