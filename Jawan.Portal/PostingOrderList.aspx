<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PostingOrderList.aspx.cs" Inherits="Jawan.Portal.PostingOrderList" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>POSTING ORDER LIST</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
     <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>
   

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

        function bindautofilldesgs() {
            $(".txtautofillempid").autocomplete({
                source: eval($("#hdempid").val()),
                minLength: 4
            });
        }

        function GetFromEmpid() {
            $('#txtfromempid').autocomplete({
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

            });
        }

        function GetToEmpid() {
            $('#txttoempid').autocomplete({
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

            });
        }

        function GetEmpid() {
            $('#txtEmpid').autocomplete({
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

                    $("#txtEmpid").attr("data-Empid", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                }
            });
        }

        function GetEmpName() {

            $('#txtName').autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: 'Autocompletion.asmx/GetFormEmpNames',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term
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
                    $("#txtName").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpNamechange(event, ui);
                }
            });

        }

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $('#txtEmpid').trigger('change');

        }

        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $('#txtName').trigger('change');

        }

        $(document).ready(function () {

            GetEmpid();
            GetEmpName();
            GetFromEmpid();
            GetToEmpid();
        });
		
    </script>

    <script type="text/javascript">
       

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
            height: 120px;
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
    </style>

     <script type="text/javascript">

         function pageLoad(sender, args) {
             if (!args.get_isPartialLoad()) {
                 //  add our handler to the document's
                 //  keydown event
                 $addHandler(document, "keydown", onKeyDown);
             }
         }

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
                 select: function (event, ui) { $("#ddlFromClientid").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                 select: function (event, ui) { $("#ddlCname").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientnamechange(event, ui); },
                 minLength: 4
             });

             $(".ddlautocomplete").combobox({
                 select: function (event, ui) { $("#ddlToClientid").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                 select: function (event, ui) { $("#ddlCname").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientnamechange(event, ui); },
                 minLength: 4
             });
         }

         $(document).ready(function () {
             setProperty();
         });

         function OnAutoCompleteDDLClientidchange(event, ui) {
             $('#ddlFromClientid').trigger('change');

         }

         function OnAutoCompleteDDLClientidchange(event, ui) {
             $('#ddlToClientid').trigger('change');

         }

         function OnAutoCompleteDDLClientnamechange(event, ui) {

             $('#ddlCname').trigger('change');
         }

         function bindautofilldesgs() {
             $(".txtautofilldesg").autocomplete({
                 source: eval($("#hdDesignations").val()),
                 minLength: 4
             });
         }

    </script>

     <style type="text/css">
        #social div {
            display: block;
        }

        .HeaderStyle {
            text-align: Left;
        }


        .modalBackground {
            background-color: Gray;
            z-index: 10000;
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

        .PnlBackground {
            background-color: rgba(128, 128, 128,0.5);
            z-index: 10000;
        }
    </style>

</head>
<body>
    <form id="PostingOrderList1" runat="server">
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
                    <%--<li><a href="clientattendance.aspx" id="Clientattendancelink" runat="server" ><span>Entries</span></a></li>--%>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
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
                                        <li class="current"><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server">
                                            <span>Transfers</span></a></li>
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
                 <%--<div align="right"> <b>Import Data: </b> <asp:FileUpload  ID="fileupload1" runat="server" Width="50px"/> 
                 <asp:Button ID="btnImportData" runat="server" ValidationGroup="b"
                     Text="Import"  
                        class=" btn save" onclick="btnImportData_Click"  /></div> --%>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="Div1">
                <div class="dashboard_full">
                
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Posting Order
                            </h2>
                            
                            
                        </div>
                        </div>
                        <div class="contentarea">
                            <div class="boxinc">
                                <ul>
                                    <li class="left leftmenu">
                                  
                                    </li>
                                    <li class="right" style="height: auto">
                                        <table width="100%" border="0" cellpadding="0" cellspacing="0">
                                            <tr>
                                                <td>
                                                    <asp:ScriptManager runat="server" ID="Scriptmanager1">
                                                    </asp:ScriptManager>
                                                    <!--  Content to be add here> -->
                                                 

                                                     <div style="margin: 20px;width:140%">
                                        <asp:HiddenField ID="hdempid" runat="server" />
                                        <div>

                                            <table style="width: 100%" cellpadding="10">

                                                <tr>

                                                    <td>
                                                        <asp:Label runat="server" ID="lblempid" Width="50px" Text="Emp ID"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmpid" runat="server" CssClass="form-control" AutoPostBack="true" style="width:200px" OnTextChanged="txtEmpid_TextChanged"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblempname" Width="50px" Text="Name"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtName" runat="server" CssClass="form-control" AutoPostBack="true" style="width:200px" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr>
                                                    <td>Designation </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server"   class="form-control" Enabled="false" style="width:225px"></asp:DropDownList>
                                                    </td>
                                                    <td>D.O.J </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDOJ" runat="server" Enabled="false" style="width:200px"  CssClass="form-control"></asp:TextBox>
                                                    </td>

                                                </tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr>
                                                    <td>From Client ID</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlFromClientid" runat="server" CssClass="ddlautocomplete chosen-select"  style="width:225px" ></asp:DropDownList>
                                                        </td>

                                                    <td>To Client ID</td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlToClientid" runat="server" CssClass="ddlautocomplete chosen-select" style="width:225px"></asp:DropDownList>
                                                    </td>
                                                   
                                                </tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr>
                                                    <td>From Period
                                                    </td>
                                                    <td>
                                                         <asp:TextBox ID="txtFromPeriod" runat="server" Text="" CssClass="form-control" style="width:200px" autocomplete="off"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                TargetControlID="txtFromPeriod" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True" TargetControlID="txtFromPeriod"
                                                ValidChars="/0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                                    </td>

                                                    <td>To Period</td>
                                                    <td>
                                                         <asp:TextBox ID="txtToPeriod" runat="server" Text="" CssClass="form-control" style="width:200px" autocomplete="off"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                TargetControlID="txtToPeriod" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True" TargetControlID="txtToPeriod"
                                                ValidChars="/0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>
                                                <tr>

                                                </tr>
                                                <tr></tr>
                                                <tr></tr>
                                                <tr>
                                                    <td>Date Of Transfer
                                                    </td>
                                                    <td>
                                                       
                                            <asp:TextBox ID="txtmonth" runat="server" Text="" CssClass="form-control" style="width:200px" autocomplete="off"></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                TargetControlID="txtmonth" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtmonth"
                                                ValidChars="/0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                       
                                                    </td>
                                                    <td>Reason</td>
                                                    <td>
                                                        <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine" CssClass="form-control" style="width:200px"></asp:TextBox>
                                                        </td>
                                                    </tr>   <tr></tr>

                                                <tr>
                                                    <td>Order ID
                                                    </td>
                                                    <td>
                                                    <asp:TextBox ID="txtorderid" CssClass="form-control" runat="server" Enabled="False" style="width:200px"></asp:TextBox>
                                                     </td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="btntransfer" runat="server"  Text="Transfer" class="btn save"
                                                                                    OnClick="btntransfer_Click" OnClientClick='return confirm("Are you sure you want to give posting order to this employee?");' />&nbsp;&nbsp;


                                                        <asp:Button ID="btnPDF" runat="server" Text="PDF" OnClick="btnPDF_Click" />
                                                    </td>
                                                   
                                                  </tr>
                                                

                                            </table>
                                           

                                        </div>
                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </li>
                                </ul>
                              
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
                    <br />
                </div>
                <!-- DASHBOARD CONTENT END -->
            </div>
        </div>
        <!-- CONTENT AREA END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered byWeb Wonders</a></div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>| <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </div>
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


                  GetEmpid();
                  GetEmpName();
                  GetFromEmpid();
                  GetToEmpid();

              });
          };
    </script>
</body>
</html>