<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpIDCard.aspx.cs" Inherits="Jawan.Portal.EmpIDCard" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>REPORT: EMPLOYEE ID CARD</title>
    <link rel="shortcut icon" href="assets/Mushroom.ico" />
    <link href="css/chosen.css" rel="stylesheet" />
    <link href="css/global.css" rel="stylesheet" type="text/css" />

     <script src="script/jquery.min.js" type="text/javascript"></script>

    <script type="text/javascript" src="script/jscript.js"></script>

    
   
    <style type="text/css">
        .style1 {
            width: 135px;
        }

        .drpdwn {
            width: 350px;
            padding: 10px;
        }

        .completionList {
            background: white;
            border: 1px solid #DDD;
            border-radius: 3px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            min-width: 165px;
            height: 120px;
            overflow: auto;
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

     <script src="script/chosen.jquery.js" type="text/javascript"></script>
   <script type="text/javascript">
       jQuery(document).ready(function mchoose() {
           jQuery(".chosen").data("placeholder", "Select Frameworks...").chosen();
       });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <!-- HEADER SECTION BEGIN -->
        <div id="headerouter">
            <!-- LOGO AND MAIN MENU SECTION BEGIN -->
            <div id="header">
                <!-- LOGO BEGIN -->
                <div id="logo">
                    <a href="default.aspx">
                        <img border="1" src="assets/logo.png" alt="FACILITY MANAGEMENT SOFTWARE" title="FACILITY MANAGEMENT SOFTWARE" /></a>
                </div>
                <!-- LOGO END -->
                <!-- TOP INFO BEGIN -->
                <div id="toplinks">
                    <ul>
                        <li><a href="Reminders.aspx">Reminders</a></li>
                        <li>Welcome <b>
                            <asp:Label ID="lblDisplayUser" runat="server" Text="Label" Font-Bold="true"></asp:Label></b></li>
                        <li class="lang"><a href="Login.aspx">Logout</a></li>
                    </ul>
                </div>
                <!-- TOP INFO END -->
                <!-- MAIN MENU BEGIN -->
                <div id="mainmenu">
                    <ul>
                        <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>Employees</span></a>
                        </li>
                        <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a>
                        </li>
                        <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server"><span>Company Info</span></a>
                        </li>
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span>Inventory</span></a>
                        </li>
                        <li><a href="Reports.aspx" id="ReportsLink" runat="server" class="current"><span>Reports</span></a></li>
                        <li class="after"><a href="CreateLogin.aspx" id="SettingsLink" runat="server"><span>Settings</span></a> </li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>Logout</span></span></a>
                        </li>
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
                                            &nbsp;
                                        </div>
                                        <div class="submenuactions">
                                            &nbsp;
                                        </div>
                                        <ul>
                                            <li class="current"><a href="ActiveEmployeeReports.aspx" id="ActiveEmployeesLink"
                                                runat="server"><span>Employees</span></a></li>
                                            <li><a href="ActiveClientReports.aspx" id="ClientsReportsLink" runat="server"><span>Clients</span></a> </li>
                                            <li><a href="ListOfItemsReports.aspx" id="InventoryReportsLink" runat="server"><span>Inventory</span></a> </li>
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
                        <li class="active"><a href="EmpIDCard.aspx" style="z-index: 7;" class="active_bread">EMPLOYEE ID CARD</a></li>
                    </ul>
                </div>
                <!-- DASHBOARD CONTENT BEGIN -->
                <div class="contentarea" id="contentarea">
                    <div class="dashboard_center">
                        <div class="sidebox" style="background:none">
                            <div class="boxhead">
                                <h2 style="text-align: center">ID CARD
                                </h2>
                            </div>
                            <div  style="padding: 5px 5px 5px 5px;">
                                <div >
                                    <asp:ScriptManager runat="server" ID="ScriptEmployReports">
                                    </asp:ScriptManager>

                                     <div align="center"> <asp:Label ID="lblMsg" runat="server" style="border-color: #f0c36d;background-color: #f9edbe;width:auto;font-weight:bold;color:#CC3300;"></asp:Label></div> 
                    <div align="center"> <asp:Label ID="lblSuc" runat="server" style="border-color: #f0c36d;background-color: #f9edbe;width:auto;font-weight:bold;color:#000;"></asp:Label></div> 
              
                                     <table style="margin-top:8px;margin-bottom:8px" width="80%">
                                        
                        <tr>
                            <td style="font-weight: bold;width:120px">
                                Employee ID / Name:
                            </td>
                            <td width="180px">
                              
                                <asp:ListBox ID="lstEmpIdName" runat="server"  SelectionMode="Multiple" class="chosen"
                                ToolTip="Enter Searched Employee ID Or Name" Width="180px"></asp:ListBox>
                            </td>
                           
                            <td>
                         <asp:Button ID="BtnIDCard" runat="server" Text="Download ID Card" OnClick="BtnIDCard_Click" />

                            </td>
                             <td>
                         <asp:Button ID="Button2" runat="server" Text="ID Card" OnClick="BtnIDNewCard_Click" />

                            </td>
                            <td>
                         <asp:Button ID="Button1" runat="server" Text="Download ID Card old" OnClick="Button1_Click" />

                            </td>
                            <td>
                                <asp:Button ID="btnIDCardNew" runat="server" Text="Download ID Card New" OnClick="btnIDCardNew_Click" />
                            </td>
                        </tr>
                    </table>

                                                           
            <div style="height:auto">
                           
                                    <table cellspacing="5" cellpadding="5" border="0" style="height: 50px" width="100%">
                                        <tr>
                                            <td width="20%">
                                               <%-- Operational Manager<span style=" color:Red">*</span>--%>
                                            </td>
                                            <td width="30%">
                                              <%--  <asp:DropDownList ID="ddloperationalmanager" runat="server" Width="160px" 
                                                    AutoPostBack="True" 
                                                    onselectedindexchanged="ddloperationalmanager_SelectedIndexChanged">
                                                    <asp:ListItem>--Select--</asp:ListItem>
                                                </asp:DropDownList>--%>
                                            </td>
                                            
                                        </tr>
                                    </table>  
                                    </div>
                             
                               
                               <%-- <div class="dashboard_FirstOfThree">--%>
                                <div class="rounded_corners">
                                    <asp:GridView ID="GvSearchEmp" runat="server" AllowPaging="True" AutoGenerateColumns="False" width="98%"
                                            CellPadding="5" CellSpacing="3" ForeColor="#333333" EmptyDataText="No Records Found" 
                                            GridLines="None" BorderStyle="Outset" PageSize="100" OnPageIndexChanging="GvSearchEmp_PageIndexChanging">
                                            <RowStyle BackColor="#EFF3FB" Height="30px" HorizontalAlign="Left" 
                                                VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" 
                                                Height="20px" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                            <asp:TemplateField>
                                                                <HeaderTemplate>
                                                                    <asp:CheckBox ID="chkAll" runat="server" onclick="checkAll(this);" />
                                                                </HeaderTemplate>
                                                                <ItemTemplate>
                                                                    <asp:CheckBox ID="chkindividual" runat="server"   onclick = "Check_Click(this)" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>

                                                          <%--  <asp:TemplateField HeaderText="Emp ID">
                                                                <ItemTemplate>
                                                                   <%-- <asp:Label ID="lblempid" runat="server" Text='<%#Eval("empid") %>' />
                                                               

                                                                     </ItemTemplate>
                                                            </asp:TemplateField>--%>
                                                  <asp:BoundField DataField="empid" HeaderText="ID NO"  />
                                                            <asp:BoundField DataField="FullName" HeaderText="Name" />
                                                            <asp:BoundField DataField="Designation" HeaderText="Designation" />

                                                            <asp:BoundField DataField="EmpDtofJoining" HeaderText="Date Of Joining"  />


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
                            </div>
                        </div>
                    </div>
        
                    <div class="clear">
                    </div>
               
            <!-- DASHBOARD CONTENT END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS©
                         <asp:Label ID="lblcname" runat="server" meta:resourcekey="lblcnameResource1"></asp:Label>
                    </div>
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

