<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewEmployee.aspx.cs" Inherits="Jawan.Portal.ViewEmployee" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>VIEW EMPLOYEE</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />
   
    <style type="text/css">
        .style2
        {
            text-align: left;
            font-weight: bold;
        }
        h3{color:#0088cc;text-decoration:underline}
        h4{color:Red;text-decoration:underline}
    </style>
   
</head>
<body>
    <form id="Employees1" runat="server">
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
                                    <%--    <div class="submenubeforegap">
                                        &nbsp;</div>--%>
                                    <ul>
                                        <%--<li class="current"><a href="Employees.aspx" id="AddEmployeeLink" runat="server"><span>
                                            Add</span></a></li>
                                        <li><a href="ModifyEmployee.aspx" id="ModifyEmployeeLink" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteEmployee.aspx" id="DeleteEmployeeLink" runat="server"><span>Delete</span></a></li>--%>
                                        <li><a href="AssingingWorkers.aspx" id="AssigningWorkerLink" runat="server"><span>Assign
                                            Workers</span></a></li>
                                        <li><a href="EmployeeAttendance.aspx" id="AttendanceLink" runat="server"><span>Attendance</span></a></li>
                                        <li><a href="NewLoan.aspx" id="LoanLink" runat="server"><span>Loans</span></a></li>
                                        <%--        <li><a href="ResourceMesurments.aspx" id="ResourceMesurmentslink" runat="server"><span>Masters</span></a></li>
                                        <li><a href="TrainingEmployees.aspx" id="TrainingEmployeeLink" runat="server"><span>Training</span></a></li>--%>
                                        <li><a href="EmployeePayments.aspx" id="PaymentLink" runat="server"><span>Payment</span></a></li>
                                        <li><a href="PostingOrderList.aspx" id="PostingOrderListLink" runat="server"><span>Transfers</span></a></li>
                                        <%--<li><a href="EmployeeSalaries.aspx" id="SalaryLink" runat="server"><span>Salaries</span></a></li>
                                       <li><a href="jobleaving.aspx" id="JobLeavingReasonsLink" runat="server"><span>Job Leaving Reasons</span></a></li>--%>
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
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_full">
                
                    <div class="col-md-12" style="margin-top:8px;margin-bottom:8px">
                        <div class="panel panel-inverse">
                            <div class="panel-heading">
                               <table width="100%">
                               <tr>
                               <td>  <h3 class="panel-title">
                                    View Employee</h3></td>
                               <td align="right"><< <a href="Employees.aspx" style="color:#003366">Back</a>  </td>
                               </tr>
                               </table>
                              
                                  
                            </div>
                            <div class="panel-body">
                            <h3>Personal Information</h3>
                          <table width="100%" cellpadding="10" cellspacing="10">   
                              <tr>
                              <td width="50%">
                               <table style="width:100%" cellpadding="10" cellspacing="10">
                    <tr>
                        <td class="style2" width="160px">
                            Emp Id</td>
                            <td>:</td>
                        <td style="text-align: left" width="300px">
        <asp:Label ID="lblEmpid" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Name of the Employee</td>
                            <td><b>:</b></td>
                       <td>
                            <asp:Label ID="lblNameemp" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td class="style2">
                            Gender</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblGender" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Date of Interview</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblDateofInterview" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Date of Birth</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblDateofBirth" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style2">
                            Qualification</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblQualification" runat="server"></asp:Label>
                        </td>
                    </tr>
                     <tr>
                        <td class="style2">
                            Father/Spouse Name</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblFaSpName" runat="server"></asp:Label>
                        </td>
                    </tr>
                          <tr>
                        <td class="style2">
                            Father/Spouse Age</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblFaSpAge" runat="server"></asp:Label>
                        </td>
                    </tr>
                            <tr>
                        <td class="style2">
                            Mother Name</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblMotherName" runat="server"></asp:Label>
                        </td>
                    </tr>
                           <tr>
                        <td class="style2">
                            Mother Occupation</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblMotherOccu" runat="server"></asp:Label>
                        </td>
                    </tr>
                             <tr>
                        <td class="style2">
                            Phone No</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblPhoneNo" runat="server"></asp:Label>
                        </td>
                    </tr>
                    
                              <tr>
                        <td class="style2">
                            ESI Deduct </td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblEsiDeduct" runat="server"></asp:Label>
                        </td>
                    </tr>
                           <tr>
                        <td class="style2">
                            PF Deduct</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblPFDeduct" runat="server"></asp:Label>
                        </td>
                    </tr>
                        <tr>
                        <td class="style2">
                            EMP Ex-service</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblExService" runat="server"></asp:Label>
                        </td>
                    </tr>
                       <tr>
                        <td class="style2">
                            PT Deduct</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblPtDeduct" runat="server"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td class="style2">
                            Site posted to</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblSitePosted" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <%-- <tr>
                       <td>
                            <span style="font-weight: normal">Site Posted to</span></td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblSitePosted" runat="server"></asp:Label>
                        </td>
                    </tr>--%>
                    </table> 
                    </td>
                    
		<td valign="top" width="50%">
		  <table style="width:100%" cellpadding="10" cellspacing="10">
		             <tr>
                        <td class="style2">
                            Marital Status</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblMaritalStatus" runat="server"></asp:Label>
                        </td>
                    </tr> 
                       <tr>
                        <td class="style2">
                            Date of Joining</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblDateofJoining" runat="server"></asp:Label>
                        </td>
                    </tr> 
                       <tr>
                        <td class="style2">
                            Date of Leaving</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblDateofLeaving" runat="server"></asp:Label>
                        </td>
                    </tr> 
                     <tr>
                        <td class="style2">
                            Designation</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblDesig" runat="server"></asp:Label>
                        </td>
                    </tr>
                       <tr>
                        <td class="style2">
                            Father/Spouse Occupation</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblFaSpOccu" runat="server"></asp:Label>
                        </td>
                    </tr> 
                       <tr>
                        <td class="style2">
                            Father/Spouse Relation</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblFaSpRel" runat="server"></asp:Label>
                        </td>
                    </tr> 
                       <tr>
                        <td class="style2">
                            Previous Employer</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblPreviousEmp" runat="server"></asp:Label>
                        </td>
                    </tr> 
		              <tr>
                        <td class="style2">
                            Mother Tongue</td>
                            <td>:</td>
                       <td>
                            <asp:Label ID="lblMotherTongue" runat="server"></asp:Label>
                        </td>
                    </tr> 
                     <tr>
                        <td class="style2">
                            Nationality</td>
                            <td>:</td>
                        <td>
                            <asp:Label ID="lblNationality" runat="server"></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td class="style2">
                            Religion</td>
                            <td>:</td>
                        <td>
                            <asp:Label ID="lblReligion" runat="server"></asp:Label>
                        </td>
                    </tr> 
                    <tr>
                        <td class="style2">
                            Languages Known</td>
                            <td>:</td>
                        <td>
                            <asp:Label ID="lblLanguges" runat="server"></asp:Label>
                        </td>
                    </tr> 
                    </table>
                    </td>
                    </tr>
                    </table>
                    
                    <h3>References</h3>
                     <table width="100%" cellpadding="10" cellspacing="10">   
                              <tr>
                              <td width="50%">
                               <table style="width:100%" cellpadding="10" cellspacing="10">
                    <tr>
                    <td class="style2" width="150px">
                            Ref Name & Address1</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblRefaddr1" runat="server"></asp:Label></td>
                    </tr>
                     
                     <tr>
                    <td class="style2">
                            Blood Group</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBloodG" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Physical Remarks</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPhyRema" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                    <td class="style2">
                            Identification Marks1</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblImarks1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Height</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblHeight" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Weight</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblWeight" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Door No</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDoorno" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Street</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblStreet" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Land mark</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblLandMark" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                    <td class="style2">
                            Area</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblArea" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                    <td class="style2">
                            City</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblCity" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                    <td class="style2">
                            District</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDistrict" runat="server"></asp:Label></td>
                    </tr>
                      <tr>
                    <td class="style2">
                            Pin code</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPincode" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            State</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblState" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Phone(if any)</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPhone1" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                    </td>
                    
                   <td valign="top" width="50%">
		  <table style="width:100%" cellpadding="10" cellspacing="10">
                    <tr>
                    <td class="style2" width="240px">
                            Ref Name & Address2</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblRefaddr2" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Remarks</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblRemarks" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                           Identification Marks2</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblImarks2" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            UnExpand</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblUnexpand" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Expand</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblExpand" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Door No</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDoorno1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Street</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblStreet1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Land mark</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblLandMark1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Area</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblArea1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            City</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblCity1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Perm. District</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPermdistrict" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Pin code</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPincode1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            State</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblState1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Phone(if any)</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPhone2" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                    </td>
                    </tr>
                    </table>
                    
                    <h3>Bank/PF/ESI</h3>
                    
                      <table width="100%" cellpadding="10" cellspacing="10">   
                              <tr>
                              <td width="50%">
                               <table style="width:100%" cellpadding="10" cellspacing="10">
                    <tr>
                    <td class="style2" width="150px">
                            Bank Name</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBankname" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Branch Name</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBranchName" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Branch Code</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBranchcode" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Bank App No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBankAppno" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Insurance Nominee</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblIsuNominee" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Nominee Date of Birth</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblNomDob" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Insurance Cover</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblInsuCover" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            EPF No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblEpfno" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Aadhaar No</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblAadhaarNo" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            PF Nominee</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPfNominee" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            PF Enroll Date</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPfendatee" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            PF Nominee Relation</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPfNomiRelation" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                    </td>
                    <td width="50%">
                    <table width="100%" cellpadding="10" cellspacing="10">
                     <tr>
                    <td class="style2" width="240px">
                            Bank A/C No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBankAcno" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            IFSC Code</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblIfsccode" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Bank Code No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBankcodeno" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Region Code</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblRegionCode" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Bank Card Reference</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBankCardRef" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Nominee Relation</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblNomiRelation" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Ins Debt Amount</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblInsdebtam" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            SS No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblSsno" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            ESI No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblEsiNO" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            ESI Nominee</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblEsInominee" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            ESI Disp Name</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblEsiDisname" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            ESI Nominee Relation</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblEsiNomRelation" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                    </td>
                    
                    </tr>
                    </table>
                    
                    <h3>Ex-Services</h3>
                    
                     <table width="100%" cellpadding="10" cellspacing="10">   
                              <tr>
                              <td width="50%">
                               <table style="width:100%" cellpadding="10" cellspacing="10">
                    <tr>
                    <td class="style2" width="150px">
                            Service No.</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblSerNo" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Date of Enrollment</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDtofenroll" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Crops</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblCrops" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Medical Category</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblMedicalCat" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Conduct</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblConduct" runat="server"></asp:Label></td>
                    </tr>
                    </table>
                    </td>
                    <td width="50%" valign="top">
                    <table width="100%" cellpadding="10" cellspacing="10">
                    <tr>
                    <td class="style2" width="240px">
                            Rank</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblRank" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Date of Discharge</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDateofdisc" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Trade</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblTrade" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                    <td class="style2">
                            Reason of Discharge</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblReasonDisc" runat="server"></asp:Label></td>
                    </tr>
                   
                    </table>
                    </td>
                    </tr>
                    </table>
                       <h3>Qualification</h3>       
                              
                      <table width="100%" cellpadding="10" cellspacing="10">   
                              <tr>
                              <td width="50%">
                               <table style="width:100%" cellpadding="10" cellspacing="10">
                        <tr>
                    <td class="style2">
                           <h4>SSC</h4> </td>
                            <td></td>
                    <td> </td>
                    </tr>
                      <tr>
                    <td class="style2">
                            Name & Address of School/Clg</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblNameAdSc" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Board/University</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBoardUni" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Year of Study</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblYearofStudy" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Whether Pass/Failed</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPassFail" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Percentage of Marks</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPerMarks" runat="server"></asp:Label></td>
                    </tr>
                        <tr>
                    <td class="style2">
                           <h4>Intermediate</h4></td>
                            <td></td>
                    <td> </td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Name & Address of School/Clg</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblNameAddr" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Board/University</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblBoardUniver" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Year of Study</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblYearofstudy1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Whether Pass/Failed</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPassFail1" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Percentage of Marks</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPerMarks1" runat="server"></asp:Label></td>
                    </tr>
                     </table>
                     </td>
                     <td width="50%">
                     <table width="100%" cellpadding="10" cellspacing="10">
                        <tr>
                    <td class="style2">
                           <h4>Degree</h4></td>
                            <td></td>
                    <td> </td>
                    </tr>
                      <tr>
                    <td class="style2">
                            Name & Address of School/Clg</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDnameaddr" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Board/University</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDboardUni" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Year of Study</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDyearstu" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Whether Pass/Failed</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDpassfail" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Percentage of Marks</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblDperce" runat="server"></asp:Label></td>
                    </tr>
                       <tr>
                    <td class="style2">
                           <h4>PG</h4> </td>
                            <td></td>
                    <td> </td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Name & Address of School/Clg</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPgnameaddr" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Board/University</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPgboard" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Year of Study</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPgYear" runat="server"></asp:Label></td>
                    
                     <tr>
                    <td class="style2">
                            Whether Pass/Failed</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPgPass" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                    <td class="style2">
                            Percentage of Marks</td>
                            <td>:</td>
                    <td> <asp:Label ID="lblPgPerc" runat="server"></asp:Label></td>
                    </tr>
                     <tr>
                   
                    </tr>
                    </tr>
                     </table>
                     
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
            <!-- DASHBOARD CONTENT END -->
            <%-- </div> </div>--%>
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
        </div>
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.Com" target="_blank">Powered by WebWonders</div>
                <div class="footercontent">
                    <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | ©
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
                <div class="clear">
                </div>
            </div>
            <!-- CONTENT AREA END -->
        </div>
    </form>
</body>
</html>
