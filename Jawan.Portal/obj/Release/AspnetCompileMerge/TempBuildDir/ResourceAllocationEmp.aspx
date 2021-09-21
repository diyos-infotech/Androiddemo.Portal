<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceAllocationEmp.aspx.cs" Inherits="Jawan.Portal.ResourceAllocationEmp" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>FACILITY MANAGEMENT SOFTWARE</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">

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

    </script>
</head>
<body>
    <form id="NewLoan1" runat="server">
        <asp:ScriptManager runat="server" ID="Scriptmanager1">
        </asp:ScriptManager>
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
                        <li>Welcome <b>
                            <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                        <li class="lang"><a href="Login.aspx">Logout</a></li>
                    </ul>
                </div>
                <!-- TOP INFO END -->
                <!-- MAIN MENU BEGIN -->
                <div id="mainmenu">
                    <ul>
                        <li class="first"><a href="Employees.aspx" id="Employeeslink" runat="server" class="current">
                            <span>Employees</span></a></li>
                        <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        --%>
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
                                    <div id="submenu" class="submenu">
                                        <div class="submenubeforegap">
                                            &nbsp;
                                        </div>
                                        <%--    <div class="submenuactions">
                                        &nbsp;</div> --%>
                                        <ul>
                                            <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                            <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                            <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                            <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                            <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                            <li class="current"><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                            <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payments</span></a></li>
                                            <%-- <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
                                            <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                            <%--             <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>
                                            --%>
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
            <div class="content-holder" style="height: auto">
                <h1 class="dashboard_heading">Loans Dashboard</h1>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea" style="min-height: 400px; height: auto">
                    <div class="dashboard_full">
                        <%-- <div class="sidebox">--%>
                        <%--<div class="boxhead">
                           <h1 style="font-weight: 700; color: #FFFFFF; font-size: 10pt; background-color: #006699;
                                            text-align: center;">
                        MODIFY STOCK</h1></div>--%>
                        <div class="contentarea" id="Div1">
                            <ul>
                                <li class="left leftmenu">
                                    <ul>
                                        <li><a href="NewLoan.aspx" runat="server" id="NewLoanLink">New Loan</a></li>
                                        <li><a href="ModifyLoan.aspx" runat="server" id="ModifyLoanLink">Modify Loan</a>

                                        </li>
                                        <li><a href="LoanRecovery.aspx" runat="server" id="LoanRecoveryLink">Recovery Details</a></li>
                                        <li><a href="LoanRepayment.aspx" runat="server" id="LoanRepaymentLink">Loan Repayment</a></li>
                                        <li><a href="ResourceAllocationEmp.aspx" class="sel" runat="server" id="ResourceAllocationEmpLink">Resource Issue</a></li>
                                        <li><a href="ResourceReturnEmp.aspx" runat="server" id="ResourceReturnLink">Resource
                                        Return</a></li>
                                    </ul>
                                </li>
                                <li class="right">
                                    <div id="right_content_area" style="text-align: left; font: Tahoma; margin-right: 10px; font-size: small; font-weight: bold; height: auto">
                                        <div>
                                            <div class="dashboard_firsthalf">
                                                <table>
                                                    <tr>
                                                        <td>Emp ID<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlEmpID" Width="153px" Height="20px" AutoPostBack="True"
                                                                OnSelectedIndexChanged="ddlEmpID_SelectedIndexChanged" TabIndex="1">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Issue Mode:
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlFreepaid" runat="server" Width="148px" AutoPostBack="True">
                                                                <asp:ListItem>Chargeble</asp:ListItem>
                                                                <asp:ListItem>Free Issue</asp:ListItem>
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>No Of Installments
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtnoofinstallments" runat="server" Width="148px" TabIndex="3"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td class="style3">Referred By
                                                        </td>
                                                        <td>
                                                            <%-- <asp:TextBox ID="txtRefered"  runat="server"  Width="152px" TabIndex="31"></asp:TextBox>--%>
                                                            <cc1:ComboBox ID="ddlreferedby" Width="125px" runat="server" AutoCompleteMode="SuggestAppend"
                                                                DropDownStyle="DropDownList" TabIndex="31">
                                                            </cc1:ComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="dashboard_secondhalf">
                                                <table>
                                                    <tr>
                                                        <td>Emp Name<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList runat="server" ID="ddlempmname" Width="153px" Height="20px" TabIndex="2"
                                                                AutoPostBack="True" OnSelectedIndexChanged="ddlempmname_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Loan Date<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtloandate" TabIndex="4" runat="server" size="20" MaxLength="10"
                                                                onkeyup="dtval(this,event)"></asp:TextBox>
                                                            <cc1:CalendarExtender ID="CELoanDate" runat="server" Enabled="true" TargetControlID="txtLoanDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FTBELoanDate" runat="server" Enabled="True" TargetControlID="txtLoanDate"
                                                                ValidChars="/0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Loan Id
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtloanid" runat="server" Width="148px" ReadOnly="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>Paid Amount
                                                        </td>
                                                        <td>
                                                            <asp:TextBox runat="server" Width="120px" ID="txtPaidAmnt" TabIndex="5"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server" Enabled="True"
                                                                TargetControlID="txtPaidAmnt" ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <%--<tr>
                                                     	 <td>
                                                </tr>--%>
                                                </table>
                                            </div>
                                        </div>
                                        <asp:TextBox ID="txttotal" runat="server" ReadOnly="true"></asp:TextBox>

                                        <br />
                                        <div style="margin-top: 90px; overflow: scroll; height: 300px; width: 100%">
                                            <asp:GridView ID="gvresources" runat="server" CellPadding="4" AutoGenerateColumns="false"
                                                ForeColor="#333333" GridLines="None" OnRowDataBound="gvresources_databound">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select" ItemStyle-Width="10%">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CbChecked" runat="server" Checked="false" AutoPostBack="true" OnCheckedChanged="CbChecked_CheckedChanged" />
                                                        </ItemTemplate>

                                                        <ItemStyle Width="10%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="ID" ItemStyle-Width="5%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresourceid" runat="server" Text="<%#Bind('ResourceID')%>"></asp:Label>
                                                            <%--  --%>
                                                        </ItemTemplate>

                                                        <ItemStyle Width="5%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Resource Name" ItemStyle-Width="40%">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblresourcename" runat="server" Text="<%#Bind('ItemName')%>"></asp:Label>
                                                            <%--  --%>
                                                        </ItemTemplate>

                                                        <ItemStyle Width="40%"></ItemStyle>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Quantity" ItemStyle-Width="19%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtQty" runat="server" Width="50%" Text="1"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEQty" runat="server" Enabled="True" TargetControlID="txtQty"
                                                                ValidChars="0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                            <%----%>
                                                        </ItemTemplate>

                                                        <ItemStyle Width="19%"></ItemStyle>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Price" ItemStyle-Width="20%">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtresourceprice" runat="server" Width="90%" Enabled="false" Text="<%#Bind('Price') %>"></asp:TextBox>
                                                            <%----%>
                                                        </ItemTemplate>

                                                        <ItemStyle Width="20%"></ItemStyle>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#2461BF" />
                                                <AlternatingRowStyle BackColor="White" />
                                            </asp:GridView>
                                            <asp:Label ID="lblTotalamt" runat="server" Text=""></asp:Label>
                                        </div>
                                    </div>
                                </li>
                            </ul>
                        </div>
                    </div>
                    <div class="clear">
                        <br />
                    </div>
                    <!-- DASHBOARD CONTENT END -->
                </div>
                <div class=" buttons_holder" style="text-align: right; padding: 5px 30px 20px 0px;">
                    <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"></asp:Label>
                    <asp:Button ID="btnSave" runat="server" ValidationGroup="a" Text="SAVE" ToolTip="SAVE"
                        TabIndex="5" class="btn save" OnClientClick='return confirm("Are you sure you want to generate a new loan?");'
                        OnClick="btnSave_Click" />
                    <asp:Button ID="btncancel" runat="server" ValidationGroup="b" TabIndex="6" Text="CANCEL"
                        ToolTip="CANCEL" class=" btn save" OnClientClick='return confirm("Are you sure you want  to cancel this entry?");' />
                </div>
            </div>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.Com" target="_blank">Powered byWeb Wonders/a&gt;
                    </div>
                    <div class="footercontent">
                        <a href="#">Terms &amp; Conditions</a>| <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <!-- FOOTER END -->
        </div>
    </form>
</body>
</html>
