<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forms.aspx.cs" Inherits="Jawan.Portal.Forms" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: FORMS</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style2 {
            font-size: 10pt;
            font-weight: bold;
            color: #333333;
            background: #cccccc;
            padding: 5px 5px 2px 10px;
            border-bottom: 1px solid #999999;
            height: 26px;
        }
    </style>
</head>
<body>
    <form id="ClentwiseEmployeesSalaryreports1" runat="server">
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
                        <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a></li>
                        <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                        <li class="after"><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                    </ul>
                </div>
                <!-- MAIN MENU SECTION END -->
            </div>
            <!-- LOGO AND MAIN MENU SECTION END -->
            <!-- SUB NAVIGATION SECTION BEGIN -->
            <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
            <div id="submenu" style="width: 100%">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div style="display: inline">
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>Employees</span></a></li>
                                            <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>Inventory</span></a></li>
                                            <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"><span>Companyinfo</span></a>
                                            </li>
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
                <div id="breadcrumb">
                    <ul class="crumbs">
                        <li class="first"><a href="#" style="z-index: 9;"><span></span>Reports</a></li>
                        <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Forms</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Forms
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>

                                    <div class="dashboard_firsthalf" style="width: 100%">

                                        <table width="60%" style="margin: 0px auto; margin-top: 10px">
                                            <tr>
                                                <td>Forms
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlforms" runat="server" AutoPostBack="True"
                                                        class="sdrop" OnSelectedIndexChanged="DropDownListTemplate_SelectedIndexChanged">
                                                        <asp:ListItem> -Select- </asp:ListItem>
                                                        <asp:ListItem> Form-VI </asp:ListItem>
                                                        <asp:ListItem> Form-XII </asp:ListItem>
                                                        <asp:ListItem> Form-XIV </asp:ListItem>
                                                        <asp:ListItem> form-XV </asp:ListItem>
                                                        <asp:ListItem> Form-XV </asp:ListItem>
                                                        <asp:ListItem> Form-XVII </asp:ListItem>
                                                        <asp:ListItem> Form-XXIII </asp:ListItem>
                                                        <asp:ListItem> Form-XXIV </asp:ListItem>
                                                        <asp:ListItem> Form-XIV </asp:ListItem>
                                                        <asp:ListItem> Form-XVI </asp:ListItem>
                                                        <asp:ListItem> Form-XIX</asp:ListItem>
                                                        <asp:ListItem> Form-XVIII </asp:ListItem>
                                                        <asp:ListItem> Form-VII </asp:ListItem>
                                                        <asp:ListItem> Form-XXI </asp:ListItem>
                                                        <asp:ListItem> Form-XXIX </asp:ListItem>
                                                        <asp:ListItem> Form-IV </asp:ListItem>
                                                        <asp:ListItem> Form-XIII </asp:ListItem>
                                                        <asp:ListItem> Form-XX </asp:ListItem>
                                                        <asp:ListItem> Form-XXII </asp:ListItem>
                                                        <asp:ListItem> Form-VA </asp:ListItem>
                                                        <asp:ListItem> Form-X </asp:ListItem>
                                                        <asp:ListItem> Form-9 </asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>

                                                <td>Month
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="Txt_Month" Width="120px" runat="server" AutoPostBack="true" class="sinput"
                                                        Text="" Visible="true"></asp:TextBox>
                                                    <cc1:CalendarExtender ID="Txt_Month_CalendarExtender" runat="server"
                                                        Enabled="true" Format="dd/MM/yyyy" TargetControlID="Txt_Month">
                                                    </cc1:CalendarExtender>

                                                </td>


                                            </tr>
                                            <asp:Panel ID="clientdetails" runat="server" Visible="false">
                                                <table width="60%" style="margin: 0px auto; margin-top: 10px">
                                                    <tr>
                                                        <td>Client Id
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlclientid" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlclientid_SelectedIndexChanged" class="sdrop">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>Name
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlcname" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged" class="sdrop">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </asp:Panel>
                                            <asp:Panel ID="empdetails" runat="server" Visible="false">
                                                <table width="60%" style="margin: 0px auto; margin-top: 10px">

                                                    <tr>
                                                        <td>Emp Id
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlempid" runat="server" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlempid_SelectedIndexChanged" class="sdrop">
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>Name
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlempmname" class="sdrop" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlempmname_OnSelectedIndexChanged">
                                                            </asp:DropDownList>
                                                            &nbsp;
                                                        </td>

                                                    </tr>
                                                </table>
                                            </asp:Panel>

                                        </table>

                                        <table style="margin: 0px auto; margin-top: 20px">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnformVI" runat="server" Text="FORM-VI" Width="105px"
                                                        Style="" class="btn save" Visible="true" OnClick="BtnformVI_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnformXII" runat="server" Text="FORM-XII" Width="105px"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXII_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="ButtonXIV" runat="server" Text="FORM-XIV" Width="105px"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="Buttonform_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnformXV" runat="server" Text="FORM-XV" Width="105px"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXV_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnformXVI1" runat="server" Text="FORM-XVII" Width="105px"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXVII1_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnformXXIII" runat="server" Text="FORM-XXIII" Width="105px"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="Btnform014_Click" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnformXXIV" runat="server" Text="FORM-XXIV" Width="105px"
                                                        Style="" class="btn save" Visible="true" OnClick="Btnform14_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Btnform2XIV" runat="server" Text="FORM2-XIV" Width="105px"
                                                        Style="margin-left: 5px;" Visible="true" class="btn save" OnClick="Btnform2XIV_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="BtnformXVI" runat="server" Text="FORM2-XVI" Width="105px"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXVI_Click" />
                                                </td>

                                                <td>
                                                    <asp:Button ID="BtnformXIX" runat="server" Text="FORM-XIX"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXIX_Click" Width="105px" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="BtnformXVIII" runat="server" Text="FORM-XVIII"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXVIII_Click" Width="105px" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="BtnformVII" runat="server" Text="FORM-VII"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformVII_Click" Width="105px" />
                                                </td>


                                            </tr>

                                            <tr>
                                                <td></td>
                                            </tr>

                                            <tr>

                                                <td>
                                                    <asp:Button ID="BtnformXXI" runat="server" Text="FORM-XXI"
                                                        Style="" class="btn save" Visible="true" OnClick="BtnformXXI_Click1" Width="105px" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="BtnformXXII" runat="server" Text="FORM-XXIX"
                                                        Style="margin-left: 5px;" Visible="true" class="btn save" OnClick="BtnformXXII_Click" Width="105px" />
                                                </td>

                                                <td>
                                                    <asp:Button ID="BtnformIV" runat="server" Text="FORM-IV"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformIV_Click" Width="105px" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="BtnformXIII" runat="server" Text="FORM-XIII"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXIII_Click" Width="105px" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="BtnformXX" runat="server" Text="FORM-XX"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnformXX_Click" Width="105px" />
                                                </td>



                                                <td>
                                                    <asp:Button ID="BtnnformXXII" runat="server" Text="FORM-XXII"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="BtnnformXXII_Click" Width="105px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td></td>
                                            </tr>

                                            <tr>
                                                <td>
                                                    <asp:Button ID="BtnformVA" runat="server" Text="FORM-VA"
                                                        Style="" class="btn save" Visible="false" OnClick="BtnformVA_Click" Width="105px" />
                                                </td>

                                                <td>
                                                    <asp:Button ID="BtnformX" runat="server" Text="FORM-X"
                                                        Style="margin-left: 5px;" Visible="false" class="btn save" OnClick="BtnformX_Click" Width="105px" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="Btnform9" runat="server" Text="FORM-9" class="btn save" Width="105px"
                                                        OnClick="Btnform9_Click" Visible="false" Style="margin-left: 5px;" />
                                                </td>
                                            </tr>
                                            <tr>

                                                <td>
                                                    <asp:Button ID="btnmusterroll" runat="server" Text="MUSTER-ROLL" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnmusterroll_Click" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="btnleavewages" runat="server" Text="LEAVE WITH WAGES" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnleavewages_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btngratuity" runat="server" Text="Gratutity" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btngratuity_Click" />
                                                </td>
                                                <%--       form(form-3a) eagle date 27-03-2016--%>
                                                <td>
                                                    <asp:Button ID="btnform3a" runat="server" Text="Form-3A  " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnform3a_Click" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="btnform5" runat="server" Text="FORM-5"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="btnform5_Click" Width="105px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnform10" runat="server" Text="FORM-10"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="btnform10_Click" Width="105px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnform12" runat="server" Text="FORM 12-A"
                                                        Style="margin-left: 5px;" class="btn save" Visible="true" OnClick="btnform12_Click" Width="105px" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnclearence" runat="server" Text="Clearence From" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnclearence_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btndeclaration" runat="server" Text="Declaration From" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btndeclaration_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnesicform" runat="server" Text="ESIC Form" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnesicform_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnesicform72" runat="server" Text="ESIC-72 Form" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnesicform72_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnform13" runat="server" Text="Form 13 " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnform13_Click" />
                                                </td>
                                                </tr>
                                                <tr>
                                                <td>
                                                    <asp:Button ID="btnform16" runat="server" Text="Form 16 " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnform16_Click" />
                                                </td>

                                                     <td>
                                                    <asp:Button ID="btnforma1" runat="server" Text="FormA1 " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnforma1_Click" />
                                                </td>

                                                       <td>
                                                    <asp:Button ID="btnformq" runat="server" Text="FormQ " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnformq_Click" />
                                                </td>
                                                     <td>
                                                    <asp:Button ID="btnpoliceverification" runat="server" Text="Police Verification " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnpoliceverification_Click" />
                                                </td>

                                                     <td>
                                                    <asp:Button ID="btnpfdeclaration" runat="server" Text="PF Declaration " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnpfdeclaration_Click" />
                                                </td>
                                                     <td>
                                                    <asp:Button ID="btnmedicalcertificate" runat="server" Text="Medical Certification " class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnmedicalcertificate_Click" />
                                                </td>

                                                </tr>
                                                    <tr>
                                                     <td>
                                                    <asp:Button ID="btnresign" runat="server" Text="Resign From" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnresign_Click" />
                                                </td>

                                                         <td>
                                                    <asp:Button ID="btnb2" runat="server" Text="B2 From" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnb2_Click" />
                                                </td>

                                                          <td>
                                                    <asp:Button ID="btnb5" runat="server" Text="B5 From" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnb5_Click" />
                                                </td>

                                                         <td>
                                                    <asp:Button ID="btnfinsettle" runat="server" Text="FinSettle" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnfinsettle_Click" />
                                                </td>

                                                           <td>
                                                    <asp:Button ID="btn3" runat="server" Text="B3 From" class="btn save" Width="105px"
                                                        Style="margin-left: 5px;" OnClick="btnb3_Click" />
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
            </div>
            <!-- DASHBOARD CONTENT END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS </a>
                    </div>
                    <!--    <div class="footerlogo">&nbsp;</div> -->
                    <div class="footercontent">
                        <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <!-- FOOTER END -->
            <!-- CONTENT AREA END -->
        </div>
    </form>
</body>
</html>
