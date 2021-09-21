<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddClient.aspx.cs" Inherits="Jawan.Portal.AddClient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ADD CLIENT</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style3 {
            height: 24px;
        }
    </style>
    <script type="text/javascript" src="script/jscript.js">
    </script>

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

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
                select: function (event, ui) { $("#ddlLinkedClient").attr("data-clientId", ui.item.value); },
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });


    </script>

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
    </style>

</head>
<body>
    <form id="Clients1" runat="server">
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
                        <li>
                            <a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
                        <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                        <%--  <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div> --%>
                                        <ul>

                                            <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                            <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                            <li><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server"><span>Attendance</span></a></li>
                                            <li><a href="ClientBilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a>   </li>
                                            <li><a href="Receipts.aspx" id="ReceiptsLink" runat="server"><span>Receipts</span></a></li>

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
                <div class="col-md-12" style="margin-top: 8px; margin-bottom: 8px">
                    <div align="center">
                        <asp:Label ID="lblMsg" runat="server" Style="border-color: #f0c36d; background-color: #f9edbe; width: auto; font-weight: bold; color: #CC3300;"></asp:Label>
                    </div>
                    <div align="center">
                        <asp:Label ID="lblSuc" runat="server" Style="border-color: #f0c36d; background-color: #f9edbe; width: auto; font-weight: bold; color: #000;"></asp:Label>
                    </div>
                    <div class="panel panel-inverse">
                        <div class="panel-heading">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Add Client</h3>


                                    </td>
                                    <td align="right"><< <a href="Clients.aspx" style="color: #003366">Back</a>  </td>
                                </tr>
                            </table>


                        </div>
                        <div class="panel-body">
                            <asp:ScriptManager runat="server" ID="Scriptmanager1">
                            </asp:ScriptManager>



                            <div class="dashboard_firsthalf" style="width: 100%">
                                <table width="100%" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td valign="top">

                                            <table width="100%" cellpadding="5" cellspacing="5">
                                                <tr>
                                                    <td>Client ID
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtCId" class="sinput" ReadOnly="true" TabIndex="1" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Short Name
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtshortname" class="sinput" TabIndex="3" runat="server" MaxLength="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Contact Person<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtcontactperson" runat="server" TabIndex="5" class="sinput" MaxLength="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Phone No(s)<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtphonenumbers" runat="server" TabIndex="7" class="sinput" MaxLength="50"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                            ControlToValidate="txtphonenumbers" SetFocusOnError="true" Display="Dynamic" ValidationGroup="a" Text="*"></asp:RequiredFieldValidator>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                            runat="server" Enabled="True" TargetControlID="txtphonenumbers"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>


                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Email-ID
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtemailid" runat="server" class="sinput" TabIndex="9" MaxLength="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Branch<span style="color: Red">*</span>

                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlBranch" class="sdrop" TabIndex="11"></asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>Our GSTIN
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlOurGSTIN" runat="server" class="sdrop" TabIndex="9"></asp:DropDownList>

                                                    </td>
                                                </tr>

                                                <tr runat="server" visible="false">
                                                    <td>Department
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddldepartment" runat="server" TabIndex="32"
                                                            class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr runat="server" visible="false">
                                                    <td>Division
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlDivision" runat="server" TabIndex="31"
                                                            class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr runat="server" visible="false">
                                                    <td>ESI Branch
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlESIBranch" runat="server" TabIndex="31"
                                                            class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>

                                                <tr style="font-weight: bold">
                                                    <td colspan="2">Billing Details
                                                    </td>

                                                </tr>


                                                <tr>
                                                    <td style="font-weight: bold; text-decoration: underline">Bill To
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 1</td>
                                                    <td>
                                                        <asp:TextBox ID="txtchno" runat="server" class="sinput" TabIndex="12">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 2</td>
                                                    <td>
                                                        <asp:TextBox ID="txtstreet" runat="server" class="sinput" TabIndex="12">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 3</td>
                                                    <td>
                                                        <asp:TextBox ID="txtarea" runat="server" class="sinput" TabIndex="12">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 4</td>
                                                    <td>
                                                        <asp:TextBox ID="txtcolony" runat="server" class="sinput" TabIndex="12">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 5</td>
                                                    <td>
                                                        <asp:TextBox ID="txtcity" runat="server" class="sinput" TabIndex="12">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 6</td>
                                                    <td>
                                                        <asp:TextBox ID="txtstate" runat="server" class="sinput" TabIndex="12">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>State
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlstate" TabIndex="19" class="sdrop" AutoPostBack="true" OnSelectedIndexChanged="ddlstate_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>State Code</td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlStateCode" TabIndex="21" class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>GSTIN/Unique ID </td>
                                                    <td>
                                                        <asp:TextBox ID="txtGSTUniqueID" runat="server" TabIndex="21"
                                                            class="sinput"></asp:TextBox>
                                                    </td>
                                                </tr>

                                                <%--  <tr>
                                                    <td>PAN No </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCPanno" runat="server" TabIndex="21"
                                                            class="sinput"></asp:TextBox>
                                                    </td>
                                                </tr>--%>


                                                <tr>

                                                    <td>Description(if any)
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdescription" runat="server" TabIndex="17" TextMode="multiline" MaxLength="500"
                                                            class="sinput"></asp:TextBox>

                                                    </td>
                                                </tr>


                                                <tr style="display: none">
                                                    <td>Our Contact Person
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlEmpId" TabIndex="19" class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>



                                                <tr>
                                                    <td>Location&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtLocation" runat="server" TabIndex="20" class="sinput"></asp:TextBox>
                                                        &nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td>Buyer's Order No&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txtBuyerOrderNo" runat="server" TabIndex="20" class="sinput"></asp:TextBox>
                                                        &nbsp;</td>
                                                </tr>


                                                <tr>
                                                    <td>Field Officer </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlstaffid" TabIndex="19" class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>


                                            </table>

                                        </td>

                                        <td valign="top" align="right">
                                            <table width="100%" cellpadding="5" cellspacing="5">
                                                <tr>
                                                    <td>Name<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtCname" TabIndex="2" class="sinput" MaxLength="200"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Client Name (Official Use)</td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtCNameOfficial" TabIndex="4" class="sinput"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Segment 
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlsegment" runat="server" ValidationGroup="a" TabIndex="6" class="sdrop">
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Person Designation<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddldesgn" runat="server" ValidationGroup="a" class="sdrop" TabIndex="8">
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Landline No.</td>
                                                    <td class="style3">
                                                        <asp:TextBox ID="txtpin" runat="server" TabIndex="10" class="sinput" MaxLength="7"> </asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3"
                                                            runat="server" Enabled="True" TargetControlID="txtpin"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Fax No.</td>
                                                    <td>
                                                        <asp:TextBox ID="txtfaxno" runat="server" TabIndex="11" class="sinput" MaxLength="30"></asp:TextBox>
                                                        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                            runat="server" Enabled="True" TargetControlID="txtfaxno"
                                                            ValidChars="0123456789">
                                                        </cc1:FilteredTextBoxExtender>
                                                    </td>
                                                </tr>


                                                <tr runat="server" visible="false">
                                                    <td>KEONICS</td>
                                                    <td>
                                                        <asp:RadioButton ID="rdbkeonicsyes" runat="server" Text="YES" GroupName="KEONICS" TabIndex="22" AutoPostBack="true" OnCheckedChanged="rdbkeonicsyes_CheckedChanged" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdbkeonicsNo" runat="server" Text="NO" GroupName="KEONICS" TabIndex="21" AutoPostBack="true" OnCheckedChanged="rdbkeonicsyes_CheckedChanged" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblKeonicsClientID" runat="server" Text="Linked Client ID" Visible="false"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlLinkedClient" TabIndex="21" Visible="false" CssClass="ddlautocomplete chosen-select">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>

                                                <tr>
                                                    <td style="font-weight: bold; text-decoration: underline; padding-top: 26px">Ship To
                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 1</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToLine1" runat="server" class="sinput" TabIndex="13">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 2</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToLine2" runat="server" class="sinput" TabIndex="13">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 3</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToLine3" runat="server" class="sinput" TabIndex="13">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 4</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToLine4" runat="server" class="sinput" TabIndex="13">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 5</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToLine5" runat="server" class="sinput" TabIndex="13">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="padding-left: 40px">Line 6</td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToLine6" runat="server" class="sinput" TabIndex="13">
                                                        </asp:TextBox>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>State
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlShipToSate" TabIndex="19" class="sdrop" AutoPostBack="true" OnSelectedIndexChanged="ddlBillToSate_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>State Code</td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlShipToStateCode" TabIndex="21" class="sdrop">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>GSTIN/Unique ID </td>
                                                    <td>
                                                        <asp:TextBox ID="txtShipToGSTIN" runat="server" TabIndex="21"
                                                            class="sinput"></asp:TextBox>
                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td>
                                                        <asp:CheckBox runat="server" ID="chkSubUnit" Text=" Sub Unit" TabIndex="18"
                                                            AutoPostBack="True" OnCheckedChanged="chkSubUnit_CheckedChanged" />
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlUnits" TabIndex="21" class="sdrop" Visible="false">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Main Unit<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="radioyesmu" runat="server" Text="YES" GroupName="mainunit" TabIndex="22" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radionomu" runat="server" Text="NO" GroupName="mainunit" TabIndex="21" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Invoice<span style="color: Red">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:RadioButton ID="radioinvoiceyes" runat="server" Text="YES" GroupName="invoice" TabIndex="23" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radioinvoiceno" runat="server" Text="NO" GroupName="invoice" TabIndex="22" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>PaySheet<span style="color: Red">*</span>
                                                    </td>

                                                    <td>
                                                        <asp:RadioButton ID="radiopaysheetyes" runat="server" Text="YES" GroupName="paysheet" TabIndex="24" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="radiopaysheetno" runat="server" Text="NO" GroupName="paysheet" TabIndex="23" />
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>PT State
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList runat="server" ID="ddlPTState" TabIndex="19" class="sdrop" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td style="padding-left: 50px; padding-top: 20px">
                                                        <asp:Button ID="btnaddclint" runat="server" Text="Save" ToolTip="Add Client" class=" btn save" TabIndex="25"
                                                            ValidationGroup="a1" OnClick="btnaddclint_Click"
                                                            OnClientClick='return confirm(" Are you sure you want to add the client details ?");' />
                                                        <asp:Button ID="btncancel" runat="server" Text="Cancel" ToolTip="Cancel Client"
                                                            OnClientClick='return confirm(" Are you sure you want to cancel this entry ?");'
                                                            class=" btn save" OnClick="btncancel_Click" TabIndex="25" /></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>

                                            </table>
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
        <!-- DASHBOARD CONTENT END -->

        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS<!--<img 
            alt="Powered by Businessface" src:"Pages/assets/footerlogo.png"/>--></a>
                </div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>|
                    <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
        </div>
    <!-- CONTENT AREA END -->
    </form>
</body>
</html>


