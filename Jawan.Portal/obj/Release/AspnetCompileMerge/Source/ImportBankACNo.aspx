<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportBankACNo.aspx.cs" Inherits="Jawan.Portal.ImportBankACNo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>IMPORT BankAC NOS.</title>
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
                       Import BankAC NOs</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                               Import BankAC NOs
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;
                            font-weight: normal">
                            <div align="right">
                               <asp:LinkButton ID="lnkMaster" runat="server" onclick="lnkMaster_Click" style="margin-left:15px" >Export Bank Names Master</asp:LinkButton>
                               <asp:LinkButton ID="lnkSample" runat="server" onclick="lnkSample_Click"  >Export Sample Sheet</asp:LinkButton>
                            </div>
                            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                <tr>
                                    <td width="100%" class="FormSectionHead">
                                        Select Options
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                            <div >
                                 <br />
                                <table width="80%" >
                                    <tr>
                                        <td style="width: 80px" > Select File: </td>
                                        <td >
                                          <asp:FileUpload ID="FlUploadBankAC" runat="server" />
                                        </td>
                                        <td >
                                            <asp:Button ID="BtnSave" runat="server" Text="Import Data" class="btn save" onclick="BtnSave_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="BtnUnSave" runat="server" Text="Export UnSaved Data" Visible="false" class="btn save"
                                                onclick="BtnUnSave_Click" />
                                        </td>
                                       
                                     </tr>       
                                           
                                </table>


                                <asp:GridView ID="GVBankNames" runat="server" AutoGenerateColumns="False" Width="90%" Visible="false"
                                    ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center;margin: 0px auto;margin-top: 10px;">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="Bank ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankID" Text=' <%#Eval("bankid")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankName" Text='<%#Eval("bankname")%>'></asp:Label>
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
                             
                              <asp:GridView ID="GvNotInsertedlist" runat="server" AutoGenerateColumns="False" Width="90%" Visible="false"
                                    ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center;margin: 0px auto;margin-top: 10px;">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="Emp ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblempid" Text=' <%#Eval("EmpID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank AC No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankACNo" Text='<%#Eval("BankACNo","{0:X}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Card Ref No">
                                         <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankCardRefNo" Text='<%#Eval("BankCardRefNo","{0:X}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank Name">
                                         <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankName" Text='<%#Eval("BankName")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Remarks">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblRemarks" Text='<%#Eval("Remark")%>'></asp:Label>
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
                            
                                <asp:GridView ID="gvlistofemp" runat="server" AutoGenerateColumns="False" Width="90%" Visible= "false"
                                    ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center">
                                    <RowStyle BackColor="#EFF3FB" Height="30" />
                                    <Columns>
                                       
                                        <asp:TemplateField HeaderText="Emp ID">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblempid" Text=' <%#Eval("EmpID")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Bank AC No">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankACNo" Text='<%#Eval("BankACNo","{0:x4}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Bank Card Ref No">
                                         <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankCardRefNo" Text='<%#Eval("BankCardRefNo","{0:x4}")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="IFSC Code">
                                         <ItemTemplate>
                                                <asp:Label runat="server" ID="lblifsccode" Text='<%#Eval("IFSCCode")%>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                         <asp:TemplateField HeaderText="Bank Name">
                                         <ItemTemplate>
                                                <asp:Label runat="server" ID="lblBankName" Text='<%#Eval("BankName")%>'></asp:Label>
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
