<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ModifyCompanyInfo.aspx.cs" Inherits="Jawan.Portal.ModifyCompanyInfo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

 </script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>MODIFY COMPANY INFO</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
        .style2
        {
            height: 19px;
        }
        .style3
        {
            width: 135px;
            height: 19px;
        }
        .textwidth
        {
            height: 30px;
        }
        .style4
        {
            height: 34px;
        }
        .style5
        {
            width: 335px;
        }
        .style6
        {
            height: 19px;
            width: 335px;
        }
        .style7
        {
            height: 34px;
            width: 335px;
        }
    </style>
</head>
<body>

<form id="ModifyCompanyInfo1" runat="server">
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
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>
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
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                                   <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                  <ul>
                                        <li><a href="companyinfo.aspx" id="AddCompanyInfoLink" runat="server"><span>Add</span></a></li>
                                        <li class="current"><a href="ModifyCompanyInfo.aspx" id="ModifyCompanyInfoLink" runat="server">
                                            <span>Modify</span></a></li>
                                        <li><a href="DeleteCompanyInfo.aspx" id="DeleteCompanyInfoLink" runat="server"><span>Delete</span></a></li>
                                        <%--    <li><a href="default_packaging.html" id="packaging" runat="server"><span>Packaging</span></a></li>
                <li><a href="default_shipping.html" id="shippinglink" runat="server"><span>Shipping</span></a></li>
                <li><a href="default_administrator.html" id="administratorlink" runat="server"><span>Administrator</span></a></li>
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
                CompanyInfo Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                MODIFY COMPANY INFORMATION
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px; height:400px">
                            <div class=" dashboard_firsthalf">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tr>
                                        <td class="style8">
                                            Company Name
                                        </td>
                                        <td class="style8">
                                            <asp:DropDownList ID="ddlcname" runat="server" Width="155px" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlcname_SelectedIndexChanged">
                                                <asp:ListItem Value="1" Selected="true">--Select Client Id </asp:ListItem>
                                                
                                                
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="a1"
                                                ControlToValidate="ddlcname" ErrorMessage="please enter the company name">*</asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            Company Short Name
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtcsname" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr class="style8">
                                        <td>
                                            Address
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtaddress" runat="server" TextMode="MultiLine" Width="152px" Height="20px"></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="rfvaddress" runat="server" ControlToValidate="txtaddress"
                                                Text="*" SetFocusOnError="true" EnableClientScript="true" ErrorMessage="Please Enter The Address"
                                                ValidationGroup="a1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            PF no
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtpfno" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            ESI NO
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtesino" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            Bill Description
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtbilldesc" runat="server" TextMode="MultiLine" Width="152px" Height="20px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            Company Info
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtcinfo" runat="server" TextMode="MultiLine" Width="150px" Height="20px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            Bill Notes
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtbnotes" runat="server" TextMode="multiline" Width="152px" Height="20px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            Billsq
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtbillsq" runat="server" Width="150px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="style8">
                                            Labour Rule
                                        </td>
                                        <td class="style8">
                                            <asp:TextBox ID="txtlabour" runat="server" TextMode="multiline" Width="152px" Height="20px"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="dashboard_secondhalf" style="height: 200px">
                                <div style="margin-top: 100px; position: fixed; width: 105px">
                                    Company Logo
                                </div>
                                <div style="margin-left: 120px; margin-top: 50px; height: 143px">
                                   <img ID="imglogo" runat="server" Height="100" Width="100" />
                                    <div style="margin-top: 10px">
                                        <asp:Button ID="btnphoto" runat="server" Text="Select Photo" class="btn save" onclick="btnphoto_Click" 
                                        />
                                    
                                    <asp:FileUpload  ID="fcpicture" runat="server" Visible="false"/>
                                    
                                    </div>
                                </div>
                            </div>
                            <%--<div class="dashboard_secondhalf" style=" height:200px">
                            
                           Company Logo      <asp:Image ID="imglogo" style=" margin-top:100px" runat="server" Height="100px" Width="100px" />
                            <asp:FileUpload ID="FileUpload1" runat="server" />
                                    <asp:Button  ID="btniamge" runat="server" Text="Select Logo" 
                                />
                            
                            
                            </div>--%>
                            <div style="margin-left: 50px; float: right; margin-top: 150px">
                               <asp:Label ID="lblresult" runat="server" Visible="false" style=" color:Red"></asp:Label>
                                <asp:Button ID="btnaddclint" runat="server" Text="SAVE" ToolTip="Add Client" class=" btn save"
                                    ValidationGroup="a1" OnClick="btnaddclint_Click" onclientclick='return confirm(" Are You Want to Add The Record  ?");' />
                                <asp:Button ID="btncancel" runat="server" Text="CANCEL" ToolTip="Cancel Client" 
                                    class=" btn save" onclick="btncancel_Click" onclientclick='return confirm(" Are You Want to Cancel  This Entry ?");' /></div>
                        </div>
                    </div>
                    <!-- DASHBOARD CONTENT END -->
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
                <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | &copy;
                <asp:Label ID="lblcname" runat="server"></asp:Label>.</div>
            <div class="clear">
            </div>
        </div>
    </div>
        </div>
    <!-- CONTENT AREA END -->
</body>
</form>
</html>
