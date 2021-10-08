<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LeaveBalance_HistoryReport.aspx.cs" Inherits="Jawan.Portal.LeaveBalance_HistoryReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: Leave Balance & History Report</title>
    <link href="../css/global.css" rel="stylesheet" type="text/css" />
    <link href="../css/Calendar.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
        <style type="text/css">
         .style1 {
            width: 135px;
        }

        .completionList {
            background: white;
            border: 1px solid #DDD;
            border-radius: 3px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            min-width: 165px;
            height: 200px;
            overflow: auto;
        }

        .listItem {
            display: block;
            padding: 5px 5px;
            border-bottom: 1px solid #DDD;
        }

        .itemHighlighted {
            color: black;
            background-color: rgba(0, 0, 0, 0.1);
            text-decoration: none;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            border-bottom: 1px solid #DDD;
            display: block;
            padding: 5px 5px;
        }

        .visibility {
            visibility: hidden;
        }

        .auto-style1 {
            height: 23px;
        }

        .style2 {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }

        .chosen-container {
    width: 230px !important; /* or any value that fits your needs */
}
    </style>

        
    <script src="https://harvesthq.github.io/chosen/chosen.jquery.js" type="text/javascript"></script>
           
    <script type="text/javascript">

        jQuery(document).ready(function mchoose() {
            jQuery(".chosen").data("placeholder", "Select Frameworks...").chosen();
        });

   <%-- <script>
        $(document).ready(function () {
            $("#<%=ddlCombinewith.ClientID%>").select2({
                //placeholder: "--Select--",
                placeholder: {
                    id: '', // the value of the option
                    text: ''
                },
                minimumInputLength: 0,
                multiple: true,
                allowClear: false
            });
            $('#<%=ddlCombinewith.ClientID%>').on('change', function () {
                //alert($(this).val())
                $('#<%=hfSelected.ClientID%>').val($(this).val());
            });
        })--%>

    </script>
    <script type="text/javascript">
        function AssignExportHTML() {

            document.getElementById('hidGridView').value = htmlEscape(forExport.innerHTML);
        }
        function htmlEscape(str) {
            return String(str)
                .replace(/&/g, '&amp;')
                .replace(/"/g, '&quot;')
                .replace(/</g, '&lt;')
                .replace(/>/g, '&gt;');
        }


    </script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>



    <style type="text/css">
        #social div {
            display: block;
        }

        .HeaderStyle {
            text-align: Left;
        }

        .style3 {
            height: 24px;
        }

        .modalBackground {
            background-color: Gray;
            z-index: 10000;
        }

        .slidingDiv {
            background-color: #99CCFF;
            padding: 10px;
            margin-top: 10px;
            border-bottom: 5px solid #3399FF;
        }

        .show_hide {
            display: none;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
        }

        .custom-combobox-toggle {
            position: absolute;
            top: 0;
            bottom: 0;
            margin-left: -1px;
            padding: 0;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }
    </style>

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

        //function OnAutoCompleteDDLFoidchange(event, ui) {

        //    $('#ddlFOID').trigger('change');
        //}

    </script>
</head>
<body>
    <form id="Segment1" runat="server">
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
                        <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a></li>
                        <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"  class="current"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class=" after last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                        <ul>
                                            <li><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeesLink" runat="server"><span>Employees</span></a></li>
                                            <li  class="current"><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server"><span>Clients</span></a> </li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportsLink" runat="server"><span>Inventory</span></a> </li>
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
                        <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Leave Balance & History Report</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">Leave Balance & History Report
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div style="margin-left: 20px">

                                    <div style="float: right">
                                        <asp:LinkButton ID="lbtn_Export" runat="server" Text="Export to Excel" OnClick="lbtn_Export_Click" Visible="false"></asp:LinkButton>
                                    </div>

                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table width="100%" cellpadding="5" cellspacing="5">

                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lblReportType" Text="Select Report"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="form-control" Width="230px" AutoPostBack="true" OnSelectedIndexChanged="ddlReportType_SelectedIndexChanged">
                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                        <asp:ListItem>Leave Balance</asp:ListItem>
                                                        <asp:ListItem>Leave History</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                
                                                <td>
                                                    <asp:Label runat="server" ID="lblYear" Text=" Select Year" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlyears" runat="server" CssClass="form-control" Width="230px" Visible="false"></asp:DropDownList>
                                                </td>


                                            </tr>


                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lblEmpId" Text="Emp. ID" Visible="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmpId" runat="server" CssClass="form-control" AutoPostBack="True" Width="230px" OnSelectedIndexChanged="ddlEmpId_SelectedIndexChanged" Visible="true">
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblEmpName" Text="Emp. Name" Visible="true"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlEmpName" runat="server" CssClass="form-control" AutoPostBack="true" Width="230px" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" Visible="true">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lblDateType" Text="Period" Visible="false"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:DropDownList ID="ddlDateType" runat="server" CssClass="form-control" Width="230px" AutoPostBack="true" OnSelectedIndexChanged="ddlDateType_SelectedIndexChanged" Visible="false" Enabled="false">
                                                        <asp:ListItem>Year Wise</asp:ListItem>
                                                        <asp:ListItem>From-To</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>


<%--                                                <td>
                                                    <asp:Label runat="server" ID="lblYear" Text=" Select Year" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlyears" runat="server" CssClass="form-control" Width="230px" Visible="false"></asp:DropDownList>
                                                </td>--%>

                                            </tr>

                                            <tr>

                                                <td>
                                                    <asp:Label runat="server" ID="lblFromDate" Text="From Month  " Visible="false"></asp:Label>
                                                </td>

                                                <td>

                                                    <asp:TextBox ID="txtFromDate" CssClass="form-control"  runat="server" AutoPostBack="true" class="sinput"
                                                        Text="" Visible="false"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                        Enabled="true" Format="MMM-yyyy" TargetControlID="txtFromDate" DefaultView="Months" 
                                                        OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown"></cc1:CalendarExtender>

                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblToDate" Text="To Month" Visible="false"></asp:Label>
                                                </td>

                                                <td>

                                                    <asp:TextBox ID="txtToDate" CssClass="form-control" runat="server" AutoPostBack="true" class="sinput"
                                                        Text="" Visible="false"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar2"
                                                        Enabled="true" Format="MMM-yyyy" TargetControlID="txtToDate" DefaultView="Months" 
                                                        OnClientHidden="onCalendarHidden1"  OnClientShown="onCalendarShown1"></cc1:CalendarExtender>

                                                </td>


                                            </tr>

                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:Button ID="Btn_Search" runat="server" OnClick="Btn_Search_Click" Text="Search" Visible="true"
                                                        Style="float: right" class="btn save" />
                                                </td>
                                            </tr>

                                        </table>


                                    </div>
                                    <div class="rounded_corners" style="overflow-x: scroll; text-align: center; width: 95%; margin-left: 17px; margin-right: 17px; margin-bottom: 30px">
                                        <asp:GridView ID="GvLeaveBalance" runat="server" AutoGenerateColumns="True" Visible="false"
                                            Width="100%" CellPadding="4" CellSpacing="3" CssClass="table table-striped table-bordered table-condensed table-hover">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>

                                    <div class="rounded_corners" style="overflow-x: scroll; text-align: center; width: 95%; margin-left: 17px; margin-right: 17px; margin-bottom: 30px" ite>
                                        <asp:GridView ID="GvLeaveHistory" runat="server" AutoGenerateColumns="True" Visible="false"
                                            Width="100%" CellPadding="4" CellSpacing="3" CssClass="table table-striped table-bordered table-condensed table-hover">
                                            <Columns>
                                            </Columns>
                                        </asp:GridView>
                                    </div>


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

            <!-- CONTENT AREA END -->
        </div>
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


                jQuery(document).ready(function mchoose() {
                    jQuery(".chosen").data("placeholder", "Select").chosen();
                });

            });
        };


    </script>
        <!-- DASHBOARD CONTENT END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS              
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
