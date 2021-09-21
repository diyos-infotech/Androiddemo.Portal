<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListOfUsersReport.aspx.cs" Inherits="Jawan.Portal.ListOfUsersReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>LIST OF USERS</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
     <style type="text/css">
        .style2
        {
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
    <form id="ListOfUsersReport1" runat="server">
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
                    <li><a href="Reminders.aspx">Reminders</a></li><li>Welcome <b>
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
                   <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current">  <span>Reports</span></a></li>
                    <li class="after"><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span> Logout</span></span></a></li>
                       
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
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li class="current"><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"  runat="server"><span>Employees</span></a></li>
                                        <li><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span> Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>  Inventory</span></a></li>
                                        <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"> <span>Companyinfo</span></a>  </li>           
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
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">User Details</a></li>
                </ul>
            </div>

            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
               
                   <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               User Details
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin" style="height: 650px">


                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                        <div class="dashboard_firsthalf" style="width: 100%">
                             <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                <tr>
                                    <td width="100%" class="FormSectionHead">
                                        List of Users and their Privileges
                                        
                                          <asp:LinkButton ID="linkdelete" runat="server" Text="Delete_Bills"  ></asp:LinkButton>&nbsp&nbsp&nbsp
                                          <asp:LinkButton ID="LinkDeleteLoan" runat="server" Text="Delete_Loan"></asp:LinkButton>
                                          &nbsp&nbsp&nbsp
                                          <asp:LinkButton ID="Link_Change_Bill_No" runat="server" Text="Modify Bill No"></asp:LinkButton>
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                                
                                </table>
                                
                                 <%--Begin Modify Bill No --%> 
                                <div>
                                
                                <cc1:ModalPopupExtender ID="MPE_Modify_Bill_No" runat="server" 
                                PopupControlID="Pnl_Modify_Bill_No" TargetControlID="Link_Change_Bill_No" CancelControlID="Btn_cancle_midifyBill">
                                </cc1:ModalPopupExtender>
                                
                              <asp:Panel ID="Pnl_Modify_Bill_No" runat="server" Height="500" Width="650" Style="display:none; background-color:Silver; max-width:1000; max-height:1000" >
                                    
                                    
                                    <asp:UpdatePanel ID="UP_Modify_Bill_No" runat="server" UpdateMode="Conditional" >
                                    <ContentTemplate>
                                    
                                    <table width="100%" cellpadding="5" cellspacing="5" style="margin-left:15px">
                                    <tr>
                                        <td><div style="margin-bottom:145px;"><table>
                                       <tr>
                                        <td>Bill type</td> 
                                        <td><asp:RadioButton  ID="Rdb_Bill_Type_Normal" OnCheckedChanged="Rdb_Bill_Type_Normal_CheckedChanged"  AutoPostBack="true" TabIndex="1" runat="server" GroupName="Modify_Bill_No" Text="Normal" Checked="true"/>
                                        <asp:RadioButton  ID="Rdb_Bill_Type_Manual" OnCheckedChanged="Rdb_Bill_Type_Manual_CheckedChanged"  AutoPostBack="true" TabIndex="2" runat="server" GroupName="Modify_Bill_No" Text="Manual"/>
                                        </td>  
                                     </tr>
                                     
                                    <tr>   
                                        <td>Enter old bill no </td>  
                                        <td> 
                                         <asp:TextBox ID="Txt_Old_Bill_No_Modify_Bill" TabIndex="3" runat="server"  class="sinput" 
                                                AutoPostBack="true" OnTextChanged="Txt_Old_Bill_No_Modify_Bill_OnTextChanged"> </asp:TextBox> 
                                          <cc1:FilteredTextBoxExtender runat="server" ID="Ftd_Old_Bill_No_Modify_Bill" TargetControlID="Txt_Old_Bill_No_Modify_Bill" 
                                          FilterMode="InvalidChars" InvalidChars="!@#$%^&amp;*()~?><|\';:"></cc1:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    
                                     <tr>  
                                      <td>Client id </td> 
                                      
                                       <td>
                                         <asp:TextBox ID="Txt_Client_Id_Modify_Bill" runat="server"  class="sinput" ReadOnly="true"> 
                                         </asp:TextBox> 
                                       </td>
                                     
                                      </tr>
                                    
                                     <tr>  
                                      <td>Client name </td> 
                                      <td> 
                                       <asp:TextBox ID="Txt_Client_Name_Modify_Bill" runat="server"  class="sinput" ReadOnly="true"> 
                                       </asp:TextBox>
                                      </td>
                                    </tr>
                                    
                                    <tr>  
                                      <td>For the month of</td> 
                                      <td> 
                                       <asp:TextBox ID="Txt_Month_Of_Modify_Bill" runat="server"  class="sinput" ReadOnly="true"> 
                                       </asp:TextBox>
                                      </td>
                                    </tr>
                                    
                                     <tr>  
                                      <td>Grand total</td> 
                                      <td> 
                                       <asp:TextBox ID="Txt_Grand_Total_Modify_Bill" runat="server" class="sinput" ReadOnly="true"> 
                                       </asp:TextBox>
                                      </td>
                                    </tr>
                                   
                                     <tr>  
                                      <td>New bill no</td> 
                                      <td colspan="5">
                                       <asp:TextBox ID="Txt_New_Bill_No__Modify_Bill" TabIndex="4" runat="server" Width="108" class="sinput"  ReadOnly="true"></asp:TextBox>
                                       <asp:TextBox ID="Txt_New_Bill_No__Modify_Bill2"  style="margin-left:2px" TabIndex="5"  runat="server" Width="33" class="sinput"  MaxLength="5"
                                        ></asp:TextBox>
                                       <cc1:FilteredTextBoxExtender runat="server" ID="FillExtend" TargetControlID="Txt_New_Bill_No__Modify_Bill2" ValidChars="0123456789"></cc1:FilteredTextBoxExtender>
                                      </td>
                                    </tr>
                                     <tr>
                                  <td colspan="2" align="right"><div style="margin-top:10px"><asp:Button ID="Btn_Modify_Bill_Update" runat="server" Text="Update" CssClass="btn save" onsubmit="validateForm()"
                                  OnClientClick='return confirm(" Are you sure you  want to  Update bill ?");'
                                    OnClick="Btn_Modify_Bill_Update_Click"/> 
                                    <asp:Button ID="Btn_Modify_Bill_Cancel"  OnClick="Btn_Modify_Bill_Cancel_Click" runat="server" Text="Clear"  CssClass="btn save" style="width:100px" />
                                    <asp:Button ID="Btn_cancle_midifyBill" runat="server" Text="Cancel/Close"  CssClass="btn save" style="width:100px" OnClick="Btn_cancle_midifyBill_Click" Autopostback="true"/>
                                    </div></td>
                                
                                    </tr>
                                    </table></div></td>
                                    <td><table>
                                        <tr><%--<td><div style="color:#1950A3; margin-bottom:145px; font-weight:bold; font-size:13px;"><asp:Label runat="server" ID="lblEmptybill" Text="List_of_empty_bill_numbers:"/>
                                        </div></td>--%>
                                        <td><div style="margin-bottom:205px;"><asp:GridView
                                        ID="GVEmptyBill" runat="server"  AllowPaging="True"   onpageindexchanging="GVEmptyBill_PageIndexChanging" PageSize="18" AutoGenerateColumns="False" Width="100%"
                                          CellPadding="5" ForeColor="#333333" GridLines="None" CellSpacing="2" style="text-align:center">
                                        <RowStyle BackColor="#FFFFFF" Height="22"/>
                                        <Columns>
                                        <asp:TemplateField HeaderText="List_of_empty_bill_numbers" HeaderStyle-ForeColor="#1950A3">
                                        <ItemTemplate><asp:Label runat="server" ID="lblEmptybill" Text="<%#Bind('MissedBillNo') %>"></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        </Columns></asp:GridView></div></td>
                                        </tr>
                                    </table></td>
                                    </tr>
                                    </table>
                                    </ContentTemplate>
                                    
                                     <Triggers >
                                        <asp:AsyncPostBackTrigger ControlID="Btn_Modify_Bill_Update"  />
                                    </Triggers>
                                    
                                    </asp:UpdatePanel>
                                    
                              </asp:Panel>
                                
                                </div>
                               <%--End Modify Bill No --%> 
                                
                                    <div>
                                        
                                         <cc1:ModalPopupExtender ID="mpebilldelete" runat="server" TargetControlID="linkdelete"
                                    PopupControlID="pnlbilldeletedetails" CancelControlID="btncancel">
                                </cc1:ModalPopupExtender>
                                
                                   <asp:Panel ID="pnlbilldeletedetails" runat="server" Height="200px" Width="400px" 
                                   Style="display: none; background-color:Silver">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                   <ContentTemplate>
                                   <table width="100%" cellpadding="5" cellspacing="5" style="margin-left:15px">
                                   
                                    <tr>
                                        <td>Bill Type</td> 
                                        <td><asp:RadioButton  ID="radionormal" runat="server" GroupName="Billtype" Text="Normal" Checked="true"/>
                                        <asp:RadioButton  ID="radiomanual" runat="server" GroupName="Billtype" Text="Manual"/>
                                        </td>  
                                    </tr>
                                   
                                   <tr>   
                                        <td>Enter Bill No </td>  
                                        <td>  <asp:TextBox ID="txtbillno" runat="server"  class="sinput" AutoPostBack="true" OnTextChanged="txtbillno_OnTextChanged" > </asp:TextBox> </td>
                                    </tr>
                                    
                                    <br />
                                       <tr>   <td>Client Id </td>  <td>
                                         <asp:TextBox ID="txtclientid" runat="server"  class="sinput"> 
                                     </asp:TextBox> </td> </tr>
                                    
                                     <tr>   <td>Client Name </td> 
                                      <td> 
                                       <asp:TextBox ID="txtclientname" runat="server"  class="sinput"> 
                                   
                                     </asp:TextBox> </td>
                                     
                                    </tr>
                                   
                                   </table>
                                   
                                  </ContentTemplate>
                                    <Triggers >
                                        <asp:AsyncPostBackTrigger ControlID="btndelelte"  />
                                    </Triggers>
            
                             </asp:UpdatePanel>
        
                              <table width="100%" cellpadding="5" cellspacing="5" style="margin-left:15px">
                              
                              <tr>
                                  <td><asp:Button ID="btndelelte" runat="server" Text="Delete" CssClass="btn save" 
                                  OnClientClick='return confirm(" Are you sure you  want to  delete bill ?");'
                                    OnClick="btndelelte_Click"/> <asp:Button ID="btncancel" runat="server" Text="Cancel/Close"  CssClass="btn save" style="width:100px" />  </td>
                                
                              </tr>
                               </table>
        
                            </asp:Panel></div>
                            <div>
                            <cc1:ModalPopupExtender ID="ModelpopExDeleteLoan" runat="server" TargetControlID="LinkDeleteLoan"
                                    PopupControlID="pnlbillDeleteLoan" CancelControlID="btncancelLoanDelete">
                                </cc1:ModalPopupExtender>
                            <asp:panel ID="pnlbillDeleteLoan" runat="server" Height="150px" Width="450px" 
                                   Style="display: none; background-color:Silver">
                                    <asp:UpdatePanel ID="UpdatePanelLoanDelete" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate><br />
                                      <table width="100%" cellpadding="5" cellspacing="5" style="margin-left:15px">
                                       <tr>
                                       <td>
                                       Enter LoanNo :
                                     </td>
                                      <td> <asp:TextBox runat="server" ID='txtLoanno' class="sinput" OnTextChanged="txtLoanno_OnTextChanged" AutoPostBack="true"></asp:TextBox>
                                        <cc1:FilteredTextBoxExtender ID="FTBELoanNo" runat="server" Enabled="True" TargetControlID="txtLoanno" ValidChars="1234567890"></cc1:FilteredTextBoxExtender> 
                                        </td>
                                        </tr>
                                        </table>
                                        
                                      <br/>
                                      
                                        <div id="divLoanDelete" runat="server" visible="false">
                                        <hr/><tr><td></td></tr>
                                            <tr>
                                            <td>&nbsp</td>         
                                            <td>EmpId</td><td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
                                            <td><asp:TextBox runat="server" ID="txtEmpid" ReadOnly="true"></asp:TextBox></td>
                                            <td>&nbsp</td>
                                            <td>EmpName</td><td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
                                            <td><asp:TextBox runat="server" ID="txtEmpName" ReadOnly="true"></asp:TextBox></td>
                                            </tr><br/>
                                            <tr><td>&nbsp</td>
                                            <td>LoanType</td>
                                            <td><asp:TextBox runat="server" ID="txtLoanType" ReadOnly="true"></asp:TextBox></td>
                                            <td>&nbsp</td>
                                            <td>LoanAmount</td><td>&nbsp&nbsp&nbsp&nbsp&nbsp</td>
                                            <td><asp:TextBox runat="server" ID="txtLoanAmt" ReadOnly="true"></asp:TextBox></td>
                                            </tr><br/>
                                            <tr>
                                            <td>&nbsp</td>
                                            <td>No.OfInst</td><td>&nbsp</td>
                                            <td><asp:TextBox runat="server" ID="txtNoofInst" ReadOnly="true"></asp:TextBox></td>
                                            <td>&nbsp</td>
                                            <td>LoanIssueDate</td>
                                            <td><asp:TextBox runat="server" ID="txtLoanIssuedte" ReadOnly="true"></asp:TextBox></td>
                                            </tr>
                                            <asp:Table runat="server" HorizontalAlign="center">
                                            <asp:TableRow><asp:TableCell Width="48.4%"></asp:TableCell>
                                            <asp:TableCell>LoanCutMonth</asp:TableCell><asp:TableCell>&nbsp</asp:TableCell>
                                            <asp:TableCell><asp:TextBox runat="server" ID="txtLoanCutmonth" ReadOnly="true"></asp:TextBox></asp:TableCell>
                                            </asp:TableRow></asp:Table>
                                            <br/>
                                        </div>
                                    </ContentTemplate>
                                    <Triggers >
                                        <asp:AsyncPostBackTrigger ControlID="btndelelte"  />
                                    </Triggers>
                                    </asp:UpdatePanel><div>
                                    <asp:Table runat="server" style="margin-left:180px">  
                                   <asp:Tablerow>
                                  <asp:TableCell><asp:Button ID="btnLoanDelete" runat="server" Text="Delete" CssClass="btn save" Visible="true"
                                  OnClientClick='return confirm(" Are you sure you  want to  delete bill ?");'
                                    OnClick="btnLoanDelete_OnClick"/> </asp:TableCell>
                                  <asp:TableCell>  <asp:Button ID="btncancelLoanDelete"  runat="server" Text="Cancel/Close"  style="width:90px"  CssClass="btn save" OnClick="btncancelLoanDelete_OnClick" AutoPostBack="true"/> </asp:TableCell>
                              </asp:Tablerow>
                               </asp:Table></div>
                            </asp:panel>
                        </div>
           
                                
                           
                           
                            <div class="rounded_corners">
                                <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%" Height="50px"
                                    CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Emp ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpId" Text="<%# Bind('Emp_Id') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblName" Text="<%# Bind('EmpMName') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lbldesignation" Text="<%# Bind('EmpDesgn') %>"></asp:Label>
                                            </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="User Name">
                                           <ItemTemplate>
                                             <asp:Label runat="server" ID="lblUserName" Text="<%# Bind('UserName') %>"></asp:Label>
                                            </ItemTemplate>
                                          </asp:TemplateField>
                                          <asp:TemplateField HeaderText="Privilege">
                                           <ItemTemplate>
                                             <asp:Label runat="server" ID="lblPrivilege" Text="<%# Bind('Name') %>"></asp:Label>
                                            </ItemTemplate>
                                          </asp:TemplateField>
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                <asp:Label ID="LblResult" runat="server" Text="" style=" color:Red"></asp:Label>
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
                    <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
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

