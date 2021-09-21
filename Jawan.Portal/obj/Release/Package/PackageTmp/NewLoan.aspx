<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewLoan.aspx.cs" Inherits="Jawan.Portal.NewLoan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>NEW LOAN</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />

      <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

    <%--  <style type="text/css">
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
    </style>--%>
        <style type="text/css">
        .lbl-thin {
            font-weight: 100 !important;
        }

        #fade {
            display: none;
            position: absolute;
            top: 0%;
            left: 0%;
            width: 100%;
            height: 2000px;
            background-color: #ababab;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .70;
            filter: alpha(opacity=80);
        }

        #modal {
            display: none;
            position: absolute;
            top: 45%;
            left: 45%;
            width: 100px;
            height: 100px;
            padding: 30px 15px 0px;
            border: 3px solid #ababab;
            box-shadow: 1px 1px 10px #ababab;
            border-radius: 20px;
            background-color: white;
            z-index: 1002;
            text-align: center;
            overflow: auto;
        }

        #results {
            font-size: 1.25em;
            color: red;
        }

        .ui-autocomplete {
            max-height: 200px;
            overflow-y: auto; /* prevent horizontal scrollbar */
            overflow-x: hidden;
        }
        /* IE 6 doesn't support max-height
   * we use height instead, but this forces the menu to always be this tall
   */ * html .ui-autocomplete {
            height: 200px;
        }

        .custom-combobox {
            position: relative;
            display: inline-block;
            width: 84%;
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
            width: 100%;
        }

        .btnhgtwt {
            top: 0px;
            height: 31px;
        }

        .num-txt {
            padding: 0 5px;
            width: 40px;
        }
    </style>

    <style type="text/css">
        #social div
        {
            display: block;
        }
        .HeaderStyle
        {
            text-align: Left;
        }
        .style3
        {
            height: 24px;
        }
        
         .modalBackground
            {
            background-color: Gray;
            z-index: 10000;
            }
        
           .slidingDiv
        {
            background-color: #99CCFF;
            padding: 10px;
            margin-top: 10px;
            border-bottom: 5px solid #3399FF;
        }
        .show_hide
        {
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


        $(document).ready(function () {
            $('#txtEmpid').autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: 'Autocompletion.asmx/GetFormEmpIDs',
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
                    $("#txtEmpid").attr("data-Empid", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                }
            });




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
        });

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $('#txtEmpid').trigger('change');

        }
        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $('#txtName').trigger('change');

        }


        // auto suggest

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
                select: function (event, ui) { $("#ddlEmpId").attr("data-EmpId", ui.item.value); OnAutoCompleteDDLEmpIDchange(event, ui); },
                select: function (event, ui) { $("#ddlEmpName").attr("data-EmpId", ui.item.value); OnAutoCompleteDDLEmpNamechange(event, ui); },
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLEmpIDchange(event, ui) {
            $('#ddlEmpId').trigger('change');

        }

        function OnAutoCompleteDDLEmpNamechange(event, ui) {

            $('#ddlEmpName').trigger('change');
        }
        

    </script>
</head>
<body>
    <form id="NewLoan1" runat="server">
    <asp:ScriptManager runat="server" ID="Scriptmanager1">
    </asp:ScriptManager>
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE" /></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="employeeslink" runat="server" class="current">
                        <span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
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
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                                    <%--  <div class="submenubeforegap">
                                        &nbsp;</div>--%>
                                    <ul>
                                        <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li class="current"><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                        <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payments</span></a></li>
                                        <%--          <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
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
        <!-- SUBNAVIGATION SECTION END -->
    </div>
    <!-- HEADER SECTION END -->
    <!-- CONTENT AREA BEGIN -->
    <div id="content-holder">
        <div class="content-holder" style="height: auto">
            <h1 class="dashboard_heading">
                Loans Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                
                        
                           <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               New Loan
                                 
                            </h2>
                            
                               
                        
                    <div class="contentarea" id="Div1">
                        <div class="boxinc">
                            <ul>
                                <li class="left leftmenu">
                                    <ul>
                                        <li><a href="NewLoan.aspx" class="sel">New Loan</a></li>
                                        <li><a id="A1" href="ModifyLoan.aspx" runat="server">Modify Loan</a> </li>
                                        <li><a href="LoanRecovery.aspx">Recovery Details</a></li>
                                        <li><a href="LoanRepayment.aspx">Loan Repayment</a></li>
                                        <li><a href="EmpResourceAllocation.aspx">Resource Issue</a></li>
                                         <li><a href="ResourceReturnEmp.aspx" runat="server" id="ResourceReturnLink">Resource Return</a></li>  
                                    </ul>
                                </li>
                                <li class="right">
                                    <div class="dashboard_firsthalf" style="width:100%;margin-left:20px">
                                        <table width="100%">
                                            <tr>
                                                <td valign="top" width="45%">
                                                    <table cellpadding="5" cellspacing="5">
                                                        <tr style="height:32px">
                                                            <td width="100px">
                                                                Emp ID<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                         <%--   <asp:TextBox ID="txtEmpid" runat="server"  CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEmpid_TextChanged" Width="190px"></asp:TextBox>  --%>

                                                                <asp:DropDownList ID="ddlEmpId" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpId_SelectedIndexChanged" Width="120px">

                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr style="display: none">
                                                            <td>
                                                                Middle Name
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlempmname" class="sdrop" AutoPostBack="True"
                                                                    >
                                                                </asp:DropDownList>

                                                                  
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr style="height:32px">
                                                            <td valign="top">
                                                                Loan Amount<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtNewLoan" class="form-control" Width="190px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                    TargetControlID="txtNewLoan" ValidChars="/0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:32px">
                                                            <td>
                                                                Description
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TextMode="MultiLine" ID="txtDescripition"  class="form-control" Width="190px"> </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td>
                                                    <table cellpadding="5" cellspacing="5">
                                                        <tr style="height:32px">
                                                            <td width="140px">
                                                                First Name
                                                            </td>
                                                            <td>
                                                           <%--     <asp:TextBox ID="txtName" runat="server"  TabIndex="2" class="form-control" Width="190px" AutoPostBack="true" OnTextChanged="txtName_TextChanged"></asp:TextBox> --%>

                                                                <asp:DropDownList ID="ddlEmpName" runat="server" placeholder="select" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlEmpName_SelectedIndexChanged" style="width:355px">

                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                           <tr style="height:32px">
                                                            <td>
                                                                Loan Type<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:DropDownList runat="server" ID="ddlLoanType" class="form-control">
                                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                                    <asp:ListItem>Sal. Adv</asp:ListItem>
                                                                    <asp:ListItem>Uniform</asp:ListItem>
                                                                    <asp:ListItem>Security Deposit</asp:ListItem>
                                                                    <asp:ListItem>Loan</asp:ListItem>
                                                                    <asp:ListItem>ATM</asp:ListItem>
                                                                    <asp:ListItem>Others</asp:ListItem>
                                                                    <asp:ListItem>ID card deduction</asp:ListItem>
                                                                    <asp:ListItem>Telephone Bill</asp:ListItem>
                                                                    <asp:ListItem>PF & ESI Contribution</asp:ListItem>
                                                                     <asp:ListItem>TDS Ded </asp:ListItem>
                                                                    
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:32px">
                                                            <td>
                                                                No. Of Installments<span style="color: Red">*</span>
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" ID="txtnoofinstall" class="form-control" Width="190px"></asp:TextBox>
                                                                <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                                    TargetControlID="txtnoofinstall" ValidChars="0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:32px">
                                                            <td>
                                                                Loan Cutting Month
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtLoanDate" TabIndex="11" runat="server" class="form-control" Width="190px" MaxLength="10"
                                                                    onkeyup="dtval(this,event)"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CELoanDate" runat="server" Enabled="true" TargetControlID="txtLoanDate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FTBELoanDate" runat="server" Enabled="True" TargetControlID="txtLoanDate"
                                                                    ValidChars="-/0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:32px">
                                                            <td>
                                                                Loan Issue Date
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtloanissuedate" TabIndex="11" runat="server" class="form-control" Width="190px" MaxLength="10"
                                                                    onkeyup="dtval(this,event)"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CEloanissuedate" runat="server" Enabled="true" TargetControlID="txtloanissuedate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FTBEloanissuedate" runat="server" Enabled="True"
                                                                    TargetControlID="txtloanissuedate" ValidChars="-/0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </td>
                                                        </tr>
                                                        <tr style="height:32px">
                                                            <td>
                                                                Loan No.
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="txtloanid" runat="server" Enabled="False" class="form-control" Width="190px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                &nbsp;
                                                            </td>
                                                            <td align="right">
                                                                <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"></asp:Label>
                                                                <asp:Button ID="Button1" runat="server" ValidationGroup="a" Text="SAVE" ToolTip="SAVE"
                                                                    class="btn save" OnClick="Button1_Click" OnClientClick='return confirm("Are you sure you want to generate a new loan?");' />
                                                                <asp:Button ID="btncancel" runat="server" ValidationGroup="b" Text="CANCEL" ToolTip="CANCEL"
                                                                    class=" btn save" OnClientClick='return confirm("Are you sure you want  to cancel this entry?");' />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </li>
                            </ul>
                            <div class="rounded_corners">
                                <asp:GridView ID="gvNewLoan" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None" Style="text-align: center">
                                    <RowStyle BackColor="#EFF3FB" Height="30px" />
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
                                    
                                        <asp:TemplateField HeaderText="LoanID" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLoanid" runat="server" Text="<%#Bind('LoanId')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Amount" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLoanAmount" runat="server" Text="<%#Bind('LoanAmount')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Loan Type" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLoanType" runat="server" Text="<%#Bind('TypeOfLoan')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                          </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Due Amount" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDueAmount" runat="server" Text=""></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="No.of.Installments" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblNoofInstalments" runat="server" Text="<%#Bind('NoInstalments')%>"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Date" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblDate" runat="server" Text='<%#Eval("LoanDt", "{0:dd/MM/yyyy}")%>'></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Loan Status" ItemStyle-Width="60px">
                                            <ItemTemplate>
                                                <asp:Label ID="lblLoanStatus" runat="server" Text='<%# (Eval("LoanStatus")!=DBNull.Value ? ((Convert.ToBoolean(Eval("LoanStatus"))!=false)? "Completed":"Incomplete"):"NULL")%>'>Completed</asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle HorizontalAlign="Center" />
                                            <ItemStyle Width="60px"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:GridView ID="gvresources" runat="server" CellPadding="4" CellSpacing="3" AutoGenerateColumns="false"
                                    ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="CbChecked" runat="server" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text="<%#Bind('ResourceId') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text="<%#Bind('Resourcename') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price1">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text="<%#Bind('Price') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price2">
                                            <ItemTemplate>
                                                <asp:TextBox ID="lblsno" runat="server" Text=""></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>

                                 <div align="right" style="visibility:hidden"> <b>Import Data: </b> <asp:FileUpload  ID="fileupload1" runat="server" Width="170px"/> 
                 <asp:Button ID="btnImportData" runat="server" ValidationGroup="b"
                     Text="Import"  
                        class=" btn save" onclick="btnImportData_Click"  /></div> 
                            
                        </div>
                                </div>
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
</body>
</html>
