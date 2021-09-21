<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ContractDetailsReports.aspx.cs" Inherits="Jawan.Portal.ContractDetailsReports" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: CONTRACT DETAILS REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 135px;
        }
    </style>
</head>
<body>
    <form id="ContractDetailsReports1" runat="server">
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
                   <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current">
                        <span>Reports</span></a></li>
                    <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>
                        Settings</span></a></li>
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
                            <div style="display: inline">
                                <div id="submenu" class="submenu">
                                    <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li ><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeesLink"
                                            runat="server"><span>Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server"><span>
                                            Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportsLink" runat="server"><span>
                                            Inventory</span></a></li>
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
                    <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Contract Details</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                             Contract Details
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                            
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                    
                       <div class="dashboard_firsthalf" style="width: 100%">
                       
                             <div align="right">
                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" >Export to Excel</asp:LinkButton>
                            </div>
                            
                        
                                <table width="100%" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblEmployee" runat="server" Text="Select Client ID "> </asp:Label><span style=" color:Red">*</span></td>
                                         <td>   <asp:DropDownList runat="server" AutoPostBack="true" ID="ddlClientID" 
                                                class="sdrop" onselectedindexchanged="ddlClientID_SelectedIndexChanged"> 
                                                
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                              <asp:Label ID="lblClientname" runat="server" Text="Name" > </asp:Label> </td>
                                        <td><asp:DropDownList ID="ddlcname" runat="server" class="sdrop" AutoPostBack="true" OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged"></asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblContractID" runat="server" Text="Contract ID"> </asp:Label></td>
                                           <td> <asp:TextBox ID="txtContractID" class="sinput" runat="server" Enabled="false"></asp:TextBox>
                                          
                                           </td>
                                    </tr>   
                                    <tr>
                                       <td>
                                            <asp:Label ID="lblDays" runat="server" Text="Days Per Month " > </asp:Label></td>
                                        <td>    <asp:TextBox ID="txtDays" runat="server" class="sinput" Enabled="false" ></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblServiceCharge" runat="server" Text="Service Charge" > </asp:Label></td>
                                          <td>  <asp:TextBox ID="txtServiceCharge" class="sinput" runat="server" Enabled="false"></asp:TextBox>
                                        </td>
                                              
                                               <td>
                                            <asp:Label ID="lblMachinaryCost" runat="server" Text="Machinary Cost"> </asp:Label></td>
                                         <td>   <asp:TextBox ID="txtMachinaryCost" runat="server" class="sinput" Enabled="false"></asp:TextBox>
                                        </td>   
                                    </tr>        
                                    <tr>
                                      
                                        <td>
                                            <asp:Label ID="lblMaterialCost" runat="server" Text="Material Cost"> </asp:Label></td>
                                          <td>  <asp:TextBox ID="txtMaterialCost" runat="server" class="sinput" Enabled="false"></asp:TextBox>
                                        </td>
                                         <td>
                                            <asp:Label ID="lblOTpercent" runat="server" Text="OT Percent"> </asp:Label></td>
                                           <td> <asp:TextBox ID="txtOTpercent" runat="server" class="sinput" Enabled="false"></asp:TextBox>
                                        </td>
                                        <td>&nbsp;</td>
                                        <td align="left">
                                          <asp:Button runat="server" ID="btnSubmit" Text="Submit" class="btn save" 
                                                onclick="btnSubmit_Click" /><br />
                                                <asp:Label ID="LblResult" runat="server" Visible="false" style=" color:Red"> </asp:Label>
                                           </td>
                                    </tr> 
                                                           
                                </table>
                                
                                
                            </div>                            
                            <div class="rounded_corners" style="overflow:scroll">
                                <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellSpacing="3" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                    
                                      <asp:TemplateField HeaderText="Clientid">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblClientid" Text="<%# Bind('Clientid') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                      <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblclientname" Text="<%# Bind('clientname') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                       <asp:TemplateField HeaderText="Contract ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblclientname" Text="<%# Bind('ContractId') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>      
                                            
                                              <asp:TemplateField HeaderText="No Of Days For Billing">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNoofDays" 
                                                Text='<%# (Eval("NoofDays")!=DBNull.Value ? ((Convert.ToInt32(Eval("NoofDays"))==0)? "General":Eval("NoofDays")):"NULL")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                             
                                              <asp:TemplateField HeaderText="No Of Days For Wages">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblNoOfDaysWages"
                                                Text='<%# (Eval("NoOfDaysWages")!=DBNull.Value ? ((Convert.ToInt32(Eval("NoOfDaysWages"))==0)? "General":Eval("NoOfDaysWages")):"NULL")%>'>
                                               </asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        <asp:TemplateField HeaderText="Designation">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpId" Text="<%# Bind('Design') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                            
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpFirstName" Text="<%# Bind('Quantity') %>"></asp:Label>
                                            </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Basic">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text="<%# Bind('Basic') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="DA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('DA') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="HRA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text="<%# Bind('HRA') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="CCA">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('CCA') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Conveyance">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text="<%# Bind('Conveyance') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Wash Allowance">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('WashAllowance') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Other Allownce">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text="<%# Bind('OtherAllowance') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Pay Rate">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('Amount') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Leave Amount">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('LeaveAmount') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bonus">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text="<%# Bind('Bonus') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Gratuity">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('Gratuity') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF %">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text="<%# Bind('PF') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ESI %">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text="<%# Bind('ESI') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="PF On">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpMiddleName" Text='<%# (Eval("PfFrom")!=DBNull.Value ? ((Convert.ToInt32(Eval("PfFrom"))!=0)? "Basic+DA":"Basic"):"NULL")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ESI On">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblEmpLastName" Text='<%# (Eval("ESIFrom")!=DBNull.Value ? ((Convert.ToInt32(Eval("ESIFrom"))!=0)? "Gross":"Gross-WA"):"NULL")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                          <asp:TemplateField HeaderText="OT Percent">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblotpercent" Text='<%# Eval("OTPersent")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Material Cost">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblmaterialcost" Text="<%# Bind('MaterialCostPerMonth') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Machinary Cost">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblmachinarycost" Text="<%# Bind('MachinaryCostPerMonth') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                          </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
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
