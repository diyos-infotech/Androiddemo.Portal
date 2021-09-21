<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeSalaries.aspx.cs" Inherits="Jawan.Portal.EmployeeSalaries" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EMPLOYEE SALARIES</title>
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
    <form id="EmployeeSalaries1" runat="server">
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
                                        <li ><a href="EmployeeAttendance.aspx" id="AttenDanceLink" runat="server">
                                            <span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                     <li ><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>
                                         Payment</span></a></li>
                                 <%--       <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>  --%>        
<li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfer</span></a></li>
                                        <li class="current"><a href="EmployeeSalaries.aspx" id="SalaryLink" runat="server"><span>Salaries</span></a></li>
                                   
                                      
                                      
                                          <li ><a href="jobleaving.aspx" id="JobLeavingReasonLink" runat="server"><span>Job Leaving Reasons</span></a></li>
                                  
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
            <div class="contentarea" id="contentarea" style="min-height:400px; height: auto">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               SALARY DETAILS</h2>
                        </div>
                        <!--  CBox BOdy Starts Here here> -->
                        <%--<div Todays Orders</h2>
                        </div>--%>
                        <!--  CBox BOdy Starts Here here> -->
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height: 300px; height: auto">
                     
                     
                     
                          <div class="dashboard_firsthalf">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                Basic
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="1" ID="txtBasic">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                HRA
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="3" ID="txtHRA">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Washing Allowence
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="5" ID="txtWashingallowence">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Convences
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="7" ID="txtCOnvences">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                CTC
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="9" ID="txtCTC"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Food Deductions
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="11" ID="txtFoodDeductions"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Loan Deductions
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="13" ID="txtLoanDeductions"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Penalties
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="15" ID="txtPenalties"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                <div class="dashboard_secondhalf">
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                DA
                                                            </td>
                                                            <td>
                                                                <asp:TextBox TabIndex="2" runat="server" ID="txtDA">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                CCA
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="4" ID="txtCCA">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Other Allowences
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="6" ID="txtOtherAlow">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Gross
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="8" ID="txtGross">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Net
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="10" ID="txtNet">
                                                                </asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Income Tax
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="12" ID="txtIncomeTax"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Uniform Deductions
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="14" ID="txtUnifoemDeductions"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                Other Deductions
                                                            </td>
                                                            <td>
                                                                <asp:TextBox runat="server" TabIndex="16" ID="txtOtherDeductions"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                        <td colspan="3" >      <asp:Label runat="server" ID="lblSalRes" Visible="false" Style="color: Red"></asp:Label> </td>
                                                        </tr>
                                                        <tr><td></td> <td colspan="2"> <asp:Button ID="Button3" runat="server" Text="Save" class="btn save"
                                                          TabIndex="17"
                                                        OnClientClick='return confirm("Are You Sure You Want to Add Record?");' />
                                                    <asp:Button ID="Button4" runat="server" Text="Cancel" class="btn save" 
                                                        OnClientClick='return confirm("Are You Sure You Want to Cancel the Details?");' />
                                                     </td>  </tr>
                                                        
                                                    </table>
                                                </div>
                    
    </div>
    
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

