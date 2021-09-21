<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForStock.aspx.cs" Inherits="Jawan.Portal.ReportForStock" %>
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
        
        
         body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        .Grid td
        {
            background-color: #A1DCF2;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .Grid th
        {
            background-color: #3AC0F2;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid td
        {
            background-color: #eee !important;
            color: black;
            font-size: 10pt;
            line-height:200%
        }
        .ChildGrid th
        {
            background-color: #6C6C6C !important;
            color: White;
            font-size: 10pt;
            line-height:200%
        }
    </style>
    
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">

    $("[src*=plus]").live("click", function() {
        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
        $(this).attr("src", "css/assets/minus.png");
    });
    
    
    $("[src*=minus]").live("click", function() {
    $(this).attr("src", "css/assets/plus.png");
        $(this).closest("tr").next().remove();
    });
</script>
    
    
    <script type="text/javascript">

       function dtval(d, e) {
           var pK = e ? e.which : window.event.keyCode;
           if (pK == 8) { d.value = substr(0, d.value.length - 1); return; }
           var dt = d.value;
           var da = dt.split('/');
           for (var a = 0; a < da.length; a++) { if (da[a] != +da[a]) da[a] = da[a].substr(0, da[a].length - 1); }
           if (da[0] > 31) { da[1] = da[0].substr(da[0].length - 1, 1); da[0] = '0' + da[0].substr(0, da[0].length - 1); }
           if (da[1] > 12) { da[2] = da[1].substr(da[1].length - 1, 1); da[1] = '0' + da[1].substr(0, da[1].length - 1); }
           if (da[2] > 9999) da[1] = da[2].substr(0, da[2].length - 1);
           dt = da.join('/');
           if (dt.length == 2 || dt.length == 5) dt += '/';
           d.value = dt;
       }
		
    </script>
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a></li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a></li>
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
            <h1>
                Settings Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <ul>
                    <li class="left leftmenu">
                        <ul>
                            <li><a href="ListOfItemsReports.aspx" id="ListOfItemsLink" runat="server">List Of Items</a></li>
                               <li><a href="StockInHandReports.aspx" id="StockInHandReportsLink" runat="server">Stock In  Hand</a></li>
                             <li><a href="DipatchStockReports.aspx" id="DispatchStockReportsLink"  runat="server">MRF Details</a></li>
                             <li><a href="ReportForStock.aspx" id="ReportForStockLink" class="sel" runat="server">Stock Details</a></li>
                            <li><a href="EmpInvReport.aspx" id="EmpInvReport" runat="server">Emp INV</a></li>
              
                        </ul>
                    </li>
                    <li class="right" style="min-height: 200px; height: auto">
                    <asp:ScriptManager runat="server" ID="ScriptEmployReports"></asp:ScriptManager>
                        <div id="right_content_area" style="text-align: left; font: Tahoma; font-size: small;  font-weight:normal">
                       
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
                            
                            <table> 
                             <tr>
                            <td  colspan="2">Inventory Type</td>
                                
                            <td> <asp:DropDownList ID="Ddl_Inventory_type" runat="server">
                            <asp:ListItem>Inflow</asp:ListItem>
                            <asp:ListItem>Dispatch</asp:ListItem>
                            <asp:ListItem>Inflow vs Dispatch</asp:ListItem>
                            </asp:DropDownList></td>
                          
                           <td  colspan="2">Report Type</td>
                            <td> <asp:DropDownList ID="Ddl_Report_Type" runat="server">
                            <asp:ListItem>Between Dates</asp:ListItem>
                            <asp:ListItem>Monthly</asp:ListItem>
                            <asp:ListItem>Yearly</asp:ListItem>
                            </asp:DropDownList></td>
        
                                 <td>Branch</td>
                            <td> <asp:DropDownList ID="ddlbranch" runat="server">
                            <asp:ListItem Value="ADG">HYDERABAD</asp:ListItem>
                            <asp:ListItem Value="VZ">VIZAG</asp:ListItem>
                            <asp:ListItem Value="BB">BHUBANESHWAR</asp:ListItem>
                            <asp:ListItem Value="BN">BANGALORE</asp:ListItem>
                            <asp:ListItem Value="CL">CALCUTA</asp:ListItem>
                            <asp:ListItem Value="VJ">VIJAYAWADA</asp:ListItem>
                            <asp:ListItem Value="NE">NELLORE</asp:ListItem>
                            </asp:DropDownList></td>

                         <td style="padding-left:10px" > <asp:LinkButton ID="btnPDF" Text="PDF" runat="server" onclick="btnPDF_Click" /></td>
                         <td style="padding-left:5px"><asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" >Export to Excel</asp:LinkButton></td>
                       </tr>
                       <tr>  </tr>
                       
                            </table>
                            <table>
                             <tr>
                            <td> From Date<span style=" color:Red">*</span></td> 
                            <td>
                              <asp:TextBox id="Txt_From_Date" runat="server" Width="150px"></asp:TextBox>
                                                               <cc1:CalendarExtender ID="CE_From_Date" runat="server"  Enabled="true"
                                                                    TargetControlID="Txt_From_Date" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                                  <cc1:FilteredTextBoxExtender ID="FTBE_From_Date"
                                                                 runat="server" Enabled="True" TargetControlID="Txt_From_Date"
                                                                  ValidChars="/0123456789">
                                                                  </cc1:FilteredTextBoxExtender> 
                               </td>     
                               
                                  <td>To Date<span style=" color:Red">*</span>
                            </td> 
                            <td>
                              <asp:TextBox id="Txt_ToDate" runat="server" Width="150px"></asp:TextBox>
                              
                                 
                                                               <cc1:CalendarExtender ID="CE_Txt_ToDate" runat="server"  Enabled="true"
                                                                    TargetControlID="Txt_ToDate" Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                                               
                                                                  <cc1:FilteredTextBoxExtender ID="FTBE_ToDate"
                                                                 runat="server" Enabled="True" TargetControlID="Txt_ToDate"
                                                                  ValidChars="/0123456789">
                                                                  </cc1:FilteredTextBoxExtender> 
                               </td>  
                                         
                             <td>
                                            <asp:Button runat="server" ID="btn_Submit" Text="Submit"   OnClick="Btn_Submit_OnClick"
                                                class="btn save" /></td>  </tr>  </table>
                            <div>
                            
                            <asp:GridView ID="GVListOfItems" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CssClass="datagrid" CellPadding="4" ForeColor="#333333" 
                                      >
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                    
                                     <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                                HeaderStyle-Width="10%">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Item Id" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemId" runat="server" Text='<%# Bind("itemid") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderText="Item Name" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblItemName" runat="server" Text='<%# Bind("itemname") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Opening Stock" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOpeningstock" runat="server" Text='<%# Bind("Openingstock") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Inflow Stock" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblInflowStock" runat="server" Text='<%# Bind("InflowStock") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Resource Issued" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="Resource Returned" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblResourceReturned" runat="server" Text='<%# Bind("ResourceReturn") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                       <%-- <asp:TemplateField HeaderText="Dispatch Stock" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblOutFlowStock" runat="server" Text='<%# Bind("OutFlowStock") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> --%>

                                        <asp:TemplateField HeaderText="Closing Stock" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClosingStock" runat="server" Text='<%# Bind("ClosingStock") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                         <%--<asp:TemplateField HeaderText="IB Price(Total)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblIBPrice" runat="server" Text='<%# Bind("IBPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="IS Price(Total)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblISPrice" runat="server" Text='<%# Bind("ISPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> 

                                        <asp:TemplateField HeaderText="DS Price(Total)" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center"
                                               >
                                                <ItemTemplate>
                                                    <asp:Label ID="lblDSPrice" runat="server" Text='<%# Bind("DSPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                     </asp:TemplateField> --%>
                                      
                                      <%--<asp:BoundField DataField="itemid" HeaderText="Item Id" />
                                      <asp:BoundField DataField="itemname" HeaderText="Item Name" />
                                      <asp:BoundField DataField="Openingstock" HeaderText="Opening Stock" />
                                      <asp:BoundField DataField="InflowStock" HeaderText="Inflow Stock" />
                                     <asp:BoundField DataField="Quantity" HeaderText="Resource Issue" />
                                      <asp:BoundField DataField="OutFlowStock" HeaderText="Dispatch Stock" />
                                      <asp:BoundField DataField="ClosingStock" HeaderText="Closing Stock" />
                                      
                                      <asp:BoundField DataField="IBPrice" HeaderText="IB Price(Total)" />
                                      <asp:BoundField DataField="ISPrice" HeaderText="IS Price(Total)" />
                                      <asp:BoundField DataField="DSPrice" HeaderText="DS Price(Total)" />--%>
                                    
                                      
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                                
                            </div>
                            
                            
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
        <!-- CONTENT AREA END -->
    </div>
    </form>
</body>
</html>