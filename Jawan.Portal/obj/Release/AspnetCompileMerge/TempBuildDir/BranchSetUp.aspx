<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BranchSetUp.aspx.cs" Inherits="Jawan.Portal.BranchSetUp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>SETTINGS: BRANCH SETUP</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="Segment1" runat="server">
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
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server" class="current"><span>
                        Settings</span></a></li>
                    <li class=" after last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
                        Logout</span></span></a></li>
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
                                        <li class="current"><a href="Designation.aspx" id="saleslink" runat="server"><span>Main</span></a>
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
            <h1>
                Settings Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <ul>
                    <li class="left leftmenu">
                        <ul>
                            <li><a href="CreateLogin.aspx" id="CreateLoginLink" runat="server">Create Login</a></li>
                            <li><a href="ChangePassword.aspx" id="ChangePasswordLink" runat="server">Change Password</a></li>
                            <li><a href="Department.aspx" id="DepartmentLink" runat="server">Department</a></li>
                            <li><a href="Designation.aspx" id="DesignationLink" runat="server">Designation</a></li>
                            <li><a href="Segment.aspx" id="SegmentLink" runat="server">Segment</a></li>
                            <li><a href="BankNames.aspx" id="Banknamelink" runat="server">Bank Names</a></li>
                            <li><a href="Categories.aspx" id="Categorieslink" runat="server">Categories</a></li>
                            <li><a href="Resources.aspx" id="Resourceslink" runat="server">Resources</a></li>
                            <li><a href="SalaryBreakup.aspx" id="SalaryBreakupLink" runat="server">SalaryBreakupDetails</a></li>
                            <li><a href="BillingAndSalary.aspx" id="BillingAndSalaryLink" runat="server">Billing/SalaryDetails</a></li>
                            <li><a href="ActivateEmployee.aspx" id="activeEmployeeLink" runat="server">Active/Inactive</a></li>
                            <li><a href="BranchSetUp.aspx" class="sel" id="BranchSetUpLink" runat="server">Branches</a></li>
                        </ul>
                    </li>
                    <li class="right" style="min-height: 200px; height: auto">
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: x-large;
                            font-weight: bold">
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                <tr>
                                    <td width="100%" class="FormSectionHead">
                                        Select Options
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left; font-size: medium; font-weight: normal">
                                        <asp:GridView ID="gvbranches" runat="server" AutoGenerateColumns="false" Width="100%"
                                            CssClass="datagrid" OnRowCancelingEdit="gvbranches_RowCancelingEdit"
                                            OnRowEditing="gvbranches_RowEditing1" OnRowUpdating="gvbranches_RowUpdating1" AllowPaging="True"
                                            OnPageIndexChanging="gvbranches_PageIndexChanging" PageSize="15">
                                            <Columns>
                                                <asp:TemplateField HeaderText="S.No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranchesid" runat="server" Text="<%#Bind('branchid') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:Label ID="lblbranchesid" runat="server" Text="<%#Bind('branchid') %>"></asp:Label>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Branch Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranchesName" runat="server" Text="<%#Bind('branchname') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbranchesName" runat="server" Text="<%#Bind('branchname') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="EMP-Prefix">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranchesEMPrefix" runat="server" Text="<%#Bind('EmpPrefix') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbranchesNameEMPrefix" runat="server" Width="60px" Text="<%#Bind('EmpPrefix') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="CLIENT-Prefix">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbranchesCLIENTPrefix" runat="server" Text="<%#Bind('ClientIDPrefix') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbranchesCLIENTPrefix" runat="server" Width="60px" Text="<%#Bind('ClientIDPrefix') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="With ST">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbillnowithst" runat="server" Text="<%#Bind('BillnoWithServicetax') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbillnowithst" Width="80px" runat="server" Text="<%#Bind('BillnoWithServicetax') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                 <asp:TemplateField HeaderText="With Out ST">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbillnowithoutst" runat="server" Text="<%#Bind('BillNoWithoutServiceTax') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbillnowithoutst" Width="80px" runat="server" Text="<%#Bind('BillNoWithoutServiceTax') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                   <asp:TemplateField HeaderText="Bill Prefix With ST">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbrancheswithostbillprefix" runat="server" Text="<%#Bind('BillprefixWithST') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbrancheswithostbillprefix" Width="80px" runat="server" Text="<%#Bind('BillprefixWithST') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                
                                                   <asp:TemplateField HeaderText="Bill Prefix With Out ST">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblbrancheswithostbillprefixwithout" runat="server" Text="<%#Bind('BillprefixWithoutST') %>"></asp:Label>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtbrancheswithostbillprefixwithout" Width="80px" runat="server" Text="<%#Bind('BillprefixWithoutST') %>"></asp:TextBox>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="linkedit" runat="server" CommandName="Edit" Text="Edit"></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="linkupdate" runat="server" CommandName="update" Text="Update"
                                                            OnClientClick='return confirm(" Are you  sure you  want to update  the Branch?");'></asp:LinkButton>
                                                        <asp:LinkButton ID="linkcancel" runat="server" CommandName="cancel" Text="Cancel"
                                                            OnClientClick='return confirm(" Are you  sure you  want to cancel  the Branch?");'></asp:LinkButton>
                                                    </EditItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    Brnches:<br />
                                      &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:Label ID="lblbranches" runat="server" Text="Name" class="fontstyle"></asp:Label><span style=" color:Red">*</span>
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:TextBox ID="txtbranches" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                        
                                        <br />
                                        Prefix:
                                        
                                        <br />
                                        
                                          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                          Employee<span style=" color:Red">*</span>  &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:TextBox ID="TxtbranchesEmpprefix" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                        
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                        Client:<span style=" color:Red">*</span>
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                        <asp:TextBox ID="TxtbranchesClientprefix" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                        
                                       <br />
                                       Service Tax 
                                        <br />
                                          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                          With<span style=" color:Red">*</span> &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:TextBox ID="TxtbranchesbillnowithST" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                        
                                          With OUT<span style=" color:Red">*</span>  &nbsp;&nbsp; &nbsp;&nbsp;  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        <asp:TextBox ID="TxtbranchesbillnowithOutST" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                         &nbsp;&nbsp; &nbsp;&nbsp; 
                                         <br />
                                        
                                         Bill Prefix 
                                       <br />
                                          &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                          With ST<span style=" color:Red">*</span>  &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
                                        <asp:TextBox ID="TxtbranchesbillnowithSTbillprefix" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                        &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; 
                                        
                                          With OUT ST<span style=" color:Red">*</span>  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;  &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;
                                        <asp:TextBox ID="TxtbranchesbillnowithOutSTbillprefix" runat="server" Width="120px" class="fontstyle"></asp:TextBox>
                                         &nbsp;&nbsp; &nbsp;&nbsp; 
                                         <br />
                                        <asp:Button ID="btnbranches" runat="server" Text="Add" class="btn save" Width="120px"
                                            OnClick="btnbranches_Click" OnClientClick='return confirm(" Are you sure you want to add the Branch?");' />
                                        <asp:Label ID="lblresult" runat="server" Text="" Visible="false" Style="color: Red"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                           
                        </div>
                    </li>
                </ul>
                <div class="clear">
                </div>
            </div>
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
