<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TrainingEmployees.aspx.cs" Inherits="Jawan.Portal.TrainingEmployees" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
   <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>TRAINING EMPLOYEES</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Calendar.css" rel="stylesheet" type="text/css" />

    <script type="text/javascript" src="script/jscript.js">
    </script>

</head>
<body>
    <form id="TrainingEmployees1" runat="server">
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="Default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Management Software" title="Facility Management Software" /></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="Employeeslink" runat="server" class="current"><span>Employees</span></a></li>
                    <li class="after"><a href="clients.aspx" id="ClientsLink" runat="server" ><span>Clients</span></a></li>
                    <li ><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                        
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span> Logout</span></span></a></li>
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
                                        &nbsp;</div>
                                    <%--    <div class="submenuactions">
                                        &nbsp;</div> --%>
                          <ul>
                                        <li><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign Workers</span></a></li>
                                            <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                                <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                                <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                   <%--             
                                                <li class="current"><a  href="TrainingEmployees.aspx"  id="TrainingEmployeeLink" runat="server"   ><span>Training</span> </a> </li> --%>  
                                 <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                     
                                <%--    <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>
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
                Clients Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               STAFF TRAINING SHEET</h2>
                        </div>
                                              <asp:ScriptManager runat="server" ID="Scriptmanager1">
                            </asp:ScriptManager>
                         
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height:400px; height:auto">
                            <!--  Content to be add here> -->
                          <div class="dashboard_firsthalf"  style=" min-height:100px; height:auto">
                                                         <table style=" height:50px">
                   
                   <tr><td>Training Id </td> <td><asp:TextBox ID="txttrainingid" runat="server" ReadOnly="true" Height="19px"></asp:TextBox> </td>   </tr>
                   
                   <tr>
                   <td style=" height:10px">   Trainer ID </td>
                   <td>  <asp:DropDownList ID="ddltrainerid" runat="server" > 
                   
                   <asp:ListItem Value="0" Enabled="true" >--Select TrainerId--  </asp:ListItem>
                   
                   
                     </asp:DropDownList> </td>
                   </tr>
                   <tr ><td style=" height:10px">Site </td>  <td><asp:TextBox ID="txtsite" runat="server" ></asp:TextBox> </td> </tr>
                   <tr> <td style=" height:10px">Start Time   </td> <td><asp:TextBox ID="txtstarttime" runat="server" > </asp:TextBox> 
                   
                     <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="txtstarttime"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                   </td>  </tr>
                   <tr><td style=" height:10px"> Total Staff</td> <td> <asp:TextBox ID="txtstaff" runat="server"> </asp:TextBox></td>  </tr>
                
                <tr><td style="font-weight: bold"> TOPIC COVERED </td>  <td colspan="3"> <asp:TextBox ID="txttopiccoverd" runat="server" TextMode="MultiLine" Width="200px" > </asp:TextBox> </td>   </tr>
                
                  
                   </table>
                   
                                            </div>
        <div class="dashboard_secondhalf"  style=" min-height:100px; height:auto">
                                                 <table  style=" height:auto">  
               
               <tr> <td style=" height:10px">Trainer Name </td>  <td><asp:TextBox ID="txttrainername" runat="server" ></asp:TextBox> </td>   </tr>
               <tr><td style=" height:10px">Date  </td> <td><asp:TextBox ID="txtdate" runat="server"> </asp:TextBox>
                                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="true" TargetControlID="txtdate"
                                                                    Format="MM/dd/yyyy">
                                                                </cc1:CalendarExtender>
                   
                </td> </tr>
               <tr><td style=" height:10px">Ending Time </td> <td><asp:TextBox ID="txtendingtime" runat="server"> 
               </asp:TextBox> 
               
                                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="true" TargetControlID="txtendingtime"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                   </td> </tr>
               <tr><td style=" height:10px">No Of Staff Attended </td> <td> <asp:TextBox ID="txtnoofstaffattend" runat="server" > </asp:TextBox></td> </tr>
               
               <tr><td></td> <td></td> </tr>
               </table>
         
                                            </div>
                                         
 
                                        
                                        <div style="font-family: Arial; font-weight: normal; font-variant: normal; min-height:100px; height:auto; font-size: 13px">
                                            
                                                 <asp:GridView id="gvtraining" runat="server" AutoGenerateColumns="False" 
                                                     Width="100%" Height="40px" 
                                                     onpageindexchanging="gvtraining_PageIndexChanging" 
                                                     onrowdatabound="gvtraining_RowDataBound">
                                                     <PagerSettings Position="Bottom"/>
               <Columns>
               <asp:TemplateField HeaderText="Employee Id">
               <ItemTemplate>
              <asp:DropDownList ID="ddlempid" runat="server" Width="150px" AutoPostBack="true" OnSelectedIndexChanged="ddlempid_getempname" > 
              <asp:ListItem Selected="True" Value="0">--Select EmpId-- </asp:ListItem>
               </asp:DropDownList>               
               </ItemTemplate>
                <ItemStyle Width="30px" /> 
               
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Emp Name">
               
               <ItemTemplate>
               <asp:Label ID="lblempname" runat="server">Hari </asp:Label>
               
               </ItemTemplate>
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Remarks">
               <ItemTemplate>
              <asp:TextBox ID="txtremarks" runat="server"  MaxLength="200" Width="300px"></asp:TextBox>
               </ItemTemplate>
               <ItemStyle Width="50px" /> 
               </asp:TemplateField>
               <asp:TemplateField HeaderText="Post Training Analysis"   >
               <ItemTemplate>
               <asp:TextBox ID="txtpta" runat="server"  Width="200px" MaxLength="200" ></asp:TextBox>
               </ItemTemplate>
                 <ItemStyle Width="50px" /> 
                </asp:TemplateField>
               </Columns>
                                                     <PagerStyle Width="0px" />
               
               </asp:GridView>
                                        </div>
                                        <div> 
                                        <asp:Button  ID="btnadddesgn" runat="server" class="btn save" 
                                                style=" margin-left:9px" Text="Add Employee" onclick="btnadddesgn_Click" 
                                              />
                                         </div>
                                            <asp:Label ID="lblreslt" runat="server" Text="" style="color:Red; margin-left:450px" Visible="false"></asp:Label>
                                        <div style="margin-left:690px; margin-top:6px">
                                            <asp:Button ID="btnsave" runat="server" Text="Save" class="btn save" style="margin-bottom:6px"
                                             OnClientClick='return confirm("Are You Want To Add The Record ?"); ' onclick="btnsave_Click" 
                                                />
                                            <asp:Button ID="btncancel" runat="server" Text="Cancel" class="btn save"  style="margin-bottom:6px"
                                           OnClientClick='return confirm(" Are You Want to Cancel The Record?");'
                                            
                                            />
                                            </div>
                                        </div>
                                    </div>
                                 </div>
                  <div class="clear"> </div>
                 
                   </div>
                    <!-- DASHBOARD CONTENT END -->
              </div>
            <!-- DASHBOARD CONTENT END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
        <div class="footer">
            <div class="footerlogo">
                <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
            </div>
            <!--    <div class="footerlogo">&nbsp;</div> -->
            <div class="footercontent">
                <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
            <div class="clear">
            </div>
        </div>
    </div>
        <!-- FOOTER END -->
    </div>
    <!-- CONTENT AREA END -->
    </form>
</body>
</html>

     
               
               
               