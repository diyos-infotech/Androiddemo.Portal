<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FullandFinal.aspx.cs" Inherits="Jawan.Portal.FullandFinal" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: FULL AND FINAL</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />

    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />
    <script src="script/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="script/jscript.js"> </script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
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

         function GetEmpid() {
             $('#txtemplyid').autocomplete({
                 source: function (request, response) {


                     $.ajax({
                         url: 'Autocompletion.asmx/GetFormEmpIDs',
                         method: 'post',
                         contentType: 'application/json;charset=utf-8',
                         data: JSON.stringify({
                             term: request.term,
                         }),
                         datatype: 'json',
                         success: function (data) {
                             response(data.d);
                         },
                         error: function (err) {
                             alert(err);
                         }
                     });
                 },
                 minLength: 4,
                 select: function (event, ui) {

                     $("#txtemplyid").attr("data-Empid", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                 }
             });
         }

         function GetEmpName() {

             $('#txtFname').autocomplete({
                 source: function (request, response) {
                     $.ajax({

                         url: 'Autocompletion.asmx/GetFormEmpNames',
                         method: 'post',
                         contentType: 'application/json;charset=utf-8',
                         data: JSON.stringify({
                             term: request.term,
                         }),
                         datatype: 'json',
                         success: function (data) {
                             response(data.d);
                         },
                         error: function (err) {
                             alert(err);
                         }
                     });
                 },
                 minLength: 4,
                 select: function (event, ui) {
                     $("#txtFname").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpNamechange(event, ui);
                 }
             });

         }

         function OnAutoCompletetxtEmpidchange(event, ui) {
             $('#txtemplyid').trigger('change');

         }
         function OnAutoCompletetxtEmpNamechange(event, ui) {
             $('#txtFname').trigger('change');

         }

         $(document).ready(function () {

             GetEmpid();
             GetEmpName();

         });



         function onCalendarShown() {

             var cal = $find("calendar1");
             //Setting the default mode to month
             cal._switchMode("months", true);

             //Iterate every month Item and attach click event to it
             if (cal._monthsBody) {
                 for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                     var row = cal._monthsBody.rows[i];
                     for (var j = 0; j < row.cells.length; j++) {
                         Sys.UI.DomEvent.addHandler(row.cells[j].firstChild, "click", call);
                     }
                 }
             }
         }

         function onCalendarHidden() {
             var cal = $find("calendar1");
             //Iterate every month Item and remove click event from it
             if (cal._monthsBody) {
                 for (var i = 0; i < cal._monthsBody.rows.length; i++) {
                     var row = cal._monthsBody.rows[i];
                     for (var j = 0; j < row.cells.length; j++) {
                         Sys.UI.DomEvent.removeHandler(row.cells[j].firstChild, "click", call);
                     }
                 }
             }

         }
         </script>
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
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">FULL AND FINAL</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               FULL AND FINAL
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div class="dashboard_firsthalf" style="width: 100%">
                                    <table width="85%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td>
                                                Employee ID<span style="color: Red">*</span> </td>
                                              <td>  <%--<asp:DropDownList runat="server" AutoPostBack="true" ID="ddlEmployee" class="sdrop"
                                                    OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                                </asp:DropDownList>--%>

                                                  <asp:TextBox runat="server" ID="txtemplyid" class="form-control" AutoPostBack="true" OnTextChanged="txtemplyid_TextChanged" Width="200px"></asp:TextBox>
                                            </td>
                                            <td>
                                                Employee Name<span style="color: Red">*</span></td>
                                               <td> <%--<asp:DropDownList runat="server" AutoPostBack="true" ID="ddlempname" class="sdrop"
                                                    OnSelectedIndexChanged="ddlempname_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                                   <asp:TextBox runat="server" ID="txtFname" class="form-control" AutoPostBack="true" OnTextChanged="txtFname_TextChanged" Width="200px"></asp:TextBox>
                                            </td>
                                            
                                            <td>
                                                <asp:Button runat="server" ID="btnSearchFullandFinal" Text="Submit" class="btn save" OnClick="btnSearchFullandFinal_Click"
                                                    />
                                            </td>
                                             
                                        </tr>
                                        
                                        <tr>
                                            <td>Month
                                                </td>
                                                <td>

                                                    <asp:TextBox ID="txtmonth" AutoComplete="off" runat="server" Text="" class="sinput"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Txt_Month_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                        Enabled="true" Format="MMM-yyyy" TargetControlID="txtmonth" DefaultView="Months" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                                    </cc1:CalendarExtender>
                                                </td>
                                        </tr>
                                        
                                    </table>
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
