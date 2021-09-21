<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="jobleaving.aspx.cs" Inherits="Jawan.Portal.jobleaving" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>JOB LEAVING</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />

    <script language="javascript" src="scripts\Calendar.js" type="text/javascript"></script>

    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .imagesize
        {
            height: 70px;
            width: 125px;
        }
        .textboxsize
        {
            width: 120px;
        }
        .divsize
        {
            top: 500px;
            float: right;
        }
        .style6
        {
            height: 21px;
        }
        .reasons
        {
        	float:left;
        	width:100%;
        	padding-left:-50px;
        }
    </style>
</head>
<body>
    <form id="jobleaving1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Product Tracking System" title="Product Tracking System" /></a></div>
            <!-- LOGO END -->
            <!-- TOP INFO BEGIN -->
            <div id="toplinks">
                <ul>
                    <li><a href="Reminders.aspx">Reminders</a></li><li>Welcome <b>
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
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <%--  <li class="current"><a href="clients.aspx" id="clientlink" runat="server"><span>Clients</span></a></li>
                    <li class="after"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Compny Info</span></a></li>
                    --%>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
                        logout</span></span></a></li>
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
                                        &nbsp;</div>
                              <%--    <div class="submenuactions">
                                        &nbsp;</div> --%>
                                <ul>
                                             <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>
                                            Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>
                                            Delete</span></a></li>
                                       
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign Workers</span></a></li>
                                        <li ><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server">
                                            <span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                     <li ><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>
                                         Payment</span></a></li>
                                      <%--    <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
                                   <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                       <%--<li><a href="EmployeeSalaries.aspx" id="SalaryLink" runat="server"><span>Salaries</span></a></li>--%>
                                   
                               <%--           <li class="current"><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>
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
        <div class="content-holder">
            <h1 class="dashboard_heading">
                Job Leaving Details</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea" style="min-height: 400px; height: auto">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                JOB LEAVING REASON FORM</h2>
                        </div>
                        <!--  CBox BOdy Starts Here here> -->
                        <%--<div Todays Orders</h2>
                        </div>--%>
                        <!--  CBox BOdy Starts Here here> -->
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height: 300; height: auto">
                            <!--  Content to be add here> -->
                            <asp:ScriptManager runat="server" ID="ScriptJobleft"></asp:ScriptManager>
                            <div>
                                <div class="dashboard_firsthalf">
                                    <table style="height:30px">
                                        <tr>
                                            <td>
                                                Emp ID
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" Width="140px" ID="ddlEmpId" AutoPostBack="true"
                                                    OnSelectedIndexChanged="ddlEmpId_SelectedIndexChanged">
                                                    <asp:ListItem>Choose ID</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Employee Name
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtEmpName"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Date Of Joining
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtDtOfJoining"></asp:TextBox>
                                                <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                                    TargetControlID="txtDtOfJoining" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div class="dashboard_secondhalf">
                                    <table style="height:30px">
                                        <tr>
                                            <td>
                                                Current Site
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtCurrentSite"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Designation
                                            </td>
                                            <td>
                                                <asp:DropDownList runat="server" Width="140px" ID="ddlDesignation">
                                                    <asp:ListItem>Choose Designation</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="style6">
                                                Date Of Leaving
                                            </td>
                                            <td class="style6">
                                                <asp:TextBox runat="server" ID="txtDtOfLeaving"></asp:TextBox>
                                                 <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true"
                                                                    TargetControlID="txtDtOfLeaving" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <%--  <asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="100%" MaxLength="300"></asp:TextBox>--%>
                            <%--<asp:GridView runat="server" ID="GvJobLeving" Width="100%" 
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" 
                                    GridLines="None" >--%>
                            <div><asp:TextBox runat="server" ID="txtRemarks" TextMode="MultiLine" Width="100%" MaxLength="300"></asp:TextBox>  </div>
                            
                            <asp:GridView ID="GvJobLeving" runat="server" Width="100%" AutoGenerateColumns="false"
                                 Style="text-align: center" CellPadding="4" ForeColor="#333333"
                                GridLines="None" onrowdatabound="GvJobLeving_RowDataBound" >
                                <RowStyle BackColor="#EFF3FB" />
                               
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="10px" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                <asp:Label runat="server" ID="lblSNO" ><%#Eval("SNo")%></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="REASONS FOR LEAVING THE JOB" FooterStyle-CssClass="reasons">
                                <ItemTemplate>
                                <asp:Label runat="server" ID="lblReasons" style="float:left;padding-left:50px;"><%#Eval("REASONS")%></asp:Label>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="YES">
                                <ItemTemplate>
                                 <asp:RadioButton runat="server" ID="rbYes" Checked="true"  GroupName="a"/>
                                </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="No">
                                <ItemTemplate>
                                  <asp:RadioButton  runat="server" ID="rbNo" GroupName="a" />
                                </ItemTemplate>
                                </asp:TemplateField>
                                </Columns>
                                
                                
                            </asp:GridView>
                            <asp:TextBox runat="server" ID="txtEditbox" TextMode="MultiLine" Width="100%" MaxLength="300">
                            </asp:TextBox>
                        </div>
                        
                        
                        
                        <div> <asp:Label ID="LblResult" runat="server" Text="" Visible="false"> </asp:Label>  </div>
                        <div style=" margin-left:850px"> <asp:Button ID="Btn_JobLeaving_Resons" 
                                runat="server" Text="Save" class=" btn save" 
                                onclick="Btn_JobLeaving_Resons_Click" />  </div>
                        
                    </div>
                </div>
            </div>
            <div class="clear">
            </div>
            <!-- DASHBOARD CONTENT END -->
            <%-- </div> </div>--%>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
        </div>
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS</a></div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a>|
                    <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>
                </div>
                <div class="clear">
                </div>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
