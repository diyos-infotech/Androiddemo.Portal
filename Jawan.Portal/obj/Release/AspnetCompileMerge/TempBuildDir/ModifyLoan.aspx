<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyLoan.aspx.cs" Inherits="Jawan.Portal.ModifyLoan" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MODIFY LOAN</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />

    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.2/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script type="text/javascript" src="//code.jquery.com/ui/1.11.2/jquery-ui.js"></script>
    <script type="text/javascript">

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


        $(document).ready(function () {
            $('#txtEmpid').autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: 'Autocompletion.asmx/GetFormEmpIDs',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term
                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#txtEmpid").attr("data-Empid", ui.item.value); OnAutoCompletetxtEmpidchange(event, ui);
                }
            });




            $('#txtName').autocomplete({
                source: function (request, response) {
                    $.ajax({

                        url: 'Autocompletion.asmx/GetFormEmpNames',
                        method: 'post',
                        contentType: 'application/json;charset=utf-8',
                        data: JSON.stringify({
                            term: request.term
                        }),
                        datatype: 'json',
                        success: function (data) {
                            response(data.d);
                        },
                        error: function (err) {
                            alert(err);
                        }
                    });
                },
                minLength: 4,
                select: function (event, ui) {
                    $("#txtName").attr("data-EmpName", ui.item.value); OnAutoCompletetxtEmpNamechange(event, ui);
                }
            });
        });

        function OnAutoCompletetxtEmpidchange(event, ui) {
            $('#txtEmpid').trigger('change');

        }

        function OnAutoCompletetxtEmpNamechange(event, ui) {
            $('#txtName').trigger('change');

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
                        <li class="first"><a href="Employees.aspx" id="employeeslink" runat="server" class="current">
                            <span>Employees</span></a></li>
                        <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                                            <%--          <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
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
                <div class="contentarea" id="contentarea" style="height: auto">
                    <div class="dashboard_full">
                        <div class="sidebox">
                            <div class="boxhead">
                                <h2 style="text-align: center">Modify Loan
                                </h2>
                            </div>
                            <div class="contentarea" id="Div1">
                                <div class="boxinc">
                                    <ul>
                                        <li class="left leftmenu">
                                            <ul>
                                                <li><a href="NewLoan.aspx">New Loan</a></li>
                                                <li><a href="ModifyLoan.aspx" runat="server" class="sel">Modify Loan</a> </li>
                                                <li><a href="LoanRecovery.aspx">Recovery Details</a></li>
                                                <li><a href="LoanRepayment.aspx">Loan Repayment</a></li>
                                                <li><a href="ResourceAllocationEmp.aspx">Resource Issue</a></li>
                                                <li><a href="ResourceReturnEmp.aspx" runat="server" id="ResourceReturnLink">Resource Return</a></li>

                                            </ul>
                                        </li>
                                        <li class="right">
                                            <div class="dashboard_firsthalf" style="width: 100%">
                                                <table width="90%" cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellpadding="5" cellspacing="5" style="margin: 10px">
                                                                <tr style="height: 37px">
                                                                    <td>Emp ID<span style="color: Red">*</span>
                                                                    </td>
                                                                    <td>

                                                                        <asp:TextBox ID="txtEmpid" runat="server" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEmpid_TextChanged" Width="220px"></asp:TextBox>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Designation
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtdesignation" runat="server" ReadOnly="true" CssClass="form-control"> </asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td align="right">
                                                            <table cellpadding="5" cellspacing="5" style="margin: 10px">
                                                                <tr style="height: 37px">
                                                                    <td>Name<span style="color: Red">*</span>
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtName" runat="server" Width="220px" TabIndex="2" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>Month
                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtMonth" runat="server" Width="220px" TabIndex="2" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>
                                                                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" BehaviorID="calendar1"
                                                                            Enabled="true" Format="MMM-yyyy" TargetControlID="txtMonth" DefaultView="Months" OnClientHidden="onCalendarHidden" OnClientShown="onCalendarShown">
                                                                        </cc1:CalendarExtender>

                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>&nbsp;
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label ID="lblresult" runat="server" Text="" Visible="true" Style="color: Red"></asp:Label>
                                                                        <asp:Button ID="btnsavemodifyloan" runat="server" ValidationGroup="a" Text="SAVE"
                                                                            Visible="false" ToolTip="SAVE" class="btn save" OnClientClick='return confirm("Are you sure you want to generate a new loan?");'
                                                                            OnClick="btnsavemodifyloan_Click" />
                                                                        <asp:Button ID="btnedit" runat="server" Text="EDIT" OnClick="btnedit_OnClick" Visible="false"
                                                                            class="btn save" />
                                                                        <asp:Button ID="btncancel" runat="server" ValidationGroup="b" Text="CANCEL" ToolTip="CANCEL"
                                                                            Visible="false" class=" btn save" OnClientClick='return confirm("Are you sure you want  to cancel this entry?");' />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </div>
                                            <div class="rounded_corners">
                                                <asp:GridView ID="gvNewLoan" runat="server" AutoGenerateColumns="False" Width="99%"
                                                    Style="text-align: center" CellPadding="4" ForeColor="#333333" CssClass="table table-striped table-bordered table-condensed table-hover"
                                                    OnRowDataBound="gvNewLoan_RowDataBound" AllowPaging="True" OnPageIndexChanging="gvNewLoan_PageIndexChanging"
                                                    OnRowEditing="gvNewLoan_RowEditing" OnRowCancelingEdit="gvNewLoan_RowCancelingEdit"
                                                    OnRowUpdating="gvNewLoan_RowUpdating">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="3%">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>



                                                        <asp:TemplateField HeaderText="LoanNo" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanNo" runat="server" Text='<%#Bind("Loanno") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblLoanNo1" runat="server" Text='<%#Bind("Loanno") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Loan Type" ItemStyle-Width="30px" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanType" runat="server" Text='<%#Bind("TypeOfLoan") %>'></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblLoanType1" runat="server" Text='<%#Bind("TypeOfLoan") %>'></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="N.Inst" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblNoInst" runat="server" Text='<%#Bind("NoInstalments") %>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtNoInst" runat="server" Text='<%#Bind("NoInstalments") %>' Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Loan Amt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanAmt" runat="server" Text='<%#Bind("loanamount")%>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLoanAmt" runat="server" Text='<%#Bind("loanamount")%>' Width="75px"></asp:TextBox>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                         <asp:TemplateField HeaderText="Total Ded Amt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalDedAmt" runat="server" Text='<%#Bind("Recamt")%>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txtDedAmt" runat="server" Text='<%#Bind("Recamt")%>' Width="75px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>


                                                        <asp:TemplateField HeaderText="Total Ded Amt Current month" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblTotalDedAmtCurrMonth" runat="server" Text='<%#Bind("CurMonthRecamt")%>' Width="75px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="txtTotalDedAmtCurrMonth" runat="server" Text='<%#Bind("CurMonthRecamt")%>' Width="75px"></asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                        
                                                        <asp:BoundField DataField="RInst" HeaderText="R.Inst" Visible="false" />
                                                        <asp:BoundField DataField="Instamt" HeaderText="inst amt" Visible="false" />
                                                        <%--    <asp:TemplateField  HeaderText="N.Inst" >
                                               <ItemTemplate>
                                               <asp:TextBox  ID="txtnoofinstallments" runat="server" Text='<%#Bind("NoInstalments")%>' Width="60px"></asp:TextBox>
                                               </ItemTemplate>
                                               </asp:TemplateField>--%>
                                                        <%-- <asp:TemplateField  HeaderText="Amt" >
                                               <ItemTemplate>
                                               <asp:TextBox  ID="txtdueamt" runat="server"  ReadOnly="true" Text='<%#Bind("instamt")%>' Width="60px"></asp:TextBox>
                                               </ItemTemplate>
                                               </asp:TemplateField> --%>
                                                        <asp:TemplateField HeaderText="Loan Cutting Month" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoancut" runat="server" Text='<%#Bind("LoanDt") %>' Width="74px"></asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="txtLoancut" runat="server" Text='<%#Bind("LoanDt") %>' Width="74px" Enabled="false"></asp:TextBox>
                                                                <cc1:CalendarExtender ID="CELoancut" runat="server" Enabled="true" TargetControlID="txtLoancut"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                                <cc1:FilteredTextBoxExtender ID="FTBELoancut" runat="server" Enabled="True" TargetControlID="txtLoancut"
                                                                    ValidChars="/0123456789">
                                                                </cc1:FilteredTextBoxExtender>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Modified amt" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblmodifiedamount" runat="server" Text=""> </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:Label ID="lblmodifiedamount1" runat="server" Text=""> </asp:Label>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText='Loan Modify Count' HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblLoanCount" runat="server" Text='<%#Bind("LoanCount")%>'></asp:Label>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>

                                                        <asp:TemplateField HeaderText="Operations" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                            HeaderStyle-Width="20%">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="linkedit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:LinkButton ID="linkupdate" runat="server" CommandName="update" Text="Update"
                                                                    OnClientClick='return confirm(" Are you sure you want to update the designation?");'></asp:LinkButton>
                                                                <asp:LinkButton ID="linkcancel" runat="server" CommandName="cancel" Text="Cancel"
                                                                    OnClientClick='return confirm(" Are you sure you want to cancel this entry?");'>
                                                                </asp:LinkButton>
                                                            </EditItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    
                                                </asp:GridView>
                                            </div>
                                        </li>
                                    </ul>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="clear">
                        <br />
                    </div>
                    <!-- DASHBOARD CONTENT END -->
                </div>
            </div>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.Com" target="_blank">Powered byWeb Wonders</a>
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
