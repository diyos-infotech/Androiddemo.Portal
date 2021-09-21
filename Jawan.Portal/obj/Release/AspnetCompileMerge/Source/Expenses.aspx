<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Expenses.aspx.cs" Inherits="Jawan.Portal.Expenses" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<script type="text/javascript" language="javascript">
      
        function CheckAllEmp(Checkbox) {
            var GridVwHeaderChckbox = document.getElementById("<%=Gv_salaryDetails_SiteWise.ClientID %>");
            for (i = 1; i < GridVwHeaderChckbox.rows.length; i++) {
                GridVwHeaderChckbox.rows[i].cells[3].getElementsByTagName("INPUT")[0].checked = Checkbox.checked;
            }
        }

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

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>EXPENSES</title>
    <link href="css/global.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .tdsize
        {
            height: 15px;
        }
        .style8
        {
            width: 335px;
            height: 29px;
        }
    </style>
    
        <style type="text/css">
        .modalBackground
        {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }
        .modalPopup
        {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 960px;
            height: auto;
        }
    </style>
 
</head>

<body>
<form id="Expenses1" runat="server">
    <!-- HEADER SECTION BEGIN -->
    <div id="headerouter">
        <!-- LOGO AND MAIN MENU SECTION BEGIN -->
        <div id="header">
            <!-- LOGO BEGIN -->
            <div id="logo">
                <a href="default.aspx">
                    <img border="0" src="assets/logo.png" alt="Facility Managment Software" title="Facility Managment Software"></a></div>
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
                    <li class="first"><a href="Employees.aspx" id="EmployeesLink" runat="server"><span>
                        Employees</span></a></li>
                    <li><a href="clients.aspx" id="ClientsLink" runat="server"><span>Clients</span></a></li>
                    <li><a href="companyinfo.aspx" id="CompanyInfoLink" runat="server" class="current"><span>Company Info</span></a></li>
                    <li class="after"><a href="ViewItems.aspx" id="InventoryLink" runat="server"><span> Inventory</span></a></li>
                    <li><a href="Reports.aspx" id="ReportsLink" runat="server"><span>Reports</span></a></li>
                    <li><a href="Settings.aspx" id="SettingsLink" runat="server"><span>Settings</span></a></li>
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
                            <div style="display: inline;">
                                <div id="submenu" class="submenu">
                               <div class="submenubeforegap">
                                        &nbsp;</div>
                                    <div class="submenuactions">
                                        &nbsp;</div>
                               <ul>
                                        <li ><a href="companyinfo.aspx" id="AddCompanyInfoLink" runat="server"> <span>Add/Modify</span></a></li>
                                        <li><a href="ModifyCompanyInfo.aspx" id="ModifyCompanyInfoLink" visible="false" runat="server"><span>Modify</span></a></li>
                                        <li><a href="DeleteCompanyInfo.aspx" id="DeleteCompanyInfoLink" visible="false" runat="server"><span> Delete</span></a></li>
                                         <li class="current"><a href="Expenses.aspx" id="ExpensesLink"  runat="server"><span>Expenses</span></a></li>
                                          
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
            <h1 class="dashboard_heading">
                Expenses Dashboard</h1>
            <!-- DASHBOARD CONTENT BEGIN -->
            <div class="contentarea" id="contentarea">
                <div class="dashboard_center">
                    <div class="sidebox">
                        <div class="boxhead">
                            <h2 style="text-align: center">
                                Expenses INFORMATION
                            </h2>
                        </div>
                           <asp:ScriptManager runat="server" ID="Scriptmanager1">
                            </asp:ScriptManager>
                        
                        
                        <asp:Button ID="btnShow" runat="server" Text="Show Modal Popup"  style="display:none" />

<!-- ModalPopupExtender -->
<cc1:ModalPopupExtender ID="ccl" runat="server" PopupControlID="Panel1" TargetControlID="btnShow"
    CancelControlID="btnClose" BackgroundCssClass="modalBackground">
</cc1:ModalPopupExtender>
<asp:Panel ID="Panel1" runat="server" CssClass="modalPopup" align="center" style = "display:none">
               
                           
                             <asp:GridView ID="Gv_View_More_Details_Salary_DEtails" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="BlueViolet"
                                        EmptyDataRowStyle-BorderColor="Aquamarine" Width="100%" CellPadding="4" CellSpacing="3" ForeColor="#333333"  GridLines="None">
                                       <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        
                                        <asp:TemplateField HeaderText="S.No" >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Sno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Client Id"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Client_id" runat="server" Text="<%#Bind('clientid')%>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Client Name"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Client_Name" runat="server" Text="<%#Bind('clientname') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Empid"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Emp_id" runat="server" Text="<%#Bind('empid') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Emp Name"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Emp_Name" runat="server" Text="<%#Bind('empname') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Amount"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Amount" runat="server" Text="<%#Bind('ActualAmount') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                  
                                       <asp:TemplateField HeaderText="For The Month Of"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_For_The_Month_Of" runat="server" Text="<%#Bind('month') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        </Columns>
                                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                          
                           
    <asp:Button ID="btnClose" runat="server" Text="Close" />
</asp:Panel>
<!-- ModalPopupExtender -->


                        <div class="boxbody" style="padding: 5px 5px 5px 5px; ">
                        
                        
                         <div class="boxin" style="height:700px">
                                   <div class=" dashboard_full">
                            
                            <asp:Panel ID="Pnl_Search" runat="server">
                           
                           <table width="100%" cellpadding="4" cellspacing="4">
                           <tr>
                           <td>
                           <table width="80%" cellpadding="4" cellspacing="4">
                           <tr>
                           <td>Search Mode</td>
                            <td><asp:DropDownList ID="Ddl_Search_Mode" runat="server"   class="sdrop">
                              <asp:ListItem>Voucher No</asp:ListItem>
                               <asp:ListItem>Purpose</asp:ListItem>
                            </asp:DropDownList></td>
                             <td><asp:TextBox  ID="Txt_Search_Mode"  runat="server"  class="sinput"></asp:TextBox> </td>
                              <td><asp:Button  ID="Btn_Search_Voucher_purpose" runat="server" Text="Search" class="btn save"
                            OnClick="Btn_Search_Voucher_purpose_Click"/></td>
                               <td></td>
                           </tr>
                           </table>
                           </td>
                           <td align="right">
                                <asp:Button ID="Btn_Add_Voucher" runat="server" Text="SAVE" ToolTip="Add voucher" class=" btn save"
                                    ValidationGroup="a1" onclick="Btn_Add_Voucher_Click"  
                                    onclientclick='return confirm(" Are you  sure you  want to add this  record ?");' />
                           &nbsp;&nbsp;
                                     <asp:Button ID="Btn_Modify_Voucher" runat="server" Text="NEW" ToolTip="Modify Voucher" class=" btn save"
                                    ValidationGroup="a1" onclick="Btn_New_Voucher_Click"  
                                    onclientclick='return confirm(" Are you  sure you  want to add this  record ?");' />
                           </td>
                          
                          </tr>
                          </table>
                            
                           
                           <div class="rounded_corners">
                         
                           <asp:GridView ID="Gv_Last5_Transactions" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="BlueViolet"
                                        EmptyDataRowStyle-BorderColor="Aquamarine" Width="100%" CellPadding="4" CellSpacing="3" ForeColor="#333333"  GridLines="None">
                                       <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        
                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Sno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Voucher No" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_VoucherNo" runat="server" Text="<%#Bind('voucherno')%>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Paid To" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Paid_To" runat="server" Text="<%#Bind('paidto') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Date" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Paid_Date" runat="server" Text="<%#Bind('date') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Purpose"   HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Purpose" runat="server" Text="<%#Bind('PurposeName') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Amount"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Amount" runat="server" Text="<%#Bind('Amount') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Payment Mode"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Payment_Mode" runat="server" Text="<%#Bind('PaymentModeName') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Cheque/DD/<br/>Transaction Id"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_DD_Cheque_transaction_No" runat="server" Text="<%#Bind('DDorchequeortransactionNo') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                           
                                          <asp:TemplateField HeaderText="Cheque/DD/<br/>Transaction Date"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_DD_Cheque_transaction_Date" runat="server" Text="<%#Bind('DDorchequeortransactionDate') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                           <asp:TemplateField HeaderText="Operations"  HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                        <asp:LinkButton ID="Lbl_View_More" runat="server" Text="View More" OnClick="Lbl_View_More_OnClick"
                                        ></asp:LinkButton>
                                        
                                         <asp:LinkButton ID="Link_Modify" runat="server" Text="Modify"  OnClick="Link_Modify_OnClick"  style="padding-left:10px"></asp:LinkButton>
                                            
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        </Columns>
                                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                           
                           </div>
                          </asp:Panel>
                            
                            <br />
                            <asp:Panel ID="Pnl_Voucher_Design" runat="server" > 
                                <table width="100%" cellspacing="5" cellpadding="5" border="0">
                                    <tr>
                                        <td>
                                           Vocher No.
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtvocherno" runat="server" class="sinput" ReadOnly="true"></asp:TextBox>
                                            <asp:DropDownList ID="Ddl_Voucher_No" runat="server"  class="sdrop"
                                            OnSelectedIndexChanged="Ddl_Voucher_No_OnSelectedIndexChanged"></asp:DropDownList> 
                                        </td>
                                        
                                         <td>
                                           Paid To
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtpaidto" runat="server" class="sinput" ></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Date
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdate" runat="server" class="sinput" 
                                             MaxLength="10" onkeyup="dtval(this,event)" ></asp:TextBox>
                                             <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="true" TargetControlID="txtdate"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                             <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1"
                                                                 runat="server" Enabled="True" TargetControlID="txtdate"
                                                                  ValidChars="0123456789/">
                                                                  </cc1:FilteredTextBoxExtender>
                                                                  
                                                                 
                                                                  
                                        
                                        </td>
                                          <td>
                                           Purpose 
                                        </td>
                                        <td>
                                    
                                     <asp:DropDownList ID="Ddl_Purpose" runat="server" class="sdrop" AutoPostBack="true"
                                      OnSelectedIndexChanged="Ddl_Purpose_OnSelectedIndexChanged"></asp:DropDownList>
                                             
                                        </td>
                                    </tr>
                                    <tr>
                                     
                                       <td>
                                            Payment Mode <span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="Ddl_Payment_Mode" runat="server"  class="sdrop"  ></asp:DropDownList>
                                        
                                           </td>
                                
                                        <td>
                                         DD/Cheque/Transaction Date<span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="Txt_DD_CheQue_Transaction_Date" runat="server" class="ssinput" MaxLength="10" onkeyup="dtval(this,event)"></asp:TextBox>
                                         <cc1:CalendarExtender ID="CE_DD_CheQue_Transaction_Date" runat="server" Enabled="true" 
                                         TargetControlID="Txt_DD_CheQue_Transaction_Date"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                        <cc1:FilteredTextBoxExtender ID="FTBE_DD_CheQue_Transaction_Date"
                                                                 runat="server" Enabled="True" TargetControlID="Txt_DD_CheQue_Transaction_Date"
                                                                  ValidChars="0123456789/">
                                                                  </cc1:FilteredTextBoxExtender>
                                           </td>
                                    
                                    </tr>
                                    
                                    
                                    <tr>
                                      <td>DD/Cheque/Transaction No.</td>
                                    <td>  <asp:TextBox ID="Txt_Dd_CheQue_N0"  runat="server" class="sinput"></asp:TextBox> </td>
                                     
                                     <td>&nbsp;</td>
                                     
                                     <td>&nbsp;</td>
                                     </tr>
                                    
                                    
                                    
                                          <tr>
                                    
                                     <td>
                                           <asp:Label id="Lbl_Bank_Name_Dynamic" runat="server" Text="From Bank"></asp:Label> 
                                           <span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="Ddl_From_Bank" runat="server"  class="sdrop"  ToolTip="From Bank" ></asp:DropDownList>
                                        
                                           </td>
                                       
                                        <td>
                                            Approved By<span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="Ddl_Approved_By_Empids" runat="server"  class="sdrop" >
                                        </asp:DropDownList>
                                        </td>
                                       
                                    
                                    </tr>
                                    
                                    <tr>
                                    <td>
                                    
                                    <asp:Label ID="Lbl_Modified_Bank_Name" runat="server" Text="Modified Bank Name"></asp:Label>
                                    </td>
                                    <td> 
                                    <asp:DropDownList ID="Ddl_Modified_Bank_Name" runat="server" class="sdrop"></asp:DropDownList>
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    </tr>
                                    
                                    
                                    
                                     <tr>
                                     
                                        
                                        <td>
                                            Amount<span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="txtamount" runat="server" class="sinput" 
                                                ontextchanged="txtamount_TextChanged" AutoPostBack="true" ></asp:TextBox>
                                            &nbsp;
                                            <asp:RequiredFieldValidator ID="rfvaddress" runat="server" ControlToValidate="txtamount"
                                                Text="*" SetFocusOnError="true" EnableClientScript="true" ErrorMessage="Please Enter The Address"
                                                ValidationGroup="a1"></asp:RequiredFieldValidator>
                                            <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2"
                                                                 runat="server" Enabled="True" TargetControlID="txtamount"
                                                                  ValidChars="0123456789">
                                                                  </cc1:FilteredTextBoxExtender>
                                        </td>
                                     
                                     <td>
                                         Remarks<span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="Txt_Remarks" runat="server" TextMode="MultiLine" class="ssinput"></asp:TextBox>
                                                 </td>
                                    </tr>
                                    
                                    <tr>
                                   <td>
                                   <asp:Label ID="Lbl_Modified_Amount" runat="server" Text="Modified Amount" ></asp:Label>
                                   </td>
                                   <td>
                                   <asp:TextBox ID="Txt_Modified_Amount" runat="server"  class="sinput"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RFV_Modified_Amount" runat="server" ControlToValidate="Txt_Modified_Amount"
                                                Text="*" SetFocusOnError="true" EnableClientScript="true" ErrorMessage="Please Enter The Address"
                                                ValidationGroup="a1"></asp:RequiredFieldValidator>
                                     <cc1:FilteredTextBoxExtender ID="FTBE_Modified_Amount"
                                                                 runat="server" Enabled="True" TargetControlID="Txt_Modified_Amount"
                                                                  ValidChars="0123456789">
                                                                  </cc1:FilteredTextBoxExtender>
                                     </td>
                                   <td>&nbsp;</td>
                                   <td>&nbsp;</td>
                                   </tr>
                                    
                                 
                                      <tr>
                                     
                                        
                                        <td>
                                       <asp:Label ID="Lbl_Client_Id" runat="server" Text="Client ID" > </asp:Label>    <span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="Ddl_ClientId" runat="server" 
                                        OnSelectedIndexChanged="Ddl_ClientId_OnSelectedIndexChanged"  AutoPostBack="true"  CssClass="sdrop"> </asp:DropDownList>     
                                        </td>
                                     
                                     <td>
                                      <asp:Label ID="Lbl_Client_Name" runat="server" Text="Client Name" > </asp:Label>    <span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:DropDownList ID="Ddl_Cname" runat="server"   
                                         OnSelectedIndexChanged="Ddl_Cname_OnSelectedIndexChanged" AutoPostBack="true"   CssClass="sdrop"> </asp:DropDownList>     
                                       
                                                 </td>
                                    </tr>
                                  
                                  <tr>
                                        <td>
                                          
                                           
                                           <asp:Label ID="Lbl_Month" runat="server" Text=" Month" > </asp:Label>
                                           <span style=" color:Red">*</span>
                                        </td>
                                        <td>
                                        <asp:TextBox ID="Txt_Month_Selected_Salary" runat="server"  CssClass="sinput" MaxLength="10" onkeyup="dtval(this,event)"
                                        AutoPostBack="true" OnTextChanged="Txt_Month_Selected_Salary_OnTextChanged" > </asp:TextBox>
                                        
                                         <cc1:CalendarExtender ID="CE_Month_Selected_Salary" runat="server" Enabled="true" TargetControlID="Txt_Month_Selected_Salary"
                                                                    Format="dd/MM/yyyy">
                                                                </cc1:CalendarExtender>
                                         <cc1:FilteredTextBoxExtender ID="FTBE__Month_Selected_Salary"
                                                                 runat="server" Enabled="True" TargetControlID="Txt_Month_Selected_Salary"
                                                                  ValidChars="0123456789/">
                                                                  </cc1:FilteredTextBoxExtender>
                                        
                                        </td>
                                     
                                     <td>
                                          &nbsp;
                                        </td>
                                        <td>
                                        &nbsp;
                                                 </td>
                                    </tr>
                                 
                                    <tr> 
                                    <td colspan="4" class=" style8" style=" color:Red">
                                    <asp:Label ID="lblresult" Text="" runat="server"></asp:Label>
                                     </td>
                                    </tr>
                                    
                                    <tr>
                                    <td>&nbsp;</td>
                                     <td>&nbsp;</td>
                                      <td>&nbsp;</td>
                                    <td style="padding-left:30px;display:none"> 
                                    
                                    
                                    
                                    
                                      <asp:Button ID="Btn_Search" runat="server" Text="SEARCH" ToolTip="Search Voucher" class=" btn save"
                                    ValidationGroup="a1" onclick="Btn_Search_Voucher_Click"  
                                    onclientclick='return confirm(" Are you  sure you  want to add this  record ?");' />
                                    
                                       <asp:Button ID="Btn_Cancel_Voucher" runat="server" Text="CANCEL" ToolTip="Cancel Voucher"
                                 onclientclick='return confirm(" Are you sure you want to cancel this entry ?");'
                                    class=" btn save" /></td>
                                    </tr>
                                    
                                    
                                    
                                  </table>
                                  </asp:Panel>
                                   
                                  </div>
                           
                            
                            
                             <div class="rounded_corners" style="overflow: auto; width: 99%; margin-left: 17px">
                                   <asp:GridView ID="Gv_salaryDetails_SiteWise" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="BlueViolet"
                                        EmptyDataRowStyle-BorderColor="Aquamarine" Width="95%" CellPadding="4" CellSpacing="3"
                                        ForeColor="#333333" GridLines="None">
                                        <RowStyle BackColor="#EFF3FB" Height="30" />
                                        <Columns>
                                  
                                            <asp:TemplateField ItemStyle-Width="40px">
                        <HeaderTemplate>
                            <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true"  OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                        </HeaderTemplate>
                        <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                        <ItemTemplate>
                            <asp:CheckBox ID="chkEmp" runat="server" 
                             Checked='<%# Eval("status") %>'
                             ></asp:CheckBox>
                        </ItemTemplate>
                 </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Empid">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Empid" runat="server" Text="<%#Bind('empid')%>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Emp Name">
                                                <ItemTemplate>
                                                    <asp:Label ID="Lbl_Emp_Name" runat="server" Text="<%#Bind('name')%>"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Net Amount">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_Net_Amount" runat="server" Text="<%#Bind('ActualAmount')%>"></asp:TextBox>
                                                    
                                                     <cc1:FilteredTextBoxExtender ID="FTBE__Net_Amount_Selected_Salary"
                                                                 runat="server" Enabled="True" TargetControlID="Txt_Net_Amount"
                                                                  ValidChars="0123456789/">
                                                                  </cc1:FilteredTextBoxExtender>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="Txt_Remarks" runat="server"  TextMode="MultiLine" Text=""></asp:TextBox>
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
                            </div>
                           <div>
                           <asp:Panel ID="Pnl_View_More_Details" runat="server">
                           
                          <asp:GridView ID="GV_View_More_Details_Voucher" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="BlueViolet"
                                        EmptyDataRowStyle-BorderColor="Aquamarine" Width="100%" CellPadding="4" CellSpacing="3" ForeColor="#333333"  GridLines="None">
                                       <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        
                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Sno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Client Id" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Client_id" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Voucher No" HeaderStyle-Width="1%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_VoucherNo" runat="server" Text="<%#Bind('voucherno')%>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Paid To" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Paid_To" runat="server" Text="<%#Bind('paidto') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Date" HeaderStyle-Width="3%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Paid_Date" runat="server" Text="<%#Bind('date') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Purpose"   HeaderStyle-Width="5%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Purpose" runat="server" Text="<%#Bind('PurposeName') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Amount"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Amount" runat="server" Text="<%#Bind('Amount') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Payment Mode"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Payment_Mode" runat="server" Text="<%#Bind('PaymentModeName') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Cheque/DD/<br/>Transaction Id"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_DD_Cheque_transaction_No" runat="server" Text="<%#Bind('DDorchequeortransactionNo') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                           
                                          <asp:TemplateField HeaderText="Cheque/DD/<br/>Transaction Date"   HeaderStyle-Width="2%">
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_DD_Cheque_transaction_Date" runat="server" Text="<%#Bind('DDorchequeortransactionDate') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        </Columns>
                                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                           
                           
                  <%--          <asp:GridView ID="Gv_View_More_Details_Salary_DEtails" runat="server" AutoGenerateColumns="False" EmptyDataRowStyle-BackColor="BlueViolet"
                                        EmptyDataRowStyle-BorderColor="Aquamarine" Width="100%" CellPadding="4" CellSpacing="3" ForeColor="#333333"  GridLines="None">
                                       <RowStyle BackColor="#EFF3FB" />
                                        <Columns>
                                        
                                        <asp:TemplateField HeaderText="S.No" >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Sno" runat="server" Text="<%#Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Client Id"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Client_id" runat="server" Text="<%#Bind('clientid')%>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                          <asp:TemplateField HeaderText="Client Name"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Client_Name" runat="server" Text="<%#Bind('clientname') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Empid"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Emp_id" runat="server" Text="<%#Bind('empid') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        
                                          <asp:TemplateField HeaderText="Emp Name"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Emp_Name" runat="server" Text="<%#Bind('empname') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField HeaderText="Amount"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_Amount" runat="server" Text="<%#Bind('ActualAmount') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                  
                                       <asp:TemplateField HeaderText="For The Month Of"  >
                                        <ItemTemplate>
                                        <asp:Label ID="Lbl_For_The_Month_Of" runat="server" Text="<%#Bind('month') %>"></asp:Label>
                                        </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        </Columns>
                                          <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#2461BF" />
                                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView> --%>
                           
                           </asp:Panel>
                           </div>
                           
                           
                        </div>
                    </div>
                    <!-- DASHBOARD CONTENT END -->
                </div>
                <div  class="clear"></div>
            </div>
        </div>
   
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
    </div>
    <!-- CONTENT AREA END -->
   
</form>
</body>
</html>
