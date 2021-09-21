<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddNewItem.aspx.cs" Inherits="Jawan.Portal.AddNewItem" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>INVENTORY: ADD ITEM</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <link href="css/boostrap/css/bootstrap.css" rel="stylesheet" />



</head>
<body>
    <form id="AddNewItem1" runat="server">
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
                        <%--<li><a href="Reminders.aspx">Reminders</a></li>--%>
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
                        <li><a href="ViewItems.aspx" id="InventoryLink" runat="server" class="current"><span>Inventory</span></a></li>
                        <li class="after"><a href="ActiveEmployeeReports.aspx" id="ReportsLink" runat="server">
                            <span>Reports</span></a></li>
                        <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
                        <li class="last"><a href="login.aspx" id="LogOutLink" runat="server"><span><span>logout</span></span></a></li>
                    </ul>
                </div>
                <!-- MAIN MENU SECTION END -->
            </div>
            <!-- LOGO AND MAIN MENU SECTION END -->
            <!-- SUB NAVIGATION SECTION BEGIN -->
            <div id="submenu">
                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                    <tbody>
                        <tr>
                            <td>
                                <div style="display: inline;">
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <!-- HEADER SECTION END -->
        <!-- CONTENT AREA BEGIN -->
        <div id="content-holder">
            <div class="content-holder">
                <div id="breadcrumb">
                <ul class="crumbs">
                                       <li class="first"><a href="ViewItems.aspx" style="z-index: 9;"><span></span>Inventory</a></li>

                    <li class="active"><a href="AddNewItem.aspx" style="z-index: 7;" class="active_bread">
                       Add New Item </a></li>
                </ul>
            </div>
                 <asp:ScriptManager runat="server" ID="Scriptmanager1">
                        </asp:ScriptManager>
                <div class="dashboard_full">
                    <div style="float: right; font-weight: bold">
                        <%-- Select Import Data: --%>
                        <asp:FileUpload ID="fileupload1" runat="server" Width="50px" Visible="false" />
                        <asp:Button ID="btnImport" runat="server" Text="Import" class=" btn save" Visible="false"
                            OnClick="btnImport_Click" />

                    </div>
                    <!-- DASHBOARD CONTENT BEGIN -->
                    <div class="contentarea" id="contentarea">

                        <div class="sidebox">
                            <div class="boxhead">

                                <h2 style="text-align: center">Add New Item     
                                </h2>
                            </div>
                            <div class="contentarea" id="Div1">
                                <div class="boxinc">

                                    <ul>
                                        <%-- <li class="left leftmenu">
                                            <ul>


                                                <li><a href="InvPODetails.aspx" >PO Details</a></li>
                                                <li><a href="InvInflowDetails.aspx" >Inflow </a></li>
                                             <li><a href="InvVendorMaster.aspx">Vendor Details</a></li>
                                                <li><a href="InvClientMaster.aspx">Client Rate Details</a></li>
                                           <li><a href="AddnewItem.aspx" class="sel">Add New Item</a></li>


                                        

                                            </ul>
                                        </li>--%>
                                        <li class="right">

                                            <table width="130%" cellpadding="5" cellspacing="5" style="margin-left: 10px">
                                                <tr>
                                                    <td>
                                                        <table width="100%" cellpadding="5" cellspacing="5" style="margin: 10px">
                                                            <tr style="height: 32px">
                                                                <td>Item ID
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtitemid" runat="server" class="form-control" ReadOnly="true" Width="190px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>Units of Measure<span style="color: Red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlmesure" runat="server" class="form-control" Width="190px">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>Brand
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmy" runat="server" class="form-control" Width="190px"> </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>Buying Price
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtprice" runat="server" class="form-control" Width="190px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" Enabled="True"
                                                                        TargetControlID="txtprice" ValidChars="0123456789.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>HSN Number
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtHSNNumber" runat="server" class="form-control" Width="190px"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>VAT
                                                                </td>
                                                                <td>
                                                                    <asp:CheckBox runat="server" Checked="false" ID="ChkVATCmp1" Text="" Visible="false" />
                                                                    &nbsp;&nbsp;
                                                                    <asp:CheckBox runat="server" Checked="false" ID="ChkVATCmp2" Text="" Visible="false" />
                                                                     &nbsp;&nbsp;
                                                                    <asp:CheckBox runat="server" Checked="false" ID="ChkVATCmp3" Text="" Visible="false" />
                                                                     &nbsp;&nbsp;
                                                                    <asp:CheckBox runat="server" Checked="false" ID="ChkVATCmp4" Text="" Visible="false" />
                                                                     &nbsp;&nbsp;
                                                                    <asp:CheckBox runat="server" Checked="false" ID="ChkVATCmp5" Text="" Visible="false" />
                                                                </td>

                                                            </tr>
                                                        </table>
                                                    </td>

                                                    <td valign="top">
                                                        <table width="100%" cellpadding="5" cellspacing="5" style="margin: 10px">

                                                            <tr style="height: 32px">
                                                                <td>Item Name<span style="color: Red">*</span>
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtitemname" runat="server" class="form-control" Width="190px"> </asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>Minimum Quantity
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtmq" runat="server" class="form-control" Width="190px"> </asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                        TargetControlID="txtmq" ValidChars="0123456789">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>Category
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlCategory" runat="server" class="form-control" Width="190px">
                                                                        <asp:ListItem value="0">--Select--</asp:ListItem>
                                                                        <asp:ListItem value="General">General</asp:ListItem>
                                                                        <asp:ListItem value="Uniform">Uniform</asp:ListItem>
                                                                        <asp:ListItem value="Inventory">Inventory</asp:ListItem>
                                                                        <asp:ListItem value="Misc">Misc</asp:ListItem>
                                                                        <asp:ListItem value="Labels">Labels</asp:ListItem>
                                                                        <asp:ListItem value="Other">Other</asp:ListItem>
                                                                        </asp:DropDownList>

                                                                </td>
                                                            </tr>
                                                            <tr style="height: 32px">
                                                                <td>Selling price
                                                                </td>
                                                                <td>
                                                                    <asp:TextBox ID="txtsellingprice" runat="server" class="form-control" Width="190px"></asp:TextBox>
                                                                    <cc1:FilteredTextBoxExtender ID="FTBEsellingprice" runat="server" Enabled="True"
                                                                        TargetControlID="txtsellingprice" ValidChars="0123456789.">
                                                                    </cc1:FilteredTextBoxExtender>
                                                                </td>
                                                            </tr>

                                                            <tr style="height: 32px">
                                                                <td>GST %
                                                                </td>
                                                                <td>
                                                                    <asp:DropDownList ID="ddlGSTPer" runat="server" class="form-control" Width="190px">
                                                                        <asp:ListItem>0</asp:ListItem>
                                                                        <asp:ListItem>5</asp:ListItem>
                                                                        <asp:ListItem>12</asp:ListItem>
                                                                        <asp:ListItem>18</asp:ListItem>
                                                                        <asp:ListItem>28</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>



                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div align="right" style="margin-right: 15px">
                                                <asp:Label ID="lblresult" runat="server" Text="" Visible="false" Style="color: Red"></asp:Label>
                                                <asp:Button ID="Button1" runat="server" ValidationGroup="a1" Text="Save" OnClientClick='return confirm("Are you sure you want to add this Item?");'
                                                    ToolTip="SAVE" class=" btn save" OnClick="BtnSave_Click" />
                                                <asp:Button ID="btncancel" runat="server" ValidationGroup="a1" Text="Cancel" ToolTip="CANCEL"
                                                    class=" btn save" OnClientClick='return confirm("Are you sure you want to cancel this entry?");' />
                                            </div>

                                        </li>
                                    </ul>


                                </div>
                                <div class="clear">
                                </div>
                            </div>
                        </div>
                        <%--   </div>--%>
                    </div>
                    <div class="clear">
                    </div>
                    <!-- DASHBOARD CONTENT END -->
                </div>
            </div>
           
            <!-- CONTENT AREA END -->
            <!-- FOOTER BEGIN -->
            <div id="footerouter">
                <div class="footer">
                    <div class="footerlogo">
                        <a href="http://www.diyostech.Com" target="_blank">Powered by DIYOS </a>
                    </div>
                    <!--    <div class="footerlogo">&nbsp;</div> -->
                    <div class="footercontent">
                        <a href="#">Terms &amp; Conditions</a> | <a href="#">Privacy Policy</a> | &copy;
                <asp:Label ID="lblcname" runat="server"></asp:Label>.
                    </div>
                    <div class="clear">
                    </div>
                </div>
            </div>
            <!-- FOOTER END -->
             </div>
    </form>
</body>
</html>
