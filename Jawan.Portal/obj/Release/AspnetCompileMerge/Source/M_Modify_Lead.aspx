<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_Modify_Lead.aspx.cs" Inherits="Jawan.Portal.M_Modify_Lead" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MODIFY LEAD</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />

    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>
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
                select: function (event, ui) { $("#ddlLeadGenratedby").attr("data-clientId", ui.item.value); OnAutoCompleteDDLClientidchange(event, ui); },
                minLength: 4
            });
        }

        $(document).ready(function () {
            setProperty();
        });

        function OnAutoCompleteDDLClientidchange(event, ui) {
            $('#ddlLeadGenratedby').trigger('change');

        }

        $(function () {
            bindautofilldesgs();
        });
        var prmInstance = Sys.WebForms.PageRequestManager.getInstance();
        prmInstance.add_endRequest(function () {
            //you need to re-bind your jquery events here
            bindautofilldesgs();
        });


    </script>

    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
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
             width: 195px;
        }

        .PnlBackground {
            background-color: rgba(128, 128, 128,0.5);
            z-index: 10000;
        }
    </style>
</head>
<body>
    <form id="ModifyLead" runat="server" autocomplete="off">
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
                        <li class="last"><a href="M_Leads.aspx" id="LinkMarketting" runat="server"><span><span>Marketting</span></span></a></li>
                    </ul>
                </div>
                <!-- MAIN MENU SECTION END -->
            </div>
            <!-- LOGO AND MAIN MENU SECTION END -->
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

                <div class="container">

                    <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                        <ContentTemplate>

                            <table width="100%" style="margin-top: -10px; margin-bottom: 10px">
                                <tr>
                                    <td align="right"><< <a href="M_Leads.aspx" style="color: #003366">Back</a>  </td>
                                </tr>
                            </table>


                            <table class="header" style="margin-bottom: 10px">
                                <tr style="font-weight: bold; height: 25px; font-size: 17px; text-align: center;">
                                    <td>MODIFY LEAD DETAILS
                                    </td>
                                </tr>
                            </table>


                            <table cellpadding="5" cellspacing="5" width="100%" style="margin: 10px; margin-left: 20px">
                                <div align="center" style="margin-right: 30px; margin-top: 10px">
                                    <asp:Label ID="lblMsg" runat="server" Style="border-color: #f0c36d; background-color: #f9edbe; width: auto; font-weight: bold; color: #CC3300;"></asp:Label>
                                </div>
                                <tr style="height: 32px">
                                    <td>

                                        <asp:Label ID="lblLeadID" runat="server" Text="Lead ID"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtleadId" ReadOnly="true" Enabled="false" TabIndex="1" MaxLength="10"></asp:TextBox>
                                    </td>
                                    <td style="width: 140px"></td>

                                    <td>
                                        <asp:Label ID="lblleadname" runat="server" Text="Company Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtleadname" TabIndex="2"></asp:TextBox>
                                    </td>

                                </tr>
                                <tr style="height: 32px">
                                    <td>Source of Lead</td>
                                    <td>
                                        <asp:DropDownList ID="ddlSource" runat="server" TabIndex="3" AutoPostBack="True">
                                            <asp:ListItem>-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 140px"></td>
                                    <td>Segment
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlsegment" runat="server" TabIndex="4" AutoPostBack="True"></asp:DropDownList>
                                    </td>

                                </tr>

                                <tr>
                                    <td>Lead Generated On
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLeadGeneratedOn" runat="server" TabIndex="5"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtLeadGeneratedOn"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FTBLeadGeneratedOn" runat="server" Enabled="True" TargetControlID="txtLeadGeneratedOn"
                                            ValidChars="/0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td></td>
                                    <td>Lead Generated By</td>
                                    <td>
                                        <asp:DropDownList ID="ddlLeadGenratedby" runat="server" CssClass="ddlautocomplete chosen-select" TabIndex="6" AutoPostBack="True">
                                            <asp:ListItem>-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr style="height: 32px">

                                    <td>Expected Date of Order</td>
                                    <td>
                                        <asp:TextBox ID="txtexpecteddate" runat="server" TabIndex="7"></asp:TextBox>
                                        <cc1:CalendarExtender ID="CE_From_Date" runat="server" Enabled="True" TargetControlID="txtexpecteddate"
                                            Format="dd/MM/yyyy">
                                        </cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FTBE_From_Date" runat="server" Enabled="True" TargetControlID="txtexpecteddate"
                                            ValidChars="/0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                    </td>
                                    <td></td>
                                    <td>Status</td>
                                    <td>
                                        <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="8" AutoPostBack="True">
                                            <asp:ListItem>-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr style="height: 32px">
                                    <td>State
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpreStates" runat="server" OnSelectedIndexChanged="ddlpreStates_SelectedIndexChanged1" AutoPostBack="True" TabIndex="9">
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td>City
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlpreCity" runat="server" TabIndex="10" AutoPostBack="True" Enabled="false"></asp:DropDownList>
                                    </td>
                                </tr>

                                <tr style="height: 32px">

                                    <td>Address</td>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" TabIndex="11" MaxLength="11"></asp:TextBox>

                                    </td>
                                    <td>

                                    </td>
                                    <td>Branch
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlBranch" runat="server" TabIndex="12"></asp:DropDownList>
                                    </td>
                                </tr>


                                <tr style="height: 32px">
                                </tr>
                                <tr>
                                </tr>

                            </table>
                            <table>
                                <tr>
                                    <td style="display: none">
                                        <asp:Label ID="lblOwnernameID" runat="server" Text="Owner Name"></asp:Label>
                                    </td>
                                    <td style="display: none">
                                        <asp:TextBox ID="txtownername" runat="server" TabIndex="13"></asp:TextBox>
                                    </td>
                                    <td style="display: none">Region
                                    </td>
                                    <td style="display: none">
                                        <asp:DropDownList ID="ddlRegion" runat="server" TabIndex="14" AutoPostBack="True">
                                            <asp:ListItem>-Select-</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>

                              <table class="header" style="margin-bottom: 10px">
                                <tr style="font-weight: bold; height: 25px; font-size: 15px">
                                    <td>Existing Vendor Details
                                    </td>
                                </tr>
                                  </table>
                           <table cellpadding="5" cellspacing="5" width="100%" style="margin: 10px; margin-left: 20px;">
                                 <tr style="height: 32px">
                                     <td>
                                         Existing Vendor Name
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtexistvendor" runat="server" TabIndex="15"></asp:TextBox>
                                     </td>

                                     <td style="width: 140px"></td>
                                     <td>
                                         Remarks
                                     </td>
                                     <td>
                                         <asp:TextBox ID="txtRemarks" runat="server" TextMode="MultiLine" TabIndex="16"></asp:TextBox>
                                     </td>
                                 </tr>
                            </table>
                            <br />

                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" GridLines="None"
                                ShowHeader="true" CellPadding="15" CellSpacing="5" Style="text-align: center; font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif" HeaderStyle-HorizontalAlign="Center">
                                <Columns>

                                    <asp:BoundField DataField="RowNumber" HeaderText="SNo" />

                                    <%-- 1 --%>
                                  

                                     <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtdesgn" runat="server" placeholder="Designation" TabIndex="17" Width="190px" Text="" Style="margin: 4px; height: 35px"></asp:TextBox>
                                        </ItemTemplate>
                                    </asp:TemplateField>


                                    <%-- 2 --%>
                                     <asp:TemplateField HeaderText="Quotation" HeaderStyle-BackColor="#fcf8e3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuatation" placeholder="Quotation" runat="server" Text="" TabIndex="18" Width="150px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                            <cc1:FilteredTextBoxExtender ID="FTBE_Quatation" runat="server" Enabled="True" TargetControlID="txtQuatation"
                                            ValidChars="/0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- 3 --%>
                                    <asp:TemplateField HeaderText="Salary" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsalary" runat="server" Text="" placeholder="Salary" TabIndex="19" Width="150px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                              <cc1:FilteredTextBoxExtender ID="FTBE_Salary" runat="server" Enabled="True" TargetControlID="txtsalary"
                                            ValidChars="/0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- 4 --%>
                                    <asp:TemplateField HeaderText="Hours" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtHours" runat="server" Text="" placeholder="Hours" TabIndex="19" Width="150px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                              <cc1:FilteredTextBoxExtender ID="FTBE_Hours" runat="server" Enabled="True" TargetControlID="txtHours"
                                            ValidChars="/0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%-- 5 --%>
                                    <asp:TemplateField HeaderText="Strength" HeaderStyle-BackColor="#fcf8e3">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStrength" runat="server" Text="" placeholder="Strength" TabIndex="20" Width="150px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                             <cc1:FilteredTextBoxExtender ID="FTBE_Strength" runat="server" Enabled="True" TargetControlID="txtStrength"
                                            ValidChars="/0123456789">
                                        </cc1:FilteredTextBoxExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="imgdelete" ImageUrl="~/Marketing_Images/Deleteicon.png" runat="server" OnClientClick='return confirm("Are you sure you want to delete this entry?");' OnClick="imgdeleteexist_Click" />
                                        </ItemTemplate>
                                        <ItemStyle Width="40px"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                             <div align="right">
                                <asp:ImageButton ID="ImageButton1" ImageUrl="~/Marketing_Images/Addicon.png" ToolTip="Add" runat="server" OnClick="Imgaddexist_Click" />
                            </div>

                            <br />

                            <table class="header" style="margin-bottom: 10px">
                                <tr style="font-weight: bold; height: 25px; font-size: 15px">
                                    <td>Contact Details
                                    </td>
                                </tr>
                            </table>

                            <div style="margin-top: 10px;">
                                <asp:GridView ID="GVleads" runat="server" AutoGenerateColumns="False" Width="100%" GridLines="None"
                                    ShowHeader="false" CellPadding="4" CellSpacing="5" Style="text-align: center; margin: 0px auto" HeaderStyle-HorizontalAlign="Center">

                                    <Columns>

                                        <asp:BoundField DataField="RowNumber" HeaderText="SNo" />


                                        <%-- 1 --%>
                                        <asp:TemplateField HeaderText="Name" HeaderStyle-BackColor="#fcf8e3">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtname" placeholder="Name" TabIndex="21" runat="server" Text="" Width="200px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>



                                        <%-- 2 --%>
                                        <asp:TemplateField HeaderText="Desgntion" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtdesgn" TabIndex="22" runat="server" placeholder="Designation" Text="" Width="200px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                            </ItemTemplate>

                                        </asp:TemplateField>


                                        <%-- 3 --%>

                                        <asp:TemplateField HeaderText="Mobile" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtmobile" TabIndex="23" runat="server" Text="" placeholder="Mobile" Width="200px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                            </ItemTemplate>

                                        </asp:TemplateField>

                                        <%-- 4 --%>
                                        <asp:TemplateField HeaderText="E-Mail" HeaderStyle-BackColor="#fcf8e3">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtemail" TabIndex="24" runat="server" Text="" placeholder="E-mail" Width="200px" Style="margin: 4px; height: 35px"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgdelete" CommandName="delete" ImageUrl="~/Marketing_Images/Deleteicon.png" OnClientClick='return confirm("Are you sure you want to delete this entry?");' OnClick="imgdelete_Click" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle Width="40px"></ItemStyle>
                                            <HeaderStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <div align="right">
                                    <asp:ImageButton ID="Imgadd" ImageUrl="~/Marketing_Images/Addicon.png" OnClick="Imgadd_Click" ToolTip="Add" runat="server" />
                                </div>

                                <div align="right" style="margin-top: 10px">
                                    <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"></asp:Label>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" ToolTip="Save"
                                        TabIndex="25" class="btn save" OnClientClick='return confirm("Are you sure you want to Modify this Lead?");'
                                        OnClick="btnaddclint_Click" />
                                </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
        </div>
        <!-- DASHBOARD CONTENT END -->

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


    </form>
</body>
</html>
