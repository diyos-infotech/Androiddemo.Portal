<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InvPODetails.aspx.cs" Inherits="Jawan.Portal.InvPODetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVENTORY: PO MASTER</title>
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
                select: function (event, ui) { $("#ddlVendorID").attr("data-clientId", ui.item.value); OnAutoCompleteDDLVendoridchange(event, ui); },
                select: function (event, ui) { $("#ddlVendorName").attr("data-clientId", ui.item.value); OnAutoCompleteDDLVendornamechange(event, ui); },
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLVendoridchange(event, ui) {
            $('#ddlVendorID').trigger('change');

        }

        function OnAutoCompleteDDLVendornamechange(event, ui) {

            $('#ddlVendorName').trigger('change');
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
                        <%--<li><a href="Reminders.aspx">Reminders</a></li>--%>
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
                        <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server" class="current"><span>Inventory</span></a></li>
                        <li class="after"><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server">
                            <span>Reports</span></a></li>
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
                                <div style="display: inline;">
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
                        <li class="first"><a href="ViewItems.aspx" style="z-index: 9;"><span></span>Inventory</a></li>

                        <li class="active"><a href="InvPODetails.aspx" style="z-index: 7;" class="active_bread">PO Details</a></li>
                    </ul>
                </div>
                <asp:ScriptManager runat="server" ID="Scriptmanager1">
                </asp:ScriptManager>
                <div class="dashboard_full">
                    <div style="float: right; font-weight: bold">
                    </div>
                    <!-- DASHBOARD CONTENT BEGIN -->
                    <div class="contentarea" id="contentarea">

                        <div class="sidebox">
                            <div class="boxhead">

                                <h2 style="text-align: center">PO Details
                                </h2>
                            </div>
                            <div class="contentarea" id="Div1">
                                <div class="boxinc">

                                    <ul>
                                        <%-- <li class="left leftmenu">
                                            <ul>
                                             <li><a href="InvPODetails.aspx" class="sel">PO Details</a></li>
                                                <li><a href="InvInflowDetails.aspx" >Inflow </a></li>
                                                <li><a href="InvVendorMaster.aspx">Vendor Details</a></li>
                                                <li><a href="InvClientMaster.aspx">Client Rate Details</a></li>
                                                <li><a href="AddnewItem.aspx">Add New Item</a></li>

                                               
                                            </ul>
                                        </li>--%>
                                        <li class="right">
                                            <asp:UpdatePanel ID="uppanel" runat="server" UpdateMode="Conditional">
                                                <ContentTemplate>

                                                    <table width="130%" cellpadding="5" cellspacing="5" style="margin-left: 10px">
                                                        <tr>
                                                            <td>
                                                                <table width="100%" cellpadding="5" cellspacing="5" style="margin: 10px">

                                                                    <tr style="height: 36px">
                                                                        <td>Vendor Id
                                                                        </td>
                                                                        <td>

                                                                            <asp:DropDownList ID="ddlVendorID" runat="server" CssClass="ddlautocomplete chosen-select" TabIndex="2" Style="width: 150px"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlVendorID_OnSelectedIndexChanged">
                                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                            </asp:DropDownList>

                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 36px">
                                                                        <td>PO No
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtPONo" runat="server" class="form-control" Width="228px"></asp:TextBox>

                                                                        </td>
                                                                    </tr>
                                                                    
                                                                     <tr style="height: 36px">
                                                                        <td>Remarks
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtRemarks" runat="server" class="form-control" Width="228px" TextMode="MultiLine"> </asp:TextBox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 36px">
                                                                        <td>
                                                                            <asp:Label ID="lblmonth" runat="server" Text="Month" Visible="false"></asp:Label>
                                                                        </td>

                                                                        <td>
                                                                            <asp:TextBox ID="txtMonth" runat="server" class="form-control" Width="228px" Visible="false"></asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar1"
                                                                                Enabled="true" Format="MMM-yyyy" TargetControlID="txtMonth" DefaultView="Months" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                                                            </cc1:CalendarExtender>

                                                                        </td>
                                                                    <tr style="height: 36px">
                                                                        <td></td>
                                                                        <td></td>
                                                                    </tr>

                                                                </table>
                                                            </td>

                                                            <td valign="top">
                                                                <table width="100%" cellpadding="5" cellspacing="5" style="margin: 10px">

                                                                    <tr style="height: 36px; width: 105px">
                                                                        <td>Vendor Name
                                                                        </td>
                                                                        <td>

                                                                            <asp:DropDownList ID="ddlVendorName" runat="server" CssClass="ddlautocomplete chosen-select" TabIndex="2" Style="width: 150px"
                                                                                AutoPostBack="true" OnSelectedIndexChanged="ddlVendorName_OnSelectedIndexChanged">
                                                                                <asp:ListItem Value="0">-Select-</asp:ListItem>
                                                                            </asp:DropDownList>


                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 36px">
                                                                        <td>Date
                                                                        </td>
                                                                        <td>
                                                                            <asp:TextBox ID="txtdate" runat="server" class="form-control" Width="228px"> </asp:TextBox>
                                                                            <cc1:CalendarExtender ID="CEDtofInterview" runat="server" Enabled="true" TargetControlID="txtdate"
                                                                                Format="dd/MM/yyyy">
                                                                            </cc1:CalendarExtender>
                                                                            <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtdate"
                                                                                ValidChars="/0123456789">
                                                                            </cc1:FilteredTextBoxExtender>
                                                                        </td>
                                                                    </tr>
                                                                   
                                                                     <tr style="height: 36px">
                                                                        <td>
                                                                            <asp:Label ID="lblclientid" runat="server" Text="Client ID" Visible="false"></asp:Label>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlclientid" runat="server" CssClass="ddlautocomplete chosen-select" Visible="false">
                                                                            </asp:DropDownList>

                                                                        </td>
                                                                    </tr>
                                                                    <tr style="height: 36px">
                                                                        <td><%--Delivery at --%>
                                                                        </td>
                                                                        <td>
                                                                            <asp:DropDownList ID="ddlDeliveryAt" runat="server" class="form-control" Width="228px" AutoPostBack="true" OnSelectedIndexChanged="ddlDeliveryAt_OnSelectedIndexChanged" Visible="false">
                                                                                <asp:ListItem Value="Office">Office</asp:ListItem>
                                                                                <asp:ListItem Value="Client">Client</asp:ListItem>

                                                                            </asp:DropDownList>

                                                                        </td>
                                                                    </tr>
                                                                   

                                                                    </tr>
                                                                    <tr style="height: 36px">
                                                                        <td></td>
                                                                        <td>
                                                                            <asp:Label ID="lblresult" runat="server" Text="" Visible="false" Style="color: Red"></asp:Label>
                                                                            <asp:Button ID="btnsave" runat="server" ValidationGroup="a1" Text="Save" OnClientClick='return confirm("Are you sure you want to add this Item?");'
                                                                                ToolTip="SAVE" class=" btn save" OnClick="BtnSave_Click" />
                                                                            <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="Cancel" ToolTip="CANCEL"
                                                                                class=" btn save" OnClientClick='return confirm("Are you sure you want to cancel this entry?");' />
                                                                        </td>
                                                                    </tr>

                                                                </table>
                                                            </td>
                                                        </tr>
                                                    </table>



                                                    <div class="rounded_corners" style="width: 125%; margin-left: -1px; margin-top: 10px;overflow-x:scroll">
                                                        <asp:GridView ID="GVInvPODetails" runat="server" AutoGenerateColumns="False" Width="125%" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                            CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="none" Style="margin-left: -2px" OnRowDataBound="GVInvPODetails_RowDataBound">

                                                            <Columns>

                                                                

                                                                <asp:TemplateField HeaderText="Item ID" ItemStyle-HorizontalAlign="Center">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitemid" runat="server" Text='<%#Bind("itemid") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Item Name" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblitemname" runat="server" Text='<%#Bind("itemname") %>' Width="120px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField HeaderText="HSN Number" ItemStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblHSNNo" runat="server" Text='<%#Bind("HSNNumber") %>' Width="120px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="UOM" HeaderStyle-HorizontalAlign="Left">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblUOM" runat="server" Text='<%#Bind("UnitMeasure")%>'> </asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Quantity" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtQuantity" runat="server" Text="0" CssClass="form-control" Width="50px" AutoPostBack="true" OnTextChanged="txtQuantity_OnTextChanged"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Remarks" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="80px">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="txtItemRemarks" runat="server" Text="" CssClass="form-control" Width="80px" TextMode="MultiLine"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Stock In Hand" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblStockInHand" runat="server" Text='<%#Bind("actualquantity") %>' Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Buying Price per unit" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBuyingPrice" runat="server" Text='<%#Bind("BuyingPrice","{0:0.##}") %>' Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Total Buying Price" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotalBuyingPrice" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                    <asp:TemplateField HeaderText="GST Per." HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGStPer" runat="server" Text='<%#Bind("GSTPer","{0:0.##}") %>' Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="GST Amount" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGSTAmt" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                               <asp:TemplateField HeaderText="VATCmp1" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVATCmp1" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="VATCmp2" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVATCmp2" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="VATCmp3" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVATCmp3" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="VATCmp4" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVATCmp4" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                                  <asp:TemplateField HeaderText="VATCmp5" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="90px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblVATCmp5" runat="server" Text="0" Width="90px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Total" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="60px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblTotal" runat="server" Text="0" Width="60px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Sub Total" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSubTotal" runat="server" Text="" Width="70px"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>

                                                            </Columns>


                                                        </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                                <Triggers>



                                                    <asp:AsyncPostBackTrigger ControlID="btnsave" EventName="Click" />

                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </li>
                                    </ul>
                                </div>

                            </div>





                        </div>
                        <%--   </div>--%>
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

                setProperty();
            });
        };
    </script>
</body>
</html>
