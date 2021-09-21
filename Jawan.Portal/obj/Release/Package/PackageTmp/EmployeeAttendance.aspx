<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeAttendance.aspx.cs" Inherits="Jawan.Portal.EmployeeAttendance"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EMPLOYEE ATTENDANCE</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        #social div {
            display: block;
        }

        .HeaderStyle {
            text-align: Left;
        }

        .style4 {
            width: 100%;
        }

        .style5 {
            width: 90px;
        }

        .style6 {
            width: 392px;
        }

        .style7 {
            width: 109px;
        }

        .style8 {
            width: 349px;
        }

        .qsf-demo-canvas {
            margin: 50px 0;
            padding: 35px 50px;
            width: 500px;
            height: 50px;
            background: url('Img/background.png') center top no-repeat;
            text-align: center;
            float: left;
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

        .GreenTypeComboBoxStyle .ajax__combobox_itemlist li {
            background-color: Red;
            border: 1px solid YellowGreen;
            color: White;
            font-size: medium;
            font-family: Courier New;
            padding-bottom: 2px;
        }

        .PnlBackground {
            background-color: rgba(128, 128, 128,0.5);
            z-index: 10000;
        }


        .GridviewScrollHeader TD {
            padding: 5px;
            font-weight: bold;
            white-space: normal;
            border-right: 1px solid #AAAAAA;
            border-bottom: 1px solid #AAAAAA;
            background-color: #EFEFEF;
            text-align: left;
            vertical-align: bottom;
        }

        .GridviewScrollItem TD {
            border-right: 1px solid #AAAAAA;
            border-bottom: 1px solid #AAAAAA;
            background-color: #FFFFFF;
            white-space: normal;
        }

        .GridviewScrollPager {
            border-top: 1px solid #AAAAAA;
            background-color: #FFFFFF;
        }



            .GridviewScrollPager TD {
                font-size: 14px;
            }

            .GridviewScrollPager A {
                color: #666666;
            }

            .GridviewScrollPager SPAN {
                font-size: 16px;
                font-weight: bold;
            }
    </style>

    <script src="http://ajax.googleapis.com/ajax/libs/jquery/1.3.2/jquery.js" type="text/javascript"></script>

    <script type="text/javascript">

        $(document).ready(function () {


            $(".slidingDiv").hide();
            $(".show_hide").show();

            $('.show_hide').click(function () {
                $(".slidingDiv").slideToggle();
            })
            if (isPostBack) { $(".slidingDiv").show(); }

        });
    </script>

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

        .form-control {
            display: block;
            width: 100%;
            height: 28px;
            padding: 0px 12px;
            font-size: 12px;
            line-height: 1.42857143;
            color: #555;
            background-color: #fff;
            background-image: none;
            border: 1px solid #ccc;
            border-radius: 4px;
            -webkit-box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            box-shadow: inset 0 1px 1px rgba(0, 0, 0, .075);
            -webkit-transition: border-color ease-in-out .15s, -webkit-box-shadow ease-in-out .15s;
            -o-transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
            transition: border-color ease-in-out .15s, box-shadow ease-in-out .15s;
        }

            .form-control:focus {
                border-color: #66afe9;
                outline: 0;
                -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
                box-shadow: inset 0 1px 1px rgba(0,0,0,.075), 0 0 8px rgba(102, 175, 233, .6);
            }

            .form-control::-moz-placeholder {
                color: #999;
                opacity: 1;
            }

            .form-control:-ms-input-placeholder {
                color: #999;
            }

            .form-control::-webkit-input-placeholder {
                color: #999;
            }

            .form-control[disabled],
            .form-control[readonly],
            fieldset[disabled] .form-control {
                cursor: not-allowed;
                background-color: #eee;
                opacity: 1;
            }

        textarea.form-control {
            height: auto;
        }

        input[type="search"] {
            -webkit-appearance: none;
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
                select: function (event, ui) { $("#ddlClientID").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                select: function (event, ui) { $("#ddlCName").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientnamechange(event, ui); },
                minLength: 4
            });
        }


        function GetEmpid() {
            $('#TxtEmpid').autocomplete({
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

                    $("#TxtEmpid").attr("data-Empid", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                }
            });
        }

        function GetEmpName() {

            $('#TxtEmpName').autocomplete({
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
                    $("#TxtEmpName").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpNamechange(event, ui);
                }
            });

        }

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $('#TxtEmpid').trigger('change');

        }

        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $('#TxtEmpName').trigger('change');

        }

        function OnAutoCompleteDDLClientidchange(event, ui) {
            $('#ddlClientID').trigger('change');
        }

        function OnAutoCompleteDDLClientnamechange(event, ui) {
            $('#ddlCName').trigger('change');
        }

        $(document).ready(function () {
            GetEmpid();
            GetEmpName();
            setProperty();
        });


    </script>

    <script language="javascript" type="text/javascript">



        $(document).ready(function () {

            $("input[type='text']").keyup(function () {
                var n = $("input[type='text']").length;
                var nextindex = $("input[type='text']").index(this) + 1;
                if ($(this).val().length == $(this).attr("maxlength")) {
                    $("input[type='text']")[nextindex].focus();
                    $("input[type='text']")[nextindex].select();
                    return false;
                }

            });

            //For navigating using left and right arrow of the keyboard
            $("input[type='text'], select").keyup(
            function (event) {

                if ((event.keyCode == 39)) {
                    var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
                    var idx = inputs.index(this);
                    if (idx == inputs.length - 1) {
                        inputs[0].select()
                    } else {
                        $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                            $(this).attr("style", "BACKGROUND-COLOR: white; COLOR: #003399");
                        });
                        inputs[idx + 1].focus();
                        inputs[idx + 1].select();
                    }
                    return false;
                }
                if ((event.keyCode == 37)) {
                    var inputs = $(this).parents("form").eq(0).find("input[type='text'], select");
                    var idx = inputs.index(this);
                    if (idx > 0) {
                        $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                            $(this).attr("style", "BACKGROUND-COLOR: white; COLOR: #003399");
                        });

                        inputs[idx - 1].focus();
                        inputs[idx - 1].select();
                    }
                    return false;
                }
            });
            //For navigating using up and down arrow of the keyboard
            $("input[type='text'], select").keyup(
               function (event) {
                   if ((event.keyCode == 40)) {
                       if ($(this).parents("tr").next() != null) {
                           var nextTr = $(this).parents("tr").next();
                           var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
                           var idx = inputs.index(this);
                           nextTrinputs = nextTr.find("input[type='text'], select");
                           if (nextTrinputs[idx] != null) {
                               $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                                   $(this).attr("style", "BACKGROUND-COLOR: white; COLOR: #003399");
                               });
                               nextTrinputs[idx].focus();
                               nextTrinputs[idx].select();
                           }
                           return false;
                       }
                       else {
                           $(this).focus();
                           $(this).select();
                       }
                   }
                   if ((event.keyCode == 38)) {
                       if ($(this).parents("tr").next() != null) {
                           var nextTr = $(this).parents("tr").prev();
                           var inputs = $(this).parents("tr").eq(0).find("input[type='text'], select");
                           var idx = inputs.index(this);
                           nextTrinputs = nextTr.find("input[type='text'], select");
                           if (nextTrinputs[idx] != null) {
                               $(this).parents("table").eq(0).find("tr").not(':first').each(function () {
                                   $(this).attr("style", "BACKGROUND-COLOR: white; COLOR: #003399");
                               });
                               nextTrinputs[idx].focus();
                               nextTrinputs[idx].select();
                           }
                           return false;
                       }
                       else {
                           $(this).focus();
                           $(this).select();
                       }
                   }
               });
        });    </script>

</head>
<body>



    <form id="clientattendance1" runat="server" autocomplete="off">
        <div id="headerouter">
            <!-- LOGO AND MAIN MENU SECTION BEGIN -->
            <div id="header">
                <!-- LOGO BEGIN -->
                <div id="logo">
                    <a href="Default.aspx">
                        <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a>
                </div>
                <!-- LOGO END -->
                <!-- TOP INFO BEGIN -->
                <div id="toplinks">
                    <ul>
                        <li><a href="Reminders.aspx" runat="server" id="RemindersLink">Reminders</a></li>
                        <li>Welcome <b>
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
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                                        &nbsp;</div> --%>
                                        <ul>
                                            <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                            <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                            <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                            <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                            <li class="current"><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server">
                                                <span>Attendance</span></a></li>
                                            <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                            <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                            <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
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
                <h1 class="dashboard_heading">Clients Dashboard</h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_full">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">EMPLOYEE ATTENDANCE</h2>
                            </div>
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <!--  Content to be add here> -->
                                <div class="boxin">
                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>
                                                    <table width="100%" cellpadding="5" cellspacing="5">
                                                        <tr>
                                                            <td width="110px">Client ID<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList ID="ddlClientID" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="True"
                                                                    OnSelectedIndexChanged="ddlClientID_SelectedIndexChanged" Width="120px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Phone N0(s)
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtphonenumbers" runat="server" class="form-control" Enabled="False" Width="205px" AutoCompleteType="Disabled"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Month
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlMonth" class="form-control" AutoPostBack="True" Width="230px"
                                                                    OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                                <asp:TextBox ID="txtmonth" runat="server" class="form-control" AutoPostBack="true" Width="205px"
                                                                    OnTextChanged="txtmonthOnTextChanged" Visible="false"></asp:TextBox>

                                                                <asp:CheckBox ID="Chk_Month" runat="server"
                                                                    Text="Old" Style="position: relative; top: -20px; left: 240px" Visible="false"/>
                                                                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true"
                                                                    Format="dd/MM/yyyy" TargetControlID="txtmonth">
                                                                </cc1:CalendarExtender>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table style="height: 100%; padding-bottom: 40px" cellpadding="5" cellspacing="5">
                                                        <tr>
                                                            <td>Client Name
                                                            </td>
                                                            <td>


                                                                <asp:DropDownList ID="ddlCName" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="True"
                                                                    Width="305px" OnSelectedIndexChanged="ddlCName_SelectedIndexChanged">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>Our Contact Person
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtocp" runat="server" class="form-control" Width="205px" Enabled="False"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <%-- OT in terms of--%>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlOTType" class="form-control" Width="230px" Visible="false">
                                                                    <asp:ListItem>Days</asp:ListItem>
                                                                    <asp:ListItem>Hours</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div>
                                        <br />
                                        &nbsp;&nbsp;&nbsp;&nbsp;Attendance Mode :
                                    <asp:RadioButton ID="radioindividual" runat="server" Text="Individual" AutoPostBack="true"
                                        Checked="true" GroupName="a" OnCheckedChanged="radioindividual_CheckedChanged" />
                                        <%--  <asp:RadioButton ID="radioall" runat="server" Text="All" AutoPostBack="true" 
                                       GroupName="a" oncheckedchanged="radioindividual_CheckedChanged"/>
                                        --%>
                                        <asp:RadioButton ID="radioall" runat="server" Text="All" AutoPostBack="true" GroupName="a"
                                            OnCheckedChanged="radioindividual_CheckedChanged" />
                                        <asp:RadioButton ID="radiospecialdays" runat="server" Text="Special Days" Visible="false"
                                            OnCheckedChanged="radioindividual_CheckedChanged" />
                                        <div style="float: right">
                                            <asp:LinkButton ID="lbtn_Export" Visible="true" runat="server" OnClick="lbtn_Export_Click">Export to Excel</asp:LinkButton>
                                        </div>

                                    </div>
                                    <br />
                                    <div class="dashboard_firsthalf" style="width: 97%; margin-left: 15px">


                                        <h2>Transfers</h2>

                                        <div>

                                            <table width="100%" style="font-size: 13px">
                                                <tr>
                                                    <td>Emp ID<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="TxtEmpid" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="TxtEmpid_TextChanged" Width="150px">
                                                        </asp:TextBox>

                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblempname" runat="server" Text="Emp Name"> </asp:Label>
                                                    </td>
                                                    <td>
                                                        <%--  <asp:DropDownList ID="ddlempname" runat="server" class="sdrop" AutoPostBack="true"
                                                            OnSelectedIndexChanged="ddlempname_SelectedIndexChanged">
                                                        </asp:DropDownList>--%>

                                                        <asp:TextBox ID="TxtEmpName" CssClass="form-control" runat="server" AutoPostBack="True" OnTextChanged="TxtEmpName_TextChanged" Width="150px">
                                                        </asp:TextBox>

                                                    </td>

                                                    <td>
                                                        <asp:Label ID="lblDesignation" runat="server" Text="Designation"> </asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDesignation" runat="server" class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button1" runat="server" Visible="true" Text="Transfer" class="btn save"
                                                            OnClick="btntransfer_Click" OnClientClick='return confirm("Are you sure you want to give posting order to this employee?");' /></td>
                                                </tr>
                                            </table>


                                            <table width="100%" style="font-size: 13px">
                                                <tr>
                                                    <td>
                                                        <table width="100%" border="0" cellpadding="5" cellspacing="5">


                                                            <tr runat="server" visible="false">
                                                                <td>Order Date<span style="color: Red">*</span>
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
                                                            <tr runat="server" visible="false">
                                                                <td>Joining Date<span style="color: Red">*</span>
                                                                </td>
                                                                <td>
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
                                                            <tr runat="server" visible="false">
                                                                <td>Remarks
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtremarks" runat="server" class="sinput" TextMode="MultiLine"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td>PF
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkpf" runat="server" Checked="true" />
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td>ESI
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkesi" runat="server" Checked="true" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                    <td align="right">
                                                        <table width="100%" border="0" cellpadding="5" cellspacing="5" style="margin-left: 150px;">


                                                            <tr runat="server" visible="false">
                                                                <td>Order ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtorderid" class="sinput" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td>Previous Unit ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtPrevUnitId" class="sinput" runat="server" Enabled="False"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                            <tr runat="server" visible="false">
                                                                <td>Relieving Date<span style="color: Red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtrelivingdate" TabIndex="9" runat="server" class="sinput" MaxLength="10"
                                                                        onkeyup="dtval(this,event)"></asp:TextBox>
                                                                    <cc1:CalendarExtender ID="CErelivingdate" runat="server" Enabled="true" TargetControlID="txtrelivingdate"
                                                                        Format="dd/MM/yyyy">
                                                                    </cc1:CalendarExtender>
                                                                    <cc1:FilteredTextBoxExtender ID="FTBErelivingdate" runat="server" Enabled="True"
                                                                        TargetControlID="txtrelivingdate" ValidChars="/0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td>PT
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox ID="chkpt" runat="server" Checked="true" />
                                                                </td>
                                                            </tr>
                                                            <tr runat="server" visible="false">
                                                                <td>Transfer Type
                                                                </td>
                                                                <td class="style8">
                                                                    <asp:DropDownList ID="ddlTransfertype" runat="server" class="sdrop">
                                                                        <asp:ListItem>--Select--</asp:ListItem>
                                                                        <asp:ListItem>Posting Order</asp:ListItem>
                                                                        <asp:ListItem>Temporary Transfer</asp:ListItem>
                                                                        <asp:ListItem>Dummy Transfer</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                    &nbsp;&nbsp;&nbsp;
                                                           
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                    <br />
                                    <br />

                                    <table width="100%">

                                        <tr>
                                            <td align="right">
                                                <asp:Button ID="btn_Save_AttenDance" runat="server" Text="Save" class=" btn save"
                                                    OnClick="btn_Save_AttenDanceClick" OnClientClick='return confirm(" Are you sure  you want to add this record ?");' />
                                                <asp:Button ID="Btn_Cancel_AttenDance" runat="server" class=" btn save" Text="Cancel"
                                                    OnClientClick='return confirm(" Are you sure you  want to cancel this entry ?");'
                                                    OnClick="Btn_Cancel_AttenDance_Click" />
                                                <br />
                                                <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"></asp:Label>
                                            </td>
                                        </tr>

                                    </table>
                                    <div style="padding: 0px; margin: 0px" class="social" id="div1">
                                        <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server"  UpdateMode="Always">
                                   <ContentTemplate>--%>
                                        <div class="rounded_corners" id="Attendance">
                                            <asp:GridView ID="GridView1" runat="server" Style="margin-left: 0px" Width="140%"
                                                AutoGenerateColumns="False" CellPadding="4" CellSpacing="3" CssClass="table table-striped table-bordered table-condensed table-hover" BorderWidth="0" GridLines="None" ShowFooter="false"
                                                OnRowDataBound="GridView1_RowDataBound" OnRowDeleting="GridView1_RowDeleting">
                                                <%--  <RowStyle BackColor="#EFF3FB" Height="30" />--%>


                                                <%-- 0--%>
                                                <Columns>

                                                     <asp:TemplateField HeaderText=" " HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkattdendance" runat="server" AutoPostBack="true" OnCheckedChanged="chkattdendance_CheckedChanged" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>" Width="40px"></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>



                                                    <%-- 1--%>
                                                    <asp:TemplateField HeaderText=" Emp Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblEmpid" runat="server" Text='<%#Bind("EmpId")%>' Style="text-align: center" Width="70px"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lbloldEmpid" runat="server" Text='<%#Bind("oldEmpId")%>' Style="text-align: center" Width="70px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <%-- 2--%>
                                                    <asp:TemplateField HeaderText="Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblName" Text=' <%#Bind("Name")%>' Width="120px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 3--%>
                                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" ItemStyle-Width="120px" HeaderStyle-Width="120px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblDesg" Text='<%#Bind("Designation")%>' Width="120px"></asp:Label>
                                                            <asp:Label runat="server" ID="lbldesignname" Text='<%#Bind("desgnID")%>' Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 4--%>
                                                    <asp:TemplateField HeaderText="Duties Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="40px" HeaderStyle-Width="40px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lbldutiesText" Text="Duties" Style="text-align: center" Width="40px"></asp:Label>
                                                            <%--  <br />--%>
                                                            <asp:Label runat="server" ID="lblOTSText" Text="OTs" Style="text-align: center" Visible="false" Width="40px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 5--%>

                                                    <asp:TemplateField HeaderText="1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtday1" runat="server" Text=' <%#Bind("day1")%>' MaxLength="1" Style="text-align: left;" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd1" runat="server" Enabled="True" TargetControlID="txtday1"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox ID="txtday1ot" runat="server" Text=' <%#Bind("day1ot")%>' Style="text-align: left" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True"
                                                                TargetControlID="txtday1ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday1" Width="20px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 6--%>

                                                    <asp:TemplateField HeaderText="2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday2" Text=' <%#Bind("day2")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd2" runat="server" Enabled="True" TargetControlID="txtday2"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday2ot" Text=' <%#Bind("day2ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday2ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday2ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday2"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 7--%>
                                                    <asp:TemplateField HeaderText="3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday3" Text=' <%#Bind("day3")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd3" runat="server" Enabled="True" TargetControlID="txtday3"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday3ot" Text=' <%#Bind("day3ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday3ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday3ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday3"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 8--%>

                                                    <asp:TemplateField HeaderText="4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday4" Text=' <%#Bind("day4")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd4" runat="server" Enabled="True" TargetControlID="txtday4"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday4ot" Text=' <%#Bind("day4ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday4ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday4ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday4"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 9--%>


                                                    <asp:TemplateField HeaderText="5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday5" Text=' <%#Bind("day5")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd5" runat="server" Enabled="True" TargetControlID="txtday5"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday5ot" Text=' <%#Bind("day5ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday5ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday5ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday5"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>


                                                    <%-- 10--%>

                                                    <asp:TemplateField HeaderText="6" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday6" Text=' <%#Bind("day6")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd6" runat="server" Enabled="True" TargetControlID="txtday6"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday6ot" Text=' <%#Bind("day6ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday6ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday6ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday6"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 11--%>
                                                    <asp:TemplateField HeaderText="7" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday7" Text=' <%#Bind("day7")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd7" runat="server" Enabled="True" TargetControlID="txtday7"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday7ot" Text=' <%#Bind("day7ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday7ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday7ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday7"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 12--%>
                                                    <asp:TemplateField HeaderText="8" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday8" Text=' <%#Bind("day8")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd8" runat="server" Enabled="True" TargetControlID="txtday8"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday8ot" Text=' <%#Bind("day8ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday8ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday8ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday8"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 13--%>

                                                    <asp:TemplateField HeaderText="9" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday9" Text=' <%#Bind("day9")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd9" runat="server" Enabled="True" TargetControlID="txtday9"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday9ot" Text=' <%#Bind("day9ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday9ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday9ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday9"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 14--%>

                                                    <asp:TemplateField HeaderText="10" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday10" Text=' <%#Bind("day10")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd10" runat="server" Enabled="True" TargetControlID="txtday10"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday10ot" Text=' <%#Bind("day10ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday10ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday10ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday10"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 15--%>

                                                    <asp:TemplateField HeaderText="11" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday11" Text=' <%#Bind("day11")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd11" runat="server" Enabled="True" TargetControlID="txtday11"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday11ot" Text=' <%#Bind("day11ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday11ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday11ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday11"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 16--%>

                                                    <asp:TemplateField HeaderText="12" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday12" Text=' <%#Bind("day12")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd12" runat="server" Enabled="True" TargetControlID="txtday12"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday12ot" Text=' <%#Bind("day12ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday12ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday12ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday12"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 17--%>

                                                    <asp:TemplateField HeaderText="13" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday13" Text=' <%#Bind("day13")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd13" runat="server" Enabled="True" TargetControlID="txtday13"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday13ot" Text=' <%#Bind("day13ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday13ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday13ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday13"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 18--%>

                                                    <asp:TemplateField HeaderText="14" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday14" Text=' <%#Bind("day14")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd14" runat="server" Enabled="True" TargetControlID="txtday14"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday14ot" Text=' <%#Bind("day14ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday14ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday14ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday14"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 19--%>

                                                    <asp:TemplateField HeaderText="15" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday15" Text=' <%#Bind("day15")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd15" runat="server" Enabled="True" TargetControlID="txtday15"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday15ot" Text=' <%#Bind("day15ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday15ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday15ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday15"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 20--%>

                                                    <asp:TemplateField HeaderText="16" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday16" Text=' <%#Bind("day16")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd16" runat="server" Enabled="True" TargetControlID="txtday16"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday16ot" Text=' <%#Bind("day16ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday16ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday16ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday16"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 21--%>


                                                    <asp:TemplateField HeaderText="17" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday17" Text=' <%#Bind("day17")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd17" runat="server" Enabled="True" TargetControlID="txtday17"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday17ot" Text=' <%#Bind("day17ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday17ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday17ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday17"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 22--%>

                                                    <asp:TemplateField HeaderText="18" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday18" Text=' <%#Bind("day18")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd18" runat="server" Enabled="True" TargetControlID="txtday18"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday18ot" Text=' <%#Bind("day18ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday18ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday18ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday18"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 23--%>


                                                    <asp:TemplateField HeaderText="19" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday19" Text=' <%#Bind("day19")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd19" runat="server" Enabled="True" TargetControlID="txtday19"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday19ot" Text=' <%#Bind("day19ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday19ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday19ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday19"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 24--%>

                                                    <asp:TemplateField HeaderText="20" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday20" Text=' <%#Bind("day20")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd20" runat="server" Enabled="True" TargetControlID="txtday20"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday20ot" Text=' <%#Bind("day20ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday20ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday20ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday20"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 25--%>


                                                    <asp:TemplateField HeaderText="21" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday21" Text=' <%#Bind("day21")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd21" runat="server" Enabled="True" TargetControlID="txtday21"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday21ot" Text=' <%#Bind("day21ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday21ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday21ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday21"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 26--%>

                                                    <asp:TemplateField HeaderText="22" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday22" Text=' <%#Bind("day22")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd22" runat="server" Enabled="True" TargetControlID="txtday22"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday22ot" Text=' <%#Bind("day22ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday22ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday22ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday22"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>
                                                    <%-- 27--%>


                                                    <asp:TemplateField HeaderText="23" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday23" Text=' <%#Bind("day23")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd23" runat="server" Enabled="True" TargetControlID="txtday23"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday23ot" Text=' <%#Bind("day23ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday23ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday23ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday23"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 28--%>

                                                    <asp:TemplateField HeaderText="24" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday24" Text=' <%#Bind("day24")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd24" runat="server" Enabled="True" TargetControlID="txtday24"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday24ot" Text=' <%#Bind("day24ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday24ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday24ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday24"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 29--%>

                                                    <asp:TemplateField HeaderText="25" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday25" Text=' <%#Bind("day25")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd25" runat="server" Enabled="True" TargetControlID="txtday25"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday25ot" Text=' <%#Bind("day25ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday25ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday25ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday25"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 30--%>


                                                    <asp:TemplateField HeaderText="26" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday26" Text=' <%#Bind("day26")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd26" runat="server" Enabled="True" TargetControlID="txtday26"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday26ot" Text=' <%#Bind("day26ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday26ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday26ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday26"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 31--%>


                                                    <asp:TemplateField HeaderText="27" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday27" Text=' <%#Bind("day27")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd27" runat="server" Enabled="True" TargetControlID="txtday27"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday27ot" Text=' <%#Bind("day27ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday27ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday27ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday27"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 32--%>


                                                    <asp:TemplateField HeaderText="28" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday28" Text=' <%#Bind("day28")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd28" runat="server" Enabled="True" TargetControlID="txtday28"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday28ot" Text=' <%#Bind("day28ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday28ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday28ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday28"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 33--%>

                                                    <asp:TemplateField HeaderText="29" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday29" Text=' <%#Bind("day29")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtd29" runat="server" Enabled="True" TargetControlID="txtday29"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday29ot" Text=' <%#Bind("day29ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextday29ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday29ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday29"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 34--%>


                                                    <asp:TemplateField HeaderText="30" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday30" Text=' <%#Bind("day30")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtddd30" runat="server" Enabled="True" TargetControlID="txtday30"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday30ot" Text=' <%#Bind("day30ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextdayyy30ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday30ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday30"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 35--%>

                                                    <asp:TemplateField HeaderText="31" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="20px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtday31" Text=' <%#Bind("day31")%>' MaxLength="1" Style="text-align: center" Width="20px" onBlur="javascript:{this.value = this.value.toUpperCase(); }"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FillTxtExtddd31" runat="server" Enabled="True" TargetControlID="txtday31"
                                                                ValidChars="AaBbCcDdNnGgWwUuLlHhXx0Pp">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <br />
                                                            <asp:TextBox runat="server" ID="txtday31ot" Text=' <%#Bind("day31ot")%>' Style="text-align: center" Visible="false" Width="20px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextdayyy31ot" runat="server" Enabled="True"
                                                                TargetControlID="txtday31ot" ValidChars="AaBbCcDdNnGgWwUuLlPp0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label runat="server" ID="lblTotalday31"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false"
                                                        ItemStyle-Width="5px" HeaderStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblTotalDts" runat="server" Text="" Width="5px"></asp:Label>
                                                            <br />
                                                            <asp:Label ID="lblTotalOts" runat="server" Text="" Width="5px" Visible="false"></asp:Label>
                                                        </ItemTemplate>
                                                        <FooterTemplate>
                                                            <asp:Label ID="lblGrandTotal" runat="server" Text="" Width="5px"></asp:Label>
                                                        </FooterTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Duties" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtDuties" Text=' <%#Bind("Noofduties")%>' Style="text-align: center" Width="70px" Enabled="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBTxtDuties" runat="server" Enabled="True"
                                                                TargetControlID="txtDuties" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="OTs" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtOTs" Text=' <%#Bind("OT")%>' Style="text-align: center" Width="70px" Enabled="true"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBOts" runat="server" Enabled="True"
                                                                TargetControlID="txtOTs" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="NHs" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtNhs" Text=' <%#Bind("Nhs")%>' Style="text-align: center" Width="70px" Enabled="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBNhs" runat="server" Enabled="True"
                                                                TargetControlID="txtNhs" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="WOs" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtWos" Text=' <%#Bind("WO")%>' Style="text-align: center" Width="70px" Enabled="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBWOs" runat="server" Enabled="True"
                                                                TargetControlID="txtWos" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Total Duties" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txttotduties" Text=' <%#Bind("TotalDuties")%>' Style="text-align: center" Width="70px" Enabled="false"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBduties" runat="server" Enabled="True"
                                                                TargetControlID="txttotduties" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Canteen Adv." HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtCanAdv" Text=' <%#Bind("CanteenAdv")%>' SStyle="text-align: center" Width="70px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                                                TargetControlID="txtCanAdv" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Penalty" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtPenalty" Text=' <%#Bind("Penalty")%>' Style="text-align: center" Width="70px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                TargetControlID="txtPenalty" ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Incentive" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                        HeaderStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:TextBox runat="server" ID="txtIncentivs" Text=' <%#Bind("Incentivs")%>' Style="text-align: center" Width="70px"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBE4" runat="server" Enabled="True" TargetControlID="txtIncentivs"
                                                                ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </ItemTemplate>
                                                        <ItemStyle></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Actions">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="linkdelete" ImageAlign="Middle" Style="padding-left: 20px" CommandName="Delete" ImageUrl="~/css/assets/DeleteIcon.png" runat="server"
                                                                OnClientClick='return confirm("Do you want to delete this record?");' ToolTip="Delete" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="80px"></ItemStyle>
                                                        <HeaderStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>

                                                </Columns>
                                                <HeaderStyle CssClass="GridviewScrollHeader" />
                                                <RowStyle CssClass="GridviewScrollItem" />
                                                <PagerStyle CssClass="GridviewScrollPager" />
                                            </asp:GridView>
                                        
                                            <asp:GridView ID="SampleGrid" runat="server" Width="100%"
                                                AutoGenerateColumns="False" CellPadding="2" CellSpacing="2"
                                                ForeColor="#333333" BorderStyle="Solid" OnRowDataBound="SampleGrid_RowDataBound"
                                                BorderColor="Black" BorderWidth="0" GridLines="None" Visible="false"
                                                HeaderStyle-CssClass="HeaderStyle">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>

                                                    <%-- 0--%>
                                                    <asp:TemplateField HeaderText="Client Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblsClientid" Width="200px" Text='<%#Bind("clientid") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 1--%>
                                                    <asp:TemplateField HeaderText="Emp Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblsEmpid" runat="server" Text='<%#Bind("empid") %>' Style="text-align: center" Width="50px"></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle VerticalAlign="Middle" />
                                                    </asp:TemplateField>

                                                    <%-- 2--%>
                                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="lblsDesg" Text='<%#Bind("design") %>' Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%-- 3--%>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            Duties
                                            <br />
                                                            OTs
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%--Duties --%>

                                                    <%-- 4--%>
                                                    <asp:TemplateField HeaderText="P21" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday21" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday21") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday21ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday21ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 5--%>
                                                    <asp:TemplateField HeaderText="P22" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday22" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday22") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday22ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday22ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 6--%>
                                                    <asp:TemplateField HeaderText="P23" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday23" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday23") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday23ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday23ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 7--%>
                                                    <asp:TemplateField HeaderText="P24" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday24" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday24") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday24ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday24ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 8--%>
                                                    <asp:TemplateField HeaderText="P25" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday25" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday25") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday25ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday25ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 9--%>
                                                    <asp:TemplateField HeaderText="P26" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday26" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday26") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday26ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday26ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 10--%>
                                                    <asp:TemplateField HeaderText="P27" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday27" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday27") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday27ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday27ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 11--%>
                                                    <asp:TemplateField HeaderText="P28" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday28" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday28") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday28ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday28ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 12--%>
                                                    <asp:TemplateField HeaderText="P29" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday29" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday29") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday29ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday29ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 13--%>
                                                    <asp:TemplateField HeaderText="P30" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday30" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday30") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday30ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday30ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 14--%>
                                                    <asp:TemplateField HeaderText="P31" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsPday31" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday31") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsPday31ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("Pday31ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 15--%>
                                                    <asp:TemplateField HeaderText="1" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday1" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day1") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday1ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day1ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 16--%>
                                                    <asp:TemplateField HeaderText="2" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday2" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day2") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday2ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day2ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 17--%>
                                                    <asp:TemplateField HeaderText="3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday3" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day3") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday3ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day3ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 18--%>
                                                    <asp:TemplateField HeaderText="4" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday4" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day4") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday4ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day4ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 19--%>
                                                    <asp:TemplateField HeaderText="5" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday5" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day5") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday5ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day5ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 20--%>
                                                    <asp:TemplateField HeaderText="6" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday6" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day6") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday6ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day6ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 21--%>
                                                    <asp:TemplateField HeaderText="7" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday7" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day7") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday7ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day7ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 22--%>
                                                    <asp:TemplateField HeaderText="8" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday8" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day8") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday8ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day8ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 23--%>
                                                    <asp:TemplateField HeaderText="9" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday9" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day9") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday9ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day9ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 24--%>
                                                    <asp:TemplateField HeaderText="10" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday10" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day10") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday10ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day10ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 25--%>
                                                    <asp:TemplateField HeaderText="11" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday11" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day11") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday11ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day11ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 26--%>
                                                    <asp:TemplateField HeaderText="12" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday12" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day12") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday12ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day12ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 27--%>
                                                    <asp:TemplateField HeaderText="13" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday13" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day13") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday13ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day13ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 28--%>
                                                    <asp:TemplateField HeaderText="14" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday14" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day14") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday14ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day14ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 29--%>
                                                    <asp:TemplateField HeaderText="15" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday15" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day15") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday15ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day15ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 30--%>
                                                    <asp:TemplateField HeaderText="16" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday16" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day16") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday16ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day16ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 31--%>
                                                    <asp:TemplateField HeaderText="17" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday17" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day17") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday17ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day17ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 32--%>
                                                    <asp:TemplateField HeaderText="18" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday18" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day18") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday18ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day18ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 33--%>
                                                    <asp:TemplateField HeaderText="19" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday19" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day19") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday19ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day19ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 34--%>
                                                    <asp:TemplateField HeaderText="20" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday20" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day20") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday20ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day20ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 35--%>
                                                    <asp:TemplateField HeaderText="21" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday21" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day21") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday21ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day21ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 36--%>
                                                    <asp:TemplateField HeaderText="22" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday22" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day22") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday22ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day22ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 37--%>
                                                    <asp:TemplateField HeaderText="23" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday23" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day23") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday23ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day23ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 38--%>
                                                    <asp:TemplateField HeaderText="24" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday24" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day24") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday24ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day24ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 39--%>
                                                    <asp:TemplateField HeaderText="25" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday25" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day25") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday25ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day25ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 40--%>
                                                    <asp:TemplateField HeaderText="26" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday26" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day26") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday26ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day26ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 41--%>
                                                    <asp:TemplateField HeaderText="27" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday27" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day27") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday27ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day27ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 42--%>
                                                    <asp:TemplateField HeaderText="28" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday28" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day28") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday28ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day28ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 43--%>
                                                    <asp:TemplateField HeaderText="29" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday29" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day29") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday29ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day29ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 44--%>
                                                    <asp:TemplateField HeaderText="30" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday30" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day30") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday30ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day30ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 45--%>
                                                    <asp:TemplateField HeaderText="31" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="20px" FooterStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="txtsday31" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day31") %>'></asp:Label>
                                                            <br />
                                                            <asp:Label ID="txtsday31ot" runat="server" Style="text-align: center" Width="20px" Text='<%#Bind("day31ot") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%-- 46--%>
                                                    <asp:TemplateField HeaderText="EL Days" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsELDays" Style="text-align: center" Width="5px" Text='<%#Bind("ELDays") %>'></asp:Label>
                                                        </ItemTemplate>

                                                    </asp:TemplateField>

                                                    <%-- 47--%>
                                                    <asp:TemplateField HeaderText="Lunch Days" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsLunchDays" Style="text-align: center" Width="5px" Text='<%#Bind("LunchDays") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 48--%>
                                                    <asp:TemplateField HeaderText="Pay 1" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsPay1" Style="text-align: center" Width="5px" Text='<%#Bind("Pay1") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <%-- 49--%>
                                                    <asp:TemplateField HeaderText="Pay 2" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsPay2" Style="text-align: center" Width="5px" Text='<%#Bind("Pay2") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%-- 50--%>
                                                    <asp:TemplateField HeaderText="Pay 3" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsPay3" Style="text-align: center" Width="5px" Text='<%#Bind("Pay3") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <%-- 51--%>
                                                    <asp:TemplateField HeaderText="Paydays" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsPaydays" Style="text-align: center" Width="5px" Text='<%#Bind("PayDays") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Canteen Adv" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="5px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsCanAdv" Style="text-align: center" Width="5px" Text='<%#Bind("CanteenAdv") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Rent" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsrent" Style="text-align: center" Width="5px" Text='<%#Bind("RentDed") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Fines" FooterStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="70px">
                                                        <ItemTemplate>
                                                            <asp:Label runat="server" ID="txtsfines" Style="text-align: center" Width="5px" Text='<%#Bind("Fines") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                </Columns>

                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />

                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>



                                        </div>
                                        <cc1:ModalPopupExtender ID="modelLogindetails" runat="server" TargetControlID="Chk_Month" PopupControlID="pnllogin"
                                            BackgroundCssClass="PnlBackground">
                                        </cc1:ModalPopupExtender>


                                        <asp:Panel ID="pnllogin" runat="server" Height="100px" Width="300px" DefaultButton="btnSubmit" Style="display: none; position: absolute; background-color: white; box-shadow: rgba(0,0,0,0.4)">

                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>
                                                    <table>
                                                        <tr>
                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="font: bold; font-size: medium">&nbsp;&nbsp;&nbsp;
                            Enter Password:
                                                            </td>
                                                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                    </table>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>
                                            <br />
                                            <table style="background-position: center;">
                                                <tr>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn Save" />
                                                    </td>
                                                    <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" class="btn Save" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </asp:Panel>

                                        <div>
                                            <table width="100%">
                                                <tr>
                                                    <td width="25%"></td>
                                                    <td width="25%">
                                                        <asp:Label ID="lblTotalDuties" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td width="25%">
                                                        <asp:Label ID="lblTotalOts" runat="server" Text="" Font-Bold="true"></asp:Label>
                                                    </td>
                                                    <td width="25%"></td>
                                                </tr>
                                                <tr>
                                                    <td width="25%"></td>
                                                    <td width="25%"></td>
                                                    <td width="25%"></td>
                                                    <td colspan="4">
                                                        <asp:Label ID="lbltotaldesignationlist" runat="server" Text=""> </asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                    </div>
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
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>

    </form>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>

    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script src="script/gridviewScroll.min.js" type="text/javascript"></script>



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
                setProperty();
            });
        };
    </script>

    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

    function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 945,
                height: 500,
                freezesize: 5
                
            });
        }
       

      <%--  function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: 660,
                height: 300,
                startVertical: $("#<%=hfGridView1SV.ClientID%>").val(),
            startHorizontal: $("#<%=hfGridView1SH.ClientID%>").val(),
            onScrollVertical: function (delta) {
                $("#<%=hfGridView1SV.ClientID%>").val(delta);
            },
            onScrollHorizontal: function (delta) {
                $("#<%=hfGridView1SH.ClientID%>").val(delta);
            }
            });
        }--%>

      <%--  function gridviewScroll() {

            document.body.style.overflow = "hidden";

            var gridWidth = $(window).width() * 0.9;
            var gridHeight = $(window).height() - 230;

            $('#<%=GridView1.ClientID%>').gridviewScroll({
                width: gridWidth,
                height: gridHeight,
                //width: 800,
                //height: 500,
                freezesize: 5,           
                startVertical: $("#<%=hfGridView1SV.ClientID%>").val(),
                startHorizontal: $("#<%=hfGridView1SH.ClientID%>").val(),
                onScrollVertical: function (delta) {
                    $("#<%=hfGridView1SV.ClientID%>").val(delta);
                },
                onScrollHorizontal: function (delta) {
                    $("#<%=hfGridView1SH.ClientID%>").val(delta);
                }
            });
         }--%>
    </script>
</body>
</html>
