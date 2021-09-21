<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportForBulkpaysheetforclients.aspx.cs" Inherits="Jawan.Portal.ReportForBulkpaysheetforclients" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: BULK PAYSHEET</title>
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


    
    <script type="text/javascript">

        function Check_Click(objRef) {
            //Get the Row based on checkbox
            var row = objRef.parentNode.parentNode;
            //Get the reference of GridView
            var GridView = row.parentNode;

            //Get all input elements in Gridview
            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {
                //The First element is the Header Checkbox
                var headerCheckBox = inputList[0];

                //Based on all or none checkboxes
                //are checked check/uncheck Header Checkbox
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;

        }
    </script>
    
    <script type="text/javascript">

        function checkAll(objRef) {
        
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                //Get the Cell To find out ColumnIndex
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        //If the header checkbox is checked
                        //check all checkboxes
                        //and highlight all rows
                        inputList[i].checked = true;
                    }
                    else {
                        //If the header checkbox is checked
                        //uncheck all checkboxes
                        inputList[i].checked = false;
                    }
                }
            }
        }
    
    </script>
    
</head>
<body>
    <form id="ClentwiseEmployeesSalaryreports1" runat="server">
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
                    <li class="after"><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                    <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a></li>
                </ul>
            </div>
            <!-- MAIN MENU SECTION END -->
        </div>
        <!-- LOGO AND MAIN MENU SECTION END -->
        <!-- SUB NAVIGATION SECTION BEGIN -->
        <!--  <div id="submenu"> <img width="1" height="5" src="assets/spacer.gif"> </div> -->
        <div id="submenu" style=" width:100%">
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
                                        <li><a href="ActiveEmployeeReports.aspx" id="EmployeeReportLink" runat="server"><span>  Employees</span></a></li>
                                        <li class="current"><a href="ActiveClientReports.aspx" id="ClientsReportLink" runat="server"> <span>Clients</span></a></li>
                                        <li><a href="ListOfItemsReports.aspx" id="InventoryReportLink" runat="server"><span> Inventory</span></a></li>
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
                    <li><a href="ClientReports.aspx" style="z-index: 8;">Client Reports</a></li>
                    <li class="active"><a href="#" style="z-index: 7;" class="active_bread">Paysheet Report</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                              Salary Details
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                        <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                        </asp:ScriptManager>
                       <div class="dashboard_firsthalf" style="width: 100%">
                         
                                <table width="95%" cellpadding="5" cellspacing="5">
                                    <tr>
                                        <td>
                                            Month
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtmonth" runat="server" Text="" class="sinput" ></asp:TextBox>
                                            <cc1:CalendarExtender ID="txtFrom_CalendarExtender" runat="server" Enabled="true"
                                                TargetControlID="txtmonth" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                            <cc1:FilteredTextBoxExtender ID="FTBEDOI" runat="server" Enabled="True" TargetControlID="txtmonth"
                                                ValidChars="/0123456789">
                                            </cc1:FilteredTextBoxExtender>
                                        </td>
                                        <td>
                                            <asp:Button runat="server" ID="btn_Submit" Text="Submit" class="btn save" OnClick="btnsearch_Click" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lbloptions" runat="server" Text="Print Options"></asp:Label>
                                        </td>
                                         <td>
                                            <asp:DropDownList ID="ddlOptions" runat="server">
                                             <asp:ListItem Text="Excel"></asp:ListItem>
                                                                                         <asp:ListItem Text="PDF"></asp:ListItem>

                                            </asp:DropDownList>
                                        </td>
                                        
                                        <td>
                                          <asp:Button ID="btnDownload" runat="server" Text="Download" class="btn save"
                                                onclick="btnDownload_Click" />
                                        </td>
                                        
                                    </tr>
                                    <tr >
                                        <td colspan="8">
                                            <asp:Label ID="LblResult" runat="server" Text="" Style="color: Red"> </asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div class="rounded_corners" style="overflow:scroll">
                                <asp:GridView ID="GVListEmployees" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="None" >
                                                                    <RowStyle BackColor="#EFF3FB" Height="30"/>
                                    <Columns>
                                    <asp:TemplateField>
                                    <HeaderTemplate>
                                    <asp:CheckBox ID="chkAll" runat="server"  onclick = "checkAll(this);"/>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                    <asp:CheckBox ID="chkindividual" runat="server" onclick = "Check_Click(this)"  />
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                     <asp:TemplateField HeaderText="Client Id">
                                    <ItemTemplate>
                                    <asp:Label ID="lblclientid" runat="server" Text='<%#Eval("clientid") %>'  />
                                    </ItemTemplate>
                                    </asp:TemplateField>
                                        <asp:BoundField DataField="clientname" HeaderText="Name" />
                                        <asp:BoundField DataField="TotalGross" HeaderText="Gross" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="TotalOTamt" HeaderText="OT AMount" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="TotalDeductions" HeaderText="Total Deductions" DataFormatString="&nbsp; {0}" />
                                         <asp:BoundField DataField="TotalNetAmount" HeaderText="NET AMount" DataFormatString="{0:0.00}" />
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </div>
                            
                            
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="100%"
                                    CellSpacing="3" CellPadding="5" ForeColor="#333333" GridLines="None" onrowdatabound="GridView1_RowDataBound" Visible="false" >
                                                                    <RowStyle BackColor="#EFF3FB" Height="30"/>
                                    <Columns>
                                   <asp:BoundField DataField="clientid" HeaderText="Client Id" />
                                        <asp:BoundField DataField="clientname" HeaderText="Name" />
                                        <asp:BoundField DataField="empid" HeaderText="Emp Id" />
                                        <asp:BoundField DataField="empmname" HeaderText="Name" />
                                        <asp:BoundField DataField="design" HeaderText="Name" />
                                        <asp:TemplateField HeaderText="Month/Year"></asp:TemplateField>
                                        <asp:BoundField DataField="NoOfDuties" HeaderText="No Of Duties" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="ot" HeaderText="No Of OT's" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="basic" HeaderText="Basic" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="da" HeaderText="DA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="HRA" HeaderText="HRA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="CCA" HeaderText="CCA" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="Conveyance" HeaderText="Conv." DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="WA" HeaderText="W.A" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="OA" HeaderText="O.A" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="LeaveEncashAmt" HeaderText="NHAmt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="LeaveEncashAmt" HeaderText="L.A.Amt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="OTAmt" HeaderText="OTAmt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="clplamt" HeaderText="CLPLAmt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="woamt" HeaderText="W.O.Amt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="totalgross" HeaderText="Gross" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="pf" HeaderText="PF" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="esi" HeaderText="ESI" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="Proftax" HeaderText="PT Amt" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="saladvded" HeaderText="SALADVDED" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="UniformDed" HeaderText="UniformDed" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="woamt" HeaderText="Reg Ded" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="woamt" HeaderText="Ins Ded" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="OtherDed" HeaderText="OtherDed" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="CanteenAdv" HeaderText="CanteenAdv" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="penalty" HeaderText="Penalty" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="PFESIContribution" HeaderText="PF & ESI Contribution" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="TotalDeductions" HeaderText="Total Ded" DataFormatString="{0:0.00}" />
                                        <asp:BoundField DataField="actualamount" HeaderText="Net AMount" DataFormatString="{0:0.00}" />
                                      
                                       <asp:TemplateField HeaderText="Bank A/C No" >
                                        <ItemTemplate>
                                        <asp:Label ID="lblbankno" runat="server" Text='<%# Eval("Empbankacno") %>' ></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                      
                                     
                                        
                                       <asp:BoundField DataField="EmpBankCardRef" HeaderText="Reference No." DataFormatString="{0}&nbsp;" />
                                    </Columns>
                                    <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                    <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                    <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                    <EditRowStyle BackColor="#2461BF" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            
                            <%--     <div>
                            
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
                            </div>--%>
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
