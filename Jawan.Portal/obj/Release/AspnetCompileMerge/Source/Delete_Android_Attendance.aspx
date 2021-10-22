<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Delete_Android_Attendance.aspx.cs" Inherits="Jawan.Portal.Delete_Android_Attendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: ANDROID ATTENDANCE REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />

    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />
    <script src="script/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="script/jscript.js"> </script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
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

        function setProperty() {
            $.widget("custom.combobox", {
                _create: function () {
                    this.wrapper = $("<span>")
                        .addClass("custom-combobox")
                        .insertAfter(this.element);

                    this.element.hide();
                    this._createAutocomplete();
                    this._createShowAllButton();
                },

                _createAutocomplete: function () {
                    var selected = this.element.children(":selected"),
                        value = selected.val() ? selected.text() : "";

                    this.input = $("<input>")
                        .appendTo(this.wrapper)
                        .val(value)
                        .attr("title", "")
                        .addClass("custom-combobox-input ui-widget ui-widget-content ui-state-default ui-corner-left")
                        .autocomplete({
                            delay: 0,
                            minLength: 0,
                            source: $.proxy(this, "_source")
                        })
                        .tooltip({
                            classes: {
                                "ui-tooltip": "ui-state-highlight"
                            }
                        });

                    this._on(this.input, {
                        autocompleteselect: function (event, ui) {
                            ui.item.option.selected = true;
                            this._trigger("select", event, {
                                item: ui.item.option
                            });
                        },

                        autocompletechange: "_removeIfInvalid"
                    });
                },

                _createShowAllButton: function () {
                    var input = this.input,
                        wasOpen = false;

                    $("<a>")
                        .attr("tabIndex", -1)
                        .attr("title", "Show All Items")
                        .tooltip()
                        .appendTo(this.wrapper)
                        .button({
                            icons: {
                                primary: "ui-icon-triangle-1-s"
                            },
                            text: false
                        })
                        .removeClass("ui-corner-all")
                        .addClass("custom-combobox-toggle ui-corner-right")
                        .on("mousedown", function () {
                            wasOpen = input.autocomplete("widget").is(":visible");
                        })
                        .on("click", function () {
                            input.trigger("focus");

                            // Close if already visible
                            if (wasOpen) {
                                return;
                            }

                            // Pass empty string as value to search for, displaying all results
                            input.autocomplete("search", "");
                        });
                },

                _source: function (request, response) {
                    var matcher = new RegExp($.ui.autocomplete.escapeRegex(request.term), "i");
                    response(this.element.children("option").map(function () {
                        var text = $(this).text();
                        if (this.value && (!request.term || matcher.test(text)))
                            return {
                                label: text,
                                value: text,
                                option: this
                            };
                    }));
                },

                _removeIfInvalid: function (event, ui) {

                    // Selected an item, nothing to do
                    if (ui.item) {
                        return;
                    }

                    // Search for a match (case-insensitive)
                    var value = this.input.val(),
                        valueLowerCase = value.toLowerCase(),
                        valid = false;
                    this.element.children("option").each(function () {
                        if ($(this).text().toLowerCase() === valueLowerCase) {
                            this.selected = valid = true;
                            return false;
                        }
                    });

                    // Found a match, nothing to do
                    if (valid) {
                        return;
                    }

                    // Remove invalid value
                    this.input
                        .val("")
                        .attr("title", value + " didn't match any item")
                        .tooltip("open");
                    this.element.val("");
                    this._delay(function () {
                        this.input.tooltip("close").attr("title", "");
                    }, 2500);
                    this.input.autocomplete("instance").term = "";
                },

                _destroy: function () {
                    this.wrapper.remove();
                    this.element.show();
                }
            });
            $(".ddlautocomplete").combobox({
                select: function (event, ui) { $("#ddlClientID").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                select: function (event, ui) { $("#ddlCName").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientnamechange(event, ui); },
                //select: function (event, ui) { $("#ddlFOID").attr("data-clientId", ui.item.value); OnAutoCompleteDDLFoidchange(event, ui); },

                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLClientidchange(event, ui) {
            $('#ddlClientID').trigger('change');

        }

        function OnAutoCompleteDDLClientnamechange(event, ui) {

            $('#ddlCName').trigger('change');
        }
        //function OnAutoCompleteDDLFoidchange(event, ui) {

        //    $('#ddlFOID').trigger('change');
        //}

                function call(eventElement) {
            var target = eventElement.target;
            switch (target.mode) {
                case "month":
                    var cal = $find("calendar1");
                    cal._visibleDate = target.date;
                    cal.set_selectedDate(target.date);
                    cal._switchMonth(target.date);
                    cal._blur.post(true);
                    cal.raiseDateSelectionChanged();
                    break;
            }
        }
        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            //Get the reference of GridView
            var GridView = row.parentNode;
            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];
                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        inputList[i].checked = false;
                    }
                }
            }
        }

    </script>
</head>
<body>
    <form id="ClentwiseEmployeesSalaryreports1" runat="server">
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
                        <%--<li><a href="Reminders.aspx">Reminders</a></li>--%><li>Welcome <b>
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
                        <li class="after"><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                    </ul>
                </div>
                <!-- MAIN MENU SECTION END -->
            </div>
            <!-- LOGO AND MAIN MENU SECTION END -->
            <!-- SUB NAVIGATION SECTION BEGIN -->
            <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
            <div id="submenu" style="width: 100%">
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
                                            <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>Employees</span></a></li>
                                            <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>Inventory</span></a></li>
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
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Android Attendance </a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Android Attendance 
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>



                                    <div class="dashboard_firsthalf" style="width: 100%">

                                        <div style="margin-right: 10px; float: right">
                                            <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" Visible="False" OnClientClick="AssignExportHTML()">Export to Excel</asp:LinkButton>
                                        </div>

                                        <table width="100%" cellpadding="5" cellspacing="5">

                                            <tr style="height: 32px">
                                                <td>Option
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddlOption" Width="125px" class="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddltypes_SelectedIndexChanged">
                                                        <asp:ListItem>Client Wise</asp:ListItem>
                                                        <asp:ListItem>FO Wise</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                                <td>Type
                                                </td>
                                                <td>
                                                    <asp:DropDownList runat="server" ID="ddltypes" Width="125px" AutoPostBack="true" class="form-control" OnSelectedIndexChanged="ddltypes_SelectedIndexChanged">
                                                        <asp:ListItem>Day Wise</asp:ListItem>
                                                        <asp:ListItem>Month Wise</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>


                                            </tr>


                                            <tr style="height: 32px">



                                                <td>
                                                    <asp:Label runat="server" ID="lblclientid" Text="Client ID"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlClientID" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlClientID_SelectedIndexChanged"
                                                        Width="120px">
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="lblclientname" Text="Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlCName" runat="server" placeholder="select" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>Month
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtmonth" AutoPostBack="true" OnTextChanged="txtmonth_TextChanged" runat="server" Text="" AutoComplete="off" class="form-control" Width="120px"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                        TargetControlID="txtmonth" Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtmonth"
                                                        ValidChars="/0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>

                                                <td>
                                                    <asp:Button runat="server" ID="btn_Submit" Text="Delete" class="btn save" OnClick="btn_Submit_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="50%" cellpadding="5" style="margin-top: -30px" cellspacing="5">
                                            <tr>
                                                <td>
                                                    <asp:Label Visible="false" runat="server" ID="lblFOId" Text="FO ID"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFOID" runat="server" Visible="false" CssClass="ddlautocomplete chosen-select"
                                                        Width="120px">
                                                    </asp:DropDownList>
                                                </td>

                                                <td>
                                                    <asp:Label runat="server" ID="Label1" Visible="false" Text="Name"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="DropDownList1" runat="server" Visible="false" placeholder="select" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCName_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>

                                            </tr>


                                        </table>

                                        <table width="50%" cellpadding="5" style="margin-top: 40px" cellspacing="5">
                                        </table>
                                    </div>

                                    <div class="rounded_corners" style="overflow: auto; width: 99%; margin-left: 17px; margin-bottom: 30px">
                                        <asp:GridView ID="GvDayWiseAttendance" runat="server" AutoGenerateColumns="false"
                                            Width="95%" CellPadding="4" CellSpacing="3" CssClass="table table-striped table-bordered table-condensed table-hover">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkindividual" runat="server" onclick="Check_Click(this)" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Client ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblclientid" runat="server" Text='<%#Eval("clientid") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Client Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblClientName" runat="server" Text='<%#Eval("ClientName") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpID" runat="server" Text='<%#Eval("EmpID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblName" runat="server" Text='<%#Eval("Name") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDate" runat="server" Text='<%#Eval("Date") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Check in time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCheckintime" runat="server" Text='<%#Eval("Checkintime") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Check out time">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCheckouttime" runat="server" Text='<%#Eval("Checkouttime") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Total Hrs">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTotalHrs" runat="server" Text='<%#Eval("TotalHrs") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Shift">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblShift" runat="server" Text='<%#Eval("Shift") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Attendance Alias">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblAttendanceAlias" runat="server" Text='<%#Eval("AttendanceAlias") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="FO ID">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFOID" runat="server" Text='<%#Eval("FOID") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Check in Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCheckinAddress" runat="server" Text='<%#Eval("CheckinAddress") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Check out Address">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblCheckoutAddress" runat="server" Text='<%#Eval("CheckoutAddress") %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                            </Columns>
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
