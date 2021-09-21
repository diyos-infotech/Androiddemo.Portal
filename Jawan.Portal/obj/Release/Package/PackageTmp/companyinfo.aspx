<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="companyinfo.aspx.cs" Inherits="Jawan.Portal.companyinfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>COMPANY INFO</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdsize
        {
            height: 15px;
        }
        .style8
        {
            width: 335px;
            height: 29px;
        }
    </style>

 
</head>
<body>
    <form id="companyinfo1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Managment Software" title="Facility Managment Software"></a></div>
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
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server" class="current"><span>
                        Company Info</span></a></li>
                    <li class="after"><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
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
                                <div id="submenu" class="submenu">
                                    <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li class="current"><a href="companyinfo.aspx" id="AddCompanyInfoLink" runat="server">
                                            <span>Add/Modify</span></a></li>
                                        <li><a href="ModifyCompanyInfo.aspx" id="ModifyCompanyInfoLink" visible="false" runat="server">
                                            <span>Modify</span></a></li>
                                        <li><a href="DeleteCompanyInfo.aspx" id="DeleteCompanyInfoLink" visible="false" runat="server">
                                            <span>Delete</span></a></li>
                                        <li><a href="Expenses.aspx" id="ExpensesLink" runat="server" visible="false"><span>Expenses</span></a></li>
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
                CompanyInfo Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                ADD COMPANY INFORMATION
                            </h2>
                        </div>
                         <asp:ScriptManager runat="server" ID="Scriptmanager1">
                                </asp:ScriptManager>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin" style="min-height: 530px;" >
                                <div class="dashboard_firsthalf" style="width: 100%">
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td valign="top">
                                                <table width="100%" cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td>
                                                            Company Name
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcname" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Company Short Name<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcsname" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr class="style8">
                                                        <td>
                                                            Address
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" class="sinput" Height="100px" Enabled="False"></asp:TextBox>
                                                            &nbsp;
                                                           
                                                        </td>
                                                    </tr>
                                                     <tr>
                                                        <td>
                                                            Phone No</td>
                                                        <td>
                                                           <asp:TextBox ID="txtPhoneno" runat="server" class="sinput"  Enabled="False"></asp:TextBox>
                                                         <%--  <cc1:FilteredTextBoxExtender ID="FilterExtenderPhone" runat="server" FilterMode="ValidChars" FilterType="Numbers" 
                                                           ValidChars="0123456789" TargetControlID="txtPhoneno"></cc1:FilteredTextBoxExtender>--%>
                                                           
                                                           </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Fax No</td>
                                                        <td>
                                                            <asp:TextBox ID="txtFaxno" runat="server" class="sinput" MaxLength="11" Enabled="False"></asp:TextBox>
                                                            <cc1:FilteredTextBoxExtender ID="FilterExtenderFaxno" runat="server" FilterMode="ValidChars" FilterType="Custom" 
                                                            ValidChars=".0123456789" TargetControlID="txtFaxno"></cc1:FilteredTextBoxExtender>
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Email</td>
                                                        <td>
                                                           <asp:TextBox ID="txtEmail" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                          
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Website</td>
                                                        <td>
                                                           <asp:TextBox ID="txtWebsite" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                          
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Service Tax No
                                                            <%--Bill notes replace with service tax no --%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbnotes" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PAN No
                                                            <%--Labour rule  replace with PAN no --%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtlabour" runat="server" MaxLength="80" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PF No
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtpfno" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            ESI No
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtesino" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            P Tax No
                                                            <%--Labour rule  replace with PAN no --%>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBANK" runat="server" MaxLength="200" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                           Corporate Identity No
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcorporateIDNo" runat="server"  class="sinput"
                                                                Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Reg.No
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtregno" runat="server"  class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    
                                                   
                                                </table>
                                            </td>
                                            <td align="right">
                                                <table width="100%" cellpadding="5" cellspacing="5" style="position: relative;bottom: 30px;">
                                                    <tr>
                                                        <td>
                                                            Billsq<span style="color: Red">*</span>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbillsq" runat="server" class="sinput" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>



                                                    <tr>
                                                        <td>
                                                            Notes
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtNotes" runat="server" TextMode="MultiLine" class="sinput" Height="100px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr>
                                                        <td>
                                                            Bill Description
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtbilldesc" runat="server" TextMode="MultiLine" class="sinput"
                                                                Height="35px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Company Info
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtcinfo" runat="server" TextMode="MultiLine" class="sinput" Height="35px" Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Category
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCategory" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                     <tr >
                                                        <td>
                                                            ESIC No for Forms
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtESICNoForms" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr >
                                                        <td>
                                                            Branch Office
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtBranchOffice" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>GST No</td>
                                                        <td>
                                                            <asp:TextBox ID="txtGSTNo" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                     <tr >
                                                        <td>
                                                          HSN NUMBER
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtHsnNummber" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr> <tr >
                                                        <td>
                                                          SAC CODE
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtSacCode" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>


                                                    <tr >
                                                        <td>
                                                          PO Contact Person
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPOContactPerson" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <tr >
                                                        <td>
                                                          PO Contact Number
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPOContactNumber" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                     <tr style="visibility:hidden">
                                                        <td>
                                                           ISO CERFT NO
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtISOCertNo" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                     <tr style="visibility:hidden">
                                                        <td>
                                                          PSARA ACT REG NO
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPsaraAct" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr style="visibility:hidden">
                                                        <td>
                                                          KSSA MEMBERSHIP NO
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtKSSAMemberShipNo" runat="server"  class="sinput"  Enabled="False"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                    <%--<tr>
                                                        <td>
                                                            PREPARE
                                                             
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPREPARE" runat="server" MaxLength="300" class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            ACCOUNT NO
                                                            
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAccountno" runat="server" MaxLength="50" class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address Line One
                                                            
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtaddresslineone" runat="server" MaxLength="100" class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Address Line Two
                                                            
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtaddresslinetwo" runat="server" MaxLength="100" class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            IFSC CODE
                                                           
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtifsccode" runat="server" MaxLength="100" class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            SASTC CODE
                                                            
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtsastcc" runat="server" MaxLength="100" class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>--%>
                                                    <tr>
                                                        <%--<td>
                                                            Company Logo
                                                        </td>--%>
                                                        <td>
                                                            <%--<img id="imglogo" runat="server" height="100" width="100" alt="Ther IS No Image" />
                                                            <div style="margin-top: 10px">
                                                                <asp:Button ID="btnphoto" runat="server" Text="Select Photo" class="btn save" OnClick="btnphoto_Click"
                                                                    OnClientClick="beforeadd()" style="width:100px" />
                                                                <asp:FileUpload ID="fcpicture" runat="server" Visible="true" OnDataBinding="btnphoto_Click" />--%>
                                                                <asp:Label ID="lblresult" runat="server" Style="color: Red" Visible="false"></asp:Label>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                                <div style=" float: right;">
                                     <asp:Button ID="btnEdit" runat="server" Text="EDIT" ToolTip="Add Client" class=" btn save"
                                        ValidationGroup="a1" OnClick="btnEdit_Click"   />
                                    <asp:Button ID="btnaddclint" runat="server" Text="SAVE" ToolTip="Add Client" class=" btn save" Enabled="false"
                                        ValidationGroup="a1" OnClick="btnaddclint_Click" OnClientClick='return confirm(" Are you sure you  want to add this record ?");' />
                                    <asp:Button ID="btncancel" runat="server" Text="CANCEL" ToolTip="Cancel Client" OnClientClick='return confirm(" Are you sure  you  want to cancel  this record?");'
                                        class=" btn save" OnClick="btncancel_Click" Enabled="false" />
                                </div>
                                 
                                                                </div>
                        </div>
                    
                </div>
                <!-- DASHBOARD CONTENT END -->
            </div>
            <div class="clear">
            </div>
        </div>
    </div>
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
