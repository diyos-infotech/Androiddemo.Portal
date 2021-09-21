<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_All_Action_Log_Scheduler.aspx.cs" Inherits="Jawan.Portal.M_All_Action_Log_Scheduler" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>ACTION LOG SCHEDULER</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script type="text/javascript">
        function ShowPopup(message, date) {
            debugger;

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

                        <%--<li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>--%>
                        <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        --%>
                       <%-- <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>--%>
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
                                            <li class="first"><a href="M_Leads.aspx" id="Employeeslink" runat="server" class="current">
                                                <span>Leads</span></a></li>
                                            <li class="after"><a href="M_All_Action_Log_Scheduler.aspx" id="ClientsLink" runat="server"><span>Calendar</span></a></li>
                                            <li class="after"><a href="MKTG_Reports.aspx" id="MKTG_ReportsLink" runat="server"><span>MKTG Reports</span></a></li>
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
                                    <td>ACTION LOG SCHEDULER (All Leads)
                                    </td>
                                </tr>
                            </table>




                            <div class="panel panel-default" style="margin-top: 10px">
                                <div class="panel-heading" style="font-size: 14px; font-weight: bold">Calendar Events</div>
                                <div class="panel-body">

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
                                            SelectionMode="None" DayStyle-ForeColor="Black" DayHeaderStyle-Font-Bold="true" OnDayRender="myCalendar_DayRender">
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

                                </div>
                            </div>


                            <div style="margin-top: 10px;">

                                <div class="panel panel-default">
                                    <div class="panel-heading" style="font-size: 14px; font-weight: bold">Event Details</div>
                                    <div class="panel-body">

                                        <table width="90%" style="margin: 0px auto">
                                            <tr>
                                                <td>From Date
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtFromDate" Width="230px" CssClass="form-control txt-calender" TabIndex="1"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CEFromDate" runat="server" Enabled="true" TargetControlID="txtFromDate"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>To Date
                                                </td>
                                                <td>
                                                    <asp:TextBox runat="server" ID="txtToDate" Width="230px" CssClass="form-control txt-calender" TabIndex="2"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="CEToDate" runat="server" Enabled="true" TargetControlID="txtToDate"
                                                        Format="dd/MM/yyyy">
                                                    </cc1:CalendarExtender>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSearch" runat="server" Text="Search" Style="margin-right: 10px"
                                                        ToolTip="Search" class="btn save" OnClick="btnSearch_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                        <div style="height: 20px"></div>


                                        <asp:GridView ID="Gvactionlog" runat="server" AutoGenerateColumns="False" Width="90%" GridLines="None" AllowPaging="true"
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

                                                <asp:TemplateField HeaderText="Lead Name" HeaderStyle-BackColor="#fcf8e3" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="left" ItemStyle-Width="200px">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeadname" ReadOnly="true" runat="server" Text='<%#Eval("leadname") %>' CssClass="form-control" Style="margin: 4px; height: 35px"></asp:Label>
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

            });
        };
    </script>
</body>
</html>
