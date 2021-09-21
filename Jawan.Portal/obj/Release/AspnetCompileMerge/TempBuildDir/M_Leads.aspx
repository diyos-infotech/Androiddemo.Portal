<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="M_Leads.aspx.cs" Inherits="Jawan.Portal.M_Leads" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">

<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LEADS</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Marketing.css" rel="stylesheet" />
    <style type="text/css">
        lnkSubmit:active {
            margin: 0px 0px 0px 0px;
            background: url(~/Marketing_Images/Addicon.png) left center no-repeat;
            padding: 0em 1.2em;
            color: #336699;
            text-decoration: none;
            font-weight: normal;
            letter-spacing: 0px;
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

                <div align="center">
                    <asp:Label ID="lblMsg" runat="server" Style="border-color: #f0c36d; background-color: #f9edbe; width: auto; font-weight: bold; color: #CC3300;"></asp:Label>
                </div>

                <asp:ScriptManager runat="server" ID="Scriptmanager1">
                </asp:ScriptManager>

                <div class="container">
                    <table cellpadding="5" cellspacing="5" width="100%" style="margin: 10px; margin-left: 20px;">
                        <tr style="height: 32px">
                            <td style="width: 120px">
                                <asp:Label ID="lblLeadID" runat="server" Text=" Lead ID/Name :"></asp:Label>
                            </td>
                            <td style="width: 150px">
                                <asp:TextBox runat="server" ID="txtsearch" TabIndex="1"></asp:TextBox>
                            </td>
                            <td style="width: 110px">
                                <asp:Button ID="btnSearch" runat="server" Text="Search" class=" btn save" OnClick="btnSearch_Click" />
                            </td>

                            <td style="padding-right: 20px; float: right">
                                <asp:ImageButton ID="imgCal" ImageUrl="~/Marketing_Images/calendar.png" runat="server" ToolTip="All Leads Action Log Scheduler" OnClick="imgCal_Click" Style="padding-right: 10px; padding-top: 10px" />
                                <asp:ImageButton ID="imgAdd" ImageUrl="~/Marketing_Images/Addicon.png" runat="server" />
                                <a href="M_Add_Lead.aspx" class="lnkSubmit">Add Lead</a></td>
                        </tr>
                    </table>


                    <table class="header" style="margin-bottom: 10px">
                        <tr style="font-weight: bold; height: 25px; font-size: 17px; text-align: center">
                            <td>LEAD DETAILS
                            </td>
                        </tr>
                    </table>



                    <div class="panel-body">
                        <asp:GridView ID="gvclient" runat="server" CellPadding="2" ForeColor="Black"
                            AutoGenerateColumns="False" Width="100%" BackColor="#f9f9f9" BorderColor="LightGray"
                            BorderWidth="1px" AllowPaging="True" OnRowDeleting="gvDetails_RowDeleting" OnPageIndexChanging="gvclient_PageIndexChanging">
                            <RowStyle Height="30px" />
                            <Columns>
                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                    HeaderStyle-Width="5%">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText=" Lead ID" ItemStyle-Width="40px" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:Label ID="lblclientid" runat="server" Text='<%#Bind("LeadID")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Lead Details" ItemStyle-Width="200px" HeaderStyle-Width="200px" ItemStyle-HorizontalAlign="Left" ItemStyle-Font-Size="12px">
                                    <ItemTemplate>
                                        <asp:Label ID="lbllname" runat="server" Text="Lead Name : " Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblname" runat="server" Text='<%#Bind("LeadName")%>'></asp:Label><br />
                                        <asp:Label ID="lbllstate" runat="server" Text="State : " Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblstate" runat="server" Text='<%#Bind("State")%>'></asp:Label><br />
                                        <asp:Label ID="lbllcity" runat="server" Text="City : " Font-Bold="true"></asp:Label>
                                        <asp:Label ID="lblcity" runat="server" Text='<%#Bind("City")%>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle Width="40px"></ItemStyle>
                                </asp:TemplateField>

                                <asp:BoundField HeaderText="Revenue" ItemStyle-Width="30px" DataField="Revenue" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:BoundField>

                                <asp:BoundField HeaderText="Expected date of closure" ItemStyle-Width="30px" DataField="ExpectedDate" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:BoundField>


                                <asp:BoundField HeaderText="Lead Status" ItemStyle-Width="30px" DataField="leadstatus" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle Width="30px"></ItemStyle>
                                </asp:BoundField>

                                <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="IbModify" ImageUrl="~/css/assets/edit.png" runat="server" ToolTip="Modify" OnClick="IbModify_Click" />
                                        <asp:ImageButton ID="IbLeadReq" ImageUrl="~/Marketing_Images/requirement.png" runat="server" ToolTip="Lead Requirement" OnClick="IbLeadReq_Click" />
                                        <asp:ImageButton ID="IbActionLog" ImageUrl="~/Marketing_Images/calendar.png" runat="server" ToolTip="Action Log" OnClick="IbActionLog_Click" />
                                    </ItemTemplate>
                                    <ItemStyle Width="40px"></ItemStyle>
                                    <HeaderStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="Tan" />
                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                            <HeaderStyle BackColor="White" Font-Bold="True" Height="30px" />
                            <AlternatingRowStyle BackColor="White" Height="30px" />
                        </asp:GridView>
                        <asp:Label ID="lblresult" runat="server" Visible="false" Text="" Style="color: Red"></asp:Label>
                    </div>



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
