<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PinMyVisits.aspx.cs" Inherits="Jawan.Portal.PinMyVisits" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: Pin My Visit</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <style type="text/css">
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



        .Grid th, .Grid td {
            border: 1px solid #66CCFF;
        }
    </style>

    <script type="text/javascript">

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
                select: function (event, ui) { $("#ddlEmpid").attr("data-clientId", ui.item.value); OnAutoCompleteDDLVendoridchange(event, ui); },
                select: function (event, ui) { $("#ddlClientID").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                select: function (event, ui) { $("#ddlCName").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientnamechange(event, ui); },
                select: function (event, ui) { $("#ddlFOID").attr("data-clientId", ui.item.value); OnAutoCompleteDDLFoidchange(event, ui); },
                minLength: 4
            });
        }

        function ShowPopup() {
            $(function () {
                $("#dialog").dialog({
                    title: "Zoomed Image",
                    width: 350,
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');

                        }
                    },
                    modal: true
                });
            });
        };

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLVendoridchange(event, ui) {
            $('#ddlEmpid').trigger('change');

        }
        function OnAutoCompleteDDLClientidchange(event, ui) {
            $('#ddlClientID').trigger('change');

        }

        function OnAutoCompleteDDLClientnamechange(event, ui) {

            $('#ddlCName').trigger('change');
        }
        function OnAutoCompleteDDLFoidchange(event, ui) {

            $('#ddlFOID').trigger('change');
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

    </script>



</head>
<body>
    <form id="ClentwiseEmployeesSalaryreports1" runat="server">
        <!-- HEADER SECTION BEGIN -->
        <div id="headerouter">
            <script type="text/javascript">            

                $(function () {


                    $("[id*=GVpinmyvisit]").find("[id*=btnview]").click(function () {


                        //Reference the GridView Row.
                        var row = $(this).closest("tr");

                        document.getElementById("<%=hfPitstopAttachmentId.ClientID %>").value = row.find("td").eq(11).find(":text").val();
                        document.getElementById("<%=btnGetImage.ClientID %>").click();


                        return false;
                    });


                });

            </script>

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
                <h1 class="dashboard_heading"></h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_full">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Pin My Visit&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </h2>
                            </div>

                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px; height: auto">
                                <!--  Content to be add here> -->
                                <div style="float: right">
                                    <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click">Export to Excel</asp:LinkButton>
                                </div>
                                <table width="100%" style="margin: 10px auto">
                                    <tr>
                                        <td>Type</td>
                                        <td>
                                            <asp:DropDownList ID="ddltype" CssClass="sdrop" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddltype_SelectedIndexChanged" runat="server">
                                                <asp:ListItem>Day Wise</asp:ListItem>
                                                <asp:ListItem>Month Wise</asp:ListItem>
                                                <asp:ListItem>From To</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>Option</td>
                                        <td>
                                            <asp:DropDownList ID="ddloption" CssClass="sdrop" AutoPostBack="true" Width="150px" OnSelectedIndexChanged="ddloption_SelectedIndexChanged" runat="server">
                                                <asp:ListItem>Employee Wise</asp:ListItem>
                                                <asp:ListItem>Client Wise</asp:ListItem>
                                                <asp:ListItem>Activity</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:Label runat="server" ID="lblclientid" Visible="false" Text="Client ID"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlClientID" Visible="false" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlClientID_SelectedIndexChanged"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:Label runat="server" Visible="false" ID="lblclientname" Text="Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlCName" Visible="false" runat="server" placeholder="select" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlCName_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>

                                    </tr>
                                    <tr>

                                        <td>
                                            <asp:Label runat="server" ID="lblempid" Text="Emp ID/Name"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmpid" runat="server" CssClass="ddlautocomplete chosen-select" Width="150px"></asp:DropDownList>
                                        </td>

                                        <td>
                                            <asp:Label ID="lblDay" runat="server" Text="Day"></asp:Label>
                                            <asp:Label ID="lblMonth" runat="server" Visible="false" Text="Month"></asp:Label>
                                        </td>
                                        
                                        <td>
                                            <asp:TextBox ID="txtMonth" runat="server" class="sinput" autocomplete="off"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" TargetControlID="txtMonth"
                                                Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server" Enabled="True"
                                                TargetControlID="txtMonth" ValidChars="/0123456789-">
                                            </cc1:FilteredTextBoxExtender>

                                        </td>
                                         <td>
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" class=" btn save" OnClick="btnSubmit_Click" ToolTip="Submit" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label Visible="false" runat="server" ID="lblActivity" Text="Activity"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlFOID" runat="server" Visible="false" CssClass="ddlautocomplete chosen-select"
                                                Width="120px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>


                                        <td>
                                            <asp:Label ID="lblfrom" runat="server" Visible="false" Text="From"></asp:Label>
                                        </td>

                                        <td>
                                            <asp:TextBox ID="txtfrom" runat="server" Visible="false" autocomplete="off" class="form-control" Width="200px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtfrom_CalendarExtender" runat="server" BehaviorID="calendar1"
                                                Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtfrom">
                                            </cc1:CalendarExtender>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblto" runat="server" Visible="false" Text="To"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtto" runat="server" Visible="false" autocomplete="off" class="form-control" Width="200px"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtto_CalendarExtender" runat="server" BehaviorID="calendar2"
                                                Enabled="true" Format="dd/MM/yyyy" TargetControlID="txtto">
                                            </cc1:CalendarExtender>
                                        </td>

                                    </tr>



                                </table>
                                <table>
                                    <tr>
                                       
                                        <td>

                                        </td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="PDF" class=" btn save" OnClick="Button1_Click" ToolTip="Submit" />
                                        </td>
                                    </tr>
                                </table>



                                <div style="width: 100%; margin-top: 30px">




                                    <asp:GridView ID="GVpinmyvisit" runat="server" CssClass="table table-striped table-bordered table-condensed table-hover" AutoGenerateColumns="false" OnRowDataBound="gvdata_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="UpdatedBy" HeaderText="Emp ID" />
                                            <asp:BoundField DataField="EmpFName" HeaderText="Name" />
                                            <asp:BoundField DataField="Clientid" HeaderText="Client ID" />
                                            <asp:TemplateField HeaderText="Created On" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblUpdatedOn" runat="server" Text='<%#Eval("UpdatedOn", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                            <asp:BoundField DataField="EmpRemarks" HeaderText="Remarks" />
                                            <asp:BoundField DataField="Activity" HeaderText="Activity" />
                                            <asp:BoundField DataField="CheckinLat" HeaderText="Checkin Lat" />
                                            <asp:BoundField DataField="CheckinLng" HeaderText="Checkin Lng" />
                                            <asp:BoundField DataField="Address" HeaderText="Checkin Address" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:Button ID="btnview" runat="server" Text="View" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:TextBox ID="lblpitstopattid" runat="server" Text='<%#Bind("PitstopAttachmentId")%>' Style="display: none"></asp:TextBox>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>


                                </div>

                                <asp:HiddenField ID="hfPitstopAttachmentId" runat="server" />
                                <asp:Button ID="btnGetImage" runat="server" OnClick="btnGetImage_Click" Style="display: none" />

                                <div id="dialog" style="display: none">

                                    <asp:Image ID="imgphoto" runat="server" Width="320" Height="300" />
                                </div>
                            </div>
                        </div>
                        <!-- DASHBOARD CONTENT END -->
                    </div>
                </div>
                <!-- CONTENT AREA END -->

            </div>
        </div>
    </form>
</body>
</html>
