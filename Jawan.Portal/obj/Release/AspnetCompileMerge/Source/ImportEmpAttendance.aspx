<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportEmpAttendance.aspx.cs" Inherits="Jawan.Portal.ImportEmpAttendance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>IMPORT EMPLOYEE ATTENDANCE</title>
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
    <form id="EmpTransferDetailReports1" runat="server">
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
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                    <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>
                        Settings</span></a></li>
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
                            <div style="display: inline">
                                <div id="submenu" class="submenu">
                                    <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                                    <ul>
                                        <li class="current"><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"
                                            runat="server"><span>Employees</span></a></li>
                                        <li><a href="ClientReports.aspx" id="ClientsReportLink" runat="server"><span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>
                                            Inventory</span></a></li>
                                        <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"><span>Companyinfo</span></a>
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
            <div id="breadcrumb">
                <ul class="crumbs">
                    <li class="first"><a href="#" style="z-index: 9;"><span></span>Reports</a></li>
                    <li><a href="Reports.aspx" style="z-index: 8;">Employee Reports</a></li>
                    <li class="active"><a href="EmpDueAmount.aspx" style="z-index: 7;" class="active_bread">
                        DUE AMOUNT</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                DUE AMOUNT
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                
                                <div class="boxbody" style="padding: 5px 5px 5px 5px; min-height: 200px; height: auto">
                            <!--  Content to be add here> -->
                            <div class="dashboard_full">
                                <div class="dashboard_full">
                                    <div>
                                        <table>
                                            <tr>
                                                <td style="width: 60px">
                                                    Client ID<span style=" color:Red">*</span>
                                                </td>
                                                 <td>
                                                    <asp:DropDownList ID="ddlClientID" runat="server" Width="125px" AutoPostBack="True"
                                                        OnSelectedIndexChanged="ddlClientID_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                <td class="style1">
                                                    Client Name
                                                </td>
                                                
                                                 <td>
                                                      <asp:DropDownList ID="ddlCName" runat="server" AutoPostBack="True" 
                                                        Width="125px" OnSelectedIndexChanged="ddlCName_SelectedIndexChanged"></asp:DropDownList>
                                                  
                                                </td>
                                                <td>
                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                </td>
                                                
                                                <td style="width: 60px" > Select File: </td>
                                                <td>
                                                <asp:FileUpload  ID="fileupload1" runat="server"/>
                                                </td>
                                                <td>
                                                 <asp:Button ID="btnImport" runat="server" Text="Import Data"  class=" btn save" OnClick="btnImport_Click"/> 
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <td style="width: 60px">
                                                    Month</td>
                                                 <td>
                                                  <asp:DropDownList runat="server" ID="ddlMonth" Width="125px" AutoPostBack="True" 
                                                             onselectedindexchanged="ddlMonth_SelectedIndexChanged"></asp:DropDownList>
                                                           
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td class="style1">
                                                    OT in terms of</td>
                                                
                                                 <td>
                                                    <asp:DropDownList runat="server" ID="ddlOTType" Width="125px" >
                                                        <asp:ListItem>Days</asp:ListItem>
                                                        <asp:ListItem>Hours</asp:ListItem>
                                                    </asp:DropDownList>
                                                  
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                
                                                <td style="width: 60px" > 
                                             
                                                 <asp:Button ID="btnClear" runat="server" Text="Clear"  class=" btn save" 
                                                         onclick="btnClear_Click" /> 
                                              
                                                </td>
                                                <td>
                                             
                                                 <asp:Button ID="btnClearAll" runat="server" Text="Clear All"  class=" btn save" 
                                                         onclick="btnClearAll_Click" /> 
                                             
                                                </td>
                                                <td>
                                                 <asp:Button ID="btnExport" runat="server" Text="Unsaved"  class=" btn save" 
                                                         onclick="btnExport_Click" Visible="false"/> 
                                                </td>
                                                
                                            </tr>
                                           
                                </table>
                                    </div>
                                </div>
                               
                            </div>
                            <div style="margin-top: 100px; padding:0px; margin:0px">
                            <asp:GridView ID="gvAttendancestatus" runat="server" AutoGenerateColumns="False" GridLines="Both"
                            ForeColor="#333333" HeaderStyle-CssClass="HeaderStyle" ShowFooter="True"
                                    Height="140px" Width="90%" 
                            Style="margin-left: 50px" 
                                   CellPadding="4">
                               <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <Columns>
                            
                            <asp:TemplateField HeaderText="S No">
                            <ItemTemplate>
                            <asp:Label ID="lblsno" runat="server" Text="<%#Container.DataItemIndex+1%>"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Designation">
                            <ItemTemplate>
                            <asp:Label ID="lblDesign" runat="server" Text="<%#Bind('Design') %>"></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Duties">
                            <ItemTemplate>
                            <asp:Label ID="lblTotDuties" runat="server" Text="<%#Bind('Duties')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblGTotDuties" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="WOs">
                            <ItemTemplate>
                            <asp:Label ID="lblTotWos" runat="server" Text="<%#Bind('Wo')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblGTotWos" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="OTs">
                            <ItemTemplate>
                            <asp:Label ID="lblTotOts" runat="server" Text="<%#Bind('ot')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblGTotOts" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                         
                            
                             <asp:TemplateField HeaderText="Canteen Advance">
                            <ItemTemplate>
                            <asp:Label ID="lblTotCanteenadv" runat="server" Text="<%#Bind('Canteenadv')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblGTotCanteenadv" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Penalty">
                            <ItemTemplate>
                            <asp:Label ID="lblTotPenalty" runat="server" Text="<%#Bind('penalty')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblGTotPenalty" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="Incentives">
                            <ItemTemplate>
                            <asp:Label ID="lblTotIncentives" runat="server" Text="<%#Bind('Incentives')%>"></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lblGTotIncentives" runat="server"></asp:Label>
                            </FooterTemplate>
                            </asp:TemplateField>
                            
                            </Columns>
                             <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" 
                                    HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" 
                                    ForeColor="#333333" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#999999" />
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            </asp:GridView>
                            </div>
                                      <asp:Label ID="lblMessage" runat="server" style="color:Red"></asp:Label>
                        <br />
              
                            <div style="margin-top: 100px; padding:0px; margin:0px" class="social">
                                        
                                <asp:GridView ID="GridView1" runat="server" Height="140px" Width="90%" Style="margin-left: 50px"
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"
                                    BorderColor="Black" BorderWidth="1px" GridLines="None" 
                                    HeaderStyle-CssClass="HeaderStyle"  >
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:TemplateField HeaderText=" Emp Id" >
                                            <ItemTemplate>
                                               <asp:Label ID="lblEmpid" runat="server" Text=" <%#Bind('EmpId')%>" Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        <ItemStyle   VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblName"  Width="180px"></asp:Label>
                                            </ItemTemplate>
                                          
                                       </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDesg" Text=" <%#Bind('Design')%>" Width="150px"></asp:Label>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                     
                                       <asp:TemplateField HeaderText="Duties">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblDuties" Text=" <%#Bind('NoOfDuties')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                        <asp:TemplateField HeaderText="WOs">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblWos" Text=" <%#Bind('Wo')%>" Width="50px"></asp:Label>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="OTs" >
                                            <ItemTemplate>
                                           <asp:Label runat="server" ID="lblOts" Text=" <%#Bind('OT')%>" Width="40px"></asp:Label>
                                            </ItemTemplate>
                                           
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Canteen Adv" >
                                            <ItemTemplate>
                                           <asp:Label runat="server" ID="lblCanteenAdv" Text=" <%#Bind('CanteenAdv')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Penalty" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblPenalty" Text=" <%#Bind('Penalty')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                          
                                       </asp:TemplateField>
                                       
                                       
                                        <asp:TemplateField HeaderText="Incentive" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblIncentivs" Text=" <%#Bind('Incentivs')%>" Width="90px"></asp:Label>
                                                                              </ItemTemplate>
                                            <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                  
                                    
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                             
                                <div>
                                
                                <div>
                                <asp:GridView ID="GridView2" runat="server" Height="140px" Width="90%" Style="margin-left: 50px"
                                    AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" BorderStyle="Solid"
                                    BorderColor="Black" BorderWidth="1px" GridLines="None" HeaderStyle-CssClass="HeaderStyle" Visible="false" >
                                   <Columns>
                                        <asp:TemplateField HeaderText=" Emp Id" >
                                            <ItemTemplate>
                                               <asp:Label ID="lblEmpid1" runat="server" Text=" <%#Bind('EmpId')%>" Width="60px"></asp:Label>
                                            </ItemTemplate>
                                        <ItemStyle   VerticalAlign="Middle"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Designation" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblDesg1" Text=" <%#Bind('Design')%>" Width="150px"></asp:Label>
                                            </ItemTemplate>
                                       </asp:TemplateField>
                                     
                                       <asp:TemplateField HeaderText="Duties">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblDuties1" Text=" <%#Bind('NoOfDuties')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="OTs" >
                                            <ItemTemplate>
                                           <asp:Label runat="server" ID="lblOts1" Text=" <%#Bind('ot')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                           
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Canteen Adv" >
                                            <ItemTemplate>
                                           <asp:Label runat="server" ID="lblCanteenAdv1" Text=" <%#Bind('CanteenAdv')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                            
                                       </asp:TemplateField>
                                       <asp:TemplateField HeaderText="Penalty" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblPenalty1" Text=" <%#Bind('Penalty')%>" Width="90px"></asp:Label>
                                            </ItemTemplate>
                                          
                                       </asp:TemplateField>
                                       
                                       
                                        <asp:TemplateField HeaderText="Incentive" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblIncentivs1" Text=" <%#Bind('Incentivs')%>" Width="90px"></asp:Label>
                                                                              </ItemTemplate>
                                            <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                       
                                       <asp:TemplateField HeaderText="Remarks" >
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRemarks" Text=" <%#Bind('Remark')%>" Width="90px"></asp:Label>
                                                                              </ItemTemplate>
                                            <ItemStyle Width="60px"></ItemStyle>
                                       </asp:TemplateField>
                                  
                                    
                                    </Columns>
                                </asp:GridView>
                                </div>
                                    <table width = "100%">
                                        <tr>
                                            <td width="25%"></td>
                                            <td width="25%">
                                                <asp:Label ID="lblTotalDuties" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>                                        
                                            <td width="25%">
                                                <asp:Label ID="lblTotalOts" runat="server" Text="" Font-Bold="true"></asp:Label>
                                            </td>
                                            <td width="25%"></td>
                                        </tr>
                                        <tr>
                                        <td colspan="4">
                                        <asp:Label ID="lbltotaldesignationlist" runat="server" Text="" > </asp:Label>
                                        </td>
                                        
                                        </tr>
                                    </table>
                                </div>
                               
                                       <br />
                                <asp:Label ID="Label1" runat="server" Text="" style="color:Red"></asp:Label>
                                       
                                
                            </div>
                      
                        </div>
                                
                                
                                <div style="margin-top: 20px">
                                    <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"></asp:Label>
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
