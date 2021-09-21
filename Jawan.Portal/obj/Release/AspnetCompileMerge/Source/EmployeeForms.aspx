<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeForms.aspx.cs" Inherits="Jawan.Portal.EmployeeForms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: Employee Forms</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2
        {
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
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                    <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>
                        Settings</span></a></li>
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
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li class="current"><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"
                                            runat="server"><span>Employees</span></a></li>
                                        <li><a href="ClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>
                                            Inventory</span></a></li>
                                        <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"><span>Companyinfo</span></a>
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
                    <li class="first"><a href="#" style="z-index: 9;"><span></span>Reports</a></li>
                    <li><a href="Reports.aspx" style="z-index: 8;">Employee Reports</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Employee Forms</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                              Employee Forms
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div class="dashboard_firsthalf" style="width: 100%">
                                    <table width="120%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td>
                                            Forms</td>
                                             <td>  <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlForms" class="sdrop"
                                                    OnSelectedIndexChanged="ddlForms_SelectedIndexChanged">
                                                 <asp:ListItem>--Select--</asp:ListItem>
                                                 <asp:ListItem>Form Q</asp:ListItem>
                                                 <asp:ListItem>Form F (Leave Wages)</asp:ListItem>
                                                 <asp:ListItem>Form F (Gratuity)</asp:ListItem>
                                                 <asp:ListItem>Form A</asp:ListItem>
                                                 <asp:ListItem>Form 5</asp:ListItem>
                                                 <asp:ListItem>Form 13</asp:ListItem>
                                                 <asp:ListItem>Declaration</asp:ListItem>
                                                 <asp:ListItem>Form-3A</asp:ListItem>
                                               

                                                </asp:DropDownList>
                                            </td>


                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="Lblempid" runat="server" Text="Employee ID " Visible="false"></asp:Label> </td>
                                              <td>  <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlEmployee" class="sdrop" Visible="false"
                                                    OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                               <asp:Label ID="lblempname" runat="server" Text=" Employee Name " Visible="false"></asp:Label></td>
                                               <td> <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlempname" class="sdrop" Visible="false"
                                                    OnSelectedIndexChanged="ddlempname_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                           
                                             </tr>
                                             <tr >
                                                        <td style="width: 100px">
                                                            <asp:Label ID="lblfrom" runat="server" Text="From" Visible="false"></asp:Label>
                                                        </td>

                                                        <td>
                                                            <asp:TextBox ID="txtfrom" runat="server" CssClass="sinput" Visible="false"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                                Enabled="true" Format="MMM-yyyy" TargetControlID="txtfrom">
                                                            </cc1:CalendarExtender>
                                                        </td>

                                                       
                                                        <td style="width: 100px">
                                                            <asp:Label ID="lblto" runat="server" Text="To" Visible="false"></asp:Label>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtto" runat="server" CssClass="sinput" Visible="false"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="txtto_CalendarExtender" runat="server" BehaviorID="calendar2"
                                                                Enabled="true" Format="MMM-yyyy" TargetControlID="txtto">
                                                            </cc1:CalendarExtender>
                                                        </td>
                                                    </tr>
                                        <tr>
                                             <td>
                                                            <asp:Label ID="lblmonth" runat="server" Text="Month" Visible="false"></asp:Label>

                                                </td>
                                         <td>
                                                    <asp:TextBox ID="TxtMonth" Width="120px" runat="server" AutoPostBack="true" class="sinput"
                                                        Text="" Visible="false"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="TxtMonth_CalendarExtender" runat="server"
                                                        Enabled="true" Format="dd/MM/yyyy" TargetControlID="TxtMonth">
                                                    </cc1:CalendarExtender>

                                                </td>

                                             <td>
                                                    <asp:Label ID="lblDOJ" runat="server" Text="  Date Of Joining" Visible="false" ></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpDtofJoining" runat="server" Text="" class="sinput" Visible="false" ></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                        TargetControlID="txtEmpDtofJoining" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FTBEDOI1" runat="server" Enabled="True" TargetControlID="txtEmpDtofJoining"
                                                        ValidChars="/0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>


                                              <td>
                                                    <asp:Label ID="lblDOL" runat="server" Text="  Date Of Leaving" Visible="false" Style="margin-left: -124px"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpDtofLeaveing" runat="server" Text="" class="sinput" Visible="false" Style="margin-left: -377px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                        TargetControlID="txtEmpDtofJoining" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FTBEDOL1" runat="server" Enabled="True" TargetControlID="txtEmpDtofLeaveing"
                                                        ValidChars="/0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                             
                                       </tr>
                                        <tr>
                                            <td>
                                                
                                            </td>
                                           
                                        </tr>
                                    </table>
                                    <asp:Button runat="server" ID="BtnSubmit" Text="Submit" class="btn save" OnClick="btnForms_Click" style="float:right;margin-right:90px"
                                                    />
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
