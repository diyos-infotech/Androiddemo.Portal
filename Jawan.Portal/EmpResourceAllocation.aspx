<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpResourceAllocation.aspx.cs" Inherits="Jawan.Portal.EmpResourceAllocation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVENTORY:ADD INFLOW DETAILS</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />

    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />
    <script src="script/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="script/jscript.js"> </script>

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

        .chosen{
            width:250px;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }
    </style>

    <script type="text/javascript">

        var IssueType = "S";

      
           

      

        function GetEmpid()
        {
            $('#txtEmpid').autocomplete({
                source: function (request, response) {
                    

                    $.ajax({
                        url: 'Autocompletion.asmx/GetEmpIDs',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term,
                            IssueType: $('input[name=g1]:checked').val(),

                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert("Invalid!!");
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

                        url: 'Autocompletion.asmx/GetEmpNames',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term,
                            IssueType: $('input[name=g1]:checked').val(),

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

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $('#txtEmpid').trigger('change');

        }
        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $('#txtName').trigger('change');

        }

        function InvalidEmpData() {
            alert("Invalid !!");
        }

        //function OnAutoCompletetxtSupervisorIDchange(event, ui) {
        //    $('#txtSupervisorID').trigger('change');

        //}
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
                select: function (event, ui) { $("#ddlSupervisorID").attr("data-SupervisorID", ui.item.value); OnAutoCompleteDDLSupervisorIDchange(event, ui); },
                
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
            GetEmpid();
            GetEmpName();
           
        });

        function OnAutoCompleteDDLSupervisorIDchange(event, ui) {
            $('#ddlSupervisorID').trigger('change');

        }
        

    </script>

    <script src="script/chosen.jquery.js" type="text/javascript"></script>

    <script type="text/javascript">
        jQuery(document).ready(function mchoose() {
            jQuery(".chosen").data("placeholder", "Select").chosen();
        });
    </script>
</head>

<body>
    
    <form id="AddNewItem1" runat="server">


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
                        <li class="first"><a href="Employees.aspx" id="Employeeslink" runat="server" class="current">
                            <span>Employees</span></a></li>
                        <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        --%>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
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
                                            <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                            <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                            <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                            <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                            <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                            <li class="current"><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                            <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payments</span></a></li>
                                            <%-- <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
                                            <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                            <%--             <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>
                                            --%>
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
            <div class="content-holder">
                
                <asp:ScriptManager runat="server" ID="Scriptmanager1">
                </asp:ScriptManager>

                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">

                    <div class="sidebox">
                        <div class="boxhead">

                            <h2 style="text-align: center">Resource Issue
                            </h2>
                        </div>
                        <div class="contentarea" id="Div1">
                            <div class="boxinc">



                                <asp:UpdatePanel ID="uppanel" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table cellpadding="5" cellspacing="5" width="100%" style="margin: 10px; margin-left: 20px">
                                            <tr>
                                                <td>Resource Mode</td>
                                                <td>

                                                    <asp:RadioButton ID="rdbIndividualWise" TabIndex="6" runat="server" value="I" Style="font-weight: normal; margin-left: -100px" GroupName="g1" Text="Individual Wise" Checked="True" AutoPostBack="true" OnCheckedChanged="rdbToSupervisor_CheckedChanged"/>
                                                    <asp:RadioButton ID="rdbToSupervisor" TabIndex="7" runat="server" value="S" Style="font-weight: normal; margin-left: 10px" GroupName="g1" Text="Issue to Field Staff" AutoPostBack="true" OnCheckedChanged="rdbToSupervisor_CheckedChanged" />
                                                    <asp:RadioButton ID="rdbFromSupervisor" runat="server" value="D" Style="font-weight: normal; margin-left: 10px" GroupName="g1" Text="Issue From Field Staff" AutoPostBack="true" OnCheckedChanged="rdbToSupervisor_CheckedChanged" />
                                                </td>
                                                

                                            </tr>
                                        </table>

                                        <table cellpadding="5" cellspacing="5" width="100%" style="margin: 10px; margin-left: 20px">
                                            <tr style="height: 32px">
                                                <td>
                                                   
                                                    <asp:Label ID="lblSupervisorID" runat="server" Text="Supervisor ID" Visible="false"></asp:Label>
                                                </td>
                                                <td>

                                                    <%--<asp:TextBox ID="txtSupervisorID" runat="server" Width="228px" TabIndex="2" CssClass="form-control" Visible="false" AutoPostBack="true" OnTextChanged="txtSupervisorID_TextChanged"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlSupervisorID" runat="server" CssClass="ddlautocomplete chosen-select" TabIndex="2" Style="width: 150px" Visible="false"
                                                        AutoPostBack="true" OnSelectedIndexChanged="ddlSupervisorID_OnSelectedIndexChanged">
                                                        <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td style="width: 150px"></td>
                                                   <td>  <asp:Label ID="lblSupUniformID" runat="server" Text="Supervisor Uniform ID" Visible="false"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlUniformID" runat="server" Width="228px" AutoPostBack="True" CssClass="form-control" Visible="false" OnSelectedIndexChanged="ddlUniformID_OnSelectedIndexChanged">
                                                        
                                                    </asp:DropDownList>
                                                </td>
                                                
                                            </tr>
                                            <tr style="height: 32px">
                                                <td>Emp ID<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEmpid" runat="server" Width="228px" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEmpid_TextChanged"></asp:TextBox>

                                                </td>
                                                <td style="width: 150px"></td>


                                                <td>Emp Name<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtName" runat="server" Width="228px" TabIndex="2" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtName_TextChanged"></asp:TextBox>

                                                </td>

                                            </tr>
                                            <tr style="height: 32px">
                                                <td>Issue Mode
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlFreepaid" runat="server" Width="228px" AutoPostBack="True" CssClass="form-control">
                                                        <asp:ListItem>Chargeble</asp:ListItem>
                                                        <asp:ListItem>Free Issue</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td></td>
                                                <td>Issue Date<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtresourceissue" TabIndex="4" runat="server" size="20" MaxLength="10" CssClass="form-control" Width="228px"
                                                        onkeyup="dtval(this,event)"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CEresourceissue" runat="server" Enabled="true" TargetControlID="txtresourceissue"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                    <cc1:FilteredTextBoxExtender ID="FTBEresourceissue" runat="server" Enabled="True" TargetControlID="txtresourceissue"
                                                        ValidChars="/0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>

                                            <tr style="height: 32px">
                                                <td>No Of Installments
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtnoofinstallments" runat="server" Width="228px" TabIndex="3" Text="1" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>Loan Cutting Month<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtloandate" TabIndex="4" runat="server" size="20" Width="228px" CssClass="form-control"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar1"
                                                        Enabled="true" Format="MMM-yyyy" TargetControlID="txtloandate" DefaultView="Months" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                                    </cc1:CalendarExtender>
                                                </td>
                                            </tr>


                                            <tr style="height: 32px">
                                                <td>Loan ID
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtloanid" runat="server" Width="228px" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td>Uniform ID
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtuniformid" runat="server" Width="228px" TabIndex="3" ReadOnly="true" CssClass="form-control"></asp:TextBox>
                                                </td>
                                            </tr>

                                            <tr style="height: 32px">
                                                <td>Guarantor
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="TxtReferedBy" runat="server" Width="228px" CssClass="form-control"></asp:TextBox>
                                                </td>

                                                <td></td>
                                                <td>Paid Amount
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" Width="228px" ID="txtPaidAmnt" TabIndex="5" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtPaidAmnt_TextChanged"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                        TargetControlID="txtPaidAmnt" ValidChars="0123456789">
                                                    </cc1:FilteredTextBoxExtender>
                                                </td>
                                            </tr>
                                        </table>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtEmpid" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="txtName" EventName="TextChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="rdbIndividualWise" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="rdbToSupervisor" EventName="CheckedChanged" />
                                        <asp:AsyncPostBackTrigger ControlID="rdbFromSupervisor" EventName="CheckedChanged" />

                                    </Triggers>
                                </asp:UpdatePanel>

                                <div align="right" style="margin-right: 30px; margin-top: 10px">
                                    <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"></asp:Label>
                                    
                                    <asp:Button ID="btnSave" runat="server" ValidationGroup="a" Text="SAVE" ToolTip="SAVE"
                                        TabIndex="5" class="btn save" OnClientClick='return confirm("Are you sure you want to generate a new loan?");'
                                        OnClick="btnSave_Click" />
                                    <asp:Button ID="btncancel" runat="server" ValidationGroup="b" TabIndex="6" Text="CANCEL"
                                        ToolTip="CANCEL" class=" btn save" OnClientClick='return confirm("Are you sure you want  to cancel this entry?");' />
                                </div>
                             
                                 <asp:UpdatePanel ID="UpTotal" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <table style="position: relative; top: -45px">
                                            <tr>

                                                <td>
                                                    <asp:Label ID="lbltotalsum" runat="server" Text="Total Sum" Style="margin-left: 20px"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txttotal" Style="margin-left: 67px; width: 228px" Enabled="false" CssClass="form-control"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="btncalculate" runat="server" ImageUrl="~/assets/calculator.png" class="btn save" OnClick="btncalculate_Click" ToolTip="Calculate" Style="margin-left: 2px" />
                                                </td>
                                            </tr>
                                        </table>
                                       


                                        <table style="margin-left: 20px" width="95%">
                                            <tr>
                                                <td>
                                                    <asp:CheckBox ID="ChkGeneral" runat="server" Text="&nbsp;General" Checked="true" AutoPostBack="true" OnCheckedChanged="ChkGeneral_CheckedChanged" />
                                                </td>
                                                
                                            </tr>
                                        </table>

                                         </ContentTemplate>
                                     </asp:UpdatePanel>

                                      
                                <table width="97%" style="margin-top:-30px">
                                    <tr>
                                        
                                            <td >
                                                    <div align="right">
                                                        <%--<asp:CheckBox ID="chkpdf" runat="server" />--%>
                                                    <asp:Button ID="btnPDF" runat="server"  TabIndex="6" Text="PDF" 
                                                        ToolTip="PDF" class=" btn save" OnClick="btnPDF_Click" Style="margin-left: 20px" />
                                                        </div>
                                                </td>
                                       
                                    </tr>
                                </table>
                                   <asp:UpdatePanel ID="UpGv" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                        <%-- AutoPostBack="true" OnCheckedChanged="CbChecked_CheckedChanged" OnRowDataBound="gvresources_databound" OnRowDataBound="gvresources_RowDataBound"--%>
                                        <div class="rounded_corners" style="margin-top: 10px;">
                                            <asp:GridView ID="gvresources" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center; margin: 0px auto" Height="50px" HeaderStyle-HorizontalAlign="Center" >

                                                <Columns>

                                                     <%-- 0 --%>
                                                    <asp:TemplateField HeaderStyle-BackColor="#fcf8e3">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server"  onclick="checkAll(this);  " />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CbChecked" runat="server" Checked="false" EnableViewState="true" AutoPostBack="true" OnCheckedChanged="CbChecked_CheckedChanged"/>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%"></ItemStyle>
                                                    </asp:TemplateField>


                                                     <%-- 1 --%>
                                                    <asp:TemplateField HeaderText="Resource ID" Visible="false" ItemStyle-Font-Italic="true" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresourceid" runat="server" Text='<%#Bind("ResourceID")%>'></asp:Label>
                                                            <%--  --%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                     <%-- 2 --%>
                                                    <asp:TemplateField HeaderText="Resource Name" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Italic="true" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresourcename" runat="server" Text='<%#Bind("ItemName")%>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40%"></ItemStyle>
                                                    </asp:TemplateField>

                                                    
                                                     <%-- 3 --%>

                                                     <asp:TemplateField HeaderText="Balance Qty" ItemStyle-Width="40%" ItemStyle-HorizontalAlign="Left"  ItemStyle-Font-Italic="true" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalanceQty" runat="server" Text='<%#Bind("Balance")%>'></asp:Label>
                                                            <%--  --%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="40%"></ItemStyle>
                                                    </asp:TemplateField>

                                                     <%-- 4 --%>
                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="70px" ItemStyle-Font-Italic="true" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" Width="70px" Text='<%#Bind("Qty")%>' Enabled="true" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtQty_TextChanged1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEQty" runat="server" TargetControlID="txtQty"
                                                                ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <%----%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                     <%--  5--%>
                                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="90px" ItemStyle-Font-Italic="true" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtresourceprice" runat="server" Width="70px" Enabled="false" Text='<%#Bind("Price") %>' CssClass="form-control"></asp:TextBox>
                                                            <%----%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%"></ItemStyle>
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="90px" Visible="false" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txt" runat="server" Width="70px" Enabled="false" Text='<%#Bind("Price") %>' CssClass="form-control"></asp:TextBox>
                                                            <%----%>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="20%"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <br />
                                             <br />


                                              <asp:GridView ID="GVUniformGrid" runat="server" AutoGenerateColumns="False" Width="100%" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center; margin: 0px auto" Height="50px" HeaderStyle-HorizontalAlign="Center" 
                                                  OnRowDataBound="GVUniformGrid_RowDataBound" >

                                                <Columns>
                                                     <%-- 0 --%>

                                                    <asp:TemplateField HeaderStyle-BackColor="#fcf8e3">
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                        </HeaderTemplate>

                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CbChecked" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CbChecked_CheckedChanged" />
                                                        </ItemTemplate>
                                                        <ItemStyle Width="10%"></ItemStyle>
                                                    </asp:TemplateField>


                                                     <%-- 1 --%>
                                                    <asp:TemplateField HeaderText="Resource ID" HeaderStyle-Width="100px"  HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresourceid" runat="server" Text='<%#Bind("ResourceID")%>' Width="100px"></asp:Label>
                                                            <%--  --%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>



                                                     <%-- 2 --%>
                                                    <asp:TemplateField HeaderText="Resource Name"  ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3" >
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresourcename" runat="server" Text='<%#Bind("ItemName")%>' Width="200px"></asp:Label>
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>

                                                    
                                                     <%-- 3 --%>

                                                     <asp:TemplateField HeaderText="Balance Qty"  ItemStyle-HorizontalAlign="Left"  HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblBalanceQty" runat="server" Text='<%#Bind("Balance")%>' Width="70px"></asp:Label>
                                                            <%--  --%>
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>

                                                     <%-- 4 --%>
                                                    <asp:TemplateField HeaderText="Quantity" HeaderStyle-Width="70px" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" Width="70px" Text='<%#Bind("Qty")%>' Enabled="true" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtQty_TextChanged1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEQty" runat="server" TargetControlID="txtQty"
                                                                ValidChars="0123456789.">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <%----%>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>


                                                     <%--  5--%>
                                                    <asp:TemplateField HeaderText="Price" HeaderStyle-Width="90px" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtresourceprice" runat="server" Width="90px" Enabled="false" Text='<%#Bind("Price") %>' CssClass="form-control"></asp:TextBox>
                                                            <%----%>
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>


                                                    <asp:TemplateField HeaderText="Category" HeaderStyle-Width="90px" Visible="false" HeaderStyle-BackColor="#fcf8e3">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCategory" runat="server" Width="70px" Enabled="false" Text='<%#Bind("category") %>' CssClass="form-control"></asp:Label>
                                                            <%----%>
                                                        </ItemTemplate>
                                                        
                                                    </asp:TemplateField>


                                                </Columns>
                                            </asp:GridView>





                                            <asp:Label ID="lblTotalamt" runat="server" Text=""></asp:Label>

                                        </div>

                                     </ContentTemplate>
                                    <%-- <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />
                                    </Triggers>--%>
                                </asp:UpdatePanel>




                            </div>

                            <%--   <div class="loading" align="center">

                                    <img src="assets/loader.gif" alt="" />
                                </div>--%>


                            <div class="clear">
                            </div>
                        </div>
                    </div>
                    <%--   </div>--%>
                </div>
                <div class="clear">
                </div>
                <!-- DASHBOARD CONTENT END -->
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

                setProperty();
                GetEmpid();
                GetEmpName();
               
                jQuery(document).ready(function mchoose() {
                    jQuery(".chosen").data("placeholder", "Select").chosen();
                });

            });
        };
    </script>
</body>


</html>
