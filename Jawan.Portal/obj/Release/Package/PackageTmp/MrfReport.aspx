<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MrfReport.aspx.cs" Inherits="Jawan.Portal.MrfReport" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: MRF REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    </head>
<body>
    <form id="DipatchStockReports1" runat="server">
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>
                        Employees</span></a></li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company 
                        Info</span></a></li>
                    <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a></li>
                    <li><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server" class="current">
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
                                        <li ><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"><span>
                                            Clients</span></a></li>
                                        <li class="current"><a href="ListOfItemsReports.aspx" id="InventoryReportLink" class="sel" runat="server"><span>
                                            Inventory</span></a></li>
                                            <li><a href="ExpensesReports.aspx" id="ExpensesReportsLink" runat="server"> <span>
                                                Companyinfo</span></a>  </li> 
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
                            <li><a href="ListOfItemsReports.aspx" id="ListOfItemsLink">List Of Items</a></li>
                               <li><a href="StockInHandReports.aspx" id="StockInHandReports">Stock In Hand</a></li>
                               <li><a href="DipatchStockReports.aspx" id="DispatchStockReportsLink" >Dispatched 
                                   Stock</a></li>
                               <li><a href="ReportonInventoryDaily.aspx" id="ReportonInventoryDailylink" >
                                   Stock Options</a></li>
                                    <li><a href="MrfReport.aspx" id="MrfReport" class="sel">
                                   MRF Status</a></li>
                        </ul>
                    </li>
                    <li class="right" style="min-height: 200px; height: auto">
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;  font-weight:normal">
                        <div align="right">
                            <asp:LinkButton ID="btnPDF" Text="PDF" runat="server" onclick="btnPDF_Click" />
                        </div>
                             <table width="100%" border="0" cellpadding="0" cellspacing="0" class="FormContainer">
                                <tr>
                                    <td width="100%" class="FormSectionHead">
                                           List Of Items   
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                </tr>
                                </table>
                            <table width="100%" cellpadding="5" cellspacing="5">
                             <tr>
                                    <td width="200px">
                                        &nbsp;Client ID</td>
                                    <td>
                                      <asp:DropDownList ID="ddlclientid" runat="server" Width="120px" AutoPostBack="true" class="sdrop"  onselectedindexchanged="DdlClientId_SelectedIndexChanged"></asp:DropDownList>

                                    </td>
                                    <td width="200px">
                                        &nbsp;Client Name</td>
                                    <td>
                                      <asp:DropDownList ID="ddlcname" runat="server"  Width="180px" AutoPostBack="true" class="sdrop"   OnSelectedIndexChanged="ddlcname_OnSelectedIndexChanged" ></asp:DropDownList>

                                    </td>
                                    <td width="150px">
                                        &nbsp;</td>
                                    <td>
                                        &nbsp;</td>
                                </tr>
                                
                                 <tr>
                                    <td>
                                        Report Type
                                    </td>
                                    <td>
                                       <asp:DropDownList ID="ddlreporttype" runat="server"  Width="120px" class="sdrop"
                                AutoPostBack="true">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <asp:ListItem>MRF Issue</asp:ListItem>
                            <asp:ListItem>MRF Approve</asp:ListItem>
                            
                            </asp:DropDownList>
                                    </td>
                                    <td>
                                        Select Date
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddldaytype" runat="server"  Width="120px" class="sdrop">
                            <asp:ListItem>--Select--</asp:ListItem>
                            <%--<asp:ListItem>Yearly</asp:ListItem>--%>
                            <asp:ListItem>Monthly</asp:ListItem>
                            <asp:ListItem>Daily</asp:ListItem>
                            </asp:DropDownList>
                                    </td>
                                    <td>
                                      &nbsp; 
                                    </td>
                                    <td>
                                       <asp:TextBox ID="txtdate" runat="server" class="sinput"></asp:TextBox>
                             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="txtdate" Format="dd/MM/yyyy">
                                                  </cc1:CalendarExtender>
                                                  <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5"  runat="server" Enabled="True"
                                                   TargetControlID="txtdate" ValidChars="/0123456789">
                                                  </cc1:FilteredTextBoxExtender>
                                    </td>
                                </tr>
                            <tr>
                                    <td>
                                      
                                    </td>
                                    <td>
                                        
                                    </td>
                                    <td>
                                       
                                    </td>
                                    <td>
                                     
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    <asp:Button runat="server" ID="btn_Submit" Text="Submit" OnClick="btn_Submit_Click"
                                                class="btn save" />
                                    </td>
                                </tr>
                            
                            
                            </table>
                           
                             
                                                
                            <div>
                            
                            <asp:GridView ID="GVListOfItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datagrid" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="true" 
                                    PageSize="15" onpageindexchanging="GVListOfItems_PageIndexChanging" 
                                    onrowdatabound="GVListOfItems_RowDataBound" >
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                       <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblsno1" runat="server" Text="<%#Container.DataItemIndex+1%>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                       <asp:BoundField DataField="MRFId" HeaderText="MRF Id" />
                                       <asp:BoundField DataField="itemid" HeaderText="Item Id" />
                                       <asp:BoundField DataField="itemname" HeaderText="Item Name" />
                                       <asp:BoundField DataField="Clientid" HeaderText="Client Id" />
                                       <asp:BoundField DataField="clientname" HeaderText="Client Name" />
                                       
                                      <%-- <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                        <asp:BoundField DataField="SellingPrice" HeaderText="Selling Price" />
                                        <asp:BoundField DataField="Total" HeaderText="Total" />--%>
                                        
                                        <asp:TemplateField HeaderText="Qty">
                                        <ItemTemplate>
                                            <asp:Label ID="lblqty" runat="server" Text="<%#Bind('Quantity')%>"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lblTotalQty" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Price">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPrice" runat="server" Text="<%#Bind('SellingPrice')%>"></asp:Label>
                                        </ItemTemplate>
                                         <FooterTemplate>
                                        <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Total">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="<%#Bind('Total')%>"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                        <asp:Label ID="lblGTotal" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                        
                                      <asp:BoundField DataField="date" HeaderText="Date" DataFormatString="{0:dd/MM/yyyy}" />
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                         
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
        <!-- CONTENT AREA END -->
    </div>
    </form>
</body>
</html>
