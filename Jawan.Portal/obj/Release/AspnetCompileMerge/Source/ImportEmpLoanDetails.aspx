<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportEmpLoanDetails.aspx.cs" Inherits="Jawan.Portal.ImportEmpLoanDetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: MENU</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <link rel="stylesheet" href="css/global.css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
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



        .Grid, .Grid th, .Grid td {
            border: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <form id="FundRequest" runat="server">
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
                        <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Import Loan Details</a></li>
                    </ul>
                </div>

                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Import Loan Details
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">

                                      <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                        </asp:ScriptManager>

                                    <div class="dashboard_firsthalf" style="width: 100%">

                                        <table width="100%" cellpadding="5" cellspacing="5">

                                            <tr>
                                                <td colspan="6">

                                                    <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="LinkSample_Click" Style="float:right">Export Sample Excel</asp:LinkButton>

                                                </td>
                                            </tr>
                                            <tr style="padding-top:10px">

                                                <td style="width: 150px">Select File:

                                                </td>
                                                <td width="20px">
                                                    <asp:FileUpload ID="FlUploadLoanDetails" runat="server" />
                                                </td>
                                                <td>

                                                    <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click"></asp:Button>


                                                </td>

                                                 <td style="padding-left:170px">Excel No</td>
                                                <td>
                                                    <asp:DropDownList ID="ddlExcelNo" runat="server" CssClass="sdrop">

                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"></asp:Button>
                                                </td>

                                            </tr>
                                        </table>

                                        </div>

                                      <asp:HiddenField ID="hidGridView" runat="server" />
                                        <div id="forExport" class="rounded_corners" style=" padding: 10px">
                                            <style type="text/css">
                                                .SubTotalRowStyle {
                                                    font-weight: bold;
                                                    
                                                }

                                                .HeaderRowStyle {
                                                    font-weight: bold;
                                                    background-color: #507CD1;
                                                    color: White;
                                                }
                                            </style>

                                         <asp:GridView ID="GvLoansImported" runat="server" AutoGenerateColumns="False" Width="100%"
                                                ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center; margin: 0px auto" Height="50px" HeaderStyle-HorizontalAlign="Center"
                                                OnRowCreated="GvLoansImported_RowCreated" OnDataBound="GvLoansImported_DataBound">
                                    <RowStyle  Height="20" />

                                                <Columns>

                                                    <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="empid" HeaderText="Emp ID" ItemStyle-Width="60px" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Empname" HeaderText="Emp Name" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="Loantype" HeaderText="Loan Type" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="LoanAmount" HeaderText="Loan Amount" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="NoInstalments" HeaderText="No of Instalments" HeaderStyle-HorizontalAlign="Center" />


                                                    <asp:TemplateField HeaderText="Loan Cutting Month"  HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCuttingMonth" runat="server" Text='<%#Bind("LoanIssuedDate") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Loan Issued Date"  HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblIssuedDate" runat="server" Text='<%#Bind("LoanDt") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="center" />
                                                    </asp:TemplateField>

                                                    <asp:BoundField DataField="Created_On" HeaderText="Uploaded Time" ItemStyle-Width="150px" HeaderStyle-HorizontalAlign="Center"/>
                                                    <asp:BoundField DataField="Created_By" HeaderText="Uploaded By" HeaderStyle-HorizontalAlign="Center"/>


                                                    <asp:BoundField DataField="typeofloan" HeaderText="typeofloan" Visible="false"  />


                                                </Columns>
                                                <FooterStyle Font-Bold="True" ForeColor="White" />
                                                <PagerStyle ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle Font-Bold="True" ForeColor="#333333" />
                                            </asp:GridView>

                                        <asp:GridView ID="GvInputEmpLoanDetails" runat="server" AutoGenerateColumns="False" Width="90%" Visible="false"
                                                    ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center; margin: 0px auto; margin-top: 10px;">
                                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="ID NO">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtidno" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Loan Type">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtloantype" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Amount">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="txtamount" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="NoofInstalments">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblinstalments" Text=" "></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="LoanIssuedDate">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblloanissueddate" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="LoanCuttingFrom">
                                                            <ItemTemplate>
                                                                <asp:Label runat="server" ID="lblcuttingmonth" Text=""></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>



                                                    </Columns>
                                                    <HeaderStyle BackColor="#fcf8e3" Font-Bold="True" ForeColor="Black" Height="28px" />
                                                </asp:GridView>

                                               
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="clear">
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
