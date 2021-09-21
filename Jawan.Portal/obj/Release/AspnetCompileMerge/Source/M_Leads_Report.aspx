<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_Leads_Report.aspx.cs" Inherits="Jawan.Portal.M_Leads_Report" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LEADS REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

</head>

<body>
    <form id="Actionlog" runat="server" autocomplete="off">
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
                        <li><a href="Reminders.aspx">Reminders</a></li>
                        <li>Welcome <b>
                            <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                        <li class="lang"><a href="Login.aspx">Logout</a></li>
                    </ul>
                </div>
                <!-- TOP INFO END -->
                <!-- MAIN MENU BEGIN -->
                <div id="mainmenu">
                    <ul>
                        <li class="first"><a href="Employees.aspx" id="Employeeslink" visible="false" runat="server" class="current">
                            <span>Leads</span></a></li>
                        <li class="after"><a href="Clients.aspx" id="ClientsLink" visible="false" runat="server"><span>Calendar</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" visible="false" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" visible="false" runat="server"><span>Inventory</span></a></li>
                        <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" visible="false" runat="server"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" visible="false" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" visible="false" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
                        <li class="last"><a href="M_Leads.aspx" id="LinkMarketting" runat="server"><span><span>Marketting</span></span></a></li>
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
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <%--    <div class="submenuactions">
                                        &nbsp;</div> --%>
                                        <ul>
                                            <li class="first"><a href="M_Leads.aspx" id="A1" runat="server" class="current">
                                                <span>Leads</span></a></li>
                                            <li class="after"><a href="M_All_Action_Log_Scheduler.aspx" id="A2" runat="server"><span>Calendar</span></a></li>
                                            <li class="after"><a href="MKTG_Reports.aspx" id="A3" runat="server"><span>MKTG Reports</span></a></li>
                                        </ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <!-- HEADER SECTION END -->
        <!-- CONTENT AREA BEGIN -->
        <div id="content-holder">
            <div class="content-holder" style="background-color: #f2f2f2">

                <asp:ScriptManager runat="server" ID="Scriptmanager1">
                </asp:ScriptManager>

                <%--  <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>--%>

                <div class="container">

                    <table width="100%" style="margin-top: -10px; margin-bottom: 10px">
                        <tr>
                            <td align="right"><< <a href="M_Leads.aspx" style="color: #003366">Back</a>  </td>
                        </tr>
                    </table>


                    <table class="header" style="margin-bottom: 20px">
                        <tr style="font-weight: bold; height: 25px; font-size: 17px; text-align: center">
                            <td>LEADS REPORT
                            </td>
                        </tr>
                    </table>

                    <table cellpadding="5" cellspacing="5" width="65%" style="margin-left: 45px">
                        <tr>
                            <td style="width: 95px">Type</td>
                            <td>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="form-control">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem>Created Date</asp:ListItem>
                                    <asp:ListItem>Lead Status</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn save" OnClick="btnSubmit_Click" />
                            </td>
                            <td>
                                <asp:LinkButton ID="LnkExcel" runat="server" Text="Export to Excel" Visible="false" OnClick="LnkExcel_Click"></asp:LinkButton>
                            </td>
                        </tr>
                    </table>



                    <table cellpadding="5" cellspacing="5" width="90%" style="margin: 0px auto">

                        <tr id="row1" runat="server">
                            <td>
                                <asp:Label ID="lblfromdate" runat="server" Text="From Date"></asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtFromDate" runat="server" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CE_From_Date" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender ID="FTBE_From_Date" runat="server" Enabled="True" TargetControlID="txtFromDate"
                                    ValidChars="/0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                            <td>
                                <asp:Label ID="lbltodate" runat="server" Text="To Date"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtToDate" runat="server" class="form-control"></asp:TextBox>
                                <cc1:CalendarExtender ID="CE_To_Date" runat="server" Enabled="True" TargetControlID="txtToDate"
                                    Format="dd/MM/yyyy">
                                </cc1:CalendarExtender>
                                <cc1:FilteredTextBoxExtender ID="FTBE_To_Date" runat="server" Enabled="True" TargetControlID="txtToDate"
                                    ValidChars="/0123456789">
                                </cc1:FilteredTextBoxExtender>
                            </td>
                        </tr>

                    </table>

                    <div style="margin-top: 10px">
                        <asp:GridView ID="GVLeadDetails" runat="server" CssClass="table table-striped table-bordered table-condensed table-hover" AutoGenerateColumns="true">
                            <Columns>
                            </Columns>
                        </asp:GridView>
                    </div>


                </div>

                <%--  </ContentTemplate>
                </asp:UpdatePanel>--%>

                <!-- DASHBOARD CONTENT END -->
            </div>
        </div>
        <!-- CONTENT AREA END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
                </div>
                <!--    <div class="footerlogo">&nbsp;</div> -->
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | &copy;
                <asp:Label ID="lblcname" runat="server"></asp:Label>.
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </form>
    <script type="text/javascript">
        Sys.Browser.WebKit = {};
        if (navigator.userAgent.indexOf('WebKit/') > -1) {
            Sys.Browser.agent = Sys.Browser.WebKit;
            Sys.Browser.version = parseFloat(navigator.userAgent.match(/WebKit\/(\d+(\.\d+)?)/)[1]);
            Sys.Browser.name = 'WebKit';
        }

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function () {


            });
        };
    </script>
</body>
</html>
