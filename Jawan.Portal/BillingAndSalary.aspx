<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BillingAndSalary.aspx.cs" Inherits="Jawan.Portal.BillingAndSalary" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: BILLING AND SALARY</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .boxed
        {
            border: 1px solid black;
        }
        .fontstyle
        {
            font-family: Arial;
            font-size: 13px;
            font-weight: normal;
            font-variant: normal;
        }
    </style>
</head>
<body>

    <form id="BillingAndSalary1" runat="server">
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
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="CreateLogin.aspx" id="SettingsLink" runat="server" class="current"><span>
                        Settings</span></a></li>
                    <%--    <li class="current"><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
                --%>
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
                                        <li class="current"><a href="CreateLogin.aspx" id="CreateLoginlink1" runat="server">
                                            <span>Main</span></a> </li>
                                        <%-- <li><a href="ViewStock.aspx" id="viewstocklink" runat="server"><span>Stock</span></a>
                                        </li>--%>
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
                    <li class="first"><a href="Settings.aspx" style="z-index: 9;"><span></span>Settings</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Billing/Salary
                        Details</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Billing/Salary Details
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <div class="dashboard_firsthalf" title="Billing Options" style="width: 100%">
                                    <table width="100%" cellpadding="5" cellspacing="5">
                                        <tr>
                                            <td valign="top" width="40%">
                                                <table cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td style="font-weight: bold; text-decoration: underline">
                                                            Billing Options
                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                    <td>Enter Financial Year</td>
                                                 <td>
                                                  <asp:TextBox ID="Txt_FY"   runat="server" MaxLength="4" Width="40px"></asp:TextBox>
                                                   <cc1:FilteredTextBoxExtender ID="FTBE_FY" runat="server" Enabled="True"
                                                                TargetControlID="Txt_FY" ValidChars="123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                 </td>  
                                                 <td><asp:Button  ID="Btn_Fy" runat="server" Text="Add" class="btn save" OnClick="Btn_Fy_Add"/></td>
                                                    </tr>
                                                    <tr>
                                                        <td>ID</td>
                                                        <td>
                                                            

                                                            <asp:DropDownList ID="ddlID" runat="server" Width="55px" CssClass="sdrop" AutoPostBack="true" OnSelectedIndexChanged="ddlID_SelectedIndexChanged"></asp:DropDownList>

                                                        </td>
                                                    </tr>
                                                    
                                                    <tr>
                                                        <td>
                                                            From Date
                                                            </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtFromDate" runat="server" Text="" class="sinput" Width="75px"> </asp:TextBox>
                                                             <cc1:CalendarExtender ID="CEStartingDate" runat="server" Enabled="true" TargetControlID="TxtFromDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEStartingDate" runat="server" Enabled="True"
                                                                TargetControlID="TxtFromDate" ValidChars="/0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                        
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                            To Date
                                                            </td>
                                                        <td>
                                                            <asp:TextBox ID="TxtToDate" runat="server" Text="" class="sinput" Width="75px" > </asp:TextBox>
                                                            <cc1:CalendarExtender ID="CEEndDate" runat="server" Enabled="true" TargetControlID="TxtToDate"
                                                                Format="dd/MM/yyyy">
                                                            </cc1:CalendarExtender>
                                                            <cc1:FilteredTextBoxExtender ID="FTBEEndDate" runat="server" Enabled="True"
                                                                TargetControlID="TxtToDate" ValidChars="/0123456789">
                                                            </cc1:FilteredTextBoxExtender>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Service Tax Including CESS(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_ServiceTax" runat="server" Text="" class="sinput" Width="35px"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Service Tax Excluding CESS(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Service_Tax_Saparate" runat="server" Text="" class="sinput"
                                                                Width="35px"> </asp:TextBox>
                                                        </td>
                                                    </tr>
                                                      <tr>
                                                        <td>
                                                            SB CESS (%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_SB_CESS" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                        </td>
                                                        </tr>
                                                     <tr>
                                                        <td>
                                                            Krishi Kalyan CESS (%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_KK_CESS" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                        </td>
                                                        </tr>
                                                    <tr>

                                                        <td>
                                                            CESS for Service Tax(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_CESS" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                        </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                SHE.CESS
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_She_Cess" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                     <tr>
                                                            <td>
                                                                CGST
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_CGST" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                     <tr>
                                                            <td>
                                                                SGST
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_SGST" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                     <tr>
                                                            <td>
                                                                IGST
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_IGST" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                     <tr>
                                                            <td>
                                                                CESS1
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_CESS1" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>

                                                     <tr>
                                                            <td>
                                                                CESS2
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="Txt_CESS2" runat="server" Text="" class="sinput" Width="35px"></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                </table>
                                               
                                            </td>

                                             <td valign="top">
                                                 <table>
                                                    <tr>
                                                        <td style="font-weight: bold; text-decoration: underline">
                                                            Employee Deductions
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PF(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Pf_Employee" runat="server" Width="35px" Text="12.00" 
                                                                class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            ESI(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_ESI_Employee" runat="server" Text="1.75" Width="35px" 
                                                                class="sinput"></asp:TextBox>
                                                        </td>
                                                    </tr>

                                                     </table>
                                               </td>


                                            <td valign="top">
                                                <table cellpadding="5" cellspacing="5">
                                                    <tr>
                                                        <td style="font-weight: bold; text-decoration: underline">
                                                            Employer Contributions
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PF(%)
                                                        </td>
                                                        <td style="margin-left: 100px">
                                                            <asp:TextBox ID="Txt_Pf_Employeer" runat="server" Text="13.61" Width="35px" class="sinput"
                                                                Style="margin-left: 15px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PF Excluding Admin Chrgs(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Pf_Employee_AdminCharge" runat="server" Width="35px" Text="12.00"
                                                                class="sinput" Style="margin-left: 15px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Pension Fund Contribution(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Employee_Pension" runat="server" Width="35px" Text="8.33" class="sinput"
                                                                Style="margin-left: 15px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            ESI(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Esi_Employeer" runat="server" Width="35px" Text="8.33" class="sinput"
                                                                Style="margin-left: 15px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            NH Days(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_NH_Days" runat="server" Width="35px" Text="8.33" class="sinput"
                                                                Style="margin-left: 15px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Bonus(%)
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="Txt_Bonus" runat="server" Width="35px" Text="8.33" class="sinput"
                                                                Style="margin-left: 15px"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>

                                            </td>


                                          
                                        </tr>
                                    </table>
                                </div>
                                 
                                                <div style="font-weight: bold; text-decoration: underline;margin-left:30px">
                                                    Professional Tax
                                                </div>
                                                <div class="rounded_corners" style="margin-top: 10px;margin-left:30px">
                                                 Select Financial Year 
                                                <asp:DropDownList ID="Ddl_Fy" runat="server" Width="">
                                                </asp:DropDownList>
                                                
                                                    <asp:GridView ID="gvservicetax" runat="server" AutoGenerateColumns="false" Width="50%" 
                                                        CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None" AllowPaging="True">
                                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                                        <Columns>
                                                            <asp:BoundField HeaderText="Range From" DataField="RangeFrom" />
                                                            <asp:BoundField HeaderText="Range To" DataField="Rangetill" />
                                                            <asp:BoundField HeaderText="Prof.Tax" DataField="ProfTax" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                        <PagerStyle BackColor="#2461BF" HorizontalAlign="Center" BorderWidth="1px" CssClass="GridPager" />
                                                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Height="30" ForeColor="White" HorizontalAlign="Center" />
                                                        <EditRowStyle BackColor="#2461BF" />
                                                        <AlternatingRowStyle BackColor="White" />
                                                    </asp:GridView>
                                                </div>
                                           
                                <div align="right">
                                    <asp:Button ID="btnupdate" runat="server" Text="Update" class="btn save" Style="margin-right: 10px"
                                        OnClick="btnupdate_Click" OnClientClick='return confirm(" Are you  sure you  want to update   the billing and salary details?");' />
                                    <asp:Label ID="lblresult" runat="server" Text="" Visible="false" Style="color: Red"> </asp:Label>
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
