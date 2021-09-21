<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_Action_Log.aspx.cs" Inherits="Jawan.Portal.M_Action_Log" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ACTION LOG</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script type="text/javascript">
        function ShowPopup(message, date) {
            $(function () {
                $("#dialog").html(message);
                $("#dialog").dialog({
                    title: date + " - Event Details",
                    buttons: {
                        Close: function () {
                            $(this).dialog('close');
                        }
                    },
                    modal: true
                });
            });
        };

        function UpdateStatus() {

            $("#popupdiv").dialog({
                title: "Update Lead Status",
                width: 350,
                height: 200,
                modal: true,
                buttons: {
                    Submit: function () {
                        $("#hnd_drp_status").val($("#ddlStatus").val());
                        if ($("#hnd_drp_status").val() == 1) {
                            alert("Lead is already in Proposal Stage ! ");
                            return true;
                        }

                        $("#but_send").click();
                        $(this).dialog('close');
                    }

                }
            });

            $("#btnupdatestatus").click(function () {
                $('#popupdiv').dialog('open');
            });
            return false;
        };

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
    </script>

    <style type="text/css">
        div.ui-dialog-titlebar {
            font-size: 13px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
        }

        div.ui-dialog-content {
            font-size: 12px;
            font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
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


</head>

<body>

    <form id="Actionlog" runat="server" autocomplete="off">

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

                <asp:UpdatePanel runat="server" ID="UpdatePanel1">
                    <ContentTemplate>

                        <div class="container">

                            <table width="100%" style="margin-top: -10px; margin-bottom: 10px">
                                <tr>
                                    <td align="right"><< <a href="M_Leads.aspx" style="color: #003366">Back</a>  </td>
                                </tr>
                            </table>


                            <table class="header" style="margin-bottom: 10px">
                                <tr style="font-weight: bold; height: 25px; font-size: 17px; text-align: center">
                                    <td>ACTION LOG
                                    </td>
                                </tr>
                            </table>


                            <table cellpadding="5" cellspacing="5" width="100%" style="margin: 0px auto">
                                <tr>
                                    <td>
                                        <asp:Label ID="lblLeadID" runat="server" Text="Lead ID"></asp:Label>
                                    </td>
                                    <td>

                                        <asp:DropDownList ID="DropLeadID" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="DropLeadID_SelectedIndexChanged"></asp:DropDownList>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Lead Name"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="DropLeadName" runat="server" CssClass="ddlautocomplete chosen-select" AutoPostBack="true" OnSelectedIndexChanged="DropLeadName_SelectedIndexChanged"></asp:DropDownList>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnupdatestatus" runat="server" Text="Update Lead Status" Style="margin-right: 10px"
                                            ToolTip="Update Status" class=" btn save" OnClientClick="javascript:UpdateStatus();return false;" />
                                    </td>
                                </tr>
                            </table>

                            <div class="panel panel-default" style="margin-top: 10px">
                                <div class="panel-heading" style="font-size: 14px; font-weight: bold">Add Calendar Events</div>
                                <div class="panel-body">

                                    <table cellpadding="5" cellspacing="5" width="90%" style="margin: 0px auto">

                                        <tr style="height: 32px">
                                            <td>
                                                <asp:Label ID="lblAction" runat="server" Text="Action"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtaction" Width="230px" Height="40px" TextMode="MultiLine" CssClass="form-control" TabIndex="1"></asp:TextBox>
                                            </td>
                                            <td>Action Date
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtactionDate" Width="230px" CssClass="form-control txt-calender" TabIndex="1"></asp:TextBox>
                                                <cc1:CalendarExtender ID="CEactionDate" runat="server" Enabled="true" TargetControlID="txtactionDate"
                                                    Format="dd/MM/yyyy">
                                                </cc1:CalendarExtender>
                                            </td>
                                            <td>
                                                <asp:Button ID="btnActionSave" runat="server" Text="Add Event" Style="margin-right: 10px"
                                                    ToolTip="Add Event" class=" btn save" OnClick="btnActionSave_Click" />
                                            </td>
                                        </tr>
                                    </table>


                                    <div style="background-color: white">

                                        <style type="text/css">
                                            .Calheader {
                                                text-align: center;
                                                border: 1px solid #aed0ea;
                                                background: #d7ebf9 url(Marketing_Images/ui-bg_glass_80_d7ebf9_1x400.png) 50% 50% repeat-x;
                                                color: #2779aa;
                                                outline: none;
                                                font-weight: bold;
                                                border-top-color: #aed0ea;
                                                height: 3px;
                                            }

                                            .cal {
                                                border: 1px solid #aed0ea;
                                                background: #f2f5f7 url(Marketing_Images/ui-bg_highlight-hard_100_f2f5f7_1x100.png) 50% 50% repeat-x;
                                                color: #2779aa;
                                                font-family: 'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif;
                                                margin: 0px auto;
                                                text-decoration: none;
                                            }

                                            .today {
                                                color: #2779aa;
                                                background-color: rgba(255,0,255,0.3) !important;
                                                border: 1px solid #ff00dc;
                                            }

                                            .cellback {
                                                background-color: rgba(0,255,255,0.3) !important;
                                                border: 1px solid #00ffff;
                                                font-size: 10px;
                                                color: black;
                                            }

                                            .cellview {
                                                color: red;
                                                text-align: right;
                                                cursor: pointer;
                                            }

                                            .cellcontent {
                                                text-align: left;
                                                color: black;
                                                background-color: rgba(255,255,0,0.8) !important;
                                                border: 1.5px solid #FFFF00;
                                                border-radius: 5px;
                                            }

                                            .tile {
                                                color: #000;
                                                height: 50px;
                                                border: 0px;
                                                border-bottom-color: #aed0ea;
                                            }

                                            .backstyle {
                                                background-color: rgba(255,0,255,0.2) !important;
                                                color: black !important;
                                                border: 1px solid #ff00dc;
                                            }

                                            .hide {
                                                visibility: hidden;
                                            }

                                            .pagerstyle {
                                                background-color: rgba(0,255,255,0.3) !important;
                                                border: 1px solid #00ffff;
                                            }
                                        </style>

                                        <asp:Calendar ID="myCalendar" BackColor="White" CssClass="cal" runat="server" DayNameFormat="Short"
                                            Font-Size="10pt" Height="600px" ShowGridLines="True" Width="700px" NextPrevFormat="CustomText"
                                            SelectionMode="None" DayStyle-ForeColor="Black" DayHeaderStyle-Font-Bold="true" OnDayRender="myCalendar_DayRender" OnSelectionChanged="myCalendar_SelectionChanged">
                                            <DayHeaderStyle Height="2px" CssClass="Calheader" />
                                            <NextPrevStyle Font-Size="15pt" ForeColor="#2779aa" Font-Bold="True" />
                                            <OtherMonthDayStyle ForeColor="#2779aa" />
                                            <SelectedDayStyle Font-Bold="True" ForeColor="Black" CssClass="backstyle" />
                                            <SelectorStyle ForeColor="Black" />
                                            <TitleStyle CssClass="tile" BackColor="White" Font-Size="15pt" Font-Bold="True" Height="50px" BorderColor="White" />
                                            <TodayDayStyle ForeColor="Black" CssClass="today" />
                                        </asp:Calendar>

                                        <asp:LinkButton ID="lblbtn" runat="server" OnClick="lbview_Click" CssClass="hide"></asp:LinkButton>
                                    </div>



                                    <div id="dialog">
                                    </div>


                                    <div id="popupdiv" title="" style="display: none" runat="server" defaultbutton="but_send">
                                        <table style="margin: 25px">
                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lblstatus" Text="Status"></asp:Label></td>
                                                <td></td>
                                                <td>
                                                    <asp:DropDownList ID="ddlStatus" runat="server" TabIndex="10">
                                                        <asp:ListItem>-Select-</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:Button ID="but_send" runat="server" Text="Submit" Style="display: none"
                                        OnClick="but_send_Click" />
                                    <asp:HiddenField ID="hnd_drp_status" runat="server" />

                                </div>
                            </div>


                            <div style="margin-top: 10px;">

                                <div class="panel panel-default">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Event Details</div>
                                    <div class="panel-body">


                                        <asp:GridView ID="Gvactionlog" runat="server" AutoGenerateColumns="False" Width="70%" GridLines="None" AllowPaging="true"
                                            PageSize="10" OnPageIndexChanging="Gvactionlog_PageIndexChanging"
                                            ShowHeader="true" CellPadding="4" CellSpacing="5" Style="text-align: center; margin: 0px auto" Height="50px">

                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSlno" runat="server" Text="<%# Container.DataItemIndex + 1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField Visible="false" ItemStyle-BackColor="#EFF3FB">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text='<%#Eval("Id") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                <asp:TemplateField HeaderText="Remarks" HeaderStyle-BackColor="#fcf8e3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtaction" placeholder="Action Name" ReadOnly="true" runat="server" Text='<%#Eval("Action") %>' CssClass="form-control" Style="margin: 4px; height: 35px"></asp:TextBox>
                                                    </ItemTemplate>

                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Action" HeaderStyle-BackColor="#fcf8e3" ItemStyle-Width="100px">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtdateaction" ReadOnly="true" CssClass="form-control" Width="100px" placeholder="Date Of Action" Text='<%#Eval("DateofAction","{0:dd/MM/yyyy}")%>' runat="server" Style="margin: 4px; height: 35px"></asp:TextBox>

                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Action Taken Remarks" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtstatus" runat="server" placeholder="Action" Text='<%#Eval("Status")%>' TextMode="MultiLine" CssClass="form-control" Style="margin: 4px; height: 35px" AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="LeadId" Visible="false" ItemStyle-HorizontalAlign="Left" HeaderStyle-BackColor="#fcf8e3">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtleadid" runat="server" placeholder="Action" Text='<%#Eval("LeadId")%>' TextMode="MultiLine" CssClass="form-control" Style="margin: 4px; height: 35px" AutoPostBack="true"></asp:TextBox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <PagerStyle CssClass="pagerstyle" HorizontalAlign="Center" />
                                        </asp:GridView>

                                        <div align="right" style="margin-right: 30px; margin-top: 10px">
                                            <asp:Button ID="btnsaves" runat="server" Text="Save" OnClick="btnsaves_Click" />
                                        </div>
                                    </div>
                                </div>

                            </div>

                        </div>

                    </ContentTemplate>
                </asp:UpdatePanel>

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
