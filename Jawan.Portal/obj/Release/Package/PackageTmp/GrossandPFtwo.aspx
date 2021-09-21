﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GrossandPFtwo.aspx.cs" Inherits="Jawan.Portal.GrossandPFtwo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>FACILITY MANAGEMENT SOFTWARE</title>
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
    <form id="UnitWiseGrossReports1" runat="server">
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
                    <li>Welcome <b>
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
                                        <li ><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink"
                                            runat="server"><span>Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>
                                            Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span>
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
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Comparison PF</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                 <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Comparison PF
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                        <div class="dashboard_firsthalf" style="width: 100%">
                        
                            <div align="right">
                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" Visible="False">Export to Excel</asp:LinkButton>
                            </div>
                            
                           
                                <table width="70%" cellpadding="5" cellspacing="5">
                              <tr>
                              
                              <td> 
                              Select  Pf Type</td> 
                               <td><asp:DropDownList  ID="ddlpftype" runat="server" class="sdrop" >
                              <asp:ListItem>PF</asp:ListItem>
                              <asp:ListItem>Non PF</asp:ListItem>
                              </asp:DropDownList>
                               </td>
                              <td>
                                Month
                              </td>
                              <td>
                           
                        Month
                              </td>
                              <td>
                           
                             <asp:TextBox ID="txtmonth"  runat="server" Text="" class="sinput"></asp:TextBox>
                              <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                TargetControlID="txtmonth" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                              <cc1:FilteredTextBoxExtender ID="FTBEDOI"
                                          runat="server" Enabled="True" TargetControlID="txtmonth"
                                           ValidChars="/0123456789">
                                           </cc1:FilteredTextBoxExtender>
                             
                              </td>
                              <td>
                               <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btnsearch_Click"  />
                            </td>
                          </tr>
                                 <tr>
                                 
                                 <td colspan="4">
                                 <asp:Label ID="LblResult" runat="server" Text=""  style=" color:Red"> </asp:Label>
                                 </td> 
                                 </tr>
                                  
                                            </table>
                            </div>
                            
                           <div class="rounded_corners">
                            <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%" Height="50px"
                                    CellPadding="5" CellSpacing="3" ForeColor="#333333" GridLines="None">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                    <asp:TemplateField HeaderText="Unit ID">
                                        <ItemTemplate>
                                          <asp:Label runat="server" ID="lblunitId" Text="<%# Bind('ClientId') %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText=" Name">
                                            <ItemTemplate>
                                               <asp:Label runat="server" ID="lblclientname" Text="<%# Bind('ClientName') %>"></asp:Label>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                        <asp:TemplateField HeaderText="Strength">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblstrength" Text="<%# Bind('strength') %>"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="S-Strength">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblstrengthtwo" Text=""></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField  DataField="Basic" HeaderText="Basic" DataFormatString="{0:0.00}" />
                                         <asp:TemplateField HeaderText="S-Basic">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblBasictwo" Text="" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                        <asp:BoundField  DataField="basicda" HeaderText="BasicDA" DataFormatString="{0:0.00}"/>
                                       
                                        <asp:TemplateField HeaderText="S-BasicDA">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblbasicdatwo" Text="" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField  DataField="otamt" HeaderText="OT  Amt" DataFormatString="{0:0.00}"/>
                                        <asp:TemplateField HeaderText="S-OT Amt">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblotamttwo" Text="" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                       
                                        <asp:BoundField  DataField="pf" HeaderText="PF" DataFormatString="{0:0.00}" />
                                         <asp:TemplateField HeaderText="S-PF">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblpftwo" Text="" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField  DataField="PFeMPR" HeaderText="PFempr" DataFormatString="{0:0.00}"/>
                                        
                                          <asp:TemplateField HeaderText="S-PFempr">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lblPFeMPRtwo" Text="" ></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:BoundField  DataField="total" HeaderText="total" DataFormatString="{0:0.00}"/>
                                          <asp:TemplateField HeaderText="S-total">
                                            <ItemTemplate>
                                              <asp:Label runat="server" ID="lbltotaltwo" Text="" ></asp:Label>
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
                           
                            <div>
                        
                            <table width="100%">
                            <tr style=" width:100%; font-weight:bold">
                            <td  style=" width:10%" >
                            <asp:Label ID="lbltamttext" runat="server" Visible="false" Text="Total Amount"></asp:Label>
                            </td>
                            
                            <td style=" width:70%" >
                          <asp:Label ID="lblstrength" runat="server" Text="" style=" margin-left:16%"></asp:Label>
                             <asp:Label ID="lblgross" runat="server" Text=""  style=" margin-left:13%"></asp:Label>
                              <asp:Label ID="lblbasicda" runat="server" Text="" style=" margin-left:5%"></asp:Label>
                             <asp:Label ID="lblpfemp" runat="server" Text=""  style=" margin-left:8%"></asp:Label>
                            <asp:Label ID="lblpfempr" runat="server" Text=""  style=" margin-left:8%"></asp:Label>
                              <asp:Label ID="lbltotal" runat="server" Text="" style=" margin-left:8%" ></asp:Label>
                             
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
            </div>
        </div>
        <!-- DASHBOARD CONTENT END -->
        <!-- FOOTER BEGIN -->
        <div id="footerouter">
            <div class="footer">
                <div class="footerlogo">
                    <a href="http://www.diyostech.in" target="_blank">Powered by DIYOS tent">
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
