﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientManPowerReq.aspx.cs" Inherits="Jawan.Portal.ClientManPowerReq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Client ManPower Req</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />
    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

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
                select: function (event, ui) { $("#ddlclientid").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                select: function (event, ui) { $("#ddlcname").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientnamechange(event, ui); },
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLClientidchange(event, ui) {
            $('#ddlclientid').trigger('change');

        }

        function OnAutoCompleteDDLClientnamechange(event, ui) {

            $('#ddlcname').trigger('change');
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
    <form id="contracts1" runat="server">
        <asp:ScriptManager runat="server" ID="Scriptmanager1">
        </asp:ScriptManager>
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
                        <li><a href="clients.aspx" id="ClientsLink" runat="server" class="current"><span>Clients</span></a></li>
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
                                        <%--     <div class="submenubeforegap">
                                        &nbsp;</div>
                                <div class="submenuactions">
                                        &nbsp;</div>  --%>
                                        <ul>

                                            <li><a href="contracts.aspx" id="ContractLink" runat="server"><span>Contracts</span></a></li>
                                            <li><a href="ClientLicenses.aspx" id="LicensesLink" runat="server"><span>Licenses</span></a></li>
                                            <li><a href="clientattendance.aspx" id="ClientAttendanceLink" runat="server"><span>Attendance</span></a></li>
                                            <li><a href="AssigningClients.aspx" id="Operationlink" runat="server"><span>Operations</span></a></li>
                                            <li><a href="ClientBilling.aspx" id="BillingLink" runat="server"><span>Billing</span></a></li>
                                            <li><a href="MaterialRequisitForm.aspx" id="MRFLink" runat="server"><span>MRF</span></a></li>
                                            <li><a href="ApproveMRF.aspx" id="ApproveMRFLink" runat="server"><span>ApproveMRF</span></a></li>
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

                    <div class="panel panel-inverse" style="height: 900px">
                        <div class="panel-heading">
                            <table width="100%">
                                <tr>
                                    <td>
                                        <h3 class="panel-title">Shift Details</h3>
                                    </td>
                                    <td align="right"><< <a href="Clients.aspx" style="color: #003366">Back</a>  </td>
                                </tr>
                            </table>

                        </div>
                        <div class="panel-body">


                            <div align="center">
                                <asp:Label ID="lblMsg" runat="server" Style="border-color: #f0c36d; background-color: #f9edbe; width: auto; font-weight: bold; color: #CC3300;"></asp:Label>
                            </div>
                            <div align="center">
                                <asp:Label ID="lblSuc" runat="server" Style="border-color: #f0c36d; background-color: #f9edbe; width: auto; font-weight: bold; color: #000;"></asp:Label>
                            </div>

                            <table width="100%" cellpadding="5" cellspacing="5" style="margin-top: 20px">

                                <tr>
                                    <td width="53%">
                                        <table width="100%">
                                            <tr>
                                                <td width="190px">Client ID<span style="color: Red">*</span>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlclientid" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="True" OnSelectedIndexChanged="ddlclientid_OnSelectedIndexChanged"
                                                        Width="120px">
                                                    </asp:DropDownList>


                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td valign="top">
                                        <table width="100%">
                                            <tr>
                                                <td>Client Name<span style="color: Red">*</span>
                                                </td>
                                                <td>

                                                    <asp:DropDownList ID="ddlcname" runat="server" placeholder="select" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged"
                                                        Style="width: 355px">
                                                    </asp:DropDownList>




                                                </td>
                                            </tr>

                                        </table>
                                    </td>
                                </tr>
                            </table>

                        </div>


                        <%--class="dashboard_full"--%>
                        <div style="font-family: Arial; font-weight: normal; font-variant: normal; min-height: 100px; height: auto; font-size: 13px; overflow: auto; padding-left: 80px"
                            class="rounded_corners">
                            <%--; overflow: scroll--%>
                            <asp:GridView ID="gvdesignation" runat="server" Width="90%" Height="50%" Style="margin-left: 5px"
                                AutoGenerateColumns="False" CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None" HorizontalAlign="Center" OnSelectedIndexChanged="gvdesignation_SelectedIndexChanged1">
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle Height="3px" BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <RowStyle Height="1px" BackColor="#EFF3FB" />
                                <Columns>

                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblsno" Text='<%#Container.DataItemIndex+1%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Designation" HeaderStyle-Width="200px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="DdlDesign" OnSelectedIndexChanged="ddlshift_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%">
                                                <asp:ListItem Selected="True" Value="0">--Select Designation-- </asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Height="3px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlshift" OnSelectedIndexChanged="ddlshift_SelectedIndexChanged" AutoPostBack="true" runat="server" Width="100%">
                                                <asp:ListItem Selected="True" Value="0">--Select Shift-- </asp:ListItem>
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Height="3px" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift Start Time" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Height="3px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSStarttime" runat="server" Enabled="false" Width="100%" Style="text-align: center"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Shift End Time" HeaderStyle-Width="100px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemStyle Height="3px" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSEndtime" runat="server" Width="100%" Enabled="false" Style="text-align: center"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Qty" HeaderStyle-Width="80px" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQty" runat="server" Width="100%" Style="text-align: center"> </asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                TargetControlID="txtQty" ValidChars="0123456789.">
                                            </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                        <div>
                            <br />
                            <div style="margin-left: 90px; float: left">
                                <asp:Button ID="btnadddesgn" runat="server" class="btn save" Text="Add Designation"
                                    OnClick="btnadddesgn_Click1" Style="width: 125px" /><br />

                            </div>

                            <div style="margin-right: 90px; float: right">
                                <asp:Button ID="btnsave" runat="server" class="btn save" Text="Save" OnClick="Btn_Save_Contracts_Click" Style="width: 125px" /><br />
                            </div>

                            <br />
                        </div>



                    </div>
                    <div class="clear">
                    </div>
                    <!-- DASHBOARD CONTENT END -->
                </div>
            </div>
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS<!--<img 
            alt="Powered by Businessface" src:"Pages/assets/footerlogo.png"/>--></a>
                    </div>
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
        <!-- CONTENT AREA END -->
    </form>
</body>
</html>

