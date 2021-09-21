<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeePayments.aspx.cs" Inherits="Jawan.Portal.EmployeePayments" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EMPLOYEE PAYSHEET</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1 {
            width: 135px;
        }


        .modalBackground {
            background-color: Gray;
            z-index: 10000;
        }
    </style>
</head>
<body>
    <form id="EmployeePayments1" runat="server">
        <!-- HEADER SECTION BEGIN -->
        <div id="headerouter">
            <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <!-- LOGO AND MAIN MENU SECTION BEGIN -->
            <div id="header">
                <!-- LOGO BEGIN -->
                <div id="logo">
                    <a>
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
                        <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server" class="current">
                            <span>Employees</span></a></li>
                        <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
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
            <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
            <div id="submenu">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div style="display: inline;">
                                    <div class="submenu">
                                        <%--<div class="submenubeforegap">
                                        &nbsp;</div>--%>
                                        <ul>
                                            <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                            <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                            <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                            <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                            <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                            <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                            <li class="current"><a href="EmployeePayments.aspx" id="PaymentLink" runat="server">
                                                <span>Payment</span></a></li>
                                            <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
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
                <h1 class="dashboard_heading">Payments Dashboard</h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Employee Payments
                                </h2>
                            </div>
                            <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                                <div class="boxin">
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td>Client ID : <span style="color: Red">*</span>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlClients" runat="server" class="sdrop" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlClients_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Client Name :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlcname" runat="server" class="sdrop" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged">
                                                </asp:DropDownList>
                                            </td>
                                            <td>Month :
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddlmonth" Width="100px" runat="server" class="sdrop" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="Txt_Month" Width="100px" runat="server" AutoPostBack="true" class="sinput"
                                                    Text="" Visible="false" OnTextChanged="Txt_Month_TextChanged"></asp:TextBox>
                                                <cc1:CalendarExtender ID="Txt_Month_CalendarExtender" runat="server"
                                                    Enabled="true" Format="dd/MM/yyyy" TargetControlID="Txt_Month">
                                                </cc1:CalendarExtender>
                                                <cc1:FilteredTextBoxExtender ID="Txt_Month_FilteredTextBoxExtender"
                                                    runat="server" Enabled="True" TargetControlID="Txt_Month"
                                                    ValidChars="/0123456789">
                                                </cc1:FilteredTextBoxExtender>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:CheckBox ID="Chk_Month" runat="server"
                                                Text="Old" />


                                                <%--  OnTextChanged="Txt_Month_OnTextChanged"--%>




                                                <cc1:ModalPopupExtender ID="modelLogindetails" runat="server" TargetControlID="Chk_Month" PopupControlID="pnllogin"
                                                    BackgroundCssClass="modalBackground">
                                                </cc1:ModalPopupExtender>

                                            </td>
                                            <td>
                                                <asp:Button ID="btnpayment" runat="server" Text="Generate Payment " class=" btn save"
                                                    OnClick="btnpayment_Click" Width="120px" OnClientClick='return confirm("Are you sure you want  to genrate  payment?");' />
                                                <asp:LinkButton ID="linkrefresh" runat="server" Text="Refresh" Visible="false" OnClick="linkrefresh_Click"></asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                    <br />
                                    <div class="dashboard_full">
                                        <table width="100%" cellpadding="5" cellspacing="5">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="Button1" runat="server" Text="Wage Slips" Visible="false" class="btn save"
                                                        OnClick="btnEmployeeWageSlip_Click" />
                                                </td>
                                                <td>
                                                    <asp:Button ID="Button3" runat="server" Text="Wage Slips New" class="btn save"
                                                        OnClick="btnEmpWageSlip_Click" />
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="ChkPerOne" runat="server" Text="Wage Slip Two" />

                                                    <asp:DropDownList ID="ddlfontSize" runat="server">
                                                        <asp:ListItem>10</asp:ListItem>
                                                        <asp:ListItem>9</asp:ListItem>
                                                        <asp:ListItem>8</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>




                                                <td>
                                                    <asp:Button ID="Button4" runat="server" Text="Wage Slip (Dts)" class="btn save" Visible="false"
                                                        OnClick="btnGameshaWageSlip_Click" />
                                                </td>


                                                <td>
                                                    <asp:Button ID="Button2" runat="server" Text="Wage Slips Only Dts" Visible="false" class="btn save"
                                                        OnClick="btnEmployeeWageSliponlydts_Click" />
                                                </td>
                                                <td>
                                                    <%-- Attendance :--%>
                                                </td>
                                                <td style="visibility: hidden">
                                                    <asp:DropDownList ID="ddlnoofattendance" class="sdrop" Width="75px" runat="server">
                                                        <asp:ListItem Selected="True">All</asp:ListItem>
                                                        <asp:ListItem>10-Above</asp:ListItem>
                                                        <asp:ListItem>0-10</asp:ListItem>
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <%-- Order By :--%>
                                                </td>
                                                <td>
                                                    <asp:RadioButton ID="radioempid" runat="server" Checked="true" GroupName="Orderby" Visible="false"
                                                        Text="Empid" />
                                                    <asp:RadioButton ID="radiobankno" runat="server" GroupName="Orderby" Visible="false" Text="Bank A/C No" />
                                                </td>
                                                <td>Payment Options :
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="ddlpaymenttype" runat="server" Width="125px" class="sdrop">
                                                        <asp:ListItem>Form T</asp:ListItem>
                                                        <asp:ListItem>With PF</asp:ListItem>
                                                        <asp:ListItem>Without PF</asp:ListItem>
                                                        <asp:ListItem>ALL</asp:ListItem>
                                                        <asp:ListItem>New Paysheet</asp:ListItem>
                                                        <%--<asp:ListItem>Form T</asp:ListItem>
                                                     <asp:ListItem>Only Duties </asp:ListItem>
                                                     <asp:ListItem>Only OTs </asp:ListItem>
                                                     <asp:ListItem>Only WOs</asp:ListItem>
                                                     <asp:ListItem>Only NHs </asp:ListItem>
                                                     <asp:ListItem>Only Duties (1)  </asp:ListItem>
                                                    <asp:ListItem>Duties+Ots</asp:ListItem>
                                                    <asp:ListItem>All</asp:ListItem>
                                                     <asp:ListItem>Incentives Sheet</asp:ListItem>--%>
                                                    </asp:DropDownList>
                                                    <%--<asp:DropDownList ID="ddl_Pf_Esi_Options" runat="server" Width="75px" class="sdrop">
                                                <asp:ListItem>ALL</asp:ListItem>
                                                <asp:ListItem>PF</asp:ListItem>
                                                <asp:ListItem>ESI</asp:ListItem>
                                                <asp:ListItem>NO PF</asp:ListItem>
                                                <asp:ListItem>NO ESI</asp:ListItem>
                                                <asp:ListItem>No PF/ESI</asp:ListItem>
                                                  </asp:DropDownList>--%>
                                                
                                                </td>

                                                <td>
                                                    <asp:Button ID="btndownloadpdffile" runat="server" Text="Download" class="btn save"
                                                        OnClick="btndownloadpdffile_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </div>


                                    <asp:Panel ID="pnllogin" runat="server" Height="100px" Width="300px" Style="display: none; position: absolute; background-color: Silver;">
                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <table>
                                                    <tr>
                                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="font: bold; font-size: medium">&nbsp;&nbsp;&nbsp;
                            Enter Password:
                                                        </td>
                                                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                </table>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        <br />
                                        <table style="background-position: center;">
                                            <tr>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                              <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" class="btn Save" />
                                                </td>
                                                <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                             <asp:Button ID="btnClose" runat="server" Text="Close" OnClick="btnClose_Click" class="btn Save" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>

                                    <br />
                                    <div class="rounded_corners" style="overflow: auto; width: 99%">


                                        <asp:Panel ID="PnlNonGeneratedPaysheet" runat="server"
                                            Visible="false">
                                            <div style="border: 1px solid #A1DCF2; margin-left: 13px; width: 98%; text-align: center; width: 94%; padding: 15px">
                                                <asp:Label ID="lblPaysheetGeneratedTime" runat="server" Text="Label"></asp:Label><br />
                                                <asp:GridView ID="GvBillVsPaysheet" runat="server" AutoGenerateColumns="False" GridLines="None" CellPadding="10" Style="margin: 0px auto; margin-top: 10px;" Visible="false">
                                                    <Columns>

                                                        <asp:TemplateField HeaderText="Type" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblType" runat="server" Text='<%#Bind("Type") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <%--<asp:TemplateField HeaderText="Billing Duties" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="130px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblBillingDuties" runat="server" Text='<%#Bind("BillingDuties") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>--%>
                                                        <asp:BoundField HeaderText="Billing Duties" DataField="BillingDuties" NullDisplayText="0" />

                                                        <asp:TemplateField HeaderText="Paysheet Duties" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="130px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblPaysheetDuties" runat="server" Text='<%#Bind("PaysheetDuties") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Difference" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="150px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDifference" runat="server" Text='<%#Bind("Difference") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                <asp:Label ID="lblText" runat="server" Text="" Visible="false"></asp:Label><br />
                                                <asp:Label ID="lblReason" runat="server" Text="" Visible="false"></asp:Label><br />
                                                <asp:GridView ID="GvNonGeneratedEmp" runat="server" AutoGenerateColumns="False" GridLines="None" CellPadding="10" Style="margin: 0px auto; margin-top: 10px;" Visible="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Bind("Designation") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                                <br />
                                                <asp:Label ID="lblEmplist" runat="server" Text="" Visible="false"></asp:Label><br />
                                                <asp:GridView ID="GvEmpList" runat="server" AutoGenerateColumns="False" GridLines="None" CellPadding="10" Style="margin: 0px auto; margin-top: 10px;" Visible="false">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="50px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSlno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>

                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Emp ID" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="100px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpid" runat="server" Text='<%#Bind("EmpId") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Emp Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblEmpName" runat="server" Text='<%#Bind("empname") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Designation" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Left" HeaderStyle-Width="200px">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblDesignation" runat="server" Text='<%#Bind("Designation") %>' Style="padding-left: 7px;"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>

                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </asp:Panel>
                                        <asp:GridView ID="gvattendancezero" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="BlueViolet"
                                            EmptyDataRowStyle-BorderColor="Aquamarine" EmptyDataText="No Records Found" Width="100%"
                                            CellPadding="4" CellSpacing="3" ForeColor="#333333" GridLines="None" OnPageIndexChanging="gvattendancezero_PageIndexChanging"
                                            ShowFooter="true">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <EmptyDataRowStyle BackColor="LightSkyBlue" BorderColor="Aquamarine" Font-Italic="false"
                                                Font-Bold="true" />
                                            <Columns>
                                                <%-- 0 OnRowDataBound="gvattendancezero_RowDataBound"--%>
                                                <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 1--%>
                                                <asp:TemplateField HeaderText="Emp Id" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempid" runat="server" Text='<%#Bind("EmpId") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 2--%>
                                                <asp:TemplateField HeaderText="Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblempname" runat="server" Text='<%#Bind("EmpMname") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Left" />
                                                </asp:TemplateField>
                                                <%-- 3--%>
                                                <asp:TemplateField HeaderText="Designation" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldesgn" runat="server" Text='<%#Bind("Desgn") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <%-- 4--%>
                                                <asp:TemplateField HeaderText="Duties" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbldutyhrs" runat="server" Text='<%#Bind("NoOfDuties") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDuties"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 5--%>
                                                <asp:TemplateField HeaderText="OTs" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOts" runat="server" Text='<%#Bind("OTs") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOTs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 6--%>
                                                <asp:TemplateField HeaderText="WO" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwos" runat="server" Text='<%#Bind("WO") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalwos"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 7--%>
                                                <asp:TemplateField HeaderText="Nhs" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNhs" runat="server" Text='<%#Bind("NHS") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNhs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 8--%>
                                                <asp:TemplateField HeaderText="Npots" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNpots" runat="server" Text='<%#Bind("npots") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNpots"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 9--%>
                                                <asp:TemplateField HeaderText="Pay Rate" ItemStyle-Width="2%" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltempgross" runat="server" Text='<%#Bind("TempGross") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotaltempgross"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>




                                                <%-- 10--%>

                                                <asp:TemplateField HeaderText="Basic" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <%-- <asp:Label ID="lblbasic" runat="server" Text='<%#Bind("basic") %>'>--%>
                                                        <asp:Label ID="lblbasic" runat="server" Text='<%#Eval("basic", "{0:0}") %>'>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalBasic"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 11--%>
                                                <asp:TemplateField HeaderText="DA" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblda" runat="server" Text='<%#Eval("da","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 12--%>
                                                <asp:TemplateField HeaderText="HRA" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblhra" runat="server" Text='<%#Bind("hra","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalHRA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 13--%>
                                                <asp:TemplateField HeaderText="CCA" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcca" runat="server" Text='<%#Bind("CCa","{0:0}") %>'>  
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalCCA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 14--%>
                                                <asp:TemplateField HeaderText="Conv" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblConveyance" runat="server" Text='<%#Bind("conveyance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalConveyance"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 15--%>
                                                <asp:TemplateField HeaderText="W.A." ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwashallowance" runat="server" Text='<%#Bind("WashAllowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalWA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 16--%>
                                                <asp:TemplateField HeaderText="O.A." ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOtherallowance" runat="server" Text='<%#Bind("OtherAllowance","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOA"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 17--%>
                                                <asp:TemplateField HeaderText="L.W" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblLeaveEncashAmt" runat="server" Text='<%#Bind("LeaveEncashAmt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalLeaveEncashAmt"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 18--%>
                                                <asp:TemplateField HeaderText="Gratuity" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGratuity" runat="server" Text='<%#Bind("Gratuity","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalGratuity"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 19--%>
                                                <asp:TemplateField HeaderText="Bonus" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblBonus" runat="server" Text='<%#Bind("Bonus","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalBonus"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 20--%>
                                                <asp:TemplateField HeaderText="Nfhs" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNfhs" runat="server" Text='<%#Bind("Nfhs","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNfhs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 21--%>
                                                <asp:TemplateField HeaderText="RC" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblrc" runat="server" Text='<%#Bind("rc","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalrc"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 22--%>
                                                <asp:TemplateField HeaderText="CS" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcs" runat="server" Text='<%#Bind("cs","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalcs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                                <%-- 23--%>
                                                <asp:TemplateField HeaderText="Gross" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGross" runat="server" Text='<%#Bind("Gross","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalGross"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 24--%>

                                                <asp:TemplateField HeaderText="OT Amt" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOTAmt" runat="server" Text='<%#Bind("OTAmt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalOTAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 25--%>

                                                <asp:TemplateField HeaderText="WO Amt" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblWoAmt" runat="server" Text='<%#Bind("WOAmt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalWOAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 26--%>

                                                <asp:TemplateField HeaderText="NHs Amt" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNhsAmt" runat="server" Text='<%#Bind("Nhsamt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNhsAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 27--%>

                                                <asp:TemplateField HeaderText="NPOTs Amt" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblNpotsAmt" runat="server" Text='<%#Bind("Npotsamt","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNpotsAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 28--%>

                                                <asp:TemplateField HeaderText="Incentivs" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblIncentivs" runat="server" Text='<%#Bind("Incentivs","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalIncentivs"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 29--%>
                                                <asp:TemplateField HeaderText="PF" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPF" runat="server" Text='<%#Bind("PF","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalPF"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 30--%>
                                                <asp:TemplateField HeaderText="ESI" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblESI" runat="server" Text='<%#Bind("ESI","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalESI"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 31--%>
                                                <asp:TemplateField HeaderText="P.T" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblProfTax" runat="server" Text='<%#Bind("ProfTax","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalProfTax"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 32--%>
                                                <asp:TemplateField HeaderText="S.A" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblsaladv" runat="server" Text='<%#Bind("SalAdvDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalsaladv"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>



                                                <%-- 33--%>
                                                <asp:TemplateField HeaderText="ADV Ded" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbladvded" runat="server" Text='<%#Bind("ADVDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotaladvded"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                <%-- 34--%>
                                                <asp:TemplateField HeaderText="WC Ded" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblwcded" runat="server" Text='<%#Bind("WCDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalwcded"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>


                                                <%-- 35--%>
                                                <asp:TemplateField HeaderText="Sec Dep" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblSecDepDed" runat="server" Text='<%#Bind("SecurityDepDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalSecDepDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 36--%>
                                                <asp:TemplateField HeaderText="Other Ded" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblOtherDed" runat="server" Text='<%#Bind("OtherDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalOtherDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 37--%>
                                                <asp:TemplateField HeaderText="Total Loan ded" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbltotalloanded" runat="server" Text='<%#Bind("LoanDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotaltotalloanded"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 38--%>
                                                <asp:TemplateField HeaderText="G.D" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblGeneralDed" runat="server" Text='<%#Bind("GeneralDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalGeneralDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 39--%>
                                                <asp:TemplateField HeaderText="C.A" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblcantadv" runat="server" Text='<%#Bind("CanteenAdv","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalcantadv"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 40--%>
                                                <asp:TemplateField HeaderText="OWF" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblowf" runat="server" Text='<%#Bind("OWF","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalowf"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 41--%>
                                                <asp:TemplateField HeaderText="Penalty" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPenalty" runat="server" Text='<%#Bind("Penalty","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalPenalty"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                   <%-- 42--%>
                                                <asp:TemplateField HeaderText="Telephone Bill Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTelephoneBillDed" runat="server" Text='<%#Bind("TelephoneBillDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: justify">
                                                            <asp:Label runat="server" ID="lblTotalTelephoneBillDed"></asp:Label>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%--<asp:TemplateField HeaderText="A.R" ItemStyle-Width="3%" ItemStyle-HorizontalAlign="Center">
                         <ItemTemplate>
                         <asp:Label ID="lblowfamt" runat="server" Text='<%#Bind("OWFAmt","{0:0}") %>'></asp:Label>
                         </ItemTemplate>     
                         <FooterTemplate>
                         
                           <div style="text-align:justify">
                            <asp:Label runat="server" ID="lblTotalowfAmt"></asp:Label>
                            </div>
                          </FooterTemplate>                     
                          </asp:TemplateField>
                                                --%>
                                                
                                                <%-- 43--%>
                                                <asp:TemplateField HeaderText="PF & ESI Contribution" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblPFESIContribution" runat="server" Text='<%#Bind("PFESIContribution","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate >
                                                        <asp:Label runat="server" ID="lblTotalPFESIContribution"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                 <%-- 44--%>
                                                 <asp:TemplateField HeaderText="TDS Ded" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTDSDed" runat="server" Text='<%#Bind("TDSDed","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate >
                                                        <asp:Label runat="server" ID="lblTotalTDSDed"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 45--%>
                                                <asp:TemplateField HeaderText="Dedn" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblDeductions" runat="server" Text='<%#Bind("TotalDeductions","{0:0}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalDeductions"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <%-- 46--%>
                                                <asp:TemplateField HeaderText="Net" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblnetamount" runat="server" Text='<%#Bind("ActualAmount","{0:0}") %>'> </asp:Label>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        <asp:Label runat="server" ID="lblTotalNetAmount"></asp:Label>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                        <br />

                                        <asp:GridView ID="GVNewpaysheet" runat="server" AutoGenerateColumns="true" OnRowDataBound="GVNewpaysheet_RowDataBound"  GridLines="None" CellPadding="10" Style="margin: 0px auto; margin-top: 10px;" Visible="false">
                                            <Columns>
                                            </Columns>

                                        </asp:GridView>
                                    </div>
                                    <div style="margin-left: 550px; margin-top: 150px; display: none">
                                        <asp:Label ID="lblpayment" runat="server" Text="Total Amount For This Month" Style="color: Red"></asp:Label>
                                        &nbsp; &nbsp; &nbsp;
                                    <asp:Label ID="lblamount" runat="server" Text=""></asp:Label>
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                                    <br />
                                        <asp:Label ID="lbltotaldesignationlist" runat="server"></asp:Label>
                                    </div>
                                    <!-- DASHBOARD CONTENT END -->
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                    </div>
                </div>
                <!-- DASHBOARD CONTENT END -->
            </div>
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS S </a>
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
        </div>
        <!-- CONTENT AREA END -->
    </form>
</body>
</html>
