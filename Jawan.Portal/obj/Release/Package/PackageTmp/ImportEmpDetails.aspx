<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ImportEmpDetails.aspx.cs" Inherits="Jawan.Portal.ImportEmpDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: EMPLOYEES REPORT</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/Load.css" rel="stylesheet" type="text/css" />
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
    
    <script language="javascript">
        function OnFocus(txt, text)
         {
            if (txt.value == text) {
                txt.value = "";
            }
        }
        
        
        function OnBlur(txt, text) {
            if (txt.value == "") {
                txt.value = text;
            }
        }
    </script>

     <style type="text/css">
        .style1 {
            width: 135px;
        }

         .completionList {
        

        background: white;
	    border: 1px solid #DDD;
	    border-radius: 3px;
	    box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
	    min-width: 165px;

        height: 120px;
        overflow:auto;
             
        } 
        .listItem {
        display: block;
	    padding: 5px 5px;
	    border-bottom: 1px solid #DDD;	
        } 
        .itemHighlighted {
        color: black;
	    background-color: rgba(0, 0, 0, 0.1);
	    text-decoration: none;
        box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
        border-bottom: 1px solid #DDD;	
        display: block;
	    padding: 5px 5px;
        }
    </style>
    

</head>
<body>
    <form id="ActiveEmployeeReports1" runat="server">
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
                    <li class="active"><a href="ActiveEmployeeReports.aspx" style="z-index: 7;" class="active_bread">
                        List of Employees</a></li>
                </ul>
            </div>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                         <div>
                            <h4 style="text-align: right">
                               <asp:LinkButton ID="lnkImportfromexcel" Text="Export Sample Excel" runat="server" 
                                    onclick="lnkImportfromexcel_Click"></asp:LinkButton> </h4>
                        </div>
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                LIST OF EMPLOYEES
                            </h2>
                        </div>
                        <div class="boxbody" style="padding: 5px 5px 5px 5px;">
                            <div class="boxin">
                                <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                </asp:ScriptManager>
                                <%--<div align="right">
                                <asp:LinkButton ID="lbtn_Export" runat="server" OnClick="lbtn_Export_Click" >Export to Excel</asp:LinkButton>
                            </div>--%>
                               
                                <div class="dashboard_firsthalf" style="width: 700px;">
                                    <br />
                                  
                                                
           
                                </div>
                                <table>
                                    <tr>
                                        <td>

                                            <asp:Label ID="lblfileupload" runat="server" Text="File Upload"></asp:Label>         

                                        </td>
                                        <td>
                                            <asp:FileUpload ID="FileUploadEmpDetails" runat="server" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnsave" runat="server" Text="Save" OnClick="btnsave_Click" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnmodify" runat="server" Text="Modify" OnClick="btnmodify_Click" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                                <div>
                                    <div class="rounded_corners" >
                                         <div style="overflow: scroll; width: auto">
                                        <asp:GridView ID="gvlistofemp" runat="server" AutoGenerateColumns="False" Width="100%" Visible="false"
                                            ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center" Height="50px">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <Columns>
                                               
                                                <asp:TemplateField HeaderText="IDNO" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblIDNO" Text='<%#Bind("IDNO") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Employee Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEmployeeName" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Fathers Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblFathersName" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               
                                                <asp:TemplateField HeaderText="Mother Name" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblMothersName" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date of Birth">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDateofBirth" Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Sex">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSex"   Text=""
                                                         ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Marital Status">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblMaritalStatus" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Designation">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDesignation"  Text=""></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Mobile No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblMobileNo"  Text=""></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UAN Number">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblUANNumber"  Text=""></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Aadhar Number">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblAadharNumber"  Text=""></asp:Label>
                                                    </ItemTemplate>
                                                      </asp:TemplateField>
                                                
                                                  <asp:TemplateField HeaderText="PAN Number">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPANNumber"  Text=""></asp:Label>
                                                    </ItemTemplate>
                                                      </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Present Address">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPresentAddress"  Text=""></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <asp:TemplateField HeaderText="Permanent Address">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPermanentAddress"  Text=""></asp:Label>
                                                         
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Client ID">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblclientid" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Department">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDepartment" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Branch">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBranch"  Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Division">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDivision" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="Bank Account No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBankAccountNo"  Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Bank Name">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblBankname" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Sal Structure">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSalStructure"   Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="Date of Joining">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDateofJoining" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               <%-- <asp:TemplateField HeaderText="Salary calculate from">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSalarycalculatefrom" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                 <asp:TemplateField HeaderText="Date of leaving" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblDateofleaving"   Text=""></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <%-- <asp:TemplateField HeaderText="Reason for leaving" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblReasonforleaving" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>

                                                 <asp:TemplateField HeaderText="ESI Applicable" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblESIApplicable"  Text=""></asp:Label>
                                                    </ItemTemplate>
                                                      </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="ESI No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblESINo"  Text="" ></asp:Label>                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                
                                                      <asp:TemplateField HeaderText="PF Applicable" ItemStyle-HorizontalAlign="Left">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPFApplicable" Text="" ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                               

                                                <asp:TemplateField HeaderText="PF No">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblPFNo"  Text="" ></asp:Label>
                                                        
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                          
                                                                                           
                                               
                                               
                                            </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>

                                     <div class="rounded_corners" style="overflow:auto">
                                        <asp:GridView ID="GvNonInsertEmployees" runat="server" AutoGenerateColumns="False" Width="137%" Visible="false"
                                            ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center" Height="238px">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <Columns>
                                                 <asp:TemplateField HeaderText="EmpId">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblEmpId"  ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Remarks">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblRemarks"  ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                 </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>



                                     <div class="rounded_corners" style="overflow:auto">
                                        <asp:GridView ID="GvListOfInstructions" runat="server" AutoGenerateColumns="False" Width="137%" Visible="false"
                                            ForeColor="#333333" GridLines="None" CellPadding="4" CellSpacing="3" Style="text-align: center" Height="238px">
                                            <RowStyle BackColor="#EFF3FB" Height="30" />
                                            <Columns>
                                                 <asp:TemplateField HeaderText="SNO">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblSNO" Text='<%#Bind("Sno") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="Instructions">
                                                    <ItemTemplate>
                                                        <asp:Label runat="server" ID="lblInstructions" Text='<%#Bind("Instructions") %>' ></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>


                                                 </Columns>
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"  Height="30" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                        </asp:GridView>
                                    </div>
<%--                                    <asp:Label ID="LblResult" runat="server" Text="" Style="color: red"></asp:Label>--%>
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
