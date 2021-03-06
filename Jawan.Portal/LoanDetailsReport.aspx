<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoanDetailsReport.aspx.cs" Inherits="Jawan.Portal.LoanDetailsReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: EMPLOYEE SUMMARY REPORT</title>
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
    <style type="text/css">
        .style1 {
            width: 135px;
        }

        .completionList {
            background: white;
            border: 1px solid #DDD;
            border-radius: 3px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            min-width: 165px;
            height: 200px;
            overflow: auto;
        }

        .listItem {
            display: block;
            padding: 5px 5px;
            border-bottom: 1px solid #DDD;
        }

        .itemHighlighted {
            color: black;
            background-color: rgba(0, 0, 0, 0.1);
            text-decoration: none;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            border-bottom: 1px solid #DDD;
            display: block;
            padding: 5px 5px;
        }

        .visibility {
            visibility: hidden;
        }
    </style>
</head>
<body>
    <form id="EmpTransferDetailReports1" runat="server">
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
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                        <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
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
                                <div style="display: inline">
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li class="current"><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"
                                                runat="server"><span>Employees</span></a></li>
                                            <li><a href="ClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
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
                        <li><a href="Reports.aspx" style="z-index: 8;">Employee Reports</a></li>
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Emp Loan Report</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Emp Loan Report
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">
                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>
                                    <div align="right">
                                        <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" Visible="False">Export to Excel</asp:LinkButton>
                                    </div>

                                    <div class="dashboard_firsthalf" style="width: 100%">
                                        <table width="80%" cellpadding="5" cellspacing="5">


                                            <tr>
                                                <td>
                                                    <asp:Label runat="server" ID="lblempid" Text="Emp ID" Width="60px"></asp:Label></td>

                                                <td>
                                                    <asp:TextBox ID="txtEmpid" runat="server" CssClass="sinput" AutoPostBack="true" OnTextChanged="txtEmpid_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="EmpIdtoAutoCompleteExtender" runat="server"
                                                        ServiceMethod="GetEmpID"
                                                        ServicePath="AutoCompleteAA.asmx"
                                                        MinimumPrefixLength="4"
                                                        CompletionInterval="10"
                                                        EnableCaching="true"
                                                        TargetControlID="TxtEmpID"
                                                        FirstRowSelected="false"
                                                        CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>
                                                <%--<td>
                                                <asp:DropDownList ID="ddlempid" runat="server" class="sdrop" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlempid_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>--%>


                                                <td>
                                                    <asp:Label runat="server" ID="lblempname" Text="Emp Name" Width="80px"></asp:Label>

                                                </td>
                                                <td>
                                                    <%--<asp:DropDownList ID="ddlempname" runat="server" class="sdrop" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlempname_SelectedIndexChanged">
                                                </asp:DropDownList>--%>

                                                    <asp:TextBox ID="txtName" runat="server" CssClass="sinput" AutoPostBack="true" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                                    <cc1:AutoCompleteExtender ID="EmpNameAutoCompleteExtender" runat="server"
                                                        ServiceMethod="GetEmpName"
                                                        ServicePath="AutoCompleteAA.asmx"
                                                        MinimumPrefixLength="4"
                                                        CompletionInterval="10"
                                                        EnableCaching="true"
                                                        TargetControlID="txtName"
                                                        FirstRowSelected="false"
                                                        CompletionListCssClass="completionList"
                                                        CompletionListItemCssClass="listItem"
                                                        CompletionListHighlightedItemCssClass="itemHighlighted">
                                                    </cc1:AutoCompleteExtender>
                                                </td>

                                                <td>
                                                    <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btn_Submit_Click" />
                                                </td>
                                            </tr>
                                        </table>

                                    </div>
                                    <div class="rounded_corners" style="overflow: scroll">
                                        <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                            CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="None"
                                            OnRowDataBound="GVListEmployees_RowDataBound">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <Columns>
                                                <asp:BoundField DataField="EmpId" HeaderText="Emp Id" />
                                                <asp:BoundField DataField="EmpMname" HeaderText="Name" />
                                                 <asp:BoundField DataField="LoanNo" ItemStyle-HorizontalAlign="Center" HeaderText="Loan No" />
                                                <asp:BoundField DataField="LoanIssuedDate" HeaderText="Issued Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd/MM/yyyy}" />
                                                <asp:BoundField DataField="LoanAmount" ItemStyle-HorizontalAlign="Right" HeaderText="Loan Amount" />
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>

                                    <div style="margin-top: 20px; margin-left: 20px">
                                        <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"></asp:Label>
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
