<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_Add_Lead_Requirement.aspx.cs" Inherits="Jawan.Portal.M_Add_Lead_Requirement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LEAD REQUIREMENT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>

    <script type="text/javascript">

        function pageLoad(sender, args) {
            if (!args.get_isPartialLoad()) {
                //  add our handler to the document's
                //  keydown event
                $addHandler(document, "keydown", onKeyDown);
            }
        }

        function onKeyDown(e) {
            if (e && e.keyCode == Sys.UI.Key.esc) {
                // if the key pressed is the escape key, dismiss the dialog

                $find('Modbilldetails').hide();
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
                select: function (event, ui) { $("#DropLeadID").attr("data-LeadID", ui.item.value); OnAutoCompleteDDLLeadIDchange(event, ui); },
                select: function (event, ui) { $("#DropLeadName").attr("data-LeadID", ui.item.value); OnAutoCompleteDDLLeadNamechange(event, ui); },
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLLeadIDchange(event, ui) {
            $('#DropLeadID').trigger('change');
        }

        function OnAutoCompleteDDLLeadNamechange(event, ui) {
            $('#DropLeadName').trigger('change');
        }



        function closeFunction() {

            var e = jQuery.Event("keyup"); // or keypress/keydown
            e.keyCode = 27; // for Esc
            $(document).trigger(e); // 


        }
    </script>
    <style type="text/css">
        .PnlBackground {
            background-color: rgba(128, 128, 128,0.5);
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

        .chosen {
            width: 250px;
        }

        .custom-combobox-input {
            margin: 0;
            padding: 5px 10px;
        }

        .close {
            cursor: pointer;
            position: absolute;
            right: 0%;
            padding: 12px 16px;
            transform: translate(0%, -50%);
        }

            .close:hover {
                background: #bbb;
            }
    </style>
</head>
<body>
    <form id="Leads" runat="server" autocomplete="off">

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
                        <li class="first"><a href="Employees.aspx" id="Employeeslink" visible="false" runat="server" class="current">
                            <span>Leads</span></a></li>
                        <li class="after"><a href="Clients.aspx" id="ClientsLink" visible="false" runat="server"><span>Calendar</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" visible="false" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" visible="false" runat="server"><span>Inventory</span></a></li>
                        <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" visible="false" runat="server"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" visible="false" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" visible="false" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
                         <li class="last"><a href="M_Leads.aspx" id="LinkMarketting"  runat="server"><span><span>Marketting</span></span></a></li>
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
                                            <li class="first"><a href="M_Leads.aspx" id="A1" runat="server" class="current">
                                                <span>Leads</span></a></li>
                                            <li class="after"><a href="M_All_Action_Log_Scheduler.aspx" id="A2" runat="server"><span>Calendar</span></a></li>
                                            <li class="after"><a href="MKTG_Reports.aspx" id="A3" runat="server"><span>MKTG Reports</span></a></li>
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
            <div class="content-holder" style="background-color: #f2f2f2">

                <asp:ScriptManager runat="server" ID="Scriptmanager1">
                </asp:ScriptManager>
                <asp:UpdatePanel ID="up1" runat="server">
                    <ContentTemplate>
                        <div class="container">

                            <table width="100%" style="margin-top: -10px; margin-bottom: 10px">
                                <tr>
                                    <td align="right"><< <a href="M_Leads.aspx" style="color: #003366">Back</a>  </td>
                                </tr>
                            </table>


                            <table class="header" style="margin-bottom: 10px">
                                <tr style="font-weight: bold; height: 25px; font-size: 17px; text-align: center">
                                    <td>LEAD REQUIREMENT
                                    </td>
                                </tr>
                            </table>

                            <table cellpadding="5" cellspacing="5" width="100%" style="margin: 10px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLeadID" runat="server" Text="Lead ID"></asp:Label>
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="DropLeadID" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="DropLeadID_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblLeadName" runat="server" Text="Lead Name"></asp:Label>
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="DropLeadName" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="DropLeadName_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnDownloadQuotation" runat="server" Text="Download Quotation" />
                                    </td>
                                </tr>
                            </table>


                            <cc1:ModalPopupExtender ID="ModalViewDetails" runat="server" TargetControlID="btnDownloadQuotation" PopupControlID="pnlDetails"
                                BackgroundCssClass="PnlBackground" BehaviorID="Modbilldetails">
                            </cc1:ModalPopupExtender>
                            <br />
                            <br />
                            <asp:Panel ID="pnlDetails" runat="server" Height="310px" Width="420px" Style="display: none; padding: 15px; position: absolute; background-color: white; border-radius: 5px; box-shadow: 7px 7px 5px #888888;">

                                <asp:Button runat="server" class="close" ID="closebtn" OnClientClick="return closeFunction()" Text="X" Style="line-height: 2px; padding: 10px; margin-right: 5px;"></asp:Button>



                                <asp:GridView ID="GridView1" runat="server"
                                    AutoGenerateColumns="false" CssClass="table" Style="margin-top: 25px">
                                    <Columns>

                                        <asp:BoundField DataField="Name" HeaderText="File Name" />
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkDownload" runat="server" Text="Download" OnClick="DownloadFile"
                                                    CommandArgument='<%# Eval("Id") %>'></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <br />

                            </asp:Panel>


                            <div class="panel panel-default">
                                <div class="panel-heading" style="font-size: 14px; font-weight: bold">Requirement Details</div>
                                <div class="panel-body">

                                    <div id="forExport" style="overflow-x: scroll; margin-left: 10px; margin-right: 50px; width: 98%;">
                                        <asp:GridView ID="GVLeadsamount" runat="server" AutoGenerateColumns="False" ShowFooter="true"
                                            CellPadding="4" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed table-hover"
                                            Style="overflow: scroll; width: 950px" OnRowDataBound="GVLeadsamount_RowDataBound" FooterStyle-HorizontalAlign="Center">

                                            <Columns>

                                                <%-- 0--%>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- 1--%>
                                                <asp:BoundField DataField="State" HeaderText="State" HeaderStyle-Font-Bold="true" />
                                                <%-- 2--%>
                                                <asp:BoundField DataField="zonename" HeaderText="Zone" HeaderStyle-Font-Bold="true" />
                                                <%-- 3--%>
                                                <asp:BoundField DataField="typename" HeaderText="Type" HeaderStyle-Font-Bold="true" />
                                                <%-- 4--%>
                                                <asp:BoundField DataField="categoryname" HeaderText="Category" HeaderStyle-Font-Bold="true" />

                                                <%-- 5--%>
                                                <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesgnationname" runat="server" Font-Bold="true" Text='<%#Bind("Design") %>'></asp:Label>
                                                        <asp:Label ID="lbldesgnation" runat="server" Visible="false" Text='<%#Bind("DesignationId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotal" Text="Totals"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 6--%>
                                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblresult" runat="server" Text='<%#Bind("TotalAmount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 7--%>
                                                <asp:TemplateField HeaderText="Service Charge" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblscharge" runat="server" Text='<%#Bind("servicecharge") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblschargeamount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 8--%>
                                                <asp:TemplateField HeaderText="CGST" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridcgst" runat="server" Text='<%#Bind("CGSTamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblcgstamunt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 9--%>
                                                <asp:TemplateField HeaderText="SGST" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridSgst" runat="server" Text='<%#Bind("SGSTamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblsgstamunt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 10--%>
                                                <asp:TemplateField HeaderText="IGST" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridIgst" runat="server" Text='<%#Bind("IGSTamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbligstamunt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 11--%>
                                                <asp:TemplateField HeaderText="Grand Total" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgrandtotal" runat="server" Text='<%#Bind("GrandTotal") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblgrandtotalamount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 12--%>
                                                <asp:TemplateField HeaderText="Basic" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbasicamount" runat="server" Text='<%#Bind("Basic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblbasictotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 13--%>

                                                <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderStyle Width="15px" />
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblda" runat="server" Text='<%#Bind("DA") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbldatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 14--%>
                                                <asp:TemplateField HeaderText="HRA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblhra" Text='<%# Bind("HRA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblhratotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 15--%>
                                                <asp:TemplateField HeaderText="Conveyence" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblconveyence" runat="server" Text='<%#Bind("Conveyence") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblconvtotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 16--%>
                                                <asp:TemplateField HeaderText="CCA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcca" runat="server" Text='<%#Bind("CCA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblccatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <%-- 17--%>
                                                <asp:TemplateField HeaderText="LA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLA" runat="server" Text='<%# Eval("LA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbllatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 18--%>
                                                <asp:TemplateField HeaderText="Gratuity" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgratuity" runat="server" Text='<%#Bind("Gratuity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblgratuitytotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 19--%>
                                                <asp:TemplateField HeaderText="Bonus" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbonus" runat="server" Text='<%#Bind("Bonus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblbonustotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 20--%>
                                                <asp:TemplateField HeaderText="WA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWA" runat="server" Text='<%#Bind("WA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblWAtotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 21--%>
                                                <asp:TemplateField HeaderText="OA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOA" runat="server" Text='<%#Bind("OA") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbloatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 22--%>
                                                <asp:TemplateField HeaderText="NFHS" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnfhs" runat="server" Text='<%#Bind("NFHS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblnfhstotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 23--%>
                                                <asp:TemplateField HeaderText="RC" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblRc" runat="server" Text='<%#Bind("RC") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblrctotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 24--%>
                                                <asp:TemplateField HeaderText="Food Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblFAW" runat="server" Text='<%#Bind("FoodAllowance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblfawtotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 25--%>
                                                <asp:TemplateField HeaderText="Travel Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTA" runat="server" Text='<%#Bind("TravelAllowance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbtatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 26--%>
                                                <asp:TemplateField HeaderText="Perf Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpfa" runat="server" Text='<%#Eval("PerformanceAllowance") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblpfatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 27--%>
                                                <asp:TemplateField HeaderText="Mob Allw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMA" runat="server" Text='<%#Eval("MoboleAllowance") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblmatotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 28--%>
                                                <asp:TemplateField HeaderText="PF" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPF" runat="server" Text='<%#Bind("pftotalamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblpftotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 29--%>
                                                <asp:TemplateField HeaderText="ESI" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblESi" runat="server" Text='<%#Bind("esitotalamount") %>'>  
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblesitotal"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 30--%>
                                                <asp:TemplateField HeaderText="Servicecharge Percent" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblschargepercent" runat="server" Text='<%#Bind("servicechargeper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                <asp:TemplateField Visible="false" HeaderText="CGSTper" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcgstper" runat="server" Text='<%#Bind("CGSTper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblcgstpercentage"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="SGSTper" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsgstper" runat="server" Text='<%#Bind("SGSTper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblsgstpercentage"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="IGSTper" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbligstper" runat="server" Text='<%#Bind("IGSTper") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lbligstpercentage"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkbasic" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkbasic" runat="server" Text='<%#Bind("chkbasic") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="ChkDA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkda" runat="server" Text='<%#Bind("chkda") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="ChkHRA" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkhra" runat="server" Text='<%#Bind("chkhra") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkconvey" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkconvey" runat="server" Text='<%#Bind("chkconvey") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkcca" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkcca" runat="server" Text='<%#Bind("chkcca") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkbonus" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkbonus" runat="server" Text='<%#Bind("chkbonus") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkgratuity" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkgratuity" runat="server" Text='<%#Bind("chkgratuity") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkla" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkla" runat="server" Text='<%#Bind("chkla") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chknfhs" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchknfhs" runat="server" Text='<%#Bind("chknfhs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkrc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkrc" runat="server" Text='<%#Bind("chkrc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkwa" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkwa" runat="server" Text='<%#Bind("chkwa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkoa" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkoa" runat="server" Text='<%#Bind("chkoa") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Chkfoodallw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkfoodallw" runat="server" Text='<%#Bind("chkfoodallw") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmedicalallow" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkmedicalallow" runat="server" Text='<%#Bind("chkmedicalallow") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chktravelallw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchktravelallw" runat="server" Text='<%#Bind("chktravelallw") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkperform" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkperform" runat="server" Text='<%#Bind("chkperform") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmobileallw" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkmobileallw" runat="server" Text='<%#Bind("chkmobileallw") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkpf" runat="server" Text='<%#Bind("chkpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkesi" runat="server" Text='<%#Bind("chkesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkservicecharge" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkservicecharge" runat="server" Text='<%#Bind("chkservicecharge") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkcgst" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkcgst" runat="server" Text='<%#Bind("chkcgst") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chksgst" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchksgst" runat="server" Text='<%#Bind("chksgst") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkigst" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgridchkigst" runat="server" Text='<%#Bind("chkigst") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkbapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkbapf" runat="server" Text='<%#Bind("chkbapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkdapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkdapf" runat="server" Text='<%#Bind("chkdapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkhrapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkhrapf" runat="server" Text='<%#Bind("chkhrapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkconveypf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkconveypf" runat="server" Text='<%#Bind("chkconveypf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkccapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkccapf" runat="server" Text='<%#Bind("chkccapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkbonuspf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkbonuspf" runat="server" Text='<%#Bind("chkbonuspf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkgratuitypf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkgratuitypf" runat="server" Text='<%#Bind("chkgratuitypf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chklapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchklapf" runat="server" Text='<%#Bind("chklapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chknfhspf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchknfhspf" runat="server" Text='<%#Bind("chknfhspf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkrcpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkrcpf" runat="server" Text='<%#Bind("chkrcpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkwapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkwapf" runat="server" Text='<%#Bind("chkwapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkoapf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkoapf" runat="server" Text='<%#Bind("chkoapf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkfoodallwpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkfoodallwpf" runat="server" Text='<%#Bind("chkfoodallwpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmedicalallwpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkmedicalallwpf" runat="server" Text='<%#Bind("chkmedicalallwpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chktravelallwpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchktravelallwpf" runat="server" Text='<%#Bind("chktravelallwpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkperfmallwpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkperfmallwpf" runat="server" Text='<%#Bind("chkperfmallwpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmobileallwpf" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkmobileallwpf" runat="server" Text='<%#Bind("chkmobileallwpf") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkbaesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkbaesi" runat="server" Text='<%#Bind("chkbaesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkdaesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkdaesi" runat="server" Text='<%#Bind("chkdaesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkhraesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkhraesi" runat="server" Text='<%#Bind("chkhraesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkconveyesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkconveyesi" runat="server" Text='<%#Bind("chkconveyesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkccaesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkccaesi" runat="server" Text='<%#Bind("chkccaesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkbonusesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkbonusesi" runat="server" Text='<%#Bind("chkbonusesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkgratuityesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkgratuityesi" runat="server" Text='<%#Bind("chkgratuityesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chklaesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchklaesi" runat="server" Text='<%#Bind("chklaesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chknfhsesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchknfhsesi" runat="server" Text='<%#Bind("chknfhsesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkrcesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkrcesi" runat="server" Text='<%#Bind("chkrcesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkwaesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkwaesi" runat="server" Text='<%#Bind("chkwaesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkoaesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkoaesi" runat="server" Text='<%#Bind("chkoaesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkfoodallwesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkfoodallwesi" runat="server" Text='<%#Bind("chkfoodallwesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmedicalallwesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkmedicalallwesi" runat="server" Text='<%#Bind("chkmedicalallwesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chktravelallwesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchktravelallwesi" runat="server" Text='<%#Bind("chktravelallwesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkperfmallwesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkperfmallwesi" runat="server" Text='<%#Bind("chkperfmallwesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmobileallwesi" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkmobileallwesi" runat="server" Text='<%#Bind("chkmobileallwesi") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkbasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkbasc" runat="server" Text='<%#Bind("chkbasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkdasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkdasc" runat="server" Text='<%#Bind("chkdasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkhrasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkhrasc" runat="server" Text='<%#Bind("chkhrasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkconveysc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkconveysc" runat="server" Text='<%#Bind("chkconveysc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkccasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkccasc" runat="server" Text='<%#Bind("chkccasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkbonussc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkbonussc" runat="server" Text='<%#Bind("chkbonussc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkgratuitysc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkgratuitysc" runat="server" Text='<%#Bind("chkgratuitysc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chklasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchklasc" runat="server" Text='<%#Bind("chklasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chknfhssc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchknfhssc" runat="server" Text='<%#Bind("chknfhssc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkrcsc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkrcsc" runat="server" Text='<%#Bind("chkrcsc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkwasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkwasc" runat="server" Text='<%#Bind("chkwasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkoasc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkoasc" runat="server" Text='<%#Bind("chkoasc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkfoodallwsc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkfoodallwsc" runat="server" Text='<%#Bind("chkfoodallwsc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmedicalallwsc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkmedicalallwsc" runat="server" Text='<%#Bind("chkmedicalallwsc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chktravelallwsc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchktravelallwsc" runat="server" Text='<%#Bind("chktravelallwsc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkperfmallwsc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkperfmallwsc" runat="server" Text='<%#Bind("chkperfmallwsc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="chkmobileallwsc" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblchkmobileallwsc" runat="server" Text='<%#Bind("chkmobileallwsc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="pflimit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpflimit" runat="server" Text='<%#Bind("pflimit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="esilimit" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblesilimit" runat="server" Text='<%#Bind("esilimit") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="pftotalamount" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblpftotalamount" runat="server" Text='<%#Bind("pftotalamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="esitotalamount" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblesitotalamount" runat="server" Text='<%#Bind("esitotalamount") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Materialcomponent" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMaterialcomponent" runat="server" Text='<%#Bind("Materialcomponent") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false" HeaderText="Machinerycomponent" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblMachinerycomponent" runat="server" Text='<%#Bind("Machinerycomponent") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>

                                    </div>


                                </div>
                            </div>


                            <div class="panel panel-default">
                                <div class="panel-heading" style="font-size: 14px; font-weight: bold">Lead Info</div>
                                <div class="panel-body">

                                    <table width="100%" cellpadding="5" cellspacing="5" style="margin-left: 30px">

                                        <tr>
                                            <td>State<span style="color: Red">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlstate" runat="server"></asp:DropDownList>
                                            </td>

                                            <td>Zone
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropZone" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr style="height: 32px">
                                            <td>Type<span style="color: Red">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropType" runat="server"></asp:DropDownList>
                                            </td>

                                            <td>Category
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="dropcategory" runat="server"></asp:DropDownList>
                                            </td>
                                        </tr>

                                        <tr style="height: 32px">
                                            <td>Designation<span style="color: Red">*</span>
                                            </td>
                                            <td>
                                                <%--OnSelectedIndexChanged="dropDesignation_SelectedIndexChanged"--%>
                                                <asp:DropDownList ID="dropDesignation" runat="server" AutoPostBack="true"></asp:DropDownList>
                                            </td>
                                            <td></td>

                                        </tr>
                                    </table>

                                    <table width="100%">
                                        <tr>
                                            <td>

                                                <asp:Button ID="btncalculate" runat="server" Text="Calculate" Width="100px"
                                                    ToolTip="Calculate" class=" btn save" OnClick="btncalculate_Click" />

                                                <asp:Button ID="btnGetvalues" runat="server" Text="Get Values" Width="100px" Style="margin-right: 10px; margin-left: 5px"
                                                    ToolTip="Reset Values" class=" btn save" OnClick="btnGetvalues_Click" />
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>

                            <div style="width: 100%">
                                <div class="panel panel-default" style="width: 48%; float: left">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Main Components</div>
                                    <div class="panel-body">
                                        <table cellpadding="4" cellspacing="4" width="60%" style="margin: 0px auto">
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>PF 
                                                </td>
                                                <td>ESI
                                                </td>
                                                <td>SC
                                                </td>
                                            </tr>
                                            <tr id="trbasic" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkba" runat="server" Checked="true" />&nbsp;<asp:Label ID="centarlbasic" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBasic" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkbapf" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkbaesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkbasc" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                            <tr id="trDA" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkda" runat="server" Checked="true" />&nbsp;<asp:Label ID="centralDA" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtDA" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkdapf" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkdaesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkdasc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trhra" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkhra" runat="server" Checked="true" />&nbsp;<asp:Label ID="centarlhra" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtHRA" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkhrapf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkhraesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkhrasc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trconv" runat="server">
                                                <td>

                                                    <asp:CheckBox ID="chkconv" runat="server" Checked="true" />&nbsp;<asp:Label ID="centralconv" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtconv" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkconvpf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkconvesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkconvsc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trcca" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkcca" runat="server" Checked="true" />&nbsp;<asp:Label ID="centarlcca" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCCA" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkccapf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkccaesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkccasc" runat="server" Checked="true" />
                                                </td>

                                            </tr>
                                        </table>

                                    </div>
                                </div>

                                <div class="panel panel-default" style="width: 48%; float: right">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Other Wages</div>
                                    <div class="panel-body">
                                        <table cellpadding="4" cellspacing="4" width="60%" style="margin: 0px auto">
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td>PF 
                                                </td>
                                                <td>ESI
                                                </td>
                                                <td>SC
                                                </td>
                                            </tr>
                                            <tr id="trbonus" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkbonus" runat="server" Checked="true" /><asp:Label ID="centralbonus" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtBonus" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkbonuspf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkbonusesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkbonussc" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                            <tr id="trgratuity" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkgratuity" runat="server" Checked="true" /><asp:Label ID="centralgratuity" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtGratuity" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkgratuitypf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkgratuityesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkgratuitysc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trla" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkla" runat="server" Checked="true" /><asp:Label ID="centralLA" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLA" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checklapf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checklaesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checklasc" runat="server" Checked="true" />
                                                </td>
                                            </tr>




                                            <tr id="trnfhs" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chknfhs" runat="server" Checked="true" />
                                                    <asp:Label ID="centralnfhs" runat="server"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtNFHS" runat="server" class="form-control" Width="80px" Checked="true"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checknfhspf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checknfhsesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checknfhssc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trrc" runat="server">

                                                <td>
                                                    <asp:CheckBox ID="chkrc" runat="server" Checked="true" />
                                                    <asp:Label ID="centralrc" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtRC" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkrcpf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkrcesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkrcsc" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%">
                                <div class="panel panel-default" style="width: 48%; float: left">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Other Allowances</div>
                                    <div class="panel-body">
                                        <table cellpadding="4" cellspacing="4" width="70%" style="margin: 0px auto">
                                            <tr>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td></td>
                                                <td>PF 
                                                </td>
                                                <td>ESI
                                                </td>
                                                <td>SC
                                                </td>
                                            </tr>
                                            <tr id="trwa" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkwa" runat="server" Checked="true" /><asp:Label ID="centralwa" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtWA" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checkwapf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkwaesi" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkwasc" runat="server" Checked="true" />
                                                </td>

                                            </tr>

                                            <tr id="troa" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkoa" runat="server" Checked="true" />
                                                    <asp:Label ID="centraloa" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOA" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checkoapf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkoaesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkoasc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trfoodallw" runat="server">
                                                <td class="auto-style3">
                                                    <asp:CheckBox ID="chkfoodallw" runat="server" Checked="true" />
                                                    <asp:Label ID="centralfoodallw" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtFoodAllowance" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checkfoodpf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkfoodesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkfoodsc" runat="server" Checked="true" />
                                                </td>
                                            </tr>




                                            <tr id="trmedicallw" runat="server">
                                                <td class="auto-style3">
                                                    <asp:CheckBox ID="chkmedicalaalw" runat="server" Checked="true" />
                                                    <asp:Label ID="centralmedicalallw" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMedicalAllowance" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checkmedicalpf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkmedicalesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkmedicalsc" runat="server" Checked="true" />
                                                </td>
                                            </tr>

                                            <tr id="trtravelallw" runat="server">

                                                <td class="auto-style3">
                                                    <asp:CheckBox ID="chktravelallw" runat="server" Checked="true" />
                                                    <asp:Label ID="centraltravelallw" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtTravelAllowance" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checktravelpf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checktravelesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checktravelsc" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                            <tr id="trperformallw" runat="server">
                                                <td class="auto-style3">
                                                    <asp:CheckBox ID="chkperfmallw" runat="server" Checked="true" />
                                                    <asp:Label ID="centralperformallw" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPerfomanceAllowance" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checkperfmpf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkperfmesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkperfmsc" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                            <tr id="trmobileallw" runat="server">
                                                <td class="auto-style3">
                                                    <asp:CheckBox ID="chkmobileallw" runat="server" Checked="true" />
                                                    <asp:Label ID="centraltxtmobileallw" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtMobileAllowance" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>
                                                <td></td>
                                                <td></td>
                                                <td>
                                                    <asp:CheckBox ID="checkmobilepf" runat="server" Checked="false" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkmobileesi" runat="server" Checked="true" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="checkmobilesc" runat="server" Checked="true" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </div>

                                <div class="panel panel-default" style="width: 48%; float: right">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Statutory Components</div>
                                    <div class="panel-body">

                                        <table cellpadding="5" cellspacing="5" width="80%" style="margin: 0px auto">

                                            <tr style="height: 32px;" id="trpf" runat="server">
                                                <td style="padding-left: 40px;">
                                                    <asp:CheckBox ID="chkpf" runat="server" Checked="true" />
                                                    <asp:Label ID="centralpf" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtPF" runat="server" class="form-control" Text="100" Width="40px" Style="margin-left: 1px;"></asp:TextBox></td>
                                                <td>%</td>
                                                <td style="width: 10px"></td>
                                                <td>
                                                    <asp:Label ID="Label4" runat="server" Text="PF Limit :"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtesipflimit" runat="server" class="form-control" Width="80px" Style="margin-left: 1px;"></asp:TextBox>
                                                </td>
                                            </tr>

                                        </table>

                                        <table cellpadding="5" cellspacing="5" width="80%" style="margin: 0px auto">
                                            <tr id="tresi" runat="server">
                                                <td style="padding-left: 40px;">
                                                    <asp:CheckBox ID="chkesi" runat="server" Checked="true" />
                                                    <asp:Label ID="centralesi" runat="server"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtEsi" runat="server" class="form-control" Text="100" Width="40px" Style="margin-left: 1px;"></asp:TextBox>
                                                </td>
                                                <td>%</td>
                                                <td style="width: 10px"></td>

                                                <td>
                                                    <asp:Label ID="Label5" runat="server" Text="ESI Limit :"></asp:Label>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtesiesilimit" runat="server" class="form-control" Width="80px" Style="margin-left: 1px;"></asp:TextBox>
                                                </td>
                                            </tr>


                                        </table>

                                        <table width="40%" style="margin-left: 150px">
                                            <tr>

                                                <td>
                                                    <asp:Label ID="pftotalamount" runat="server" Text="PF :"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="pfamount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="esitotalamount" runat="server" Text="ESI :"></asp:Label>

                                                </td>
                                                <td>
                                                    <asp:Label ID="esiamount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>



                                    </div>
                                </div>
                            </div>


                            <div style="width: 100%">
                                <div class="panel panel-default" style="width: 48%; float: right">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Material/Machinery</div>
                                    <div class="panel-body">
                                        <table cellpadding="5" cellspacing="5" width="80%" style="margin: 0px auto">

                                            <tr style="height: 32px; margin-left: 20px;" id="tr5" runat="server">
                                                <td>
                                                    <asp:Label ID="lblmaterialcomponent" runat="server" Text="Material Component" Style="margin-left: 40px;"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtmaterialcomponent" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr style="height: 32px" id="tr6" runat="server">
                                                <td>
                                                    <asp:Label ID="lblmachinerycomponent" runat="server" Text="Machinery Component" Style="margin-left: 40px;"></asp:Label>
                                                </td>

                                                <td>
                                                    <asp:TextBox ID="txtmachinerycomponent" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>
                                </div>

                                <div class="panel panel-default" style="width: 48%; float: left">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Service Charges</div>
                                    <div class="panel-body">
                                        <table cellpadding="5" cellspacing="5" width="70%" style="margin: 0px auto; height: 120px">
                                            <tr>

                                                <td>
                                                    <asp:CheckBox ID="chkservicecharge" runat="server" Checked="true" />
                                                    <asp:Label ID="centralservicecharge" runat="server"></asp:Label>
                                                </td>

                                                <td>

                                                    <asp:TextBox ID="txtschargeprc" runat="server" Width="40px"></asp:TextBox>%
                                                                     
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtschargeamount" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>

                                            </tr>
                                        </table>
                                    </div>
                                </div>
                            </div>

                            <div style="width: 100%">
                                <div class="panel panel-default" style="width: 48%; float: right">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">GST</div>
                                    <div class="panel-body">
                                        <table cellpadding="5" cellspacing="5" width="60%" style="margin: 0px auto">
                                            <tr style="height: 32px" id="tr2" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkcgst" runat="server" AutoPostBack="true" Checked="true" OnCheckedChanged="chkigst_CheckedChanged" />
                                                    <asp:Label ID="Label1" runat="server" Text="CGST @"></asp:Label>


                                                </td>

                                                <td>

                                                    <asp:TextBox ID="txtcgst" runat="server" class="form-control" Width="40px"></asp:TextBox>


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtcgstamount" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr style="height: 32px" id="tr3" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkSgst" Checked="true" AutoPostBack="true" runat="server" OnCheckedChanged="chkigst_CheckedChanged" />
                                                    <asp:Label ID="Label2" runat="server" Text="SGST @"></asp:Label>
                                                </td>

                                                <td>

                                                    <asp:TextBox ID="txtsgstper" runat="server" class="form-control" Width="40px"></asp:TextBox>


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtsgstamount" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr style="height: 32px" id="tr4" runat="server">
                                                <td>
                                                    <asp:CheckBox ID="chkigst" AutoPostBack="true" runat="server" OnCheckedChanged="chkigst_CheckedChanged" />
                                                    <asp:Label ID="Label3" runat="server" Text="IGST @"></asp:Label>
                                                </td>

                                                <td>

                                                    <asp:TextBox ID="txtigst" runat="server" class="form-control" Width="40px"></asp:TextBox>


                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtigstamount" runat="server" class="form-control" Width="80px"></asp:TextBox>
                                                </td>

                                            </tr>

                                        </table>
                                    </div>
                                </div>

                                <div style="width: 48%; float: left">
                                </div>

                            </div>


                            <table cellpadding="10px;" cellspacing="20px;" width="30%" style="font-weight: bold; font-size: 15px">
                                <tr>
                                    <td>
                                        <asp:Label ID="total" Text="Total" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblresult" runat="server"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="Grandtotal" Text="Grand Total" runat="server"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:Label ID="txtGrandTotal" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>

                            <div style="float: right;">
                                <asp:Button ID="btnadd" runat="server" Text="ADD"
                                    ToolTip="Calculate" class=" btn save" Visible="true" OnClick="btnadd_Click" />

                            </div>




                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                        .
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
        </div>
        <!-- FOOTER END -->
    </form>
</body>
</html>
